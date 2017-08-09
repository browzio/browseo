
"use strict";

const { classes: Cc, utils: Cu, interfaces: Ci, results: Cr } = Components;

Cu.import("resource://gre/modules/XPCOMUtils.jsm");


function XulfxEventHelper() {
	this.window = null;
}

XulfxEventHelper.prototype = {
	classDescription: "XulfxEventHelper",
	classID: Components.ID("{f805ca48-2158-11e7-93ae-92361f002671}"),
	contractID: "@xulfx/event/helper;1",
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIXulfxEventHelper]),

	init: function(aWindow) {
		this.window = aWindow;
	},

	createEvent: function (aType, aName, aInit) {
		if (aInit) {
			return new this.window[aType](aName, aInit);
		} else {
			return new this.window[aType](aName);
		}
	},

	createEvent2: function (aType, aName, aInit) {
		var init = { };
		if (aInit) {
			var enu = aInit.keys();
			while (enu.hasMore()) {
				var name = enu.getNext();
				init[name] = aInit.get(name);
			}
		}
		return new this.window[aType](aName, init);
	}
};
var NSGetFactory = XPCOMUtils.generateNSGetFactory([XulfxEventHelper]);
