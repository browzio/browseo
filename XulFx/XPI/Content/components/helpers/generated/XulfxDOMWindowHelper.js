"use strict"
const { classes: Cc, utils: Cu, interfaces: Ci, results: Cr } = Components;
Cu.import("resource://gre/modules/XPCOMUtils.jsm");
Cu.import("resource://gre/modules/Services.jsm");

function XulfxDOMWindowHelper() {
	this.instance = null
}
XulfxDOMWindowHelper.prototype = {
	classDescription: "XulfxDOMWindowHelper",
	classID: Components.ID("{0f38b318-1f03-11e7-93ae-92361f002671}"),
	contractID: "@xulfx/dom/window;1",
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIXulfxDOMWindowHelper]),

	init: function(window) {
	    this.instance = window;
	},
	setCursor: function(cursor) {
		this.instance.setCursor(cursor)
	},
	sizeToContent: function() {
		this.instance.sizeToContent()
	},
	updateCommands: function(action, sel, reason) {
		this.instance.updateCommands(action, sel, reason)
	},
	get realInstance() {
		return this.instance
	},
	get mozWindow() {
		return this.instance;
	},
	get windowRoot() {
		return this.instance.windowRoot
	},
	get mozPaintCount() {
		return this.instance.mozPaintCount
	},
	get controllers() {
		return this.instance.controllers
	},
	get window() {
		return this.instance.window
	},
	get self() {
		return this.instance.self
	},
	get document() {
		return this.instance.document
	},
	get name() {
		return this.instance.name
	},
	set name(value) {
		this.instance.name = value
	},
	get location() {
		return this.instance.location
	},
	get history() {
		return this.instance.history
	},
	get locationbar() {
		return this.instance.locationbar
	},
	get menubar() {
		return this.instance.menubar
	},
	get personalbar() {
		return this.instance.personalbar
	},
	get scrollbars() {
		return this.instance.scrollbars
	},
	get statusbar() {
		return this.instance.statusbar
	},
	get toolbar() {
		return this.instance.toolbar
	},
	get status() {
		return this.instance.status
	},
	set status(value) {
		this.instance.status = value
	},
	get length() {
		return this.instance.length
	},
	get top() {
		return this.instance.top
	},
	get parent() {
		return this.instance.parent
	},
	get opener() {
		return this.instance.opener
	},
	set opener(value) {
		this.instance.opener = value
	},
	get frameElement() {
		return this.instance.frameElement
	},
	get navigator() {
		return this.instance.navigator
	},
	get applicationCache() {
		return this.instance.applicationCache
	},
	get sessionStorage() {
		return this.instance.sessionStorage
	},
	get localStorage() {
		return this.instance.localStorage
	},
	get indexedDB() {
		return this.instance.indexedDB
	},
	get frames() {
		return new XulfxDOMWindowCollection(this.instance.frames)
	},
	get content() {
		return this.instance.content
	},
	get closed() {
		return this.instance.closed
	},
	get fullScreen() {
		return this.instance.fullScreen
	},
	set fullScreen(value) {
		this.instance.fullScreen = value
	},
	get screen() {
		return this.instance.screen
	},
	get screenX() {
		return this.instance.screenX
	},
	set screenX(value) {
		this.instance.screenX = value
	},
	get screenY() {
		return this.instance.screenY
	},
	set screenY(value) {
		this.instance.screenY = value
	},
	get scrollX() {
		return this.instance.scrollX
	},
	get scrollY() {
		return this.instance.scrollY
	},
	get scrollMaxX() {
		return this.instance.scrollMaxX
	},
	get scrollMaxY() {
		return this.instance.scrollMaxY
	},
	get pageXOffset() {
		return this.instance.pageXOffset
	},
	get pageYOffset() {
		return this.instance.pageYOffset
	},
	get innerWidth() {
		return this.instance.innerWidth
	},
	set innerWidth(value) {
		this.instance.innerWidth = value
	},
	get innerHeight() {
		return this.instance.innerHeight
	},
	set innerHeight(value) {
		this.instance.innerHeight = value
	},
	get outerWidth() {
		return this.instance.outerWidth
	},
	set outerWidth(value) {
		this.instance.outerWidth = value
	},
	get outerHeight() {
		return this.instance.outerHeight
	},
	set outerHeight(value) {
		this.instance.outerHeight = value
	},
	get mozInnerScreenX() {
		return this.instance.mozInnerScreenX
	},
	get mozInnerScreenY() {
		return this.instance.mozInnerScreenY
	},
	get devicePixelRatio() {
		return this.instance.devicePixelRatio
	},
	get console() {
		return this.instance.console
	},
	get performance() {
		return this.instance.performance
	},
	close: function() {
		this.instance.close()
	},
	stop: function() {
		this.instance.stop()
	},
	focus: function() {
		this.instance.focus()
	},
	blur: function() {
		this.instance.blur()
	},
	alert: function(text) {
		this.instance.alert(text)
	},
	confirm: function(text) {
		return this.instance.confirm(text)
	},
	prompt: function(aMessage, aInitial) {
		return this.instance.prompt(aMessage, aInitial)
	},
	print: function() {
		this.instance.print()
	},
	showModalDialog: function(aURI, aArgs, aOptions) {
		return this.instance.showModalDialog(aURI, aArgs, aOptions)
	},
	atob: function(aAsciiString) {
		return this.instance.atob(aAsciiString)
	},
	btoa: function(aBase64Data) {
		return this.instance.btoa(aBase64Data)
	},
	getSelection: function() {
		return this.instance.getSelection()
	},
	matchMedia: function(media_query_list) {
		return this.instance.matchMedia(media_query_list)
	},
	scroll: function(xScroll, yScroll) {
		this.instance.scroll(xScroll, yScroll)
	},
	scrollTo: function(xScroll, yScroll) {
		this.instance.scrollTo(xScroll, yScroll)
	},
	scrollBy: function(xScrollDif, yScrollDif) {
		this.instance.scrollBy(xScrollDif, yScrollDif)
	},
	scrollByLines: function(numLines) {
		this.instance.scrollByLines(numLines)
	},
	scrollByPages: function(numPages) {
		this.instance.scrollByPages(numPages)
	},
	getComputedStyle: function(elt, pseudoElt) {
		return this.instance.getComputedStyle(elt, pseudoElt)
	},
	getDefaultComputedStyle: function(elt, pseudoElt) {
		return this.instance.getDefaultComputedStyle(elt, pseudoElt)
	},
	back: function() {
		this.instance.back()
	},
	forward: function() {
		this.instance.forward()
	},
	home: function() {
		this.instance.home()
	},
	moveTo: function(xPos, yPos) {
		this.instance.moveTo(xPos, yPos)
	},
	moveBy: function(xDif, yDif) {
		this.instance.moveBy(xDif, yDif)
	},
	resizeTo: function(width, height) {
		this.instance.resizeTo(width, height)
	},
	resizeBy: function(widthDif, heightDif) {
		this.instance.resizeBy(widthDif, heightDif)
	},
	open: function(url, name, options) {
		return this.instance.open(url, name, options)
	},
	openDialog: function(url, name, options, aExtraArgument) {
		return this.instance.openDialog(url, name, options, aExtraArgument)
	},
	find: function(str, caseSensitive, backwards, wrapAround, wholeWord, searchInFrames, showDialog) {
		return this.instance.find(str, caseSensitive, backwards, wrapAround, wholeWord, searchInFrames, showDialog)
	},
}


function XulfxDOMWindowCollection(collection) {
	this.windows = collection;
}
XulfxDOMWindowCollection.prototype = {
	QueryInterface: XPCOMUtils.generateQI([Ci.nsIDOMWindowCollection]),
	get length() { return this.windows.length },
	item: function(index) { return this.windows[index] },
	namedItem: function(name) { return this.windows[name] }
}


var NSGetFactory = XPCOMUtils.generateNSGetFactory([XulfxDOMWindowHelper]);










