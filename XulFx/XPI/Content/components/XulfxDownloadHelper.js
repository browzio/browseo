
"use strict";

const { classes: Cc, utils: Cu, interfaces: Ci, results: Cr } = Components;

Cu.import("resource://gre/modules/XPCOMUtils.jsm");
Cu.import("resource://gre/modules/Task.jsm");
Cu.import("resource://gre/modules/Downloads.jsm");
Cu.import("resource://gre/modules/Services.jsm");
Cu.import("resource://gre/modules/FileUtils.jsm");
Cu.import("resource://gre/modules/NetUtil.jsm");



//#region XulfxDownloadHelper

function XulfxDownloadHelper() {
	this.list = null;
	this.view = null;
}

XulfxDownloadHelper.prototype = {
	classDescription: "XulfxDownloadHelper",
	classID: Components.ID("{3275a21c-1ec4-11e7-93ae-92361f002671}"),
	contractID: "@xulfx/download/helper;1",
	QueryInterface: XPCOMUtils.generateQI([Ci.nsICancelable, Ci.nsIRunnable]),
	_xpcom_factory: XPCOMUtils.generateSingletonFactory(XulfxDownloadHelper),

	getDownloadProxy: function (download) {
		let downloadProxy = download.__xulfxDownloadProxy;
		if(downloadProxy == null) {
			downloadProxy = new XulfxDownloadProxy(download);
			download.__xulfxDownloadProxy = downloadProxy;
		}
		return downloadProxy;
	},

	run: function() {
		if(this.list != null)
			throw Cr.NS_ERROR_ALREADY_INITIALIZED;

		var self = this;
		Task.spawn(function() {
			self.list = yield Downloads.getList(Downloads.ALL);
			self.view = {
				onDownloadAdded: function(download) {
					Services.obs.notifyObservers(self.getDownloadProxy(download), 'dl-start', null);
				},
				onDownloadChanged: function(download) {
					let subject = self.getDownloadProxy(download);
					if(download.succeeded) {
						Services.obs.notifyObservers(subject, 'dl-done', null);
					} else if(download.canceled && !download.stopped) {
						Services.obs.notifyObservers(subject, 'dl-cancel', null);
					}  else if(download.error != null) {
						Services.obs.notifyObservers(subject, 'dl-failed', download.error.toString());
					}
				}
			};
			yield self.list.addView(self.view);
		}).catch(Cu.reportError);
	},
	cancel: function() {
		var self = this;
		
		if(self.list == null || self.view == null)
			throw Cr.NS_ERROR_NOT_INITIALIZED;
		self.list.removeView(self.view);

		self.list = null;
		self.view = null;
	}
};

//#endregion



//#region XulfxDownloadProxy

function XulfxDownloadProxy(download) { 
	this.instance = download;
}

XulfxDownloadProxy.prototype = {
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIDownload, Ci.nsICancelable]),


	// nsIWebProgressListener implementation
	onStateChange: function() { },
	onProgressChange: function() { },
	onLocationChange: function() { },
	onSecurityChange: function() { },

	// nsIWebProgressListener2 implementation
	onProgressChange64: function() { },
	onRefreshAttempted: function() { },

	// nsITransfer implementation
	init: function() {
		throw Cr.NS_ERROR_NOT_AVAILABLE;
	},
	setSha256Hash: function() {
		throw Cr.NS_ERROR_NOT_AVAILABLE;
	},
	setSignatureInfo: function() {
		throw Cr.NS_ERROR_NOT_AVAILABLE;
	},
	setRedirects: function() {
		throw Cr.NS_ERROR_NOT_AVAILABLE;
	},

	// nsIDownload implementation
	cancel: function(reason) {
		this.instance.finalize(true).catch(Cu.reportError);
	},
	pause: function() {
		throw Cr.NS_ERROR_NOT_IMPLEMENTED;
	},
	resume: function() {
		throw Cr.NS_ERROR_NOT_IMPLEMENTED;
	},
	remove: function() {
		this.instance.finalize(true).catch(Cu.reportError);
	},
	retry: function() {
		throw Cr.NS_ERROR_NOT_IMPLEMENTED;
	},
	get isPrivate() { // bool
		return this.instance.source.isPrivate;
	},
	get resumable() { // bool
		return false;
	},
	get referrer() { // nsIURI
		let referrer = this.instance.source.referrer;
		if (referrer == null)
			return null;
		return NetUtil.newURI(referrer);
	},
	get state() { // int16
		let dl = this.instance;
		if(dl.succeeded)
			return Ci.nsIDownloadManager.DOWNLOAD_FINISHED;
		if(dl.canceled)
			return Ci.nsIDownloadManager.DOWNLOAD_CANCELED;
		if(dl.error != null)
			return Ci.nsIDownloadManager.DOWNLOAD_FAILED;
		return Ci.nsIDownloadManager.DOWNLOAD_DOWNLOADING;
	},
	get guid() { // AString
		throw Cr.NS_ERROR_NOT_IMPLEMENTED;
	},
	get id() { // uint32
		throw Cr.NS_ERROR_NOT_IMPLEMENTED;
	},
	get MIMEInfo() { // nsIMIMEInfo
		let contentType = this.instance.contentType;
		if (contentType == null)
			return null;
		let mimeService = Cc["@mozilla.org/mime;1"].getService(Ci.nsIMIMEService);
		if (mimeService == null)
			return null;
		return mimeService.getFromTypeAndExtension(contentType, null);
	},
	get speed() { // double
		throw Cr.NS_ERROR_NOT_IMPLEMENTED;
	},
	get startTime() { // int64
		return this.instance.startTime.getTime() * 1000;
	},
	get displayName() { // AString
		return this.instance.source.url;
	},
	get cancelable() { // nsICancelable
		if(this.instance.stopped)
			return null;
		return this;
	},
	get target() { // nsIURI
		return Services.io.newFileURI(new FileUtils.File(this.instance.target.path));
	},
	get source() { // nsIURI
		return NetUtil.newURI(this.instance.source.url);
	},
	get size() { // int64
		return this.instance.totalBytes;
		//return this.instance.target.size;
	},
	get amountTransferred() { // int64
		let dl = this.instance;
		if(dl.succeeded)
			return dl.totalBytes;
		return dl.currentBytes;
	},
	get percentComplete() { // int32
		let dl = this.instance;
		let size = dl.totalBytes;
		if (size == 0 || dl.succeeded) return 100;
		return dl.currentBytes * 100 / size;
	},
	get targetFile() { // nsIFile
		return new FileUtils.File(this.instance.target.path);
	}
};

//#endregion

var NSGetFactory = XPCOMUtils.generateNSGetFactory([XulfxDownloadHelper]);