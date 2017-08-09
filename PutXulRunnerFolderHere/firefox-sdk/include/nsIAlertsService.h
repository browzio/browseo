/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIAlertsService.idl
 */

#ifndef __gen_nsIAlertsService_h__
#define __gen_nsIAlertsService_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

#ifndef __gen_nsIObserver_h__
#include "nsIObserver.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class imgIRequest; /* forward declaration */

class nsICancelable; /* forward declaration */

class nsIPrincipal; /* forward declaration */

class nsIURI; /* forward declaration */

#define ALERT_NOTIFICATION_CONTRACTID "@mozilla.org/alert-notification;1"

/* starting interface:    nsIAlertNotificationImageListener */
#define NS_IALERTNOTIFICATIONIMAGELISTENER_IID_STR "a71a637d-de1d-47c6-a8d2-c60b2596f471"

#define NS_IALERTNOTIFICATIONIMAGELISTENER_IID \
  {0xa71a637d, 0xde1d, 0x47c6, \
    { 0xa8, 0xd2, 0xc6, 0x0b, 0x25, 0x96, 0xf4, 0x71 }}

class NS_NO_VTABLE nsIAlertNotificationImageListener : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IALERTNOTIFICATIONIMAGELISTENER_IID)

  /* void onImageReady (in nsISupports aUserData, in imgIRequest aRequest); */
  NS_IMETHOD OnImageReady(nsISupports *aUserData, imgIRequest *aRequest) = 0;

  /* void onImageMissing (in nsISupports aUserData); */
  NS_IMETHOD OnImageMissing(nsISupports *aUserData) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAlertNotificationImageListener, NS_IALERTNOTIFICATIONIMAGELISTENER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIALERTNOTIFICATIONIMAGELISTENER \
  NS_IMETHOD OnImageReady(nsISupports *aUserData, imgIRequest *aRequest) override; \
  NS_IMETHOD OnImageMissing(nsISupports *aUserData) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIALERTNOTIFICATIONIMAGELISTENER \
  NS_METHOD OnImageReady(nsISupports *aUserData, imgIRequest *aRequest); \
  NS_METHOD OnImageMissing(nsISupports *aUserData); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIALERTNOTIFICATIONIMAGELISTENER(_to) \
  NS_IMETHOD OnImageReady(nsISupports *aUserData, imgIRequest *aRequest) override { return _to OnImageReady(aUserData, aRequest); } \
  NS_IMETHOD OnImageMissing(nsISupports *aUserData) override { return _to OnImageMissing(aUserData); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIALERTNOTIFICATIONIMAGELISTENER(_to) \
  NS_IMETHOD OnImageReady(nsISupports *aUserData, imgIRequest *aRequest) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnImageReady(aUserData, aRequest); } \
  NS_IMETHOD OnImageMissing(nsISupports *aUserData) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnImageMissing(aUserData); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAlertNotificationImageListener : public nsIAlertNotificationImageListener
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIALERTNOTIFICATIONIMAGELISTENER

  nsAlertNotificationImageListener();

private:
  ~nsAlertNotificationImageListener();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAlertNotificationImageListener, nsIAlertNotificationImageListener)

nsAlertNotificationImageListener::nsAlertNotificationImageListener()
{
  /* member initializers and constructor code */
}

nsAlertNotificationImageListener::~nsAlertNotificationImageListener()
{
  /* destructor code */
}

/* void onImageReady (in nsISupports aUserData, in imgIRequest aRequest); */
NS_IMETHODIMP nsAlertNotificationImageListener::OnImageReady(nsISupports *aUserData, imgIRequest *aRequest)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onImageMissing (in nsISupports aUserData); */
NS_IMETHODIMP nsAlertNotificationImageListener::OnImageMissing(nsISupports *aUserData)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIAlertNotification */
#define NS_IALERTNOTIFICATION_IID_STR "cf2e4cb6-4b8f-4eca-aea9-d51a8f9f7a50"

#define NS_IALERTNOTIFICATION_IID \
  {0xcf2e4cb6, 0x4b8f, 0x4eca, \
    { 0xae, 0xa9, 0xd5, 0x1a, 0x8f, 0x9f, 0x7a, 0x50 }}

class NS_NO_VTABLE nsIAlertNotification : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IALERTNOTIFICATION_IID)

  /* void init ([optional] in AString aName, [optional] in AString aImageURL, [optional] in AString aTitle, [optional] in AString aText, [optional] in boolean aTextClickable, [optional] in AString aCookie, [optional] in AString aDir, [optional] in AString aLang, [optional] in AString aData, [optional] in nsIPrincipal aPrincipal, [optional] in boolean aInPrivateBrowsing, [optional] in boolean aRequireInteraction); */
  NS_IMETHOD Init(const nsAString & aName, const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction) = 0;

  /* readonly attribute AString name; */
  NS_IMETHOD GetName(nsAString & aName) = 0;

  /* readonly attribute AString imageURL; */
  NS_IMETHOD GetImageURL(nsAString & aImageURL) = 0;

  /* readonly attribute AString title; */
  NS_IMETHOD GetTitle(nsAString & aTitle) = 0;

  /* readonly attribute AString text; */
  NS_IMETHOD GetText(nsAString & aText) = 0;

  /* readonly attribute boolean textClickable; */
  NS_IMETHOD GetTextClickable(bool *aTextClickable) = 0;

  /* readonly attribute AString cookie; */
  NS_IMETHOD GetCookie(nsAString & aCookie) = 0;

  /* readonly attribute AString dir; */
  NS_IMETHOD GetDir(nsAString & aDir) = 0;

  /* readonly attribute AString lang; */
  NS_IMETHOD GetLang(nsAString & aLang) = 0;

  /* readonly attribute AString data; */
  NS_IMETHOD GetData(nsAString & aData) = 0;

  /* readonly attribute nsIPrincipal principal; */
  NS_IMETHOD GetPrincipal(nsIPrincipal * *aPrincipal) = 0;

  /* readonly attribute nsIURI URI; */
  NS_IMETHOD GetURI(nsIURI * *aURI) = 0;

  /* readonly attribute boolean inPrivateBrowsing; */
  NS_IMETHOD GetInPrivateBrowsing(bool *aInPrivateBrowsing) = 0;

  /* readonly attribute boolean requireInteraction; */
  NS_IMETHOD GetRequireInteraction(bool *aRequireInteraction) = 0;

  /* readonly attribute boolean actionable; */
  NS_IMETHOD GetActionable(bool *aActionable) = 0;

  /* readonly attribute AString source; */
  NS_IMETHOD GetSource(nsAString & aSource) = 0;

  /* nsICancelable loadImage (in unsigned long aTimeout, in nsIAlertNotificationImageListener aListener, [optional] in nsISupports aUserData); */
  NS_IMETHOD LoadImage(uint32_t aTimeout, nsIAlertNotificationImageListener *aListener, nsISupports *aUserData, nsICancelable * *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAlertNotification, NS_IALERTNOTIFICATION_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIALERTNOTIFICATION \
  NS_IMETHOD Init(const nsAString & aName, const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction) override; \
  NS_IMETHOD GetName(nsAString & aName) override; \
  NS_IMETHOD GetImageURL(nsAString & aImageURL) override; \
  NS_IMETHOD GetTitle(nsAString & aTitle) override; \
  NS_IMETHOD GetText(nsAString & aText) override; \
  NS_IMETHOD GetTextClickable(bool *aTextClickable) override; \
  NS_IMETHOD GetCookie(nsAString & aCookie) override; \
  NS_IMETHOD GetDir(nsAString & aDir) override; \
  NS_IMETHOD GetLang(nsAString & aLang) override; \
  NS_IMETHOD GetData(nsAString & aData) override; \
  NS_IMETHOD GetPrincipal(nsIPrincipal * *aPrincipal) override; \
  NS_IMETHOD GetURI(nsIURI * *aURI) override; \
  NS_IMETHOD GetInPrivateBrowsing(bool *aInPrivateBrowsing) override; \
  NS_IMETHOD GetRequireInteraction(bool *aRequireInteraction) override; \
  NS_IMETHOD GetActionable(bool *aActionable) override; \
  NS_IMETHOD GetSource(nsAString & aSource) override; \
  NS_IMETHOD LoadImage(uint32_t aTimeout, nsIAlertNotificationImageListener *aListener, nsISupports *aUserData, nsICancelable * *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIALERTNOTIFICATION \
  NS_METHOD Init(const nsAString & aName, const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction); \
  NS_METHOD GetName(nsAString & aName); \
  NS_METHOD GetImageURL(nsAString & aImageURL); \
  NS_METHOD GetTitle(nsAString & aTitle); \
  NS_METHOD GetText(nsAString & aText); \
  NS_METHOD GetTextClickable(bool *aTextClickable); \
  NS_METHOD GetCookie(nsAString & aCookie); \
  NS_METHOD GetDir(nsAString & aDir); \
  NS_METHOD GetLang(nsAString & aLang); \
  NS_METHOD GetData(nsAString & aData); \
  NS_METHOD GetPrincipal(nsIPrincipal * *aPrincipal); \
  NS_METHOD GetURI(nsIURI * *aURI); \
  NS_METHOD GetInPrivateBrowsing(bool *aInPrivateBrowsing); \
  NS_METHOD GetRequireInteraction(bool *aRequireInteraction); \
  NS_METHOD GetActionable(bool *aActionable); \
  NS_METHOD GetSource(nsAString & aSource); \
  NS_METHOD LoadImage(uint32_t aTimeout, nsIAlertNotificationImageListener *aListener, nsISupports *aUserData, nsICancelable * *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIALERTNOTIFICATION(_to) \
  NS_IMETHOD Init(const nsAString & aName, const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction) override { return _to Init(aName, aImageURL, aTitle, aText, aTextClickable, aCookie, aDir, aLang, aData, aPrincipal, aInPrivateBrowsing, aRequireInteraction); } \
  NS_IMETHOD GetName(nsAString & aName) override { return _to GetName(aName); } \
  NS_IMETHOD GetImageURL(nsAString & aImageURL) override { return _to GetImageURL(aImageURL); } \
  NS_IMETHOD GetTitle(nsAString & aTitle) override { return _to GetTitle(aTitle); } \
  NS_IMETHOD GetText(nsAString & aText) override { return _to GetText(aText); } \
  NS_IMETHOD GetTextClickable(bool *aTextClickable) override { return _to GetTextClickable(aTextClickable); } \
  NS_IMETHOD GetCookie(nsAString & aCookie) override { return _to GetCookie(aCookie); } \
  NS_IMETHOD GetDir(nsAString & aDir) override { return _to GetDir(aDir); } \
  NS_IMETHOD GetLang(nsAString & aLang) override { return _to GetLang(aLang); } \
  NS_IMETHOD GetData(nsAString & aData) override { return _to GetData(aData); } \
  NS_IMETHOD GetPrincipal(nsIPrincipal * *aPrincipal) override { return _to GetPrincipal(aPrincipal); } \
  NS_IMETHOD GetURI(nsIURI * *aURI) override { return _to GetURI(aURI); } \
  NS_IMETHOD GetInPrivateBrowsing(bool *aInPrivateBrowsing) override { return _to GetInPrivateBrowsing(aInPrivateBrowsing); } \
  NS_IMETHOD GetRequireInteraction(bool *aRequireInteraction) override { return _to GetRequireInteraction(aRequireInteraction); } \
  NS_IMETHOD GetActionable(bool *aActionable) override { return _to GetActionable(aActionable); } \
  NS_IMETHOD GetSource(nsAString & aSource) override { return _to GetSource(aSource); } \
  NS_IMETHOD LoadImage(uint32_t aTimeout, nsIAlertNotificationImageListener *aListener, nsISupports *aUserData, nsICancelable * *_retval) override { return _to LoadImage(aTimeout, aListener, aUserData, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIALERTNOTIFICATION(_to) \
  NS_IMETHOD Init(const nsAString & aName, const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Init(aName, aImageURL, aTitle, aText, aTextClickable, aCookie, aDir, aLang, aData, aPrincipal, aInPrivateBrowsing, aRequireInteraction); } \
  NS_IMETHOD GetName(nsAString & aName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetName(aName); } \
  NS_IMETHOD GetImageURL(nsAString & aImageURL) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetImageURL(aImageURL); } \
  NS_IMETHOD GetTitle(nsAString & aTitle) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTitle(aTitle); } \
  NS_IMETHOD GetText(nsAString & aText) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetText(aText); } \
  NS_IMETHOD GetTextClickable(bool *aTextClickable) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTextClickable(aTextClickable); } \
  NS_IMETHOD GetCookie(nsAString & aCookie) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCookie(aCookie); } \
  NS_IMETHOD GetDir(nsAString & aDir) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDir(aDir); } \
  NS_IMETHOD GetLang(nsAString & aLang) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLang(aLang); } \
  NS_IMETHOD GetData(nsAString & aData) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetData(aData); } \
  NS_IMETHOD GetPrincipal(nsIPrincipal * *aPrincipal) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPrincipal(aPrincipal); } \
  NS_IMETHOD GetURI(nsIURI * *aURI) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetURI(aURI); } \
  NS_IMETHOD GetInPrivateBrowsing(bool *aInPrivateBrowsing) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetInPrivateBrowsing(aInPrivateBrowsing); } \
  NS_IMETHOD GetRequireInteraction(bool *aRequireInteraction) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetRequireInteraction(aRequireInteraction); } \
  NS_IMETHOD GetActionable(bool *aActionable) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetActionable(aActionable); } \
  NS_IMETHOD GetSource(nsAString & aSource) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetSource(aSource); } \
  NS_IMETHOD LoadImage(uint32_t aTimeout, nsIAlertNotificationImageListener *aListener, nsISupports *aUserData, nsICancelable * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->LoadImage(aTimeout, aListener, aUserData, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAlertNotification : public nsIAlertNotification
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIALERTNOTIFICATION

  nsAlertNotification();

private:
  ~nsAlertNotification();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAlertNotification, nsIAlertNotification)

nsAlertNotification::nsAlertNotification()
{
  /* member initializers and constructor code */
}

nsAlertNotification::~nsAlertNotification()
{
  /* destructor code */
}

/* void init ([optional] in AString aName, [optional] in AString aImageURL, [optional] in AString aTitle, [optional] in AString aText, [optional] in boolean aTextClickable, [optional] in AString aCookie, [optional] in AString aDir, [optional] in AString aLang, [optional] in AString aData, [optional] in nsIPrincipal aPrincipal, [optional] in boolean aInPrivateBrowsing, [optional] in boolean aRequireInteraction); */
NS_IMETHODIMP nsAlertNotification::Init(const nsAString & aName, const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString name; */
NS_IMETHODIMP nsAlertNotification::GetName(nsAString & aName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString imageURL; */
NS_IMETHODIMP nsAlertNotification::GetImageURL(nsAString & aImageURL)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString title; */
NS_IMETHODIMP nsAlertNotification::GetTitle(nsAString & aTitle)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString text; */
NS_IMETHODIMP nsAlertNotification::GetText(nsAString & aText)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute boolean textClickable; */
NS_IMETHODIMP nsAlertNotification::GetTextClickable(bool *aTextClickable)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString cookie; */
NS_IMETHODIMP nsAlertNotification::GetCookie(nsAString & aCookie)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString dir; */
NS_IMETHODIMP nsAlertNotification::GetDir(nsAString & aDir)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString lang; */
NS_IMETHODIMP nsAlertNotification::GetLang(nsAString & aLang)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString data; */
NS_IMETHODIMP nsAlertNotification::GetData(nsAString & aData)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIPrincipal principal; */
NS_IMETHODIMP nsAlertNotification::GetPrincipal(nsIPrincipal * *aPrincipal)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIURI URI; */
NS_IMETHODIMP nsAlertNotification::GetURI(nsIURI * *aURI)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute boolean inPrivateBrowsing; */
NS_IMETHODIMP nsAlertNotification::GetInPrivateBrowsing(bool *aInPrivateBrowsing)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute boolean requireInteraction; */
NS_IMETHODIMP nsAlertNotification::GetRequireInteraction(bool *aRequireInteraction)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute boolean actionable; */
NS_IMETHODIMP nsAlertNotification::GetActionable(bool *aActionable)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString source; */
NS_IMETHODIMP nsAlertNotification::GetSource(nsAString & aSource)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsICancelable loadImage (in unsigned long aTimeout, in nsIAlertNotificationImageListener aListener, [optional] in nsISupports aUserData); */
NS_IMETHODIMP nsAlertNotification::LoadImage(uint32_t aTimeout, nsIAlertNotificationImageListener *aListener, nsISupports *aUserData, nsICancelable * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIAlertsService */
#define NS_IALERTSSERVICE_IID_STR "f7a36392-d98b-4141-a7d7-4e46642684e3"

#define NS_IALERTSSERVICE_IID \
  {0xf7a36392, 0xd98b, 0x4141, \
    { 0xa7, 0xd7, 0x4e, 0x46, 0x64, 0x26, 0x84, 0xe3 }}

class NS_NO_VTABLE nsIAlertsService : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IALERTSSERVICE_IID)

  /* void showPersistentNotification (in AString aPersistentData, in nsIAlertNotification aAlert, [optional] in nsIObserver aAlertListener); */
  NS_IMETHOD ShowPersistentNotification(const nsAString & aPersistentData, nsIAlertNotification *aAlert, nsIObserver *aAlertListener) = 0;

  /* void showAlert (in nsIAlertNotification aAlert, [optional] in nsIObserver aAlertListener); */
  NS_IMETHOD ShowAlert(nsIAlertNotification *aAlert, nsIObserver *aAlertListener) = 0;

  /* void showAlertNotification (in AString aImageURL, in AString aTitle, in AString aText, [optional] in boolean aTextClickable, [optional] in AString aCookie, [optional] in nsIObserver aAlertListener, [optional] in AString aName, [optional] in AString aDir, [optional] in AString aLang, [optional] in AString aData, [optional] in nsIPrincipal aPrincipal, [optional] in boolean aInPrivateBrowsing, [optional] in boolean aRequireInteraction); */
  NS_IMETHOD ShowAlertNotification(const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, nsIObserver *aAlertListener, const nsAString & aName, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction) = 0;

  /* void closeAlert ([optional] in AString aName, [optional] in nsIPrincipal aPrincipal); */
  NS_IMETHOD CloseAlert(const nsAString & aName, nsIPrincipal *aPrincipal) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAlertsService, NS_IALERTSSERVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIALERTSSERVICE \
  NS_IMETHOD ShowPersistentNotification(const nsAString & aPersistentData, nsIAlertNotification *aAlert, nsIObserver *aAlertListener) override; \
  NS_IMETHOD ShowAlert(nsIAlertNotification *aAlert, nsIObserver *aAlertListener) override; \
  NS_IMETHOD ShowAlertNotification(const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, nsIObserver *aAlertListener, const nsAString & aName, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction) override; \
  NS_IMETHOD CloseAlert(const nsAString & aName, nsIPrincipal *aPrincipal) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIALERTSSERVICE \
  NS_METHOD ShowPersistentNotification(const nsAString & aPersistentData, nsIAlertNotification *aAlert, nsIObserver *aAlertListener); \
  NS_METHOD ShowAlert(nsIAlertNotification *aAlert, nsIObserver *aAlertListener); \
  NS_METHOD ShowAlertNotification(const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, nsIObserver *aAlertListener, const nsAString & aName, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction); \
  NS_METHOD CloseAlert(const nsAString & aName, nsIPrincipal *aPrincipal); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIALERTSSERVICE(_to) \
  NS_IMETHOD ShowPersistentNotification(const nsAString & aPersistentData, nsIAlertNotification *aAlert, nsIObserver *aAlertListener) override { return _to ShowPersistentNotification(aPersistentData, aAlert, aAlertListener); } \
  NS_IMETHOD ShowAlert(nsIAlertNotification *aAlert, nsIObserver *aAlertListener) override { return _to ShowAlert(aAlert, aAlertListener); } \
  NS_IMETHOD ShowAlertNotification(const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, nsIObserver *aAlertListener, const nsAString & aName, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction) override { return _to ShowAlertNotification(aImageURL, aTitle, aText, aTextClickable, aCookie, aAlertListener, aName, aDir, aLang, aData, aPrincipal, aInPrivateBrowsing, aRequireInteraction); } \
  NS_IMETHOD CloseAlert(const nsAString & aName, nsIPrincipal *aPrincipal) override { return _to CloseAlert(aName, aPrincipal); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIALERTSSERVICE(_to) \
  NS_IMETHOD ShowPersistentNotification(const nsAString & aPersistentData, nsIAlertNotification *aAlert, nsIObserver *aAlertListener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ShowPersistentNotification(aPersistentData, aAlert, aAlertListener); } \
  NS_IMETHOD ShowAlert(nsIAlertNotification *aAlert, nsIObserver *aAlertListener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ShowAlert(aAlert, aAlertListener); } \
  NS_IMETHOD ShowAlertNotification(const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, nsIObserver *aAlertListener, const nsAString & aName, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ShowAlertNotification(aImageURL, aTitle, aText, aTextClickable, aCookie, aAlertListener, aName, aDir, aLang, aData, aPrincipal, aInPrivateBrowsing, aRequireInteraction); } \
  NS_IMETHOD CloseAlert(const nsAString & aName, nsIPrincipal *aPrincipal) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CloseAlert(aName, aPrincipal); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAlertsService : public nsIAlertsService
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIALERTSSERVICE

  nsAlertsService();

private:
  ~nsAlertsService();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAlertsService, nsIAlertsService)

nsAlertsService::nsAlertsService()
{
  /* member initializers and constructor code */
}

nsAlertsService::~nsAlertsService()
{
  /* destructor code */
}

/* void showPersistentNotification (in AString aPersistentData, in nsIAlertNotification aAlert, [optional] in nsIObserver aAlertListener); */
NS_IMETHODIMP nsAlertsService::ShowPersistentNotification(const nsAString & aPersistentData, nsIAlertNotification *aAlert, nsIObserver *aAlertListener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void showAlert (in nsIAlertNotification aAlert, [optional] in nsIObserver aAlertListener); */
NS_IMETHODIMP nsAlertsService::ShowAlert(nsIAlertNotification *aAlert, nsIObserver *aAlertListener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void showAlertNotification (in AString aImageURL, in AString aTitle, in AString aText, [optional] in boolean aTextClickable, [optional] in AString aCookie, [optional] in nsIObserver aAlertListener, [optional] in AString aName, [optional] in AString aDir, [optional] in AString aLang, [optional] in AString aData, [optional] in nsIPrincipal aPrincipal, [optional] in boolean aInPrivateBrowsing, [optional] in boolean aRequireInteraction); */
NS_IMETHODIMP nsAlertsService::ShowAlertNotification(const nsAString & aImageURL, const nsAString & aTitle, const nsAString & aText, bool aTextClickable, const nsAString & aCookie, nsIObserver *aAlertListener, const nsAString & aName, const nsAString & aDir, const nsAString & aLang, const nsAString & aData, nsIPrincipal *aPrincipal, bool aInPrivateBrowsing, bool aRequireInteraction)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void closeAlert ([optional] in AString aName, [optional] in nsIPrincipal aPrincipal); */
NS_IMETHODIMP nsAlertsService::CloseAlert(const nsAString & aName, nsIPrincipal *aPrincipal)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIAlertsDoNotDisturb */
#define NS_IALERTSDONOTDISTURB_IID_STR "c5d63e3a-259d-45a8-b964-8377967cb4d2"

#define NS_IALERTSDONOTDISTURB_IID \
  {0xc5d63e3a, 0x259d, 0x45a8, \
    { 0xb9, 0x64, 0x83, 0x77, 0x96, 0x7c, 0xb4, 0xd2 }}

class NS_NO_VTABLE nsIAlertsDoNotDisturb : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IALERTSDONOTDISTURB_IID)

  /* attribute bool manualDoNotDisturb; */
  NS_IMETHOD GetManualDoNotDisturb(bool *aManualDoNotDisturb) = 0;
  NS_IMETHOD SetManualDoNotDisturb(bool aManualDoNotDisturb) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAlertsDoNotDisturb, NS_IALERTSDONOTDISTURB_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIALERTSDONOTDISTURB \
  NS_IMETHOD GetManualDoNotDisturb(bool *aManualDoNotDisturb) override; \
  NS_IMETHOD SetManualDoNotDisturb(bool aManualDoNotDisturb) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIALERTSDONOTDISTURB \
  NS_METHOD GetManualDoNotDisturb(bool *aManualDoNotDisturb); \
  NS_METHOD SetManualDoNotDisturb(bool aManualDoNotDisturb); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIALERTSDONOTDISTURB(_to) \
  NS_IMETHOD GetManualDoNotDisturb(bool *aManualDoNotDisturb) override { return _to GetManualDoNotDisturb(aManualDoNotDisturb); } \
  NS_IMETHOD SetManualDoNotDisturb(bool aManualDoNotDisturb) override { return _to SetManualDoNotDisturb(aManualDoNotDisturb); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIALERTSDONOTDISTURB(_to) \
  NS_IMETHOD GetManualDoNotDisturb(bool *aManualDoNotDisturb) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetManualDoNotDisturb(aManualDoNotDisturb); } \
  NS_IMETHOD SetManualDoNotDisturb(bool aManualDoNotDisturb) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetManualDoNotDisturb(aManualDoNotDisturb); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAlertsDoNotDisturb : public nsIAlertsDoNotDisturb
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIALERTSDONOTDISTURB

  nsAlertsDoNotDisturb();

private:
  ~nsAlertsDoNotDisturb();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAlertsDoNotDisturb, nsIAlertsDoNotDisturb)

nsAlertsDoNotDisturb::nsAlertsDoNotDisturb()
{
  /* member initializers and constructor code */
}

nsAlertsDoNotDisturb::~nsAlertsDoNotDisturb()
{
  /* destructor code */
}

/* attribute bool manualDoNotDisturb; */
NS_IMETHODIMP nsAlertsDoNotDisturb::GetManualDoNotDisturb(bool *aManualDoNotDisturb)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsAlertsDoNotDisturb::SetManualDoNotDisturb(bool aManualDoNotDisturb)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIAlertsIconData */
#define NS_IALERTSICONDATA_IID_STR "fc6d7f0a-0cf6-4268-8c71-ab640842b9b1"

#define NS_IALERTSICONDATA_IID \
  {0xfc6d7f0a, 0x0cf6, 0x4268, \
    { 0x8c, 0x71, 0xab, 0x64, 0x08, 0x42, 0xb9, 0xb1 }}

class NS_NO_VTABLE nsIAlertsIconData : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IALERTSICONDATA_IID)

  /* void showAlertWithIconData (in nsIAlertNotification aAlert, [optional] in nsIObserver aAlertListener, [optional] in uint32_t aIconSize, [array, size_is (aIconSize), const] in uint8_t aIconData); */
  NS_IMETHOD ShowAlertWithIconData(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, uint32_t aIconSize, const uint8_t *aIconData) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAlertsIconData, NS_IALERTSICONDATA_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIALERTSICONDATA \
  NS_IMETHOD ShowAlertWithIconData(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, uint32_t aIconSize, const uint8_t *aIconData) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIALERTSICONDATA \
  NS_METHOD ShowAlertWithIconData(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, uint32_t aIconSize, const uint8_t *aIconData); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIALERTSICONDATA(_to) \
  NS_IMETHOD ShowAlertWithIconData(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, uint32_t aIconSize, const uint8_t *aIconData) override { return _to ShowAlertWithIconData(aAlert, aAlertListener, aIconSize, aIconData); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIALERTSICONDATA(_to) \
  NS_IMETHOD ShowAlertWithIconData(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, uint32_t aIconSize, const uint8_t *aIconData) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ShowAlertWithIconData(aAlert, aAlertListener, aIconSize, aIconData); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAlertsIconData : public nsIAlertsIconData
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIALERTSICONDATA

  nsAlertsIconData();

private:
  ~nsAlertsIconData();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAlertsIconData, nsIAlertsIconData)

nsAlertsIconData::nsAlertsIconData()
{
  /* member initializers and constructor code */
}

nsAlertsIconData::~nsAlertsIconData()
{
  /* destructor code */
}

/* void showAlertWithIconData (in nsIAlertNotification aAlert, [optional] in nsIObserver aAlertListener, [optional] in uint32_t aIconSize, [array, size_is (aIconSize), const] in uint8_t aIconData); */
NS_IMETHODIMP nsAlertsIconData::ShowAlertWithIconData(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, uint32_t aIconSize, const uint8_t *aIconData)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIAlertsIconURI */
#define NS_IALERTSICONURI_IID_STR "f3c82915-bf60-41ea-91ce-6c46b22e381a"

#define NS_IALERTSICONURI_IID \
  {0xf3c82915, 0xbf60, 0x41ea, \
    { 0x91, 0xce, 0x6c, 0x46, 0xb2, 0x2e, 0x38, 0x1a }}

class NS_NO_VTABLE nsIAlertsIconURI : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IALERTSICONURI_IID)

  /* void showAlertWithIconURI (in nsIAlertNotification aAlert, [optional] in nsIObserver aAlertListener, [optional] in nsIURI aIconURI); */
  NS_IMETHOD ShowAlertWithIconURI(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, nsIURI *aIconURI) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAlertsIconURI, NS_IALERTSICONURI_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIALERTSICONURI \
  NS_IMETHOD ShowAlertWithIconURI(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, nsIURI *aIconURI) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIALERTSICONURI \
  NS_METHOD ShowAlertWithIconURI(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, nsIURI *aIconURI); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIALERTSICONURI(_to) \
  NS_IMETHOD ShowAlertWithIconURI(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, nsIURI *aIconURI) override { return _to ShowAlertWithIconURI(aAlert, aAlertListener, aIconURI); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIALERTSICONURI(_to) \
  NS_IMETHOD ShowAlertWithIconURI(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, nsIURI *aIconURI) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ShowAlertWithIconURI(aAlert, aAlertListener, aIconURI); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAlertsIconURI : public nsIAlertsIconURI
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIALERTSICONURI

  nsAlertsIconURI();

private:
  ~nsAlertsIconURI();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAlertsIconURI, nsIAlertsIconURI)

nsAlertsIconURI::nsAlertsIconURI()
{
  /* member initializers and constructor code */
}

nsAlertsIconURI::~nsAlertsIconURI()
{
  /* destructor code */
}

/* void showAlertWithIconURI (in nsIAlertNotification aAlert, [optional] in nsIObserver aAlertListener, [optional] in nsIURI aIconURI); */
NS_IMETHODIMP nsAlertsIconURI::ShowAlertWithIconURI(nsIAlertNotification *aAlert, nsIObserver *aAlertListener, nsIURI *aIconURI)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIAlertsService_h__ */
