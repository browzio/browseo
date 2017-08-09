using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using Gecko;
using Gecko.Interfaces;
using Gecko.IO;
using Gecko.Javascript;
using Gecko.DOM;
using Gecko.DOM.HTML;

namespace UnitTests
{
	[TestFixture]
	public partial class JSTests : TestsBase
	{
		GeckoJavascriptBridge js;

		[SetUp]
		public override void BeforeEachTestSetup()
		{
			base.BeforeEachTestSetup();
			js = GeckoJavascriptBridge.GetService();
		}

		[Test]
		public void Evaluate_OnString_ReturnsStringValue()
		{
			using(Variant value = js.Evaluate("'hello world'"))
			{
				Assert.IsTrue(value.IsString);
				Assert.AreEqual("hello world", value.AsString());
			}
		}

		[Test]
		public void Evaluate_OnBoolean_ReturnsBooleanValue()
		{
			using (Variant value = js.Evaluate("true"))
			{
				Assert.IsTrue(value.IsBoolean);
				Assert.IsTrue(value.AsBoolean(false));
			}
			using (Variant value = js.Evaluate("false"))
			{
				Assert.IsTrue(value.IsBoolean);
				Assert.IsFalse(value.AsBoolean(true));
			}
		}

		[Test]
		public void Evaluate_OnInt_ReturnsIntValue()
		{
			using (Variant value = js.Evaluate("123456"))
			{
				Assert.IsTrue(value.IsInt);
				Assert.AreEqual(123456, value.AsInt(0));
			}
		}

		[Test]
		public void Evaluate_OnDouble_ReturnsDoubleValue()
		{
			using (Variant value = js.Evaluate("123.456"))
			{
				Assert.IsTrue(value.IsDouble);
				Assert.AreEqual(123.456, value.AsDouble(0));
			}
		}

		[Test]
		public void Evaluate_OnNumeric_ReturnsNumericValue()
		{
			using (Variant value = js.Evaluate("123.000"))
			{
				Assert.IsFalse(value.IsFloat);
				Assert.IsTrue(value.IsNumeric);
				Assert.AreEqual(123.000, value.AsNumeric(0));
			}
		}

		[Test]
		public void Evaluate_OnExpression_ReturnsExpressionResult()
		{
			using (Variant value = js.Evaluate("2 + 3"))
			{
				Assert.IsTrue(value.IsInt);
				Assert.AreEqual(5, value.AsInt(0));
			}
		}

		[Test]
		public void Call_OnFuncBody_ReturnsExpectedValue()
		{
			string fnMinBody = "return Math.min(arguments[0], arguments[1])";
			using (Variant fnMin = new Variant(fnMinBody), thisArg = new Variant(new object()), arg1 = new Variant(1), arg2 = new Variant(2))
			{
				using (Variant value = js.Call(fnMin, thisArg, arg1, arg2))
				{
					Assert.AreEqual(1, value.AsInt(0));
				}
			}
		}

		[Test]
		public void SandBox_CreateAndDisposeSandboxWithNullPrincipals()
		{
			Assert.DoesNotThrow(() => { new Sandbox(GeckoJavascriptBridge.GetService(), null, GeckoPrincipal.NullPrincipal, false).Dispose(); });
		}

		[Test]
		public void SandBox_CreateAndDisposeSandboxWithSystemPrincipals()
		{
			Assert.DoesNotThrow(() => { new Sandbox(GeckoJavascriptBridge.GetService(), null, GeckoPrincipal.NullPrincipal, false).Dispose(); });
		}

		[Test]
		public void SandBox_CreateAndDisposeSandboxWithUrlPrincipals()
		{
			Assert.DoesNotThrow(() => { new Sandbox(GeckoJavascriptBridge.GetService(), null, new Uri("http://example.com"), false).Dispose(); });
		}

		[Test]
		public void SandBox_SetProperty()
		{
			using (var sandbox = new Sandbox(null, GeckoPrincipal.NullPrincipal))
			{
				sandbox.SetProperty("aInt", 1);
				Assert.AreEqual(1, sandbox.Evaluate("aInt").AsInt(0));

				sandbox.SetProperty("aString", "string");
				Assert.AreEqual("string", sandbox.Evaluate("aString").AsString());

				sandbox.SetProperty("aBool", true);
				Assert.AreEqual(true, sandbox.Evaluate("aBool").AsBoolean(false));

				sandbox.SetProperty("aDouble", 123.456);
				Assert.AreEqual(123.456, sandbox.Evaluate("aDouble").AsDouble(0));
			}
		}

		[Test]
		public void SandBox_GetProperty()
		{
			using (var sandbox = new Sandbox(null, GeckoPrincipal.NullPrincipal))
			{
				sandbox.SetProperty("aInt", 1);
				Assert.AreEqual(1, sandbox.GetProperty("aInt").AsInt(0));

				sandbox.SetProperty("aString", "string");
				Assert.AreEqual("string", sandbox.GetProperty("aString").AsString());

				sandbox.SetProperty("aBool", true);
				Assert.AreEqual(true, sandbox.GetProperty("aBool").AsBoolean(false));

				sandbox.SetProperty("aDouble", 123.456);
				Assert.AreEqual(123.456, sandbox.GetProperty("aDouble").AsDouble(0));
			}
		}

		[Test]
		public void ProxyFunction_CreateNamedFunctionProxy()
		{
			using (var sandbox = new Sandbox(null, GeckoPrincipal.SystemPrincipal))
			{
				var proxy = new ProxyFunction(sandbox.JS, "123456");
				sandbox.SetProperty("func", sandbox.CreateFunctionProxy("funcname", new[] { "a", "b", "c" }, proxy));
				Assert.AreEqual(proxy.ReturnValue, sandbox.Evaluate("this.func()", "filename", 1).AsString());
				Assert.AreEqual(proxy.ToString(), sandbox.Evaluate("this.func.toString()", "filename", 1).AsString());

				using (Variant fName = new Variant("funcname"), fArgs = new Variant(new string[]{ "a", "b", "c" }))
				{
					sandbox.SetProperty("func", new Variant(sandbox.JS.Instance.CreateFunctionProxy(fName.Instance, fArgs.Instance, null, proxy, true)).AsEvalResult());
					Assert.AreEqual(proxy.ReturnValue, sandbox.Evaluate("this.func()", "filename", 1).AsString());
					Assert.AreEqual("function funcname(a, b, c) { }", sandbox.Evaluate("this.func.toString()", "filename", 1).AsString());
				}
			}
		}

		[Test]
		public void ProxyFunction_CreateAnonymousFunctionProxy()
		{
			using (var sandbox = new Sandbox(null, GeckoPrincipal.SystemPrincipal))
			{
				var proxy = new ProxyFunction(sandbox.JS, "123456");
				sandbox.SetProperty("func", sandbox.CreateFunctionProxy(proxy));
				Assert.AreEqual(proxy.ReturnValue, sandbox.Evaluate("func()", "filename", 1).AsString());
				Assert.AreEqual(proxy.ToString(), sandbox.Evaluate("this.func.toString()", "filename", 1).AsString());

				sandbox.SetProperty("func", new Variant(sandbox.JS.Instance.CreateFunctionProxy(null, null, null, proxy, false)).AsEvalResult());
				Assert.AreEqual(proxy.ReturnValue, sandbox.Evaluate("this.func()", "filename", 1).AsString());
				// Starting with Gecko 38 Function.prototype.toString() throws for Proxy objects (bug 1100936).
				Assert.Throws<GeckoJavaScriptException>(() => sandbox.Evaluate("this.func.toString()", "filename", 1));
			}

		}

		[Test]
		public void ProxyObject_CreateAnonymousObjectProxy()
		{
			using (var sandbox = new Sandbox(null, GeckoPrincipal.SystemPrincipal))
			{
				var proxy = new ProxyObject(sandbox.JS);
				sandbox.SetProperty("obj", sandbox.CreateObjectProxy(proxy));
				Assert.AreEqual(proxy.ToString(), sandbox.Evaluate("this.obj.toString()", "filename", 1).AsString());

				Assert.DoesNotThrow(() => ((IDisposable)sandbox.Evaluate("for(var n in this.obj) { }", "filename", 1)).Dispose(), "Proxy.enumerate isn't work!");
				Assert.DoesNotThrow(() => sandbox.Evaluate("Object.getOwnPropertyNames(this.obj)", "filename", 1).ThrowIfIsNotAndDispose(VariantDataType.Array, VariantDataType.EmptyArray), "Proxy.ownKeys isn't work!");
				Assert.DoesNotThrow(() => ((IDisposable)sandbox.Evaluate("this.obj['n'] = 123; if(this.obj['n'] !== 123) throw new Error()", "filename", 1)).Dispose(), "Proxy.get or Proxy.set isn't work!");
				Assert.DoesNotThrow(() => ((IDisposable)sandbox.Evaluate("this.obj['n'] = 123; delete this.obj['n']; if(typeof this.obj['n'] != 'undefined') throw new Error()", "filename", 1)).Dispose(), "Proxy.deleteProperty isn't work!");
				Assert.DoesNotThrow(() => ((IDisposable)sandbox.Evaluate("Object.defineProperty(this.obj, 'zz', {value: 123}); if(this.obj['zz'] !== 123) throw new Error()", "filename", 1)).Dispose(), "Proxy.defineProperty isn't work!");
				Assert.DoesNotThrow(() => ((IDisposable)sandbox.Evaluate("Object.defineProperty(this.obj, 'aa', {value: 123}); var dsc = Object.getOwnPropertyDescriptor(this.obj, 'aa'); if(!dsc || dsc.value !== 123) throw new Error()", "filename", 1)).Dispose(), "Proxy.getOwnPropertyDescriptor isn't work!");
				Assert.DoesNotThrow(() => ((IDisposable)sandbox.Evaluate("Object.preventExtensions(this.obj); var isSet = false; try { Object.defineProperty(this.obj, 'mm', { value: 1 }); isSet = true; } catch(e) { } if(isSet) throw new Error()", "filename", 1)).Dispose(), "Proxy.preventExtensions isn't work!");
				Assert.DoesNotThrow(() => ((IDisposable)sandbox.Evaluate("Object.preventExtensions(this.obj); if(Object.isExtensible(this.obj)) throw new Error()", "filename", 1)).Dispose(), "Proxy.isExtensible isn't work!");
				
				sandbox.SetProperty("obj2", new Variant(sandbox.JS.Instance.CreateObjectProxy(null, null, proxy, false)).AsEvalResult());
				Assert.AreEqual("[object Object]", sandbox.Evaluate("this.obj2.toString()", "filename", 1).AsString());
			}
		}

		[Test]
		public void ProxyObject_CreatePredefinedObjectProxy()
		{
			using (var sandbox = new Sandbox(null, GeckoPrincipal.SystemPrincipal))
			{
				var proxy = new ProxyObject(sandbox.JS);
				
				sandbox.SetProperty("obj", new Variant(sandbox.JS.Instance.CreateObjectProxy(sandbox.SandboxObject.Instance, null, proxy, false)).AsEvalResult());
				Assert.AreEqual("[object Sandbox]", sandbox.Evaluate("this.obj.toString()", "filename", 1).AsString());
			}
		}
		



	}
}
