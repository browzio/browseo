/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsPIWindowWatcher.idl
 */

#ifndef __gen_nsPIWindowWatcher_h__
#define __gen_nsPIWindowWatcher_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class mozIDOMWindowProxy; /* forward declaration */

class nsIDOMWindow; /* forward declaration */

class nsISimpleEnumerator; /* forward declaration */

class nsIWebBrowserChrome; /* forward declaration */

class nsIDocShellTreeItem; /* forward declaration */

class nsIArray; /* forward declaration */

class nsITabParent; /* forward declaration */

class nsIDocShellLoadInfo; /* forward declaration */


/* starting interface:    nsPIWindowWatcher */
#define NS_PIWINDOWWATCHER_IID_STR "d162f9c4-19d5-4723-931f-f1e51bfa9f68"

#define NS_PIWINDOWWATCHER_IID \
  {0xd162f9c4, 0x19d5, 0x4723, \
    { 0x93, 0x1f, 0xf1, 0xe5, 0x1b, 0xfa, 0x9f, 0x68 }}

class NS_NO_VTABLE nsPIWindowWatcher : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_PIWINDOWWATCHER_IID)

  /* void addWindow (in mozIDOMWindowProxy aWindow, in nsIWebBrowserChrome aChrome); */
  NS_IMETHOD AddWindow(mozIDOMWindowProxy *aWindow, nsIWebBrowserChrome *aChrome) = 0;

  /* void removeWindow (in mozIDOMWindowProxy aWindow); */
  NS_IMETHOD RemoveWindow(mozIDOMWindowProxy *aWindow) = 0;

  /* mozIDOMWindowProxy openWindow2 (in mozIDOMWindowProxy aParent, in string aUrl, in string aName, in string aFeatures, in boolean aCalledFromScript, in boolean aDialog, in boolean aNavigate, in nsISupports aArgs, in boolean aIsPopupSpam, in boolean aForceNoOpener, in nsIDocShellLoadInfo aLoadInfo); */
  NS_IMETHOD OpenWindow2(mozIDOMWindowProxy *aParent, const char * aUrl, const char * aName, const char * aFeatures, bool aCalledFromScript, bool aDialog, bool aNavigate, nsISupports *aArgs, bool aIsPopupSpam, bool aForceNoOpener, nsIDocShellLoadInfo *aLoadInfo, mozIDOMWindowProxy * *_retval) = 0;

  /* nsITabParent openWindowWithoutParent (); */
  NS_IMETHOD OpenWindowWithoutParent(nsITabParent * *_retval) = 0;

  /* nsITabParent openWindowWithTabParent (in nsITabParent aOpeningTab, in ACString aFeatures, in boolean aCalledFromJS, in float aOpenerFullZoom); */
  NS_IMETHOD OpenWindowWithTabParent(nsITabParent *aOpeningTab, const nsACString & aFeatures, bool aCalledFromJS, float aOpenerFullZoom, nsITabParent * *_retval) = 0;

  /* nsIDocShellTreeItem findItemWithName (in AString aName, in nsIDocShellTreeItem aRequestor, in nsIDocShellTreeItem aOriginalRequestor); */
  NS_IMETHOD FindItemWithName(const nsAString & aName, nsIDocShellTreeItem *aRequestor, nsIDocShellTreeItem *aOriginalRequestor, nsIDocShellTreeItem * *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsPIWindowWatcher, NS_PIWINDOWWATCHER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSPIWINDOWWATCHER \
  NS_IMETHOD AddWindow(mozIDOMWindowProxy *aWindow, nsIWebBrowserChrome *aChrome) override; \
  NS_IMETHOD RemoveWindow(mozIDOMWindowProxy *aWindow) override; \
  NS_IMETHOD OpenWindow2(mozIDOMWindowProxy *aParent, const char * aUrl, const char * aName, const char * aFeatures, bool aCalledFromScript, bool aDialog, bool aNavigate, nsISupports *aArgs, bool aIsPopupSpam, bool aForceNoOpener, nsIDocShellLoadInfo *aLoadInfo, mozIDOMWindowProxy * *_retval) override; \
  NS_IMETHOD OpenWindowWithoutParent(nsITabParent * *_retval) override; \
  NS_IMETHOD OpenWindowWithTabParent(nsITabParent *aOpeningTab, const nsACString & aFeatures, bool aCalledFromJS, float aOpenerFullZoom, nsITabParent * *_retval) override; \
  NS_IMETHOD FindItemWithName(const nsAString & aName, nsIDocShellTreeItem *aRequestor, nsIDocShellTreeItem *aOriginalRequestor, nsIDocShellTreeItem * *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSPIWINDOWWATCHER \
  NS_METHOD AddWindow(mozIDOMWindowProxy *aWindow, nsIWebBrowserChrome *aChrome); \
  NS_METHOD RemoveWindow(mozIDOMWindowProxy *aWindow); \
  NS_METHOD OpenWindow2(mozIDOMWindowProxy *aParent, const char * aUrl, const char * aName, const char * aFeatures, bool aCalledFromScript, bool aDialog, bool aNavigate, nsISupports *aArgs, bool aIsPopupSpam, bool aForceNoOpener, nsIDocShellLoadInfo *aLoadInfo, mozIDOMWindowProxy * *_retval); \
  NS_METHOD OpenWindowWithoutParent(nsITabParent * *_retval); \
  NS_METHOD OpenWindowWithTabParent(nsITabParent *aOpeningTab, const nsACString & aFeatures, bool aCalledFromJS, float aOpenerFullZoom, nsITabParent * *_retval); \
  NS_METHOD FindItemWithName(const nsAString & aName, nsIDocShellTreeItem *aRequestor, nsIDocShellTreeItem *aOriginalRequestor, nsIDocShellTreeItem * *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSPIWINDOWWATCHER(_to) \
  NS_IMETHOD AddWindow(mozIDOMWindowProxy *aWindow, nsIWebBrowserChrome *aChrome) override { return _to AddWindow(aWindow, aChrome); } \
  NS_IMETHOD RemoveWindow(mozIDOMWindowProxy *aWindow) override { return _to RemoveWindow(aWindow); } \
  NS_IMETHOD OpenWindow2(mozIDOMWindowProxy *aParent, const char * aUrl, const char * aName, const char * aFeatures, bool aCalledFromScript, bool aDialog, bool aNavigate, nsISupports *aArgs, bool aIsPopupSpam, bool aForceNoOpener, nsIDocShellLoadInfo *aLoadInfo, mozIDOMWindowProxy * *_retval) override { return _to OpenWindow2(aParent, aUrl, aName, aFeatures, aCalledFromScript, aDialog, aNavigate, aArgs, aIsPopupSpam, aForceNoOpener, aLoadInfo, _retval); } \
  NS_IMETHOD OpenWindowWithoutParent(nsITabParent * *_retval) override { return _to OpenWindowWithoutParent(_retval); } \
  NS_IMETHOD OpenWindowWithTabParent(nsITabParent *aOpeningTab, const nsACString & aFeatures, bool aCalledFromJS, float aOpenerFullZoom, nsITabParent * *_retval) override { return _to OpenWindowWithTabParent(aOpeningTab, aFeatures, aCalledFromJS, aOpenerFullZoom, _retval); } \
  NS_IMETHOD FindItemWithName(const nsAString & aName, nsIDocShellTreeItem *aRequestor, nsIDocShellTreeItem *aOriginalRequestor, nsIDocShellTreeItem * *_retval) override { return _to FindItemWithName(aName, aRequestor, aOriginalRequestor, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSPIWINDOWWATCHER(_to) \
  NS_IMETHOD AddWindow(mozIDOMWindowProxy *aWindow, nsIWebBrowserChrome *aChrome) override { return !_to ? NS_ERROR_NULL_POINTER : _to->AddWindow(aWindow, aChrome); } \
  NS_IMETHOD RemoveWindow(mozIDOMWindowProxy *aWindow) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RemoveWindow(aWindow); } \
  NS_IMETHOD OpenWindow2(mozIDOMWindowProxy *aParent, const char * aUrl, const char * aName, const char * aFeatures, bool aCalledFromScript, bool aDialog, bool aNavigate, nsISupports *aArgs, bool aIsPopupSpam, bool aForceNoOpener, nsIDocShellLoadInfo *aLoadInfo, mozIDOMWindowProxy * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OpenWindow2(aParent, aUrl, aName, aFeatures, aCalledFromScript, aDialog, aNavigate, aArgs, aIsPopupSpam, aForceNoOpener, aLoadInfo, _retval); } \
  NS_IMETHOD OpenWindowWithoutParent(nsITabParent * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OpenWindowWithoutParent(_retval); } \
  NS_IMETHOD OpenWindowWithTabParent(nsITabParent *aOpeningTab, const nsACString & aFeatures, bool aCalledFromJS, float aOpenerFullZoom, nsITabParent * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OpenWindowWithTabParent(aOpeningTab, aFeatures, aCalledFromJS, aOpenerFullZoom, _retval); } \
  NS_IMETHOD FindItemWithName(const nsAString & aName, nsIDocShellTreeItem *aRequestor, nsIDocShellTreeItem *aOriginalRequestor, nsIDocShellTreeItem * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->FindItemWithName(aName, aRequestor, aOriginalRequestor, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class _MYCLASS_ : public nsPIWindowWatcher
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSPIWINDOWWATCHER

  _MYCLASS_();

private:
  ~_MYCLASS_();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(_MYCLASS_, nsPIWindowWatcher)

_MYCLASS_::_MYCLASS_()
{
  /* member initializers and constructor code */
}

_MYCLASS_::~_MYCLASS_()
{
  /* destructor code */
}

/* void addWindow (in mozIDOMWindowProxy aWindow, in nsIWebBrowserChrome aChrome); */
NS_IMETHODIMP _MYCLASS_::AddWindow(mozIDOMWindowProxy *aWindow, nsIWebBrowserChrome *aChrome)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void removeWindow (in mozIDOMWindowProxy aWindow); */
NS_IMETHODIMP _MYCLASS_::RemoveWindow(mozIDOMWindowProxy *aWindow)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* mozIDOMWindowProxy openWindow2 (in mozIDOMWindowProxy aParent, in string aUrl, in string aName, in string aFeatures, in boolean aCalledFromScript, in boolean aDialog, in boolean aNavigate, in nsISupports aArgs, in boolean aIsPopupSpam, in boolean aForceNoOpener, in nsIDocShellLoadInfo aLoadInfo); */
NS_IMETHODIMP _MYCLASS_::OpenWindow2(mozIDOMWindowProxy *aParent, const char * aUrl, const char * aName, const char * aFeatures, bool aCalledFromScript, bool aDialog, bool aNavigate, nsISupports *aArgs, bool aIsPopupSpam, bool aForceNoOpener, nsIDocShellLoadInfo *aLoadInfo, mozIDOMWindowProxy * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsITabParent openWindowWithoutParent (); */
NS_IMETHODIMP _MYCLASS_::OpenWindowWithoutParent(nsITabParent * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsITabParent openWindowWithTabParent (in nsITabParent aOpeningTab, in ACString aFeatures, in boolean aCalledFromJS, in float aOpenerFullZoom); */
NS_IMETHODIMP _MYCLASS_::OpenWindowWithTabParent(nsITabParent *aOpeningTab, const nsACString & aFeatures, bool aCalledFromJS, float aOpenerFullZoom, nsITabParent * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsIDocShellTreeItem findItemWithName (in AString aName, in nsIDocShellTreeItem aRequestor, in nsIDocShellTreeItem aOriginalRequestor); */
NS_IMETHODIMP _MYCLASS_::FindItemWithName(const nsAString & aName, nsIDocShellTreeItem *aRequestor, nsIDocShellTreeItem *aOriginalRequestor, nsIDocShellTreeItem * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsPIWindowWatcher_h__ */
