
"use strict";

const { classes: Cc, utils: Cu, interfaces: Ci, results: Cr } = Components;

Cu.import("resource://gre/modules/XPCOMUtils.jsm");


function XulfxCookieHelper() {
	this.cm = Cc["@mozilla.org/cookiemanager;1"].getService(Ci.nsICookieManager2);
}

XulfxCookieHelper.prototype = {
	classDescription: "XulfxCookieHelper",
	classID: Components.ID("{2b44817e-2159-11e7-93ae-92361f002671}"),
	contractID: "@xulfx/cookie/helper;1",
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIXulfxCookieManager]),
	_xpcom_factory: XPCOMUtils.generateSingletonFactory(XulfxCookieHelper),

	remove: function(aHost, aName, aPath, aBlocked) {
		this.cm.remove(aHost, aName, aPath, aBlocked);
	},
	add: function(aHost, aPath, aName, aValue, aIsSecure, aIsHttpOnly, aIsSession, aExpiry) {
		this.cm.add(aHost, aPath, aName, aValue, aIsSecure, aIsHttpOnly, aIsSession, aExpiry);
	},
	cookieExists: function (aCookie) {
		return this.cm.cookieExists(aCookie);
	},
	getCookiesFromHost: function(aHost) {
		return this.cm.getCookiesFromHost(aHost);
	}
};
var NSGetFactory = XPCOMUtils.generateNSGetFactory([XulfxCookieHelper]);
