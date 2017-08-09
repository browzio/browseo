var Cu = Components.utils;
var Ci = Components.interfaces;
var Cc = Components.classes;
const nsIScriptableComObject = Ci.nsIScriptableComObject;

Cu.import("resource://gre/modules/XPCOMUtils.jsm");

function GeckoScriptBridge() {
	var callInfo = false;
	this.callInSandbox = function (sandbox, func, thisArg, aCount, args) {
		try {
			callInfo = { func: func, thisArg: thisArg, count: aCount, args: args };
			return Cu.evalInSandbox('__callInSandbox()', sandbox, 'latest', 'x-bogus://GeckoJavascriptBridge/callInSandbox/', 1);
		} catch (e) {
			return GeckoScriptBridge.prototype.errorInfo(e);
		} finally {
			callInfo = false;
		}
	};
	this.callInSandboxCallback = function () {
		if (!callInfo)
			throw new Error('InvalidOperationException');
		var ci = callInfo;
		callInfo = false;
		return GeckoScriptBridge.prototype.call(ci.func, ci.thisArg, ci.count, ci.args);
	};
}
GeckoScriptBridge.prototype = {
	classDescription: "Gecko to .NET JavaScript Bridge",
	classID: Components.ID("{917d0499-0a70-4a64-a0c8-013bd2a1251e}"),
	contractID: "@gecko/scriptbridge;1",
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIGeckoScriptBridge]),

	errorInfo: function(e) {
		let err = Cc["@mozilla.org/scripterror;1"].createInstance(Ci.nsIScriptError);
		if (e instanceof Error)
			err.init(e.name + ': ' + e.message, e.fileName, undefined, e.lineNumber, e.columnNumber, 0, 'content javascript');
		else if(e instanceof Ci.nsIScriptError)
			err = e;
		else
			err.init(e + '', e.fileName, undefined, e.lineNumber, e.columnNumber, 0, 'content javascript');
		return err;
	},

	evaluateInWindow: function (window, code) {
		try {
			with (XPCNativeWrapper.unwrap(window)) {
				return eval(code);
			}
		} catch (e) {
			return GeckoScriptBridge.prototype.errorInfo(e);
		}
	},
	createSandbox: function (global, principal, wantComponents) {
		let sandbox;
		if (typeof global == 'object' && global != null)
			sandbox = new Cu.Sandbox(principal, { sandboxPrototype: global, wantComponents: wantComponents });
		else
			sandbox = new Cu.Sandbox(principal, { wantComponents: wantComponents });
		Object.defineProperty(sandbox, '__callInSandbox', {
			value: this.callInSandboxCallback, 
			configurable: false,
			writable: false,
			enumerable: false
		});
		return sandbox;
	},
	evaluateInSandbox: function (sandbox, code, filename, line, returnAsString) {
		let result;
		try {
			result = Cu.evalInSandbox(code, sandbox, 'latest', filename, line);
		} catch (e) {
			return GeckoScriptBridge.prototype.errorInfo(e);
		}
		return returnAsString ? result + '' : result;
	},
	disposeSandbox: function (sandbox) {
		Cu.nukeSandbox(sandbox);
	},
	evaluate: function (global, principal, code, filename, line, returnAsString) {
		let sandbox, result;
		try {
			if (!principal) {
				principal = Cc["@mozilla.org/nullprincipal;1"].createInstance(Ci.nsIPrincipal);
			}
			if (typeof global == 'object' && global != null)
				sandbox = new Components.utils.Sandbox(principal, { sandboxPrototype: global, wantComponents: false });
			else
				sandbox = new Components.utils.Sandbox(principal, { wantComponents: false });
			result = Cu.evalInSandbox(code, sandbox, 'latest', filename, line);
			if (returnAsString) result += '';
		} catch (e) {
			return GeckoScriptBridge.prototype.errorInfo(e);
		} finally {
			if (sandbox) Cu.nukeSandbox(sandbox);
		}
		return result;
	},
	convertToString: function (value) {
		return XPCNativeWrapper.unwrap(value) + '';
	},
	getOwnPropertyDescriptor: function (obj, name) {
		return Object.getOwnPropertyDescriptor(obj, name);
	},
	getOwnPropertyNames: function (obj) {
		return Object.getOwnPropertyNames(obj);
	},
	getOwnPropertySymbols: function (obj) {
		return Object.getOwnPropertySymbols(obj);
	},
	hasOwnObjectProperty: function (obj, name) {
		return obj.hasOwnProperty(name);
	},
	isExtensible: function (obj) {
		return Object.isExtensible(obj);
	},
	preventExtensions: function (obj) {
		Object.preventExtensions(obj);
	},
	getPrototypeOf: function (obj) {
		return Object.getPrototypeOf(obj);
	},
	setPrototypeOf: function (obj, prototype) {
		try {
			Object.setPrototypeOf(obj, prototype);
		} catch (e) {
			return GeckoScriptBridge.prototype.errorInfo(e);
		}
	},
	defineProperty: function (obj, name, value, readonly, enumerable, configurable) {
		try {
			Object.defineProperty(XPCNativeWrapper.unwrap(obj), name, { value: value, writable: !readonly, enumerable: enumerable, configurable: configurable });
		} catch (e) {
			return GeckoScriptBridge.prototype.errorInfo(e);
		}
		return void (0);
	},
	defineProperty2: function (obj, name, propertyDescriptor) {
		Object.defineProperty(XPCNativeWrapper.unwrap(obj), name, propertyDescriptor);
	},
	setProperty: function (obj, name, value) {
		try {
			obj = XPCNativeWrapper.unwrap(obj);
			obj[name] = value;
		} catch (e) {
			return GeckoScriptBridge.prototype.errorInfo(e);
		}
		return void(0);
	},
	getProperty: function (obj, name) {
		obj = XPCNativeWrapper.unwrap(obj);
		return obj[name];
	},
	setItem: function (array, index, value) {
		try {
			array = XPCNativeWrapper.unwrap(array);
			array[index] = value;
		} catch (e) {
			return GeckoScriptBridge.prototype.errorInfo(e);
		}
		return void (0);
	},
	getItem: function (array, index) {
		array = XPCNativeWrapper.unwrap(array);
		return array[index];
	},
	dispatch: function (runnable, window) {
		var sandbox;
		//var principal = Cc["@mozilla.org/systemprincipal;1"].createInstance(Ci.nsIPrincipal);
		try {
			sandbox = new Components.utils.Sandbox(window, { sandboxPrototype: window, wantComponents: false });
			sandbox.runme = function () { runnable.run() };
			Cu.evalInSandbox("this.runme()", sandbox);
		} finally {
			if (sandbox) Cu.nukeSandbox(sandbox);
		}
	},
	createProxy: function (obj) {
		if (obj == null)
			throw Components.results.NS_ERROR_NULL_POINTER;
		var comObj = obj.QueryInterface(nsIScriptableComObject);
		if (comObj == null)
			throw Components.results.NS_ERROR_INVALID_ARG;

		var handlerObj = {};

		var isReadOnly = true,
			flags = comObj.handlerFlags;

		if (isReadOnly) {
			handlerObj.__bag = {};
		}
		if (isReadOnly || (flags & nsIScriptableComObject.PROPERTY_GETTER) || (flags & nsIScriptableComObject.PROPERTY_GETTER_LISTENER)) {
			handlerObj.get = function (proxy, name) {
				if (flags & nsIScriptableComObject.PROPERTY_GETTER_LISTENER) {
					comObj.notify(this, 'get', name, undefined);
				}
				if (flags & nsIScriptableComObject.PROPERTY_GETTER) {
					return comObj.getProperty(this, name);
				} else if (isReadOnly) {
					return this.__bag[name];
				} else {
					return proxy[name];
				}
			}
		}

		if (isReadOnly || (flags & nsIScriptableComObject.PROPERTY_SETTER) || (flags & nsIScriptableComObject.PROPERTY_SETTER_LISTENER)) {
			handlerObj.defineProperty = function (proxy, name, descriptor) {
				var value = descriptor.value;
				if (flags & nsIScriptableComObject.PROPERTY_SETTER_LISTENER) {
					var vstr;
					if (typeof value == 'object') {
						value = XPCNativeWrapper.unwrap(value);
						if (value === null) {
							vstr = 'null';
						} else if (value.constructor && value.constructor.name) {
							vstr = value.constructor.name + '( ' + value + ' )';
						} else {
							vstr = value + '';
						}
					} else {
						vstr = value + '';
					}
					comObj.notify('defineProperty', name, vstr, undefined);
				}
				if (flags & nsIScriptableComObject.PROPERTY_SETTER) {
					return comObj.defineProperty(this, name, value, descriptor);
				} else if (isReadOnly) {
					Object.defineProperty(this.__bag, name, descriptor);
				} else {
					Object.defineProperty(proxy, name, descriptor);
				}
				return true;
			};

			handlerObj.set = function (proxy, name, value) {
				if (flags & nsIScriptableComObject.PROPERTY_SETTER_LISTENER) {
					var vstr;
					if (typeof value == 'object') {
						value = XPCNativeWrapper.unwrap(value);
						if (value === null) {
							vstr = 'null';
						} else if (value.constructor && value.constructor.name) {
							vstr = value.constructor.name + '( ' + value + ' )';
						} else {
							vstr = value + '';
						}
					} else {
						vstr = value + '';
					}
					comObj.notify(this, 'set', name, vstr);
				}
				if (flags & nsIScriptableComObject.PROPERTY_SETTER) {
					return comObj.setProperty(this, name, value);
				} else if (isReadOnly) {
					this.__bag[name] = value;
				} else {
					proxy[name] = value;
				}
				return true;
			};
		}

		if (isReadOnly || (flags & nsIScriptableComObject.PROPERTY_DELETE_LISTENER)) {
			handlerObj.deleteProperty = function (proxy, name) {
				if (flags & nsIScriptableComObject.PROPERTY_DELETE_LISTENER) {
					comObj.notify(this, 'deleteProperty', name, undefined);
				}
				if (flags & nsIScriptableComObject.PROPERTY_SETTER) {
					return true;
				} else if (isReadOnly) {
					delete this.__bag[name];
				} else {
					delete proxy[name];
				}
				return true;
			}
		}

		return new Proxy(obj, handlerObj);
	},
	declareFunction: function (obj, name, handler, readonly, enumerable, configurable) {
		let fun = function () {
			try {
				return handler.call(XPCNativeWrapper.unwrap(obj), name, this, arguments.length, Array.slice(arguments));
			} catch (e) {
				let ex = handler.exception;
				if (ex != null)
					throw ex;

				ex = new Error();
				let info = /^(.*?)@(.*):(\d+):(\d+)$/.exec(ex.stack.split('\n', 2)[1] + '');
				if (info.length != 5)
					throw e;
				let ctor = this[handler.errorType || 'Error'] || Error;
				ex = new ctor(handler.errorMessage || 'runtime exception', info[2], (info[3] | 0));
				ex.columnNumber = (info[4] | 0);
				throw ex;
			}
		};
		fun.toString = fun.toSource = new Function("return 'function " + name + "() { [native code] }'");
		return GeckoScriptBridge.prototype.defineProperty(obj, name, fun, readonly, enumerable, configurable);
	},
	call: function (func, thisArg, aCount, args) {
		try {
			if (typeof func == 'string')
				func = new Function(func);
			if (typeof func != 'function')
				throw new TypeError("'func' is not a function");
			return func.apply(thisArg, args);
		} catch (e) {
			return GeckoScriptBridge.prototype.errorInfo(e);
		}
	},
	create: function (ctor, thisArg, aCount, args) {
		try {
			let self = XPCNativeWrapper.unwrap(thisArg);
			let applyToConstructor = function (constructor, argArray) {
				let args = [null].concat(argArray);
				let factoryFunction = constructor.bind.apply(constructor, args);
				return new factoryFunction();
			}
			if (typeof ctor == 'string')
				ctor = self[ctor];
			return applyToConstructor(ctor, args);
		} catch (e) {
			return GeckoScriptBridge.prototype.errorInfo(e);
		}
	},
	deleteProperty: function (obj, name, value) {
		obj = XPCNativeWrapper.unwrap(obj);
		return delete obj[name];
	},
	getValidScope: function (scope) {
		if (scope == null || typeof scope != 'object') {
			return new Object();
		} else {
			return XPCNativeWrapper.unwrap(scope);
		}
	},
	createObject: function (prototype, scope) {
		with (GeckoScriptBridge.prototype.getValidScope(scope)) {
			return Object.create(prototype);
		}
	},
	getType: function(value) {
		return typeof value;
	},
	isArray: function(value) {
		return Array.isArray(value);
	},
	createObjectProxy: function (obj, scope, handler, overrideToString) {
		with (GeckoScriptBridge.prototype.getValidScope(scope)) {
			obj = (obj != null ? obj : new Object());
			if (overrideToString) {
				obj.toString = handler.toString;
			} else if(typeof obj[Symbol.toStringTag] == 'undefined') {
				try {
					var s = obj.toString();
					if (s !== '[object Object]' && s.indexOf('[object ') === 0) {
						obj[Symbol.toStringTag] = s.substring(8, s.length - 1);
					}
				} catch(e) { }
			}
			obj = new Proxy(obj, {
				getPrototypeOf: handler.getPrototypeOf,
				setPrototypeOf: handler.setPrototypeOf,
				isExtensible: handler.isExtensible,
				preventExtensions: handler.preventExtensions,
				getOwnPropertyDescriptor: handler.getOwnPropertyDescriptor,
				defineProperty: handler.defineProperty,
				has: handler.has,
				get: function(target, name, receiver) {
					if (name == 'QueryInterface')
						return target.QueryInterface;
					return handler.get(target, name, receiver);
				},
				set: handler.set,
				deleteProperty: handler.deleteProperty,
				ownKeys: handler.ownKeys,
				apply: handler.apply,
				construct: handler.construct,
				enumerate: function*(target) {
					var enumerator = handler.enumerate(target);
					if(enumerator && typeof enumerator.next == 'function') {
						yield* enumerator;
						return;
					}
					for (var n in target) {
						yield n;
					}
				},
			});
			return obj;
		}
	},
	createFunctionProxy: function (func, args, scope, handler, overrideToString) {
		if (typeof func != 'function') {
			var name = (typeof func == 'string' && func !== '') ? func : null;
			var fArgs = new Array();
			if (args instanceof Array) {
				for (var i = 0, n = args.length; i < n; i++) {
					var arg = args[i];
					if (typeof arg != 'string')
						throw TypeError("The argument 'args' must be a string array!");
					fArgs.push(arg);
				}
			}
			if (false && name != null) {
				with (GeckoScriptBridge.prototype.getValidScope(scope)) {
					func = eval('function ' + name + '(' + fArgs.join(', ') + ') { }; ' + name);
				}
			} else {
				fArgs.push('');
				func = Function.apply(GeckoScriptBridge.prototype.getValidScope(scope), fArgs);
			}
		}
		if (overrideToString) {
			func.toString = handler.toString;
		}
		return GeckoScriptBridge.prototype.createObjectProxy(func, scope, handler, overrideToString);
	},
	getPropertyBySymbolName: function (obj, symbolName) {
		obj = XPCNativeWrapper.unwrap(obj);
		return obj[(Symbol[symbolName] || Symbol.for(symbolName))];
	},
	run: function(runnable) {
		runnable.run();
	}

};

// export
var NSGetFactory = XPCOMUtils.generateNSGetFactory([GeckoScriptBridge]);
