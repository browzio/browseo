
"use strict";

const Cu = Components.utils;

Cu.import("resource://gre/modules/XPCOMUtils.jsm");


function Xulfx() { }

Xulfx.prototype = {
	classDescription: "Xulfx",
	classID: Components.ID("{515777e1-8e19-4be9-9d0d-8ca2efdcb5a2}"),
	contractID: "@xulfx/xulfx;1",
	QueryInterface: XPCOMUtils.generateQI([]),
	_xpcom_factory: XPCOMUtils.generateSingletonFactory(Xulfx)
};
var NSGetFactory = XPCOMUtils.generateNSGetFactory([Xulfx]);
