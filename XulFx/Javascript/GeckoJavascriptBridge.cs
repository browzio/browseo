using Gecko.CustomMarshalers;
using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.Javascript
{
	public sealed class GeckoJavascriptBridge : ComObject<nsIGeckoScriptBridge>, IGeckoObjectWrapper
	{
		public static GeckoJavascriptBridge GetService()
		{
			return Xpcom.GetService<nsIGeckoScriptBridge>(Contracts.GeckoJavaScriptBridge).Wrap(instance => new GeckoJavascriptBridge(instance));
		}

		private GeckoJavascriptBridge(nsIGeckoScriptBridge instance)
			: base(instance)
		{

		}

		public Variant EvaluateInWindow(GeckoWindow window, string scriptCode)
		{
			try
			{
				return new Variant(Instance.EvaluateInWindow(window.Instance, scriptCode)).AsEvalResult();
			}
			catch (GeckoException ex)
			{
				string message = ex.Message;
				Debug.WriteLine(message);
				int start = message.IndexOf(" {file:");
				int end = message.LastIndexOf('}') + 1;
				if (start == -1 || end == 0 || end == message.Length)
					throw;
				throw new GeckoException(message.Remove(start) + message.Substring(end), ex);
			}
		}

		public Variant Evaluate(string code)
		{
			return Evaluate(null, GeckoPrincipal.NullPrincipal, code, null, 1, false);
		}

		public string EvaluateToString(string code)
		{
			using (Variant result = Evaluate(null, GeckoPrincipal.NullPrincipal, code, null, 1, true))
			{
				return result.AsString();
			}
		}

		public Variant Evaluate(object global, GeckoPrincipal principal, string code)
		{
			return Evaluate(global, principal, code, null, 1, false);
		}

		public string EvaluateToString(object global, GeckoPrincipal principal, string code)
		{
			using (Variant result = Evaluate(global, principal, code, null, 1, true))
			{
				return result.AsString();
			}
		}

		public string EvaluateToString(object global, GeckoPrincipal principal, string code, string filename, int lineno)
		{
			using (Variant result = Evaluate(global, principal, code, filename, lineno, true))
			{
				return result.AsString();
			}
		}

		public Variant Evaluate(object global, GeckoPrincipal principal, string code, string filename, int lineno, bool returnAsString)
		{
			if (principal == null)
				principal = GeckoPrincipal.SystemPrincipal;

			using (var aPrincipal = new Variant(principal.Instance))
			{
				return Evaluate(global, aPrincipal, code, filename, lineno, returnAsString);
			}
		}

		public Variant Evaluate(object global, ComObject<nsIVariant> principal, string code, string filename, int lineno, bool returnAsString)
		{
			if (code == null)
				throw new ArgumentNullException("code");
			if (lineno <= 0)
				throw new ArgumentOutOfRangeException("lineno");

			if (principal == null)
				throw new ArgumentOutOfRangeException("principal");

			var comObj = global as IComObject;
			return new Variant(Instance.Evaluate(comObj != null ? comObj.NativeInstance : global, principal.Instance, code, filename ?? "x-bogus://GeckoJavaScriptBridge/evaluate/", (uint)lineno, returnAsString)).AsEvalResult();
		}

		public Variant EvaluateInSandbox(ComObject<nsIVariant> sandbox, string code, string filename, int lineno, bool returnAsString)
		{
			if (sandbox == null)
				throw new ArgumentNullException("sandbox");
			if (code == null)
				throw new ArgumentNullException("code");
			if (lineno <= 0)
				throw new ArgumentOutOfRangeException("lineno");

			return new Variant(Instance.EvaluateInSandbox(sandbox.Instance, code, filename ?? "x-bogus://GeckoJavaScriptBridge/evaluateInSandbox/", (uint)lineno, returnAsString)).AsEvalResult();
		}

		public void DefineProperty(ComObject<nsIVariant> obj, string name, ComObject<nsIVariant> value, bool aReadonly, bool enumerable, bool configurable)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			if (name.Length == 0)
				throw new ArgumentOutOfRangeException("name");

			using (var aName = new Variant(name))
			{
				((IDisposable)new Variant(Instance.DefineProperty(obj.Instance, aName.Instance, value != null ? value.Instance : null, aReadonly, enumerable, configurable)).AsEvalResult()).Dispose();
			}
		}

		public bool DeleteProperty(ComObject<nsIVariant> obj, string name)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			if (name.Length == 0)
				throw new ArgumentOutOfRangeException("name");

			using (var aName = new Variant(name))
			{
				return Instance.DeleteProperty(obj.Instance, aName.Instance);
			}
		}

		public void SetProperty(object obj, string name, ComObject<nsIVariant> value)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			if (name.Length == 0)
				throw new ArgumentOutOfRangeException("name");

			nsIVariant variantObj = Xpcom.QueryInterface<nsIVariant>(obj);
			try
			{
				if (variantObj == null)
				{
					variantObj = Xpcom.CreateInstance<nsIWritableVariant>(Contracts.WritableVariant);
					var comObj = obj as IComObject;
					Guid iid = Variant.NS_ISUPPORTS_IID;
					((nsIWritableVariant)variantObj).SetAsInterface(ref iid, comObj != null ? comObj.NativeInstance : obj);
				}
				using (var aName = new Variant(name))
				{
					((IDisposable)new Variant(Instance.SetProperty(variantObj, aName.Instance, value != null ? value.Instance : null)).AsEvalResult()).Dispose();
				}
			}
			finally
			{
				Xpcom.FreeComObject(ref variantObj);
			}
		}

		public void SetProperty(ComObject<nsIVariant> obj, string name, ComObject<nsIVariant> value)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			if (name.Length == 0)
				throw new ArgumentOutOfRangeException("name");
			using(var aName = new Variant(name))
			{
				((IDisposable)new Variant(Instance.SetProperty(obj.Instance, aName.Instance, value != null ? value.Instance : null)).AsEvalResult()).Dispose();
			}
		}

		public void SetProperty(ComObject<nsIVariant> obj, string name, string value)
		{
			using(var aValue = new Variant(value))
			{
				SetProperty(obj, name, aValue);
			}
		}

		public void SetProperty(ComObject<nsIVariant> obj, string name, bool value)
		{
			using (var aValue = new Variant(value))
			{
				SetProperty(obj, name, aValue);
			}
		}

		public void SetProperty(ComObject<nsIVariant> obj, string name, int value)
		{
			using (var aValue = new Variant(value))
			{
				SetProperty(obj, name, aValue);
			}
		}

		public void SetProperty(ComObject<nsIVariant> obj, string name, long value)
		{
			using (var aValue = new Variant(value))
			{
				SetProperty(obj, name, aValue);
			}
		}

		public void SetProperty(ComObject<nsIVariant> obj, string name, float value)
		{
			using (var aValue = new Variant(value))
			{
				SetProperty(obj, name, aValue);
			}
		}

		public void SetProperty(ComObject<nsIVariant> obj, string name, double value)
		{
			using (var aValue = new Variant(value))
			{
				SetProperty(obj, name, aValue);
			}
		}

		public Variant GetProperty(object obj, string name)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			if (name.Length == 0)
				throw new ArgumentOutOfRangeException("name");

			nsIVariant variantObj = Xpcom.QueryInterface<nsIVariant>(obj);
			try
			{
				if (variantObj == null)
				{
					var variantTypeObj = obj as ComObject<nsIVariant>;
					if (variantTypeObj != null)
					{
						variantObj = variantTypeObj.QueryInterface<nsIVariant>();
					}
					else
					{
						variantObj = Xpcom.CreateInstance<nsIWritableVariant>(Contracts.WritableVariant);
						var comObj = obj as IComObject;
						Guid iid = Variant.NS_ISUPPORTS_IID;
						((nsIWritableVariant)variantObj).SetAsInterface(ref iid, comObj != null ? comObj.NativeInstance : obj);
					}
				}
				using (var aName = new Variant(name))
				{
					return new Variant(Instance.GetProperty(variantObj, aName.Instance)).AsEvalResult();
				}
			}
			finally
			{
				Xpcom.FreeComObject(ref variantObj);
			}
		}

		public string ConvertToString(ComObject<nsIVariant> value)
		{
			if (value == null)
				return null;
			return Instance.ConvertToString(value.Instance);
		}

		public Variant CreateProxy(nsIScriptableComObject target)
		{
			if (target == null)
				throw new ArgumentNullException("target");
			using (var targetObj = new Variant(target))
			{
				return new Variant(Instance.CreateProxy(targetObj.Instance)).AsEvalResult();
			}
			
		}

		public void DeclareFunction(ComObject<nsIVariant> obj, string name, nsIGeckoFunctionHandler handler, bool aReadonly, bool enumerable, bool configurable)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			if (handler == null)
				throw new ArgumentNullException("handler");

			using (var aName = new Variant(name))
			{
				((IDisposable)new Variant(Instance.DeclareFunction(obj.Instance, aName.Instance, handler, aReadonly, enumerable, configurable)).AsEvalResult()).Dispose();
			}
		}

		public Variant Call(ComObject<nsIVariant> func, ComObject<nsIVariant> thisArg, params ComObject<nsIVariant>[] args)
		{
			if (func == null)
				throw new ArgumentNullException("func");
			if (thisArg == null)
				throw new ArgumentNullException("thisArg");

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
			var rv = new Variant(Instance.Call(func.Instance, thisArg.Instance, (uint)count, aArgs)).AsEvalResult();
			if (args != null) GC.KeepAlive(args);
			return rv;
		}

		public void Call(nsIRunnable runnable)
		{
			if (runnable == null)
				throw new ArgumentNullException("runnable");

			Instance.Run(runnable);
		}

		public void Call(Action action)
		{
			if (action == null)
				throw new ArgumentNullException("action");

			var runnable = new Runnable(action);
			Instance.Run(runnable);
			if (runnable.Exception != null)
				throw runnable.Exception;
		}

		public void Call(Action action, GeckoWindow context)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			var runnable = new Runnable(action);
			if (context != null)
			{
				Instance.Dispatch(runnable, context.Instance);
			}
			else
			{
				Instance.Run(runnable);
			}
			if (runnable.Exception != null)
				throw runnable.Exception;
		}

		public T Call<T>(Func<T> func)
		{
			if (func == null)
				throw new ArgumentNullException("func");

			var runnable = new Runnable<T>(func);
			Instance.Run(runnable);
			if (runnable.Exception != null)
				throw runnable.Exception;
			return runnable.Result;
		}

		public T Call<T>(Func<T> func, GeckoWindow context)
		{
			if (func == null)
				throw new ArgumentNullException("func");

			var runnable = new Runnable<T>(func);
			if (context != null)
			{
				Instance.Dispatch(runnable, context.Instance);
			}
			else
			{
				Instance.Run(runnable);
			}
			if (runnable.Exception != null)
				throw runnable.Exception;
			return runnable.Result;
		}

		public Variant Create(string constructorName, ComObject<nsIVariant> thisArg, params ComObject<nsIVariant>[] args)
		{
			if (constructorName == null)
				throw new ArgumentNullException("constructorName");

			using (var constructor = new Variant(constructorName))
			{
				return Create(constructor, thisArg, args);
			}
		}

		public Variant Create(ComObject<nsIVariant> constructor, ComObject<nsIVariant> thisArg, params ComObject<nsIVariant>[] args)
		{
			if (constructor == null)
				throw new ArgumentNullException("constructor");

			if (thisArg == null)
				throw new ArgumentNullException("thisArg");

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
			var rv = new Variant(Instance.Create(constructor.Instance, thisArg.Instance, (uint)count, aArgs)).AsEvalResult();
			if (args != null)
				GC.KeepAlive(args);

			return rv;
		}

		public Variant CreateObject(ComObject<nsIVariant> prototype, ComObject<nsIVariant> scope)
		{
			return new Variant(Instance.CreateObject(prototype != null ? prototype.Instance : null, scope != null ? scope.Instance : null)).AsEvalResult();
		}

		public JSArray CreateArray(ComObject<nsIVariant> thisArg, int length)
		{
			if (length < 0)
				throw new ArgumentOutOfRangeException("length");

			using (Variant aLength = new Variant(length))
			{
				using (Variant array = Create("Array", thisArg, aLength))
				{
					return array.QueryInterface<nsIVariant>().Wrap(JSArray.Create);
				}
			}
		}

		public Variant CreateObjectProxy(ComObject<nsIVariant> obj, ComObject<nsIVariant> scope, nsIHarmonyProxyCLRHandler handler, bool overrideToString)
		{
			if (handler == null)
				throw new ArgumentNullException("handler");

			return new Variant(Instance.CreateObjectProxy(obj != null ? obj.Instance : null, scope != null ? scope.Instance : null, handler, overrideToString)).AsEvalResult();
		}

		public Variant CreateFunctionProxy(ComObject<nsIVariant> function, nsIHarmonyProxyCLRHandler handler,  bool overrideToString)
		{
			if (function == null)
				throw new ArgumentNullException("function");

			if (handler == null)
				throw new ArgumentNullException("handler");

			return new Variant(Instance.CreateFunctionProxy(function.Instance, null, null, handler, overrideToString)).AsEvalResult();
		}

		public Variant CreateFunctionProxy(string name, string[] args, ComObject<nsIVariant> scope, nsIHarmonyProxyCLRHandler handler, bool overrideToString)
		{
			if (handler == null)
				throw new ArgumentNullException("handler");

			if (name != null && !IsValidName(name))
				throw new ArgumentOutOfRangeException("name");

			if (args != null && !args.All(IsValidName))
				throw new ArgumentOutOfRangeException("args");

			using (Variant aName = new Variant(name), aArgs = new Variant(args))
			{
				return new Variant(Instance.CreateFunctionProxy(aName.Instance, aArgs.Instance, scope != null ? scope.Instance : null, handler, overrideToString)).AsEvalResult();
			}
			
		}

		public string GetType(ComObject<nsIVariant> obj)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			return Instance.GetType(obj.Instance);
		}

		public string GetType(nsIVariant obj)
		{
			if (obj == null)
				return "object";
			return Instance.GetType(obj);
		}

		public void PreventExtensions(ComObject<nsIVariant> obj)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			Instance.PreventExtensions(obj.Instance);
		}

		public void PreventExtensions(nsIVariant obj)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			Instance.PreventExtensions(obj);
		}

		public bool HasOwnObjectProperty(ComObject<nsIVariant> obj, string name)
		{
			using(var propName = new Variant(name))
			{
				return this.HasOwnObjectProperty(obj, propName);
			}
		}

		public bool HasOwnObjectProperty(ComObject<nsIVariant> obj, ComObject<nsIVariant> name)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");

			return Instance.HasOwnObjectProperty(obj.Instance, name.Instance);
		}

		public bool HasOwnObjectProperty(nsIVariant obj, nsIVariant name)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");

			return Instance.HasOwnObjectProperty(obj, name);
		}

		public void DefineProperty(nsIVariant obj, nsIVariant name, nsIVariant propertyDescriptor)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			if (propertyDescriptor == null)
				throw new ArgumentNullException("propertyDescriptor");
			Instance.DefineProperty2(obj, name, propertyDescriptor);
		}

		public bool DeleteProperty(nsIVariant obj, nsIVariant name)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			return Instance.DeleteProperty(obj, name);
		}

		public bool SetProperty(nsIVariant obj, nsIVariant name, nsIVariant value)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			nsIVariant rv = Instance.SetProperty(obj, name, value);
			try
			{
				return (VariantDataType)rv.GetDataTypeAttribute() == VariantDataType.Void;
			}
			finally
			{
				Xpcom.FreeComObject(ref rv);
			}
		}

		public nsIVariant GetProperty(nsIVariant obj, nsIVariant name)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			return Instance.GetProperty(obj, name);
		}

		public nsIVariant GetPropertyBySymbolName(nsIVariant obj, string name)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			return Instance.GetPropertyBySymbolName(obj, name);
		}

		public nsIVariant GetOwnPropertyDescriptor(nsIVariant obj, nsIVariant name)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (name == null)
				throw new ArgumentNullException("name");
			return Instance.GetOwnPropertyDescriptor(obj, name);
		}

		public nsIVariant GetOwnPropertyNames(nsIVariant obj)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			return Instance.GetOwnPropertyNames(obj);
		}

		public nsIVariant GetPrototypeOf(nsIVariant obj)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			return Instance.GetPrototypeOf(obj);
		}

		public void SetPrototypeOf(nsIVariant obj, nsIVariant prototype)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");
			((IDisposable)(new Variant(Instance.SetPrototypeOf(obj, prototype)).AsEvalResult())).Dispose();
		}

		public bool IsExtensible(nsIVariant target)
		{
			if (target == null)
				throw new ArgumentNullException("target");
			return Instance.IsExtensible(target);
		}

		internal static bool IsValidName(string name)
		{
			if (name == null || name.Length == 0)
				return false;
			char startSymbol = name[0];
			if (startSymbol == '_' || startSymbol == '$' || char.IsLetter(startSymbol))
			{
				return name.Skip(1).All(ch => ch == '_' || ch == '$' || char.IsLetterOrDigit(ch));
			}
			return false;
		}
	}
}
