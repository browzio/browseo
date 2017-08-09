/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIHttpAuthenticatorCallback.idl
 */

#ifndef __gen_nsIHttpAuthenticatorCallback_h__
#define __gen_nsIHttpAuthenticatorCallback_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIHttpAuthenticatorCallback */
#define NS_IHTTPAUTHENTICATORCALLBACK_IID_STR "d989cb03-e446-4086-b9e6-46842cb97bd5"

#define NS_IHTTPAUTHENTICATORCALLBACK_IID \
  {0xd989cb03, 0xe446, 0x4086, \
    { 0xb9, 0xe6, 0x46, 0x84, 0x2c, 0xb9, 0x7b, 0xd5 }}

class NS_NO_VTABLE nsIHttpAuthenticatorCallback : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IHTTPAUTHENTICATORCALLBACK_IID)

  /* void onCredsGenerated (in string aCreds, in unsigned long aFlags, in nsresult aResult, in nsISupports aSessionsState, in nsISupports aContinuationState); */
  NS_IMETHOD OnCredsGenerated(const char * aCreds, uint32_t aFlags, nsresult aResult, nsISupports *aSessionsState, nsISupports *aContinuationState) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIHttpAuthenticatorCallback, NS_IHTTPAUTHENTICATORCALLBACK_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIHTTPAUTHENTICATORCALLBACK \
  NS_IMETHOD OnCredsGenerated(const char * aCreds, uint32_t aFlags, nsresult aResult, nsISupports *aSessionsState, nsISupports *aContinuationState) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIHTTPAUTHENTICATORCALLBACK \
  NS_METHOD OnCredsGenerated(const char * aCreds, uint32_t aFlags, nsresult aResult, nsISupports *aSessionsState, nsISupports *aContinuationState); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIHTTPAUTHENTICATORCALLBACK(_to) \
  NS_IMETHOD OnCredsGenerated(const char * aCreds, uint32_t aFlags, nsresult aResult, nsISupports *aSessionsState, nsISupports *aContinuationState) override { return _to OnCredsGenerated(aCreds, aFlags, aResult, aSessionsState, aContinuationState); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIHTTPAUTHENTICATORCALLBACK(_to) \
  NS_IMETHOD OnCredsGenerated(const char * aCreds, uint32_t aFlags, nsresult aResult, nsISupports *aSessionsState, nsISupports *aContinuationState) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnCredsGenerated(aCreds, aFlags, aResult, aSessionsState, aContinuationState); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsHttpAuthenticatorCallback : public nsIHttpAuthenticatorCallback
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIHTTPAUTHENTICATORCALLBACK

  nsHttpAuthenticatorCallback();

private:
  ~nsHttpAuthenticatorCallback();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsHttpAuthenticatorCallback, nsIHttpAuthenticatorCallback)

nsHttpAuthenticatorCallback::nsHttpAuthenticatorCallback()
{
  /* member initializers and constructor code */
}

nsHttpAuthenticatorCallback::~nsHttpAuthenticatorCallback()
{
  /* destructor code */
}

/* void onCredsGenerated (in string aCreds, in unsigned long aFlags, in nsresult aResult, in nsISupports aSessionsState, in nsISupports aContinuationState); */
NS_IMETHODIMP nsHttpAuthenticatorCallback::OnCredsGenerated(const char * aCreds, uint32_t aFlags, nsresult aResult, nsISupports *aSessionsState, nsISupports *aContinuationState)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIHttpAuthenticatorCallback_h__ */
