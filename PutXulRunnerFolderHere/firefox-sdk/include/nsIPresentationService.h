/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPresentationService.idl
 */

#ifndef __gen_nsIPresentationService_h__
#define __gen_nsIPresentationService_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIDOMBlob; /* forward declaration */

class nsIDOMEventTarget; /* forward declaration */

class nsIInputStream; /* forward declaration */

class nsIPresentationAvailabilityListener; /* forward declaration */

class nsIPresentationRespondingListener; /* forward declaration */

class nsIPresentationSessionListener; /* forward declaration */

class nsIPresentationTransportBuilderConstructor; /* forward declaration */

class nsIPrincipal; /* forward declaration */

#define PRESENTATION_SERVICE_CID \
  { 0x1d9bb10c, 0xc0ab, 0x4fe8, \
    { 0x9e, 0x4f, 0x40, 0x58, 0xb8, 0x51, 0x98, 0x32 } }
#define PRESENTATION_SERVICE_CONTRACTID \
  "@mozilla.org/presentation/presentationservice;1"
#include "nsTArray.h"
class nsString;

/* starting interface:    nsIPresentationServiceCallback */
#define NS_IPRESENTATIONSERVICECALLBACK_IID_STR "12073206-0065-4b10-9488-a6eb9b23e65b"

#define NS_IPRESENTATIONSERVICECALLBACK_IID \
  {0x12073206, 0x0065, 0x4b10, \
    { 0x94, 0x88, 0xa6, 0xeb, 0x9b, 0x23, 0xe6, 0x5b }}

class NS_NO_VTABLE nsIPresentationServiceCallback : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONSERVICECALLBACK_IID)

  /* void notifySuccess (in DOMString url); */
  NS_IMETHOD NotifySuccess(const nsAString & url) = 0;

  /* void notifyError (in nsresult error); */
  NS_IMETHOD NotifyError(nsresult error) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationServiceCallback, NS_IPRESENTATIONSERVICECALLBACK_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONSERVICECALLBACK \
  NS_IMETHOD NotifySuccess(const nsAString & url) override; \
  NS_IMETHOD NotifyError(nsresult error) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONSERVICECALLBACK \
  NS_METHOD NotifySuccess(const nsAString & url); \
  NS_METHOD NotifyError(nsresult error); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONSERVICECALLBACK(_to) \
  NS_IMETHOD NotifySuccess(const nsAString & url) override { return _to NotifySuccess(url); } \
  NS_IMETHOD NotifyError(nsresult error) override { return _to NotifyError(error); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONSERVICECALLBACK(_to) \
  NS_IMETHOD NotifySuccess(const nsAString & url) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NotifySuccess(url); } \
  NS_IMETHOD NotifyError(nsresult error) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NotifyError(error); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationServiceCallback : public nsIPresentationServiceCallback
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONSERVICECALLBACK

  nsPresentationServiceCallback();

private:
  ~nsPresentationServiceCallback();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationServiceCallback, nsIPresentationServiceCallback)

nsPresentationServiceCallback::nsPresentationServiceCallback()
{
  /* member initializers and constructor code */
}

nsPresentationServiceCallback::~nsPresentationServiceCallback()
{
  /* destructor code */
}

/* void notifySuccess (in DOMString url); */
NS_IMETHODIMP nsPresentationServiceCallback::NotifySuccess(const nsAString & url)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void notifyError (in nsresult error); */
NS_IMETHODIMP nsPresentationServiceCallback::NotifyError(nsresult error)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIPresentationService */
#define NS_IPRESENTATIONSERVICE_IID_STR "de42b741-5619-4650-b961-c2cebb572c95"

#define NS_IPRESENTATIONSERVICE_IID \
  {0xde42b741, 0x5619, 0x4650, \
    { 0xb9, 0x61, 0xc2, 0xce, 0xbb, 0x57, 0x2c, 0x95 }}

class NS_NO_VTABLE nsIPresentationService : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONSERVICE_IID)

  enum {
    ROLE_CONTROLLER = 1U,
    ROLE_RECEIVER = 2U,
    CLOSED_REASON_ERROR = 1U,
    CLOSED_REASON_CLOSED = 2U,
    CLOSED_REASON_WENTAWAY = 3U
  };

  /* [noscript] void startSession (in URLArrayRef urls, in DOMString sessionId, in DOMString origin, in DOMString deviceId, in unsigned long long windowId, in nsIDOMEventTarget eventTarget, in nsIPrincipal principal, in nsIPresentationServiceCallback callback, in nsIPresentationTransportBuilderConstructor constructor); */
  NS_IMETHOD StartSession(const nsTArray<nsString> & urls, const nsAString & sessionId, const nsAString & origin, const nsAString & deviceId, uint64_t windowId, nsIDOMEventTarget *eventTarget, nsIPrincipal *principal, nsIPresentationServiceCallback *callback, nsIPresentationTransportBuilderConstructor *constructor) = 0;

  /* void sendSessionMessage (in DOMString sessionId, in uint8_t role, in DOMString data); */
  NS_IMETHOD SendSessionMessage(const nsAString & sessionId, uint8_t role, const nsAString & data) = 0;

  /* void sendSessionBinaryMsg (in DOMString sessionId, in uint8_t role, in ACString data); */
  NS_IMETHOD SendSessionBinaryMsg(const nsAString & sessionId, uint8_t role, const nsACString & data) = 0;

  /* void sendSessionBlob (in DOMString sessionId, in uint8_t role, in nsIDOMBlob blob); */
  NS_IMETHOD SendSessionBlob(const nsAString & sessionId, uint8_t role, nsIDOMBlob *blob) = 0;

  /* void closeSession (in DOMString sessionId, in uint8_t role, in uint8_t closedReason); */
  NS_IMETHOD CloseSession(const nsAString & sessionId, uint8_t role, uint8_t closedReason) = 0;

  /* void terminateSession (in DOMString sessionId, in uint8_t role); */
  NS_IMETHOD TerminateSession(const nsAString & sessionId, uint8_t role) = 0;

  /* [noscript] void reconnectSession (in URLArrayRef urls, in DOMString sessionId, in uint8_t role, in nsIPresentationServiceCallback callback); */
  NS_IMETHOD ReconnectSession(const nsTArray<nsString> & urls, const nsAString & sessionId, uint8_t role, nsIPresentationServiceCallback *callback) = 0;

  /* [noscript] void registerAvailabilityListener (in URLArrayRef availabilityUrls, in nsIPresentationAvailabilityListener listener); */
  NS_IMETHOD RegisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener) = 0;

  /* [noscript] void unregisterAvailabilityListener (in URLArrayRef availabilityUrls, in nsIPresentationAvailabilityListener listener); */
  NS_IMETHOD UnregisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener) = 0;

  /* void registerSessionListener (in DOMString sessionId, in uint8_t role, in nsIPresentationSessionListener listener); */
  NS_IMETHOD RegisterSessionListener(const nsAString & sessionId, uint8_t role, nsIPresentationSessionListener *listener) = 0;

  /* void unregisterSessionListener (in DOMString sessionId, in uint8_t role); */
  NS_IMETHOD UnregisterSessionListener(const nsAString & sessionId, uint8_t role) = 0;

  /* void registerRespondingListener (in unsigned long long windowId, in nsIPresentationRespondingListener listener); */
  NS_IMETHOD RegisterRespondingListener(uint64_t windowId, nsIPresentationRespondingListener *listener) = 0;

  /* void unregisterRespondingListener (in unsigned long long windowId); */
  NS_IMETHOD UnregisterRespondingListener(uint64_t windowId) = 0;

  /* void notifyReceiverReady (in DOMString sessionId, in unsigned long long windowId, in boolean isLoading, in nsIPresentationTransportBuilderConstructor constructor); */
  NS_IMETHOD NotifyReceiverReady(const nsAString & sessionId, uint64_t windowId, bool isLoading, nsIPresentationTransportBuilderConstructor *constructor) = 0;

  /* void NotifyTransportClosed (in DOMString sessionId, in uint8_t role, in nsresult reason); */
  NS_IMETHOD NotifyTransportClosed(const nsAString & sessionId, uint8_t role, nsresult reason) = 0;

  /* void untrackSessionInfo (in DOMString sessionId, in uint8_t role); */
  NS_IMETHOD UntrackSessionInfo(const nsAString & sessionId, uint8_t role) = 0;

  /* unsigned long long getWindowIdBySessionId (in DOMString sessionId, in uint8_t role); */
  NS_IMETHOD GetWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t *_retval) = 0;

  /* void updateWindowIdBySessionId (in DOMString sessionId, in uint8_t role, in unsigned long long windowId); */
  NS_IMETHOD UpdateWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t windowId) = 0;

  /* void buildTransport (in DOMString sessionId, in uint8_t role); */
  NS_IMETHOD BuildTransport(const nsAString & sessionId, uint8_t role) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationService, NS_IPRESENTATIONSERVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONSERVICE \
  NS_IMETHOD StartSession(const nsTArray<nsString> & urls, const nsAString & sessionId, const nsAString & origin, const nsAString & deviceId, uint64_t windowId, nsIDOMEventTarget *eventTarget, nsIPrincipal *principal, nsIPresentationServiceCallback *callback, nsIPresentationTransportBuilderConstructor *constructor) override; \
  NS_IMETHOD SendSessionMessage(const nsAString & sessionId, uint8_t role, const nsAString & data) override; \
  NS_IMETHOD SendSessionBinaryMsg(const nsAString & sessionId, uint8_t role, const nsACString & data) override; \
  NS_IMETHOD SendSessionBlob(const nsAString & sessionId, uint8_t role, nsIDOMBlob *blob) override; \
  NS_IMETHOD CloseSession(const nsAString & sessionId, uint8_t role, uint8_t closedReason) override; \
  NS_IMETHOD TerminateSession(const nsAString & sessionId, uint8_t role) override; \
  NS_IMETHOD ReconnectSession(const nsTArray<nsString> & urls, const nsAString & sessionId, uint8_t role, nsIPresentationServiceCallback *callback) override; \
  NS_IMETHOD RegisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener) override; \
  NS_IMETHOD UnregisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener) override; \
  NS_IMETHOD RegisterSessionListener(const nsAString & sessionId, uint8_t role, nsIPresentationSessionListener *listener) override; \
  NS_IMETHOD UnregisterSessionListener(const nsAString & sessionId, uint8_t role) override; \
  NS_IMETHOD RegisterRespondingListener(uint64_t windowId, nsIPresentationRespondingListener *listener) override; \
  NS_IMETHOD UnregisterRespondingListener(uint64_t windowId) override; \
  NS_IMETHOD NotifyReceiverReady(const nsAString & sessionId, uint64_t windowId, bool isLoading, nsIPresentationTransportBuilderConstructor *constructor) override; \
  NS_IMETHOD NotifyTransportClosed(const nsAString & sessionId, uint8_t role, nsresult reason) override; \
  NS_IMETHOD UntrackSessionInfo(const nsAString & sessionId, uint8_t role) override; \
  NS_IMETHOD GetWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t *_retval) override; \
  NS_IMETHOD UpdateWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t windowId) override; \
  NS_IMETHOD BuildTransport(const nsAString & sessionId, uint8_t role) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONSERVICE \
  NS_METHOD StartSession(const nsTArray<nsString> & urls, const nsAString & sessionId, const nsAString & origin, const nsAString & deviceId, uint64_t windowId, nsIDOMEventTarget *eventTarget, nsIPrincipal *principal, nsIPresentationServiceCallback *callback, nsIPresentationTransportBuilderConstructor *constructor); \
  NS_METHOD SendSessionMessage(const nsAString & sessionId, uint8_t role, const nsAString & data); \
  NS_METHOD SendSessionBinaryMsg(const nsAString & sessionId, uint8_t role, const nsACString & data); \
  NS_METHOD SendSessionBlob(const nsAString & sessionId, uint8_t role, nsIDOMBlob *blob); \
  NS_METHOD CloseSession(const nsAString & sessionId, uint8_t role, uint8_t closedReason); \
  NS_METHOD TerminateSession(const nsAString & sessionId, uint8_t role); \
  NS_METHOD ReconnectSession(const nsTArray<nsString> & urls, const nsAString & sessionId, uint8_t role, nsIPresentationServiceCallback *callback); \
  NS_METHOD RegisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener); \
  NS_METHOD UnregisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener); \
  NS_METHOD RegisterSessionListener(const nsAString & sessionId, uint8_t role, nsIPresentationSessionListener *listener); \
  NS_METHOD UnregisterSessionListener(const nsAString & sessionId, uint8_t role); \
  NS_METHOD RegisterRespondingListener(uint64_t windowId, nsIPresentationRespondingListener *listener); \
  NS_METHOD UnregisterRespondingListener(uint64_t windowId); \
  NS_METHOD NotifyReceiverReady(const nsAString & sessionId, uint64_t windowId, bool isLoading, nsIPresentationTransportBuilderConstructor *constructor); \
  NS_METHOD NotifyTransportClosed(const nsAString & sessionId, uint8_t role, nsresult reason); \
  NS_METHOD UntrackSessionInfo(const nsAString & sessionId, uint8_t role); \
  NS_METHOD GetWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t *_retval); \
  NS_METHOD UpdateWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t windowId); \
  NS_METHOD BuildTransport(const nsAString & sessionId, uint8_t role); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONSERVICE(_to) \
  NS_IMETHOD StartSession(const nsTArray<nsString> & urls, const nsAString & sessionId, const nsAString & origin, const nsAString & deviceId, uint64_t windowId, nsIDOMEventTarget *eventTarget, nsIPrincipal *principal, nsIPresentationServiceCallback *callback, nsIPresentationTransportBuilderConstructor *constructor) override { return _to StartSession(urls, sessionId, origin, deviceId, windowId, eventTarget, principal, callback, constructor); } \
  NS_IMETHOD SendSessionMessage(const nsAString & sessionId, uint8_t role, const nsAString & data) override { return _to SendSessionMessage(sessionId, role, data); } \
  NS_IMETHOD SendSessionBinaryMsg(const nsAString & sessionId, uint8_t role, const nsACString & data) override { return _to SendSessionBinaryMsg(sessionId, role, data); } \
  NS_IMETHOD SendSessionBlob(const nsAString & sessionId, uint8_t role, nsIDOMBlob *blob) override { return _to SendSessionBlob(sessionId, role, blob); } \
  NS_IMETHOD CloseSession(const nsAString & sessionId, uint8_t role, uint8_t closedReason) override { return _to CloseSession(sessionId, role, closedReason); } \
  NS_IMETHOD TerminateSession(const nsAString & sessionId, uint8_t role) override { return _to TerminateSession(sessionId, role); } \
  NS_IMETHOD ReconnectSession(const nsTArray<nsString> & urls, const nsAString & sessionId, uint8_t role, nsIPresentationServiceCallback *callback) override { return _to ReconnectSession(urls, sessionId, role, callback); } \
  NS_IMETHOD RegisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener) override { return _to RegisterAvailabilityListener(availabilityUrls, listener); } \
  NS_IMETHOD UnregisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener) override { return _to UnregisterAvailabilityListener(availabilityUrls, listener); } \
  NS_IMETHOD RegisterSessionListener(const nsAString & sessionId, uint8_t role, nsIPresentationSessionListener *listener) override { return _to RegisterSessionListener(sessionId, role, listener); } \
  NS_IMETHOD UnregisterSessionListener(const nsAString & sessionId, uint8_t role) override { return _to UnregisterSessionListener(sessionId, role); } \
  NS_IMETHOD RegisterRespondingListener(uint64_t windowId, nsIPresentationRespondingListener *listener) override { return _to RegisterRespondingListener(windowId, listener); } \
  NS_IMETHOD UnregisterRespondingListener(uint64_t windowId) override { return _to UnregisterRespondingListener(windowId); } \
  NS_IMETHOD NotifyReceiverReady(const nsAString & sessionId, uint64_t windowId, bool isLoading, nsIPresentationTransportBuilderConstructor *constructor) override { return _to NotifyReceiverReady(sessionId, windowId, isLoading, constructor); } \
  NS_IMETHOD NotifyTransportClosed(const nsAString & sessionId, uint8_t role, nsresult reason) override { return _to NotifyTransportClosed(sessionId, role, reason); } \
  NS_IMETHOD UntrackSessionInfo(const nsAString & sessionId, uint8_t role) override { return _to UntrackSessionInfo(sessionId, role); } \
  NS_IMETHOD GetWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t *_retval) override { return _to GetWindowIdBySessionId(sessionId, role, _retval); } \
  NS_IMETHOD UpdateWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t windowId) override { return _to UpdateWindowIdBySessionId(sessionId, role, windowId); } \
  NS_IMETHOD BuildTransport(const nsAString & sessionId, uint8_t role) override { return _to BuildTransport(sessionId, role); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONSERVICE(_to) \
  NS_IMETHOD StartSession(const nsTArray<nsString> & urls, const nsAString & sessionId, const nsAString & origin, const nsAString & deviceId, uint64_t windowId, nsIDOMEventTarget *eventTarget, nsIPrincipal *principal, nsIPresentationServiceCallback *callback, nsIPresentationTransportBuilderConstructor *constructor) override { return !_to ? NS_ERROR_NULL_POINTER : _to->StartSession(urls, sessionId, origin, deviceId, windowId, eventTarget, principal, callback, constructor); } \
  NS_IMETHOD SendSessionMessage(const nsAString & sessionId, uint8_t role, const nsAString & data) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SendSessionMessage(sessionId, role, data); } \
  NS_IMETHOD SendSessionBinaryMsg(const nsAString & sessionId, uint8_t role, const nsACString & data) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SendSessionBinaryMsg(sessionId, role, data); } \
  NS_IMETHOD SendSessionBlob(const nsAString & sessionId, uint8_t role, nsIDOMBlob *blob) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SendSessionBlob(sessionId, role, blob); } \
  NS_IMETHOD CloseSession(const nsAString & sessionId, uint8_t role, uint8_t closedReason) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CloseSession(sessionId, role, closedReason); } \
  NS_IMETHOD TerminateSession(const nsAString & sessionId, uint8_t role) override { return !_to ? NS_ERROR_NULL_POINTER : _to->TerminateSession(sessionId, role); } \
  NS_IMETHOD ReconnectSession(const nsTArray<nsString> & urls, const nsAString & sessionId, uint8_t role, nsIPresentationServiceCallback *callback) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ReconnectSession(urls, sessionId, role, callback); } \
  NS_IMETHOD RegisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RegisterAvailabilityListener(availabilityUrls, listener); } \
  NS_IMETHOD UnregisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->UnregisterAvailabilityListener(availabilityUrls, listener); } \
  NS_IMETHOD RegisterSessionListener(const nsAString & sessionId, uint8_t role, nsIPresentationSessionListener *listener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RegisterSessionListener(sessionId, role, listener); } \
  NS_IMETHOD UnregisterSessionListener(const nsAString & sessionId, uint8_t role) override { return !_to ? NS_ERROR_NULL_POINTER : _to->UnregisterSessionListener(sessionId, role); } \
  NS_IMETHOD RegisterRespondingListener(uint64_t windowId, nsIPresentationRespondingListener *listener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RegisterRespondingListener(windowId, listener); } \
  NS_IMETHOD UnregisterRespondingListener(uint64_t windowId) override { return !_to ? NS_ERROR_NULL_POINTER : _to->UnregisterRespondingListener(windowId); } \
  NS_IMETHOD NotifyReceiverReady(const nsAString & sessionId, uint64_t windowId, bool isLoading, nsIPresentationTransportBuilderConstructor *constructor) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NotifyReceiverReady(sessionId, windowId, isLoading, constructor); } \
  NS_IMETHOD NotifyTransportClosed(const nsAString & sessionId, uint8_t role, nsresult reason) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NotifyTransportClosed(sessionId, role, reason); } \
  NS_IMETHOD UntrackSessionInfo(const nsAString & sessionId, uint8_t role) override { return !_to ? NS_ERROR_NULL_POINTER : _to->UntrackSessionInfo(sessionId, role); } \
  NS_IMETHOD GetWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetWindowIdBySessionId(sessionId, role, _retval); } \
  NS_IMETHOD UpdateWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t windowId) override { return !_to ? NS_ERROR_NULL_POINTER : _to->UpdateWindowIdBySessionId(sessionId, role, windowId); } \
  NS_IMETHOD BuildTransport(const nsAString & sessionId, uint8_t role) override { return !_to ? NS_ERROR_NULL_POINTER : _to->BuildTransport(sessionId, role); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationService : public nsIPresentationService
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONSERVICE

  nsPresentationService();

private:
  ~nsPresentationService();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationService, nsIPresentationService)

nsPresentationService::nsPresentationService()
{
  /* member initializers and constructor code */
}

nsPresentationService::~nsPresentationService()
{
  /* destructor code */
}

/* [noscript] void startSession (in URLArrayRef urls, in DOMString sessionId, in DOMString origin, in DOMString deviceId, in unsigned long long windowId, in nsIDOMEventTarget eventTarget, in nsIPrincipal principal, in nsIPresentationServiceCallback callback, in nsIPresentationTransportBuilderConstructor constructor); */
NS_IMETHODIMP nsPresentationService::StartSession(const nsTArray<nsString> & urls, const nsAString & sessionId, const nsAString & origin, const nsAString & deviceId, uint64_t windowId, nsIDOMEventTarget *eventTarget, nsIPrincipal *principal, nsIPresentationServiceCallback *callback, nsIPresentationTransportBuilderConstructor *constructor)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void sendSessionMessage (in DOMString sessionId, in uint8_t role, in DOMString data); */
NS_IMETHODIMP nsPresentationService::SendSessionMessage(const nsAString & sessionId, uint8_t role, const nsAString & data)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void sendSessionBinaryMsg (in DOMString sessionId, in uint8_t role, in ACString data); */
NS_IMETHODIMP nsPresentationService::SendSessionBinaryMsg(const nsAString & sessionId, uint8_t role, const nsACString & data)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void sendSessionBlob (in DOMString sessionId, in uint8_t role, in nsIDOMBlob blob); */
NS_IMETHODIMP nsPresentationService::SendSessionBlob(const nsAString & sessionId, uint8_t role, nsIDOMBlob *blob)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void closeSession (in DOMString sessionId, in uint8_t role, in uint8_t closedReason); */
NS_IMETHODIMP nsPresentationService::CloseSession(const nsAString & sessionId, uint8_t role, uint8_t closedReason)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void terminateSession (in DOMString sessionId, in uint8_t role); */
NS_IMETHODIMP nsPresentationService::TerminateSession(const nsAString & sessionId, uint8_t role)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] void reconnectSession (in URLArrayRef urls, in DOMString sessionId, in uint8_t role, in nsIPresentationServiceCallback callback); */
NS_IMETHODIMP nsPresentationService::ReconnectSession(const nsTArray<nsString> & urls, const nsAString & sessionId, uint8_t role, nsIPresentationServiceCallback *callback)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] void registerAvailabilityListener (in URLArrayRef availabilityUrls, in nsIPresentationAvailabilityListener listener); */
NS_IMETHODIMP nsPresentationService::RegisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] void unregisterAvailabilityListener (in URLArrayRef availabilityUrls, in nsIPresentationAvailabilityListener listener); */
NS_IMETHODIMP nsPresentationService::UnregisterAvailabilityListener(const nsTArray<nsString> & availabilityUrls, nsIPresentationAvailabilityListener *listener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void registerSessionListener (in DOMString sessionId, in uint8_t role, in nsIPresentationSessionListener listener); */
NS_IMETHODIMP nsPresentationService::RegisterSessionListener(const nsAString & sessionId, uint8_t role, nsIPresentationSessionListener *listener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void unregisterSessionListener (in DOMString sessionId, in uint8_t role); */
NS_IMETHODIMP nsPresentationService::UnregisterSessionListener(const nsAString & sessionId, uint8_t role)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void registerRespondingListener (in unsigned long long windowId, in nsIPresentationRespondingListener listener); */
NS_IMETHODIMP nsPresentationService::RegisterRespondingListener(uint64_t windowId, nsIPresentationRespondingListener *listener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void unregisterRespondingListener (in unsigned long long windowId); */
NS_IMETHODIMP nsPresentationService::UnregisterRespondingListener(uint64_t windowId)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void notifyReceiverReady (in DOMString sessionId, in unsigned long long windowId, in boolean isLoading, in nsIPresentationTransportBuilderConstructor constructor); */
NS_IMETHODIMP nsPresentationService::NotifyReceiverReady(const nsAString & sessionId, uint64_t windowId, bool isLoading, nsIPresentationTransportBuilderConstructor *constructor)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void NotifyTransportClosed (in DOMString sessionId, in uint8_t role, in nsresult reason); */
NS_IMETHODIMP nsPresentationService::NotifyTransportClosed(const nsAString & sessionId, uint8_t role, nsresult reason)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void untrackSessionInfo (in DOMString sessionId, in uint8_t role); */
NS_IMETHODIMP nsPresentationService::UntrackSessionInfo(const nsAString & sessionId, uint8_t role)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* unsigned long long getWindowIdBySessionId (in DOMString sessionId, in uint8_t role); */
NS_IMETHODIMP nsPresentationService::GetWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void updateWindowIdBySessionId (in DOMString sessionId, in uint8_t role, in unsigned long long windowId); */
NS_IMETHODIMP nsPresentationService::UpdateWindowIdBySessionId(const nsAString & sessionId, uint8_t role, uint64_t windowId)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void buildTransport (in DOMString sessionId, in uint8_t role); */
NS_IMETHODIMP nsPresentationService::BuildTransport(const nsAString & sessionId, uint8_t role)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPresentationService_h__ */
