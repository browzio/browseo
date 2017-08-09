using System;
using Gecko.Interfaces;
using Gecko.Javascript;
using Gecko;

namespace UnitTests
{
	partial class JSTests
	{
		class ProxyFunction : ProxyObject
		{
			public ProxyFunction(GeckoJavascriptBridge js, string retval)
				: base(js)
			{
				this.ReturnValue = retval;
			}

			public string ReturnValue { get; set; }
			
			public override nsIVariant Apply(nsIVariant target, nsIVariant thisValue, nsIVariant args)
			{
				return new Variant(ReturnValue).QueryInterface<nsIVariant>();
			}

			public override string ToString()
			{
				return "function funcname(a, b, c) { }";
			}
		}

		class ProxyObject : nsIHarmonyProxyCLRHandler
		{
			private GeckoJavascriptBridge JS;
			

			public ProxyObject(GeckoJavascriptBridge js)
			{
				this.JS = js;
			}

			public virtual nsIVariant GetOwnPropertyDescriptor(nsIVariant target, nsIVariant name)
			{
				return JS.GetOwnPropertyDescriptor(target, name);
			}

			public nsIVariant OwnKeys(nsIVariant target)
			{
				return JS.GetOwnPropertyNames(target);
			}

			public bool DefineProperty(nsIVariant target, nsIVariant name, nsIVariant propertyDescriptor)
			{
				JS.DefineProperty(target, name, propertyDescriptor);
				return true;
			}

			public bool DeleteProperty(nsIVariant target, nsIVariant name)
			{
				return JS.DeleteProperty(target, name);
			}

			public bool PreventExtensions(nsIVariant target)
			{
				JS.PreventExtensions(target);
				return true;
			}

			public virtual bool Has(nsIVariant target, nsIVariant name)
			{
				return JS.HasOwnObjectProperty(target, name);
			}

			public virtual nsIVariant Get(nsIVariant target, nsIVariant name, nsIVariant receiver)
			{
				return JS.GetProperty(target, name);
			}

			public bool Set(nsIVariant target, nsIVariant name, nsIVariant val, nsIVariant receiver)
			{
				return JS.SetProperty(target, name, val);
			}

			public nsIVariant Enumerate(nsIVariant target)
			{
				return JS.GetPropertyBySymbolName(target, "iterator");
			}

			public virtual nsIVariant Apply(nsIVariant target, nsIVariant thisValue, nsIVariant args)
			{
				throw new NotSupportedException();
			}

			public virtual nsIVariant Construct(nsIVariant target, nsIVariant args)
			{
				throw new NotSupportedException();
			}

			public nsIVariant GetPrototypeOf(nsIVariant target)
			{
				return JS.GetPrototypeOf(target);
			}

			public void SetPrototypeOf(nsIVariant target, nsIVariant prototype)
			{
				JS.SetPrototypeOf(target, prototype);
			}

			public override string ToString()
			{
				return string.Format("[object {0}]", this.GetType().Name);
			}


			public bool IsExtensible(nsIVariant target)
			{
				return JS.IsExtensible(target);
			}
		}

	}
}
