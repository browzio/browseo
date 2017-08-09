/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIHstsPrimingCallback.idl
 */

#ifndef __gen_nsIHstsPrimingCallback_h__
#define __gen_nsIHstsPrimingCallback_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIHstsPrimingCallback */
#define NS_IHSTSPRIMINGCALLBACK_IID_STR "eca6daca-3f2a-4a2a-b3bf-9f24f79bc999"

#define NS_IHSTSPRIMINGCALLBACK_IID \
  {0xeca6daca, 0x3f2a, 0x4a2a, \
    { 0xb3, 0xbf, 0x9f, 0x24, 0xf7, 0x9b, 0xc9, 0x99 }}

class NS_NO_VTABLE nsIHstsPrimingCallback : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IHSTSPRIMINGCALLBACK_IID)

  /* [noscript,nostdcall] void onHSTSPrimingSucceeded (in bool aCached); */
  virtual nsresult OnHSTSPrimingSucceeded(bool aCached) = 0;

  /* [noscript,nostdcall] void onHSTSPrimingFailed (in nsresult aError, in bool aCached); */
  virtual nsresult OnHSTSPrimingFailed(nsresult aError, bool aCached) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIHstsPrimingCallback, NS_IHSTSPRIMINGCALLBACK_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIHSTSPRIMINGCALLBACK \
  virtual nsresult OnHSTSPrimingSucceeded(bool aCached) override; \
  virtual nsresult OnHSTSPrimingFailed(nsresult aError, bool aCached) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIHSTSPRIMINGCALLBACK \
  nsresult OnHSTSPrimingSucceeded(bool aCached); \
  nsresult OnHSTSPrimingFailed(nsresult aError, bool aCached); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIHSTSPRIMINGCALLBACK(_to) \
  virtual nsresult OnHSTSPrimingSucceeded(bool aCached) override { return _to OnHSTSPrimingSucceeded(aCached); } \
  virtual nsresult OnHSTSPrimingFailed(nsresult aError, bool aCached) override { return _to OnHSTSPrimingFailed(aError, aCached); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIHSTSPRIMINGCALLBACK(_to) \
  virtual nsresult OnHSTSPrimingSucceeded(bool aCached) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnHSTSPrimingSucceeded(aCached); } \
  virtual nsresult OnHSTSPrimingFailed(nsresult aError, bool aCached) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnHSTSPrimingFailed(aError, aCached); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsHstsPrimingCallback : public nsIHstsPrimingCallback
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIHSTSPRIMINGCALLBACK

  nsHstsPrimingCallback();

private:
  ~nsHstsPrimingCallback();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsHstsPrimingCallback, nsIHstsPrimingCallback)

nsHstsPrimingCallback::nsHstsPrimingCallback()
{
  /* member initializers and constructor code */
}

nsHstsPrimingCallback::~nsHstsPrimingCallback()
{
  /* destructor code */
}

/* [noscript,nostdcall] void onHSTSPrimingSucceeded (in bool aCached); */
nsresult nsHstsPrimingCallback::OnHSTSPrimingSucceeded(bool aCached)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript,nostdcall] void onHSTSPrimingFailed (in nsresult aError, in bool aCached); */
nsresult nsHstsPrimingCallback::OnHSTSPrimingFailed(nsresult aError, bool aCached)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIHstsPrimingCallback_h__ */
