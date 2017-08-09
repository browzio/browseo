/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPresentationControlService.idl
 */

#ifndef __gen_nsIPresentationControlService_h__
#define __gen_nsIPresentationControlService_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIPresentationControlChannel; /* forward declaration */

#define PRESENTATION_CONTROL_SERVICE_CONTACT_ID \
  "@mozilla.org/presentation/control-service;1"

/* starting interface:    nsITCPDeviceInfo */
#define NS_ITCPDEVICEINFO_IID_STR "296fd171-e4d0-4de0-99ff-ad8ed52ddef3"

#define NS_ITCPDEVICEINFO_IID \
  {0x296fd171, 0xe4d0, 0x4de0, \
    { 0x99, 0xff, 0xad, 0x8e, 0xd5, 0x2d, 0xde, 0xf3 }}

class NS_NO_VTABLE nsITCPDeviceInfo : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ITCPDEVICEINFO_IID)

  /* readonly attribute AUTF8String id; */
  NS_IMETHOD GetId(nsACString & aId) = 0;

  /* readonly attribute AUTF8String address; */
  NS_IMETHOD GetAddress(nsACString & aAddress) = 0;

  /* readonly attribute uint16_t port; */
  NS_IMETHOD GetPort(uint16_t *aPort) = 0;

  /* readonly attribute AUTF8String certFingerprint; */
  NS_IMETHOD GetCertFingerprint(nsACString & aCertFingerprint) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsITCPDeviceInfo, NS_ITCPDEVICEINFO_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSITCPDEVICEINFO \
  NS_IMETHOD GetId(nsACString & aId) override; \
  NS_IMETHOD GetAddress(nsACString & aAddress) override; \
  NS_IMETHOD GetPort(uint16_t *aPort) override; \
  NS_IMETHOD GetCertFingerprint(nsACString & aCertFingerprint) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSITCPDEVICEINFO \
  NS_METHOD GetId(nsACString & aId); \
  NS_METHOD GetAddress(nsACString & aAddress); \
  NS_METHOD GetPort(uint16_t *aPort); \
  NS_METHOD GetCertFingerprint(nsACString & aCertFingerprint); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSITCPDEVICEINFO(_to) \
  NS_IMETHOD GetId(nsACString & aId) override { return _to GetId(aId); } \
  NS_IMETHOD GetAddress(nsACString & aAddress) override { return _to GetAddress(aAddress); } \
  NS_IMETHOD GetPort(uint16_t *aPort) override { return _to GetPort(aPort); } \
  NS_IMETHOD GetCertFingerprint(nsACString & aCertFingerprint) override { return _to GetCertFingerprint(aCertFingerprint); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSITCPDEVICEINFO(_to) \
  NS_IMETHOD GetId(nsACString & aId) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetId(aId); } \
  NS_IMETHOD GetAddress(nsACString & aAddress) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAddress(aAddress); } \
  NS_IMETHOD GetPort(uint16_t *aPort) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPort(aPort); } \
  NS_IMETHOD GetCertFingerprint(nsACString & aCertFingerprint) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCertFingerprint(aCertFingerprint); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsTCPDeviceInfo : public nsITCPDeviceInfo
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSITCPDEVICEINFO

  nsTCPDeviceInfo();

private:
  ~nsTCPDeviceInfo();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsTCPDeviceInfo, nsITCPDeviceInfo)

nsTCPDeviceInfo::nsTCPDeviceInfo()
{
  /* member initializers and constructor code */
}

nsTCPDeviceInfo::~nsTCPDeviceInfo()
{
  /* destructor code */
}

/* readonly attribute AUTF8String id; */
NS_IMETHODIMP nsTCPDeviceInfo::GetId(nsACString & aId)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String address; */
NS_IMETHODIMP nsTCPDeviceInfo::GetAddress(nsACString & aAddress)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute uint16_t port; */
NS_IMETHODIMP nsTCPDeviceInfo::GetPort(uint16_t *aPort)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String certFingerprint; */
NS_IMETHODIMP nsTCPDeviceInfo::GetCertFingerprint(nsACString & aCertFingerprint)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIPresentationControlServerListener */
#define NS_IPRESENTATIONCONTROLSERVERLISTENER_IID_STR "09bddfaf-fcc2-4dc9-b33e-a509a1c2fb6d"

#define NS_IPRESENTATIONCONTROLSERVERLISTENER_IID \
  {0x09bddfaf, 0xfcc2, 0x4dc9, \
    { 0xb3, 0x3e, 0xa5, 0x09, 0xa1, 0xc2, 0xfb, 0x6d }}

class NS_NO_VTABLE nsIPresentationControlServerListener : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONCONTROLSERVERLISTENER_IID)

  /* void onServerReady (in uint16_t aPort, in AUTF8String aCertFingerprint); */
  NS_IMETHOD OnServerReady(uint16_t aPort, const nsACString & aCertFingerprint) = 0;

  /* void onServerStopped (in nsresult aResult); */
  NS_IMETHOD OnServerStopped(nsresult aResult) = 0;

  /* void onSessionRequest (in nsITCPDeviceInfo aDeviceInfo, in DOMString aUrl, in DOMString aPresentationId, in nsIPresentationControlChannel aControlChannel); */
  NS_IMETHOD OnSessionRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aUrl, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel) = 0;

  /* void onTerminateRequest (in nsITCPDeviceInfo aDeviceInfo, in DOMString aPresentationId, in nsIPresentationControlChannel aControlChannel, in boolean aIsFromReceiver); */
  NS_IMETHOD OnTerminateRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel, bool aIsFromReceiver) = 0;

  /* void onReconnectRequest (in nsITCPDeviceInfo aDeviceInfo, in DOMString url, in DOMString aPresentationId, in nsIPresentationControlChannel aControlChannel); */
  NS_IMETHOD OnReconnectRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & url, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationControlServerListener, NS_IPRESENTATIONCONTROLSERVERLISTENER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONCONTROLSERVERLISTENER \
  NS_IMETHOD OnServerReady(uint16_t aPort, const nsACString & aCertFingerprint) override; \
  NS_IMETHOD OnServerStopped(nsresult aResult) override; \
  NS_IMETHOD OnSessionRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aUrl, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel) override; \
  NS_IMETHOD OnTerminateRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel, bool aIsFromReceiver) override; \
  NS_IMETHOD OnReconnectRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & url, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONCONTROLSERVERLISTENER \
  NS_METHOD OnServerReady(uint16_t aPort, const nsACString & aCertFingerprint); \
  NS_METHOD OnServerStopped(nsresult aResult); \
  NS_METHOD OnSessionRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aUrl, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel); \
  NS_METHOD OnTerminateRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel, bool aIsFromReceiver); \
  NS_METHOD OnReconnectRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & url, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONCONTROLSERVERLISTENER(_to) \
  NS_IMETHOD OnServerReady(uint16_t aPort, const nsACString & aCertFingerprint) override { return _to OnServerReady(aPort, aCertFingerprint); } \
  NS_IMETHOD OnServerStopped(nsresult aResult) override { return _to OnServerStopped(aResult); } \
  NS_IMETHOD OnSessionRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aUrl, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel) override { return _to OnSessionRequest(aDeviceInfo, aUrl, aPresentationId, aControlChannel); } \
  NS_IMETHOD OnTerminateRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel, bool aIsFromReceiver) override { return _to OnTerminateRequest(aDeviceInfo, aPresentationId, aControlChannel, aIsFromReceiver); } \
  NS_IMETHOD OnReconnectRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & url, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel) override { return _to OnReconnectRequest(aDeviceInfo, url, aPresentationId, aControlChannel); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONCONTROLSERVERLISTENER(_to) \
  NS_IMETHOD OnServerReady(uint16_t aPort, const nsACString & aCertFingerprint) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnServerReady(aPort, aCertFingerprint); } \
  NS_IMETHOD OnServerStopped(nsresult aResult) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnServerStopped(aResult); } \
  NS_IMETHOD OnSessionRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aUrl, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnSessionRequest(aDeviceInfo, aUrl, aPresentationId, aControlChannel); } \
  NS_IMETHOD OnTerminateRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel, bool aIsFromReceiver) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnTerminateRequest(aDeviceInfo, aPresentationId, aControlChannel, aIsFromReceiver); } \
  NS_IMETHOD OnReconnectRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & url, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnReconnectRequest(aDeviceInfo, url, aPresentationId, aControlChannel); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationControlServerListener : public nsIPresentationControlServerListener
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONCONTROLSERVERLISTENER

  nsPresentationControlServerListener();

private:
  ~nsPresentationControlServerListener();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationControlServerListener, nsIPresentationControlServerListener)

nsPresentationControlServerListener::nsPresentationControlServerListener()
{
  /* member initializers and constructor code */
}

nsPresentationControlServerListener::~nsPresentationControlServerListener()
{
  /* destructor code */
}

/* void onServerReady (in uint16_t aPort, in AUTF8String aCertFingerprint); */
NS_IMETHODIMP nsPresentationControlServerListener::OnServerReady(uint16_t aPort, const nsACString & aCertFingerprint)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onServerStopped (in nsresult aResult); */
NS_IMETHODIMP nsPresentationControlServerListener::OnServerStopped(nsresult aResult)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onSessionRequest (in nsITCPDeviceInfo aDeviceInfo, in DOMString aUrl, in DOMString aPresentationId, in nsIPresentationControlChannel aControlChannel); */
NS_IMETHODIMP nsPresentationControlServerListener::OnSessionRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aUrl, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onTerminateRequest (in nsITCPDeviceInfo aDeviceInfo, in DOMString aPresentationId, in nsIPresentationControlChannel aControlChannel, in boolean aIsFromReceiver); */
NS_IMETHODIMP nsPresentationControlServerListener::OnTerminateRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel, bool aIsFromReceiver)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onReconnectRequest (in nsITCPDeviceInfo aDeviceInfo, in DOMString url, in DOMString aPresentationId, in nsIPresentationControlChannel aControlChannel); */
NS_IMETHODIMP nsPresentationControlServerListener::OnReconnectRequest(nsITCPDeviceInfo *aDeviceInfo, const nsAString & url, const nsAString & aPresentationId, nsIPresentationControlChannel *aControlChannel)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIPresentationControlService */
#define NS_IPRESENTATIONCONTROLSERVICE_IID_STR "55d6b605-2389-4aae-a8fe-60d4440540ea"

#define NS_IPRESENTATIONCONTROLSERVICE_IID \
  {0x55d6b605, 0x2389, 0x4aae, \
    { 0xa8, 0xfe, 0x60, 0xd4, 0x44, 0x05, 0x40, 0xea }}

class NS_NO_VTABLE nsIPresentationControlService : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONCONTROLSERVICE_IID)

  /* void startServer (in boolean aEncrypted, [optional] in uint16_t aPort); */
  NS_IMETHOD StartServer(bool aEncrypted, uint16_t aPort) = 0;

  /* nsIPresentationControlChannel connect (in nsITCPDeviceInfo aDeviceInfo); */
  NS_IMETHOD Connect(nsITCPDeviceInfo *aDeviceInfo, nsIPresentationControlChannel * *_retval) = 0;

  /* boolean isCompatibleServer (in uint32_t aVersion); */
  NS_IMETHOD IsCompatibleServer(uint32_t aVersion, bool *_retval) = 0;

  /* void close (); */
  NS_IMETHOD Close(void) = 0;

  /* readonly attribute uint16_t port; */
  NS_IMETHOD GetPort(uint16_t *aPort) = 0;

  /* readonly attribute uint32_t version; */
  NS_IMETHOD GetVersion(uint32_t *aVersion) = 0;

  /* attribute AUTF8String id; */
  NS_IMETHOD GetId(nsACString & aId) = 0;
  NS_IMETHOD SetId(const nsACString & aId) = 0;

  /* attribute AUTF8String certFingerprint; */
  NS_IMETHOD GetCertFingerprint(nsACString & aCertFingerprint) = 0;
  NS_IMETHOD SetCertFingerprint(const nsACString & aCertFingerprint) = 0;

  /* attribute nsIPresentationControlServerListener listener; */
  NS_IMETHOD GetListener(nsIPresentationControlServerListener * *aListener) = 0;
  NS_IMETHOD SetListener(nsIPresentationControlServerListener *aListener) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationControlService, NS_IPRESENTATIONCONTROLSERVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONCONTROLSERVICE \
  NS_IMETHOD StartServer(bool aEncrypted, uint16_t aPort) override; \
  NS_IMETHOD Connect(nsITCPDeviceInfo *aDeviceInfo, nsIPresentationControlChannel * *_retval) override; \
  NS_IMETHOD IsCompatibleServer(uint32_t aVersion, bool *_retval) override; \
  NS_IMETHOD Close(void) override; \
  NS_IMETHOD GetPort(uint16_t *aPort) override; \
  NS_IMETHOD GetVersion(uint32_t *aVersion) override; \
  NS_IMETHOD GetId(nsACString & aId) override; \
  NS_IMETHOD SetId(const nsACString & aId) override; \
  NS_IMETHOD GetCertFingerprint(nsACString & aCertFingerprint) override; \
  NS_IMETHOD SetCertFingerprint(const nsACString & aCertFingerprint) override; \
  NS_IMETHOD GetListener(nsIPresentationControlServerListener * *aListener) override; \
  NS_IMETHOD SetListener(nsIPresentationControlServerListener *aListener) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONCONTROLSERVICE \
  NS_METHOD StartServer(bool aEncrypted, uint16_t aPort); \
  NS_METHOD Connect(nsITCPDeviceInfo *aDeviceInfo, nsIPresentationControlChannel * *_retval); \
  NS_METHOD IsCompatibleServer(uint32_t aVersion, bool *_retval); \
  NS_METHOD Close(void); \
  NS_METHOD GetPort(uint16_t *aPort); \
  NS_METHOD GetVersion(uint32_t *aVersion); \
  NS_METHOD GetId(nsACString & aId); \
  NS_METHOD SetId(const nsACString & aId); \
  NS_METHOD GetCertFingerprint(nsACString & aCertFingerprint); \
  NS_METHOD SetCertFingerprint(const nsACString & aCertFingerprint); \
  NS_METHOD GetListener(nsIPresentationControlServerListener * *aListener); \
  NS_METHOD SetListener(nsIPresentationControlServerListener *aListener); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONCONTROLSERVICE(_to) \
  NS_IMETHOD StartServer(bool aEncrypted, uint16_t aPort) override { return _to StartServer(aEncrypted, aPort); } \
  NS_IMETHOD Connect(nsITCPDeviceInfo *aDeviceInfo, nsIPresentationControlChannel * *_retval) override { return _to Connect(aDeviceInfo, _retval); } \
  NS_IMETHOD IsCompatibleServer(uint32_t aVersion, bool *_retval) override { return _to IsCompatibleServer(aVersion, _retval); } \
  NS_IMETHOD Close(void) override { return _to Close(); } \
  NS_IMETHOD GetPort(uint16_t *aPort) override { return _to GetPort(aPort); } \
  NS_IMETHOD GetVersion(uint32_t *aVersion) override { return _to GetVersion(aVersion); } \
  NS_IMETHOD GetId(nsACString & aId) override { return _to GetId(aId); } \
  NS_IMETHOD SetId(const nsACString & aId) override { return _to SetId(aId); } \
  NS_IMETHOD GetCertFingerprint(nsACString & aCertFingerprint) override { return _to GetCertFingerprint(aCertFingerprint); } \
  NS_IMETHOD SetCertFingerprint(const nsACString & aCertFingerprint) override { return _to SetCertFingerprint(aCertFingerprint); } \
  NS_IMETHOD GetListener(nsIPresentationControlServerListener * *aListener) override { return _to GetListener(aListener); } \
  NS_IMETHOD SetListener(nsIPresentationControlServerListener *aListener) override { return _to SetListener(aListener); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONCONTROLSERVICE(_to) \
  NS_IMETHOD StartServer(bool aEncrypted, uint16_t aPort) override { return !_to ? NS_ERROR_NULL_POINTER : _to->StartServer(aEncrypted, aPort); } \
  NS_IMETHOD Connect(nsITCPDeviceInfo *aDeviceInfo, nsIPresentationControlChannel * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Connect(aDeviceInfo, _retval); } \
  NS_IMETHOD IsCompatibleServer(uint32_t aVersion, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->IsCompatibleServer(aVersion, _retval); } \
  NS_IMETHOD Close(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Close(); } \
  NS_IMETHOD GetPort(uint16_t *aPort) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPort(aPort); } \
  NS_IMETHOD GetVersion(uint32_t *aVersion) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetVersion(aVersion); } \
  NS_IMETHOD GetId(nsACString & aId) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetId(aId); } \
  NS_IMETHOD SetId(const nsACString & aId) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetId(aId); } \
  NS_IMETHOD GetCertFingerprint(nsACString & aCertFingerprint) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCertFingerprint(aCertFingerprint); } \
  NS_IMETHOD SetCertFingerprint(const nsACString & aCertFingerprint) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetCertFingerprint(aCertFingerprint); } \
  NS_IMETHOD GetListener(nsIPresentationControlServerListener * *aListener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetListener(aListener); } \
  NS_IMETHOD SetListener(nsIPresentationControlServerListener *aListener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetListener(aListener); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationControlService : public nsIPresentationControlService
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONCONTROLSERVICE

  nsPresentationControlService();

private:
  ~nsPresentationControlService();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationControlService, nsIPresentationControlService)

nsPresentationControlService::nsPresentationControlService()
{
  /* member initializers and constructor code */
}

nsPresentationControlService::~nsPresentationControlService()
{
  /* destructor code */
}

/* void startServer (in boolean aEncrypted, [optional] in uint16_t aPort); */
NS_IMETHODIMP nsPresentationControlService::StartServer(bool aEncrypted, uint16_t aPort)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsIPresentationControlChannel connect (in nsITCPDeviceInfo aDeviceInfo); */
NS_IMETHODIMP nsPresentationControlService::Connect(nsITCPDeviceInfo *aDeviceInfo, nsIPresentationControlChannel * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean isCompatibleServer (in uint32_t aVersion); */
NS_IMETHODIMP nsPresentationControlService::IsCompatibleServer(uint32_t aVersion, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void close (); */
NS_IMETHODIMP nsPresentationControlService::Close()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute uint16_t port; */
NS_IMETHODIMP nsPresentationControlService::GetPort(uint16_t *aPort)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute uint32_t version; */
NS_IMETHODIMP nsPresentationControlService::GetVersion(uint32_t *aVersion)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* attribute AUTF8String id; */
NS_IMETHODIMP nsPresentationControlService::GetId(nsACString & aId)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsPresentationControlService::SetId(const nsACString & aId)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* attribute AUTF8String certFingerprint; */
NS_IMETHODIMP nsPresentationControlService::GetCertFingerprint(nsACString & aCertFingerprint)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsPresentationControlService::SetCertFingerprint(const nsACString & aCertFingerprint)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* attribute nsIPresentationControlServerListener listener; */
NS_IMETHODIMP nsPresentationControlService::GetListener(nsIPresentationControlServerListener * *aListener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsPresentationControlService::SetListener(nsIPresentationControlServerListener *aListener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPresentationControlService_h__ */
