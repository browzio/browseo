
"use strict";

const { classes: Cc, utils: Cu, interfaces: Ci, results: Cr } = Components;

Cu.import("resource://gre/modules/XPCOMUtils.jsm");


function XulfxXPathEvaluator() {
	this.documentIntance = null;
}
XulfxXPathEvaluator.prototype = {
	classDescription: "XulfxXPathEvaluator",
	classID: Components.ID("{d2435f89-6709-4e00-b0b6-31ceae45c192}"),
	contractID: "@xulfx/xpath/evaluator;1",
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIXulfxDOMXPathEvaluator]),

	init: function(aDocument) {
		this.documentIntance = aDocument;
	},
	createNSResolver: function(nodeResolver) {
		return this.document.createNSResolver(nodeResolver);
	},
	createResolver: function(aCode) {
		return Function(aCode);
	},
	evaluate: function(expression, contextNode, resolver, type) {
		return new XulfxDOMXPathResult(this.document.evaluate(expression, contextNode, resolver, type, null));
	},
	get document() {
		if(!this.documentIntance)
			throw Cr.NS_ERROR_NOT_INITIALIZED;
		return this.documentIntance;
	},
};

function XulfxDOMXPathResult(result) {
	this.instance = result;
}
XulfxDOMXPathResult.prototype = {
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIXulfxDOMXPathResult]),

	get resultType() {
		return this.instance.resultType;
	},
	get numberValue() {
		return this.instance.numberValue;
	},
	get stringValue() {
		return this.instance.stringValue;
	},
	get booleanValue() {
		return this.instance.booleanValue;
	},
	get singleNodeValue() {
		return this.instance.singleNodeValue;
	},
	get invalidIteratorState() {
		return this.instance.invalidIteratorState;
	},
	get snapshotLength() {
		return this.instance.snapshotLength;
	},
	iterateNext: function() {
		return this.instance.iterateNext();
	},
	snapshotItem: function(index) {
		return this.instance.snapshotItem(index);
	}
};

var NSGetFactory = XPCOMUtils.generateNSGetFactory([XulfxXPathEvaluator]);