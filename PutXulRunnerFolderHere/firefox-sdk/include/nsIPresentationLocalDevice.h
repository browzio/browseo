/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPresentationLocalDevice.idl
 */

#ifndef __gen_nsIPresentationLocalDevice_h__
#define __gen_nsIPresentationLocalDevice_h__


#ifndef __gen_nsIPresentationDevice_h__
#include "nsIPresentationDevice.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIPresentationLocalDevice */
#define NS_IPRESENTATIONLOCALDEVICE_IID_STR "dd239720-cab6-4fb5-9025-cba23f1bbc2d"

#define NS_IPRESENTATIONLOCALDEVICE_IID \
  {0xdd239720, 0xcab6, 0x4fb5, \
    { 0x90, 0x25, 0xcb, 0xa2, 0x3f, 0x1b, 0xbc, 0x2d }}

class NS_NO_VTABLE nsIPresentationLocalDevice : public nsIPresentationDevice {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONLOCALDEVICE_IID)

  /* readonly attribute AUTF8String windowId; */
  NS_IMETHOD GetWindowId(nsACString & aWindowId) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationLocalDevice, NS_IPRESENTATIONLOCALDEVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONLOCALDEVICE \
  NS_IMETHOD GetWindowId(nsACString & aWindowId) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONLOCALDEVICE \
  NS_METHOD GetWindowId(nsACString & aWindowId); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONLOCALDEVICE(_to) \
  NS_IMETHOD GetWindowId(nsACString & aWindowId) override { return _to GetWindowId(aWindowId); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONLOCALDEVICE(_to) \
  NS_IMETHOD GetWindowId(nsACString & aWindowId) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetWindowId(aWindowId); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationLocalDevice : public nsIPresentationLocalDevice
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONLOCALDEVICE

  nsPresentationLocalDevice();

private:
  ~nsPresentationLocalDevice();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationLocalDevice, nsIPresentationLocalDevice)

nsPresentationLocalDevice::nsPresentationLocalDevice()
{
  /* member initializers and constructor code */
}

nsPresentationLocalDevice::~nsPresentationLocalDevice()
{
  /* destructor code */
}

/* readonly attribute AUTF8String windowId; */
NS_IMETHODIMP nsPresentationLocalDevice::GetWindowId(nsACString & aWindowId)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPresentationLocalDevice_h__ */
