/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIEditorUtils.idl
 */

#ifndef __gen_nsIEditorUtils_h__
#define __gen_nsIEditorUtils_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

#ifndef __gen_domstubs_h__
#include "domstubs.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIDOMBlob; /* forward declaration */

class mozIDOMWindowProxy; /* forward declaration */


/* starting interface:    nsIEditorBlobListener */
#define NS_IEDITORBLOBLISTENER_IID_STR "eb8b8ad9-5d8f-43bd-8ce5-5b943c180d56"

#define NS_IEDITORBLOBLISTENER_IID \
  {0xeb8b8ad9, 0x5d8f, 0x43bd, \
    { 0x8c, 0xe5, 0x5b, 0x94, 0x3c, 0x18, 0x0d, 0x56 }}

class NS_NO_VTABLE nsIEditorBlobListener : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IEDITORBLOBLISTENER_IID)

  /* void onResult (in ACString aResult); */
  NS_IMETHOD OnResult(const nsACString & aResult) = 0;

  /* void onError (in AString aErrorName); */
  NS_IMETHOD OnError(const nsAString & aErrorName) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIEditorBlobListener, NS_IEDITORBLOBLISTENER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIEDITORBLOBLISTENER \
  NS_IMETHOD OnResult(const nsACString & aResult) override; \
  NS_IMETHOD OnError(const nsAString & aErrorName) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIEDITORBLOBLISTENER \
  NS_METHOD OnResult(const nsACString & aResult); \
  NS_METHOD OnError(const nsAString & aErrorName); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIEDITORBLOBLISTENER(_to) \
  NS_IMETHOD OnResult(const nsACString & aResult) override { return _to OnResult(aResult); } \
  NS_IMETHOD OnError(const nsAString & aErrorName) override { return _to OnError(aErrorName); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIEDITORBLOBLISTENER(_to) \
  NS_IMETHOD OnResult(const nsACString & aResult) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnResult(aResult); } \
  NS_IMETHOD OnError(const nsAString & aErrorName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnError(aErrorName); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsEditorBlobListener : public nsIEditorBlobListener
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIEDITORBLOBLISTENER

  nsEditorBlobListener();

private:
  ~nsEditorBlobListener();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsEditorBlobListener, nsIEditorBlobListener)

nsEditorBlobListener::nsEditorBlobListener()
{
  /* member initializers and constructor code */
}

nsEditorBlobListener::~nsEditorBlobListener()
{
  /* destructor code */
}

/* void onResult (in ACString aResult); */
NS_IMETHODIMP nsEditorBlobListener::OnResult(const nsACString & aResult)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onError (in AString aErrorName); */
NS_IMETHODIMP nsEditorBlobListener::OnError(const nsAString & aErrorName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIEditorUtils */
#define NS_IEDITORUTILS_IID_STR "4bf94928-575e-4bd1-8321-a2c4b3d0119e"

#define NS_IEDITORUTILS_IID \
  {0x4bf94928, 0x575e, 0x4bd1, \
    { 0x83, 0x21, 0xa2, 0xc4, 0xb3, 0xd0, 0x11, 0x9e }}

class NS_NO_VTABLE nsIEditorUtils : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IEDITORUTILS_IID)

  /* void slurpBlob (in nsIDOMBlob aBlob, in mozIDOMWindowProxy aScope, in nsIEditorBlobListener aListener); */
  NS_IMETHOD SlurpBlob(nsIDOMBlob *aBlob, mozIDOMWindowProxy *aScope, nsIEditorBlobListener *aListener) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIEditorUtils, NS_IEDITORUTILS_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIEDITORUTILS \
  NS_IMETHOD SlurpBlob(nsIDOMBlob *aBlob, mozIDOMWindowProxy *aScope, nsIEditorBlobListener *aListener) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIEDITORUTILS \
  NS_METHOD SlurpBlob(nsIDOMBlob *aBlob, mozIDOMWindowProxy *aScope, nsIEditorBlobListener *aListener); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIEDITORUTILS(_to) \
  NS_IMETHOD SlurpBlob(nsIDOMBlob *aBlob, mozIDOMWindowProxy *aScope, nsIEditorBlobListener *aListener) override { return _to SlurpBlob(aBlob, aScope, aListener); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIEDITORUTILS(_to) \
  NS_IMETHOD SlurpBlob(nsIDOMBlob *aBlob, mozIDOMWindowProxy *aScope, nsIEditorBlobListener *aListener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SlurpBlob(aBlob, aScope, aListener); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsEditorUtils : public nsIEditorUtils
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIEDITORUTILS

  nsEditorUtils();

private:
  ~nsEditorUtils();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsEditorUtils, nsIEditorUtils)

nsEditorUtils::nsEditorUtils()
{
  /* member initializers and constructor code */
}

nsEditorUtils::~nsEditorUtils()
{
  /* destructor code */
}

/* void slurpBlob (in nsIDOMBlob aBlob, in mozIDOMWindowProxy aScope, in nsIEditorBlobListener aListener); */
NS_IMETHODIMP nsEditorUtils::SlurpBlob(nsIDOMBlob *aBlob, mozIDOMWindowProxy *aScope, nsIEditorBlobListener *aListener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIEditorUtils_h__ */
