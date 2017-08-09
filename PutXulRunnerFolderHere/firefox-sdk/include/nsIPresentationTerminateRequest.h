/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPresentationTerminateRequest.idl
 */

#ifndef __gen_nsIPresentationTerminateRequest_h__
#define __gen_nsIPresentationTerminateRequest_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIPresentationDevice; /* forward declaration */

class nsIPresentationControlChannel; /* forward declaration */

#define PRESENTATION_TERMINATE_REQUEST_TOPIC "presentation-terminate-request"

/* starting interface:    nsIPresentationTerminateRequest */
#define NS_IPRESENTATIONTERMINATEREQUEST_IID_STR "3ddbf3a4-53ee-4b70-9bbc-58ac90dce6b5"

#define NS_IPRESENTATIONTERMINATEREQUEST_IID \
  {0x3ddbf3a4, 0x53ee, 0x4b70, \
    { 0x9b, 0xbc, 0x58, 0xac, 0x90, 0xdc, 0xe6, 0xb5 }}

class NS_NO_VTABLE nsIPresentationTerminateRequest : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONTERMINATEREQUEST_IID)

  /* readonly attribute nsIPresentationDevice device; */
  NS_IMETHOD GetDevice(nsIPresentationDevice * *aDevice) = 0;

  /* readonly attribute DOMString presentationId; */
  NS_IMETHOD GetPresentationId(nsAString & aPresentationId) = 0;

  /* readonly attribute nsIPresentationControlChannel controlChannel; */
  NS_IMETHOD GetControlChannel(nsIPresentationControlChannel * *aControlChannel) = 0;

  /* readonly attribute boolean isFromReceiver; */
  NS_IMETHOD GetIsFromReceiver(bool *aIsFromReceiver) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationTerminateRequest, NS_IPRESENTATIONTERMINATEREQUEST_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONTERMINATEREQUEST \
  NS_IMETHOD GetDevice(nsIPresentationDevice * *aDevice) override; \
  NS_IMETHOD GetPresentationId(nsAString & aPresentationId) override; \
  NS_IMETHOD GetControlChannel(nsIPresentationControlChannel * *aControlChannel) override; \
  NS_IMETHOD GetIsFromReceiver(bool *aIsFromReceiver) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONTERMINATEREQUEST \
  NS_METHOD GetDevice(nsIPresentationDevice * *aDevice); \
  NS_METHOD GetPresentationId(nsAString & aPresentationId); \
  NS_METHOD GetControlChannel(nsIPresentationControlChannel * *aControlChannel); \
  NS_METHOD GetIsFromReceiver(bool *aIsFromReceiver); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONTERMINATEREQUEST(_to) \
  NS_IMETHOD GetDevice(nsIPresentationDevice * *aDevice) override { return _to GetDevice(aDevice); } \
  NS_IMETHOD GetPresentationId(nsAString & aPresentationId) override { return _to GetPresentationId(aPresentationId); } \
  NS_IMETHOD GetControlChannel(nsIPresentationControlChannel * *aControlChannel) override { return _to GetControlChannel(aControlChannel); } \
  NS_IMETHOD GetIsFromReceiver(bool *aIsFromReceiver) override { return _to GetIsFromReceiver(aIsFromReceiver); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONTERMINATEREQUEST(_to) \
  NS_IMETHOD GetDevice(nsIPresentationDevice * *aDevice) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDevice(aDevice); } \
  NS_IMETHOD GetPresentationId(nsAString & aPresentationId) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPresentationId(aPresentationId); } \
  NS_IMETHOD GetControlChannel(nsIPresentationControlChannel * *aControlChannel) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetControlChannel(aControlChannel); } \
  NS_IMETHOD GetIsFromReceiver(bool *aIsFromReceiver) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIsFromReceiver(aIsFromReceiver); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationTerminateRequest : public nsIPresentationTerminateRequest
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONTERMINATEREQUEST

  nsPresentationTerminateRequest();

private:
  ~nsPresentationTerminateRequest();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationTerminateRequest, nsIPresentationTerminateRequest)

nsPresentationTerminateRequest::nsPresentationTerminateRequest()
{
  /* member initializers and constructor code */
}

nsPresentationTerminateRequest::~nsPresentationTerminateRequest()
{
  /* destructor code */
}

/* readonly attribute nsIPresentationDevice device; */
NS_IMETHODIMP nsPresentationTerminateRequest::GetDevice(nsIPresentationDevice * *aDevice)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute DOMString presentationId; */
NS_IMETHODIMP nsPresentationTerminateRequest::GetPresentationId(nsAString & aPresentationId)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIPresentationControlChannel controlChannel; */
NS_IMETHODIMP nsPresentationTerminateRequest::GetControlChannel(nsIPresentationControlChannel * *aControlChannel)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute boolean isFromReceiver; */
NS_IMETHODIMP nsPresentationTerminateRequest::GetIsFromReceiver(bool *aIsFromReceiver)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPresentationTerminateRequest_h__ */
