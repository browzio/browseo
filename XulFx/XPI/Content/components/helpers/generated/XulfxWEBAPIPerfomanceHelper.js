"use strict"
const { classes: Cc, utils: Cu, interfaces: Ci, results: Cr } = Components;
Cu.import("resource://gre/modules/XPCOMUtils.jsm");

function XulfxPerformanceTiming() {
	this.instance = null
}
XulfxPerformanceTiming.prototype = {
	classDescription: "XulfxPerformanceTiming",
	classID: Components.ID("{c85aaf5d-7efd-4ec4-85f4-504c6c861357}"),
	contractID: "@xulfx/webapi/perfomance-timing;1",
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIXulfxPerformanceTiming]),

	init: function(timing) {
		this.instance = timing;
		return this;
	},
	get realInstance() {
		return this.instance
	},
	get navigationStart() {
		return this.instance.navigationStart
	},
	get unloadEventStart() {
		return this.instance.unloadEventStart
	},
	get unloadEventEnd() {
		return this.instance.unloadEventEnd
	},
	get redirectStart() {
		return this.instance.redirectStart
	},
	get redirectEnd() {
		return this.instance.redirectEnd
	},
	get fetchStart() {
		return this.instance.fetchStart
	},
	get domainLookupStart() {
		return this.instance.domainLookupStart
	},
	get domainLookupEnd() {
		return this.instance.domainLookupEnd
	},
	get connectStart() {
		return this.instance.connectStart
	},
	get connectEnd() {
		return this.instance.connectEnd
	},
	get secureConnectionStart() {
		return this.instance.secureConnectionStart
	},
	get requestStart() {
		return this.instance.requestStart
	},
	get responseStart() {
		return this.instance.responseStart
	},
	get responseEnd() {
		return this.instance.responseEnd
	},
	get domLoading() {
		return this.instance.domLoading
	},
	get domInteractive() {
		return this.instance.domInteractive
	},
	get domContentLoadedEventStart() {
		return this.instance.domContentLoadedEventStart
	},
	get domContentLoadedEventEnd() {
		return this.instance.domContentLoadedEventEnd
	},
	get domComplete() {
		return this.instance.domComplete
	},
	get loadEventStart() {
		return this.instance.loadEventStart
	},
	get loadEventEnd() {
		return this.instance.loadEventEnd
	},
}

function XulfxPerformanceNavigation() {
	this.instance = null
}
XulfxPerformanceNavigation.prototype = {
	classDescription: "XulfxPerformanceNavigation",
	classID: Components.ID("{c20ee8e7-1360-4e88-872d-4e991fd3a003}"),
	contractID: "@xulfx/webapi/perfomance-navigation;1",
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIXulfxPerformanceNavigation]),

	init: function(navigation) {
		this.instance = navigation;
		return this;
	},
	get realInstance() {
		return this.instance
	},
	get type() {
		return this.instance.type
	},
	get redirectCount() {
		return this.instance.redirectCount
	},
}

function XulfxWEBAPIPerfomanceHelper() {
	this.instance = null
}
XulfxWEBAPIPerfomanceHelper.prototype = {
	classDescription: "XulfxWEBAPIPerfomanceHelper",
	classID: Components.ID("{eb787633-0758-47fb-abf5-81c3bbf40e93}"),
	contractID: "@xulfx/webapi/perfomance;1",
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIXulfxWEBAPIPerfomanceHelper]),

	init: function(perfomance) {
		this.instance = perfomance
	},
	get realInstance() {
		return this.instance
	},
	get timing() {
		return new XulfxPerformanceTiming().init(this.instance.timing);
	},
	get navigation() {
		return new XulfxPerformanceNavigation().init(this.instance.navigation);
	},
	now: function() {
		return this.instance.now()
	},
	mark: function(name) {
		this.instance.mark(name)
	},
	clearResourceTimings: function() {
		this.instance.clearResourceTimings()
	},
	setResourceTimingBufferSize: function(maxSize) {
		this.instance.setResourceTimingBufferSize(maxSize)
	},
	measure: function(measureName, startMarkName, endMarkName) {
		this.instance.measure(measureName, startMarkName, endMarkName)
	},
}



var NSGetFactory = XPCOMUtils.generateNSGetFactory([XulfxPerformanceTiming, XulfxPerformanceNavigation, XulfxWEBAPIPerfomanceHelper]);
