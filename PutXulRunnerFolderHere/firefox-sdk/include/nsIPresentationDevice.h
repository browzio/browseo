/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPresentationDevice.idl
 */

#ifndef __gen_nsIPresentationDevice_h__
#define __gen_nsIPresentationDevice_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIPresentationControlChannel; /* forward declaration */


/* starting interface:    nsIPresentationDevice */
#define NS_IPRESENTATIONDEVICE_IID_STR "b1e0a7af-5936-4066-8f2e-f789fb9a7e8f"

#define NS_IPRESENTATIONDEVICE_IID \
  {0xb1e0a7af, 0x5936, 0x4066, \
    { 0x8f, 0x2e, 0xf7, 0x89, 0xfb, 0x9a, 0x7e, 0x8f }}

class NS_NO_VTABLE nsIPresentationDevice : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONDEVICE_IID)

  /* readonly attribute AUTF8String id; */
  NS_IMETHOD GetId(nsACString & aId) = 0;

  /* readonly attribute AUTF8String name; */
  NS_IMETHOD GetName(nsACString & aName) = 0;

  /* readonly attribute AUTF8String type; */
  NS_IMETHOD GetType(nsACString & aType) = 0;

  /* nsIPresentationControlChannel establishControlChannel (); */
  NS_IMETHOD EstablishControlChannel(nsIPresentationControlChannel * *_retval) = 0;

  /* void disconnect (); */
  NS_IMETHOD Disconnect(void) = 0;

  /* boolean isRequestedUrlSupported (in DOMString requestedUrl); */
  NS_IMETHOD IsRequestedUrlSupported(const nsAString & requestedUrl, bool *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationDevice, NS_IPRESENTATIONDEVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONDEVICE \
  NS_IMETHOD GetId(nsACString & aId) override; \
  NS_IMETHOD GetName(nsACString & aName) override; \
  NS_IMETHOD GetType(nsACString & aType) override; \
  NS_IMETHOD EstablishControlChannel(nsIPresentationControlChannel * *_retval) override; \
  NS_IMETHOD Disconnect(void) override; \
  NS_IMETHOD IsRequestedUrlSupported(const nsAString & requestedUrl, bool *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONDEVICE \
  NS_METHOD GetId(nsACString & aId); \
  NS_METHOD GetName(nsACString & aName); \
  NS_METHOD GetType(nsACString & aType); \
  NS_METHOD EstablishControlChannel(nsIPresentationControlChannel * *_retval); \
  NS_METHOD Disconnect(void); \
  NS_METHOD IsRequestedUrlSupported(const nsAString & requestedUrl, bool *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONDEVICE(_to) \
  NS_IMETHOD GetId(nsACString & aId) override { return _to GetId(aId); } \
  NS_IMETHOD GetName(nsACString & aName) override { return _to GetName(aName); } \
  NS_IMETHOD GetType(nsACString & aType) override { return _to GetType(aType); } \
  NS_IMETHOD EstablishControlChannel(nsIPresentationControlChannel * *_retval) override { return _to EstablishControlChannel(_retval); } \
  NS_IMETHOD Disconnect(void) override { return _to Disconnect(); } \
  NS_IMETHOD IsRequestedUrlSupported(const nsAString & requestedUrl, bool *_retval) override { return _to IsRequestedUrlSupported(requestedUrl, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONDEVICE(_to) \
  NS_IMETHOD GetId(nsACString & aId) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetId(aId); } \
  NS_IMETHOD GetName(nsACString & aName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetName(aName); } \
  NS_IMETHOD GetType(nsACString & aType) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetType(aType); } \
  NS_IMETHOD EstablishControlChannel(nsIPresentationControlChannel * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->EstablishControlChannel(_retval); } \
  NS_IMETHOD Disconnect(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Disconnect(); } \
  NS_IMETHOD IsRequestedUrlSupported(const nsAString & requestedUrl, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->IsRequestedUrlSupported(requestedUrl, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationDevice : public nsIPresentationDevice
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONDEVICE

  nsPresentationDevice();

private:
  ~nsPresentationDevice();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationDevice, nsIPresentationDevice)

nsPresentationDevice::nsPresentationDevice()
{
  /* member initializers and constructor code */
}

nsPresentationDevice::~nsPresentationDevice()
{
  /* destructor code */
}

/* readonly attribute AUTF8String id; */
NS_IMETHODIMP nsPresentationDevice::GetId(nsACString & aId)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String name; */
NS_IMETHODIMP nsPresentationDevice::GetName(nsACString & aName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String type; */
NS_IMETHODIMP nsPresentationDevice::GetType(nsACString & aType)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsIPresentationControlChannel establishControlChannel (); */
NS_IMETHODIMP nsPresentationDevice::EstablishControlChannel(nsIPresentationControlChannel * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void disconnect (); */
NS_IMETHODIMP nsPresentationDevice::Disconnect()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean isRequestedUrlSupported (in DOMString requestedUrl); */
NS_IMETHODIMP nsPresentationDevice::IsRequestedUrlSupported(const nsAString & requestedUrl, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPresentationDevice_h__ */
