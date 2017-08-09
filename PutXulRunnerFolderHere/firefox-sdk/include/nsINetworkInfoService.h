/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsINetworkInfoService.idl
 */

#ifndef __gen_nsINetworkInfoService_h__
#define __gen_nsINetworkInfoService_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIListNetworkAddressesListener */
#define NS_ILISTNETWORKADDRESSESLISTENER_IID_STR "c4bdaac1-3ab1-4fdb-9a16-17cbed794603"

#define NS_ILISTNETWORKADDRESSESLISTENER_IID \
  {0xc4bdaac1, 0x3ab1, 0x4fdb, \
    { 0x9a, 0x16, 0x17, 0xcb, 0xed, 0x79, 0x46, 0x03 }}

class NS_NO_VTABLE nsIListNetworkAddressesListener : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ILISTNETWORKADDRESSESLISTENER_IID)

  /* void onListedNetworkAddresses ([array, size_is (aAddressArraySize)] in string aAddressArray, in unsigned long aAddressArraySize); */
  NS_IMETHOD OnListedNetworkAddresses(const char * *aAddressArray, uint32_t aAddressArraySize) = 0;

  /* void onListNetworkAddressesFailed (); */
  NS_IMETHOD OnListNetworkAddressesFailed(void) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIListNetworkAddressesListener, NS_ILISTNETWORKADDRESSESLISTENER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSILISTNETWORKADDRESSESLISTENER \
  NS_IMETHOD OnListedNetworkAddresses(const char * *aAddressArray, uint32_t aAddressArraySize) override; \
  NS_IMETHOD OnListNetworkAddressesFailed(void) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSILISTNETWORKADDRESSESLISTENER \
  NS_METHOD OnListedNetworkAddresses(const char * *aAddressArray, uint32_t aAddressArraySize); \
  NS_METHOD OnListNetworkAddressesFailed(void); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSILISTNETWORKADDRESSESLISTENER(_to) \
  NS_IMETHOD OnListedNetworkAddresses(const char * *aAddressArray, uint32_t aAddressArraySize) override { return _to OnListedNetworkAddresses(aAddressArray, aAddressArraySize); } \
  NS_IMETHOD OnListNetworkAddressesFailed(void) override { return _to OnListNetworkAddressesFailed(); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSILISTNETWORKADDRESSESLISTENER(_to) \
  NS_IMETHOD OnListedNetworkAddresses(const char * *aAddressArray, uint32_t aAddressArraySize) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnListedNetworkAddresses(aAddressArray, aAddressArraySize); } \
  NS_IMETHOD OnListNetworkAddressesFailed(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnListNetworkAddressesFailed(); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsListNetworkAddressesListener : public nsIListNetworkAddressesListener
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSILISTNETWORKADDRESSESLISTENER

  nsListNetworkAddressesListener();

private:
  ~nsListNetworkAddressesListener();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsListNetworkAddressesListener, nsIListNetworkAddressesListener)

nsListNetworkAddressesListener::nsListNetworkAddressesListener()
{
  /* member initializers and constructor code */
}

nsListNetworkAddressesListener::~nsListNetworkAddressesListener()
{
  /* destructor code */
}

/* void onListedNetworkAddresses ([array, size_is (aAddressArraySize)] in string aAddressArray, in unsigned long aAddressArraySize); */
NS_IMETHODIMP nsListNetworkAddressesListener::OnListedNetworkAddresses(const char * *aAddressArray, uint32_t aAddressArraySize)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onListNetworkAddressesFailed (); */
NS_IMETHODIMP nsListNetworkAddressesListener::OnListNetworkAddressesFailed()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIGetHostnameListener */
#define NS_IGETHOSTNAMELISTENER_IID_STR "3ebdcb62-2df4-4042-8864-3fa81abd4693"

#define NS_IGETHOSTNAMELISTENER_IID \
  {0x3ebdcb62, 0x2df4, 0x4042, \
    { 0x88, 0x64, 0x3f, 0xa8, 0x1a, 0xbd, 0x46, 0x93 }}

class NS_NO_VTABLE nsIGetHostnameListener : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IGETHOSTNAMELISTENER_IID)

  /* void onGotHostname (in AUTF8String aHostname); */
  NS_IMETHOD OnGotHostname(const nsACString & aHostname) = 0;

  /* void onGetHostnameFailed (); */
  NS_IMETHOD OnGetHostnameFailed(void) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIGetHostnameListener, NS_IGETHOSTNAMELISTENER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIGETHOSTNAMELISTENER \
  NS_IMETHOD OnGotHostname(const nsACString & aHostname) override; \
  NS_IMETHOD OnGetHostnameFailed(void) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIGETHOSTNAMELISTENER \
  NS_METHOD OnGotHostname(const nsACString & aHostname); \
  NS_METHOD OnGetHostnameFailed(void); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIGETHOSTNAMELISTENER(_to) \
  NS_IMETHOD OnGotHostname(const nsACString & aHostname) override { return _to OnGotHostname(aHostname); } \
  NS_IMETHOD OnGetHostnameFailed(void) override { return _to OnGetHostnameFailed(); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIGETHOSTNAMELISTENER(_to) \
  NS_IMETHOD OnGotHostname(const nsACString & aHostname) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnGotHostname(aHostname); } \
  NS_IMETHOD OnGetHostnameFailed(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnGetHostnameFailed(); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsGetHostnameListener : public nsIGetHostnameListener
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIGETHOSTNAMELISTENER

  nsGetHostnameListener();

private:
  ~nsGetHostnameListener();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsGetHostnameListener, nsIGetHostnameListener)

nsGetHostnameListener::nsGetHostnameListener()
{
  /* member initializers and constructor code */
}

nsGetHostnameListener::~nsGetHostnameListener()
{
  /* destructor code */
}

/* void onGotHostname (in AUTF8String aHostname); */
NS_IMETHODIMP nsGetHostnameListener::OnGotHostname(const nsACString & aHostname)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onGetHostnameFailed (); */
NS_IMETHODIMP nsGetHostnameListener::OnGetHostnameFailed()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsINetworkInfoService */
#define NS_INETWORKINFOSERVICE_IID_STR "55fc8dae-4a58-4e0f-a49b-901cbabae809"

#define NS_INETWORKINFOSERVICE_IID \
  {0x55fc8dae, 0x4a58, 0x4e0f, \
    { 0xa4, 0x9b, 0x90, 0x1c, 0xba, 0xba, 0xe8, 0x09 }}

class NS_NO_VTABLE nsINetworkInfoService : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_INETWORKINFOSERVICE_IID)

  /* void listNetworkAddresses (in nsIListNetworkAddressesListener aListener); */
  NS_IMETHOD ListNetworkAddresses(nsIListNetworkAddressesListener *aListener) = 0;

  /* void getHostname (in nsIGetHostnameListener aListener); */
  NS_IMETHOD GetHostname(nsIGetHostnameListener *aListener) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsINetworkInfoService, NS_INETWORKINFOSERVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSINETWORKINFOSERVICE \
  NS_IMETHOD ListNetworkAddresses(nsIListNetworkAddressesListener *aListener) override; \
  NS_IMETHOD GetHostname(nsIGetHostnameListener *aListener) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSINETWORKINFOSERVICE \
  NS_METHOD ListNetworkAddresses(nsIListNetworkAddressesListener *aListener); \
  NS_METHOD GetHostname(nsIGetHostnameListener *aListener); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSINETWORKINFOSERVICE(_to) \
  NS_IMETHOD ListNetworkAddresses(nsIListNetworkAddressesListener *aListener) override { return _to ListNetworkAddresses(aListener); } \
  NS_IMETHOD GetHostname(nsIGetHostnameListener *aListener) override { return _to GetHostname(aListener); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSINETWORKINFOSERVICE(_to) \
  NS_IMETHOD ListNetworkAddresses(nsIListNetworkAddressesListener *aListener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ListNetworkAddresses(aListener); } \
  NS_IMETHOD GetHostname(nsIGetHostnameListener *aListener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetHostname(aListener); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsNetworkInfoService : public nsINetworkInfoService
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSINETWORKINFOSERVICE

  nsNetworkInfoService();

private:
  ~nsNetworkInfoService();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsNetworkInfoService, nsINetworkInfoService)

nsNetworkInfoService::nsNetworkInfoService()
{
  /* member initializers and constructor code */
}

nsNetworkInfoService::~nsNetworkInfoService()
{
  /* destructor code */
}

/* void listNetworkAddresses (in nsIListNetworkAddressesListener aListener); */
NS_IMETHODIMP nsNetworkInfoService::ListNetworkAddresses(nsIListNetworkAddressesListener *aListener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void getHostname (in nsIGetHostnameListener aListener); */
NS_IMETHODIMP nsNetworkInfoService::GetHostname(nsIGetHostnameListener *aListener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif

#define NETWORKINFOSERVICE_CONTRACT_ID \
  "@mozilla.org/network-info-service;1"

#endif /* __gen_nsINetworkInfoService_h__ */
