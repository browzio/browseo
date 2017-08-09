/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIChannelWithDivertableParentListener.idl
 */

#ifndef __gen_nsIChannelWithDivertableParentListener_h__
#define __gen_nsIChannelWithDivertableParentListener_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
namespace mozilla {
namespace net {
class ADivertableParentChannel;
}
}

/* starting interface:    nsIChannelWithDivertableParentListener */
#define NS_ICHANNELWITHDIVERTABLEPARENTLISTENER_IID_STR "c073d79f-2503-4dff-ba87-d3071f8b433b"

#define NS_ICHANNELWITHDIVERTABLEPARENTLISTENER_IID \
  {0xc073d79f, 0x2503, 0x4dff, \
    { 0xba, 0x87, 0xd3, 0x07, 0x1f, 0x8b, 0x43, 0x3b }}

class NS_NO_VTABLE nsIChannelWithDivertableParentListener : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ICHANNELWITHDIVERTABLEPARENTLISTENER_IID)

  /* void MessageDiversionStarted (in ADivertableParentChannelPtr aParentChannel); */
  NS_IMETHOD MessageDiversionStarted(mozilla::net::ADivertableParentChannel *aParentChannel) = 0;

  /* void MessageDiversionStop (); */
  NS_IMETHOD MessageDiversionStop(void) = 0;

  /* void SuspendInternal (); */
  NS_IMETHOD SuspendInternal(void) = 0;

  /* void ResumeInternal (); */
  NS_IMETHOD ResumeInternal(void) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIChannelWithDivertableParentListener, NS_ICHANNELWITHDIVERTABLEPARENTLISTENER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSICHANNELWITHDIVERTABLEPARENTLISTENER \
  NS_IMETHOD MessageDiversionStarted(mozilla::net::ADivertableParentChannel *aParentChannel) override; \
  NS_IMETHOD MessageDiversionStop(void) override; \
  NS_IMETHOD SuspendInternal(void) override; \
  NS_IMETHOD ResumeInternal(void) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSICHANNELWITHDIVERTABLEPARENTLISTENER \
  NS_METHOD MessageDiversionStarted(mozilla::net::ADivertableParentChannel *aParentChannel); \
  NS_METHOD MessageDiversionStop(void); \
  NS_METHOD SuspendInternal(void); \
  NS_METHOD ResumeInternal(void); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSICHANNELWITHDIVERTABLEPARENTLISTENER(_to) \
  NS_IMETHOD MessageDiversionStarted(mozilla::net::ADivertableParentChannel *aParentChannel) override { return _to MessageDiversionStarted(aParentChannel); } \
  NS_IMETHOD MessageDiversionStop(void) override { return _to MessageDiversionStop(); } \
  NS_IMETHOD SuspendInternal(void) override { return _to SuspendInternal(); } \
  NS_IMETHOD ResumeInternal(void) override { return _to ResumeInternal(); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSICHANNELWITHDIVERTABLEPARENTLISTENER(_to) \
  NS_IMETHOD MessageDiversionStarted(mozilla::net::ADivertableParentChannel *aParentChannel) override { return !_to ? NS_ERROR_NULL_POINTER : _to->MessageDiversionStarted(aParentChannel); } \
  NS_IMETHOD MessageDiversionStop(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->MessageDiversionStop(); } \
  NS_IMETHOD SuspendInternal(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SuspendInternal(); } \
  NS_IMETHOD ResumeInternal(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ResumeInternal(); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsChannelWithDivertableParentListener : public nsIChannelWithDivertableParentListener
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSICHANNELWITHDIVERTABLEPARENTLISTENER

  nsChannelWithDivertableParentListener();

private:
  ~nsChannelWithDivertableParentListener();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsChannelWithDivertableParentListener, nsIChannelWithDivertableParentListener)

nsChannelWithDivertableParentListener::nsChannelWithDivertableParentListener()
{
  /* member initializers and constructor code */
}

nsChannelWithDivertableParentListener::~nsChannelWithDivertableParentListener()
{
  /* destructor code */
}

/* void MessageDiversionStarted (in ADivertableParentChannelPtr aParentChannel); */
NS_IMETHODIMP nsChannelWithDivertableParentListener::MessageDiversionStarted(mozilla::net::ADivertableParentChannel *aParentChannel)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void MessageDiversionStop (); */
NS_IMETHODIMP nsChannelWithDivertableParentListener::MessageDiversionStop()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void SuspendInternal (); */
NS_IMETHODIMP nsChannelWithDivertableParentListener::SuspendInternal()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void ResumeInternal (); */
NS_IMETHODIMP nsChannelWithDivertableParentListener::ResumeInternal()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIChannelWithDivertableParentListener_h__ */
