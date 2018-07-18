using Gecko.CustomMarshalers;
using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.Javascript
{
	public delegate nsIVariant JavascriptFunctionCallback(nsIVariant proxy, nsIVariant thisArg, nsIVariant[] args);

    [DebuggerNonUserCode, DebuggerStepThrough]
    public class Sandbox : IDisposable, nsIGeckoFunctionHandler
    {
        private GeckoJavascriptBridge _js;
        private Variant _sandbox;
        private Exception _exception;
        private Dictionary<string, JavascriptFunctionCallback> _functions;
        private bool _systemPrincipalEnabled;

        #region .ctor & .dtor

        public Sandbox(ComObject<nsIVariant> global, GeckoPrincipal principal)
            : this(GeckoJavascriptBridge.GetService(), global, principal, false)
        {

        }

        public Sandbox(GeckoJavascriptBridge js, ComObject<nsIVariant> global, GeckoPrincipal principal, bool wantComponents)
        {
            if (js == null)
                throw new ArgumentNullException("js");

            _functions = new Dictionary<string, JavascriptFunctionCallback>();

            if (principal == null)
            {
                principal = GeckoPrincipal.NullPrincipal;
                _systemPrincipalEnabled = false;
            }
            else
            {
                _systemPrincipalEnabled = principal == GeckoPrincipal.SystemPrincipal;
            }


            _js = js;
            if (global == null)
                global = Variant.Void;

            using (var aPrincipal = new Variant(principal.Instance))
            {
                _sandbox = new Variant(_js.Instance.CreateSandbox(global.Instance, aPrincipal.Instance, wantComponents)).AsEvalResult();
            }
        }

        public Sandbox(GeckoJavascriptBridge js, GeckoWindow window)
        {
            if (js == null)
                throw new ArgumentNullException("js");
            if (window == null)
                throw new ArgumentNullException("window");

            _functions = new Dictionary<string, JavascriptFunctionCallback>();
            _systemPrincipalEnabled = false;

            _js = js;

            using (Variant aWindow = new Variant(window.Instance))
            {
                _sandbox = new Variant(_js.Instance.CreateSandbox(aWindow.Instance, aWindow.Instance, false)).AsEvalResult();
            }
        }

        public Sandbox(GeckoJavascriptBridge js, ComObject<nsIVariant> global, Uri url, bool wantComponents)
        {
            if (js == null)
                throw new ArgumentNullException("js");
            if (url == null)
                throw new ArgumentNullException("url");
            if (!url.IsAbsoluteUri)
                throw new ArgumentOutOfRangeException("url");

            _functions = new Dictionary<string, JavascriptFunctionCallback>();
            _systemPrincipalEnabled = false;

            _js = js;
            if (global == null)
                global = Variant.Void;
            using (var aPrincipal = new Variant(url.AbsoluteUri))
            {
                _sandbox = new Variant(_js.Instance.CreateSandbox(global.Instance, aPrincipal.Instance, wantComponents)).AsEvalResult();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_sandbox != null)
                {
                    Variant sandbox = _sandbox;
                    _sandbox = null;
                    _js.Instance.DisposeSandbox(sandbox.Instance);
                    ((IDisposable)sandbox).Dispose();
                    _js = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~Sandbox()
        {
            Dispose(false);
        }

        #endregion

        public GeckoJavascriptBridge JS
        {
            get
            {
                if (_js == null)
                    throw new ObjectDisposedException(this.GetType().Name);
                return _js;
            }
        }

        public virtual Variant SandboxObject
        {
            get
            {
                if (_sandbox == null)
                    throw new ObjectDisposedException(this.GetType().Name);
                return _sandbox;
            }
        }

        public virtual void DefineProperty(string name, ComObject<nsIVariant> value, bool aReadonly, bool enumerable, bool configurable)
        {
            JS.DefineProperty(SandboxObject, name, value, aReadonly, enumerable, configurable);
        }

        public virtual void DeleteProperty(string name)
        {
            JS.DeleteProperty(SandboxObject, name);
        }

        public virtual void SetProperty(string name, int value)
        {
            using (var aValue = new Variant(value))
            {
                JS.SetProperty(SandboxObject, name, aValue);
            }
        }

        public virtual void SetProperty(string name, bool value)
        {
            using (var aValue = new Variant(value))
            {
                JS.SetProperty(SandboxObject, name, aValue);
            }
        }

        public virtual void SetProperty(string name, double value)
        {
            using (var aValue = new Variant(value))
            {
                JS.SetProperty(SandboxObject, name, aValue);
            }
        }

        public virtual void SetProperty(string name, string value)
        {
            using (var aValue = new Variant(value))
            {
                JS.SetProperty(SandboxObject, name, aValue);
            }
        }

        public virtual void SetProperty(string name, ComObject<nsIVariant> value)
        {
            JS.SetProperty(SandboxObject, name, value);
        }

        public virtual Variant GetProperty(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name.Length == 0)
                throw new ArgumentOutOfRangeException("name");

            return JS.GetProperty(SandboxObject.Instance, name);
        }

        public virtual Variant Evaluate(string code)
        {
            if (code == null)
                throw new ArgumentNullException("code");
            return new Variant(JS.Instance.EvaluateInSandbox(SandboxObject.Instance, code, "x-bogus://GeckoJavaScriptBridge/evaluateInSandbox/", 1, false)).AsEvalResult();
        }

        public virtual Variant Evaluate(string code, string filename, int lineno)
        {
            if (code == null)
                throw new ArgumentNullException("code");
            if (lineno <= 0)
                throw new ArgumentOutOfRangeException("lineno");
            return new Variant(JS.Instance.EvaluateInSandbox(SandboxObject.Instance, code, filename ?? "x-bogus://GeckoJavaScriptBridge/evaluateInSandbox/", (uint)lineno, false)).AsEvalResult();
        }

        public virtual void DeclareFunction(string name, JavascriptFunctionCallback handler)
        {
            JS.DeclareFunction(SandboxObject, name, this, true, true, false);
            _functions[name] = handler;
        }

        public virtual void DeclareFunction(string name, JavascriptFunctionCallback handler, bool aReadonly, bool enumerable, bool configurable)
        {
            JS.DeclareFunction(SandboxObject, name, this, aReadonly, enumerable, configurable);
            _functions[name] = handler;
        }

        public virtual Variant CreateObjectProxy(ComObject<nsIVariant> obj, nsIHarmonyProxyCLRHandler handler)
        {
            return JS.CreateObjectProxy(obj, null, handler, _systemPrincipalEnabled);
        }

        public virtual Variant CreateObjectProxy(nsIHarmonyProxyCLRHandler handler)
        {
            return JS.CreateObjectProxy(null, this.SandboxObject, handler, _systemPrincipalEnabled);
        }

        public virtual Variant CreateFunctionProxy(nsIHarmonyProxyCLRHandler handler, ComObject<nsIVariant> function)
        {
            return JS.CreateFunctionProxy(function, handler, _systemPrincipalEnabled);
        }

        public virtual Variant CreateFunctionProxy(nsIHarmonyProxyCLRHandler handler)
        {
            return JS.CreateFunctionProxy(null, null, this.SandboxObject, handler, _systemPrincipalEnabled);
        }

        public virtual Variant CreateFunctionProxy(string[] args, nsIHarmonyProxyCLRHandler handler)
        {
            return JS.CreateFunctionProxy(null, args, this.SandboxObject, handler, _systemPrincipalEnabled);
        }

        public virtual Variant CreateFunctionProxy(string name, string[] args, nsIHarmonyProxyCLRHandler handler)
        {
            return JS.CreateFunctionProxy(name, args, this.SandboxObject, handler, _systemPrincipalEnabled);
        }

        public virtual JSArray CreateArray(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length");

            using (Variant aLength = new Variant(length))
            {
                using (Variant array = Create("Array", aLength))
                {
                    return array.QueryInterface<nsIVariant>().Wrap(JSArray.Create);
                }
            }
        }

        public virtual Variant CreateObject(ComObject<nsIVariant> prototype = null)
        {
            using (Variant ctor = new Variant("Object"))
            {
                if (prototype != null)
                    return Create(ctor, prototype);
                return Create(ctor);
            }
        }

        public virtual Variant Create(string constructorName, params ComObject<nsIVariant>[] args)
        {
            if (constructorName == null)
                throw new ArgumentNullException("constructorName");
            if (constructorName.Length == 0)
                throw new ArgumentOutOfRangeException("constructorName");

            using (var constructor = new Variant(constructorName))
            {
                return Create(constructor, args);
            }
        }

        public virtual Variant Create(ComObject<nsIVariant> constructor, params ComObject<nsIVariant>[] args)
        {
            return JS.Create(constructor, SandboxObject, args);
        }

        public virtual Variant Call(string func, ComObject<nsIVariant> thisArg, params ComObject<nsIVariant>[] args)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            using (var aFunc = new Variant(func))
            {
                return Call(aFunc, thisArg, args);
            }
        }

        public virtual Variant Call(ComObject<nsIVariant> func, ComObject<nsIVariant> thisArg, params ComObject<nsIVariant>[] args)
        {
            if (func == null)
                throw new ArgumentNullException("func");
            if (thisArg == null)
                thisArg = this.SandboxObject;

            int count = 0;
            nsIVariant[] aArgs = null;
            if (args != null)
            {
                count = args.Length;
                if (count > 0)
                {
                    aArgs = new nsIVariant[args.Length];
                    for (int i = 0; i < count; i++)
                    {
                        aArgs[i] = args[i].Instance;
                    }
                }
            }
            var rv = new Variant(JS.Instance.CallInSandbox(SandboxObject.Instance, func.Instance, thisArg.Instance, (uint)count, aArgs)).AsEvalResult();
            if (args != null) GC.KeepAlive(args);
            return rv;
        }

        protected virtual nsIVariant OnCall(nsIVariant proxy, string aName, nsIVariant thisArg, uint aCount, nsIVariant[] aArgs)
        {
            _exception = null;
            try
            {
                JavascriptFunctionCallback func;
                if (!_functions.TryGetValue(aName, out func))
                    throw new MissingMethodException(string.Format("Method '{0}' not found.", aName));
                return func(proxy, thisArg, aArgs ?? new nsIVariant[0]);
            }
            catch (Exception e)
            {
                _exception = e;
                throw;
            }
        }

        nsIVariant nsIGeckoFunctionHandler.Call(nsIVariant proxy, string aName, nsIVariant thisArg, uint aCount, nsIVariant[] aArgs)
        {
            return OnCall(proxy, aName, thisArg, aCount, aArgs);
        }

        nsIScriptError nsIGeckoFunctionHandler.GetExceptionAttribute()
        {
            return _exception as GeckoJavaScriptException;
        }

        void nsIGeckoFunctionHandler.GetErrorMessageAttribute(nsAStringBase result)
        {
            result.SetData(_exception != null ? _exception.Message : null);
        }

        void nsIGeckoFunctionHandler.GetErrorTypeAttribute(nsAStringBase result)
        {
            if (_exception is ArgumentException)
            {
                if (_exception is ArgumentOutOfRangeException)
                    result.SetData("RangeError");
                else
                    result.SetData("TypeError");
            }
            else
            {
                result.SetData("Error");
            }
        }
    }
}
