/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsITransportProvider.idl
 */

#ifndef __gen_nsITransportProvider_h__
#define __gen_nsITransportProvider_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIHttpUpgradeListener; /* forward declaration */

namespace mozilla {
namespace net {
class PTransportProviderChild;
}
}

/* starting interface:    nsITransportProvider */
#define NS_ITRANSPORTPROVIDER_IID_STR "6fcec704-cfd2-46ef-a394-a64d5cb1475c"

#define NS_ITRANSPORTPROVIDER_IID \
  {0x6fcec704, 0xcfd2, 0x46ef, \
    { 0xa3, 0x94, 0xa6, 0x4d, 0x5c, 0xb1, 0x47, 0x5c }}

class NS_NO_VTABLE nsITransportProvider : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ITRANSPORTPROVIDER_IID)

  /* void setListener (in nsIHttpUpgradeListener listener); */
  NS_IMETHOD SetListener(nsIHttpUpgradeListener *listener) = 0;

  /* [noscript] PTransportProviderChild getIPCChild (); */
  NS_IMETHOD GetIPCChild(mozilla::net::PTransportProviderChild **_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsITransportProvider, NS_ITRANSPORTPROVIDER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSITRANSPORTPROVIDER \
  NS_IMETHOD SetListener(nsIHttpUpgradeListener *listener) override; \
  NS_IMETHOD GetIPCChild(mozilla::net::PTransportProviderChild **_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSITRANSPORTPROVIDER \
  NS_METHOD SetListener(nsIHttpUpgradeListener *listener); \
  NS_METHOD GetIPCChild(mozilla::net::PTransportProviderChild **_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSITRANSPORTPROVIDER(_to) \
  NS_IMETHOD SetListener(nsIHttpUpgradeListener *listener) override { return _to SetListener(listener); } \
  NS_IMETHOD GetIPCChild(mozilla::net::PTransportProviderChild **_retval) override { return _to GetIPCChild(_retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSITRANSPORTPROVIDER(_to) \
  NS_IMETHOD SetListener(nsIHttpUpgradeListener *listener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetListener(listener); } \
  NS_IMETHOD GetIPCChild(mozilla::net::PTransportProviderChild **_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIPCChild(_retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsTransportProvider : public nsITransportProvider
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSITRANSPORTPROVIDER

  nsTransportProvider();

private:
  ~nsTransportProvider();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsTransportProvider, nsITransportProvider)

nsTransportProvider::nsTransportProvider()
{
  /* member initializers and constructor code */
}

nsTransportProvider::~nsTransportProvider()
{
  /* destructor code */
}

/* void setListener (in nsIHttpUpgradeListener listener); */
NS_IMETHODIMP nsTransportProvider::SetListener(nsIHttpUpgradeListener *listener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] PTransportProviderChild getIPCChild (); */
NS_IMETHODIMP nsTransportProvider::GetIPCChild(mozilla::net::PTransportProviderChild **_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsITransportProvider_h__ */
