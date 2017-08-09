/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIContentSignatureVerifier.idl
 */

#ifndef __gen_nsIContentSignatureVerifier_h__
#define __gen_nsIContentSignatureVerifier_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIContentSignatureReceiverCallback; /* forward declaration */


/* starting interface:    nsIContentSignatureVerifier */
#define NS_ICONTENTSIGNATUREVERIFIER_IID_STR "45a5fe2f-c350-4b86-962d-02d5aaaa955a"

#define NS_ICONTENTSIGNATUREVERIFIER_IID \
  {0x45a5fe2f, 0xc350, 0x4b86, \
    { 0x96, 0x2d, 0x02, 0xd5, 0xaa, 0xaa, 0x95, 0x5a }}

class NS_NO_VTABLE nsIContentSignatureVerifier : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ICONTENTSIGNATUREVERIFIER_IID)

  /* boolean verifyContentSignature (in ACString aData, in ACString aContentSignatureHeader, in ACString aCertificateChain, in ACString aName); */
  NS_IMETHOD VerifyContentSignature(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName, bool *_retval) = 0;

  /* void createContext (in ACString aData, in ACString aContentSignatureHeader, in ACString aCertificateChain, in ACString aName); */
  NS_IMETHOD CreateContext(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName) = 0;

  /* void createContextWithoutCertChain (in nsIContentSignatureReceiverCallback aCallback, in ACString aContentSignatureHeader, in ACString aName); */
  NS_IMETHOD CreateContextWithoutCertChain(nsIContentSignatureReceiverCallback *aCallback, const nsACString & aContentSignatureHeader, const nsACString & aName) = 0;

  /* void update (in ACString aData); */
  NS_IMETHOD Update(const nsACString & aData) = 0;

  /* boolean end (); */
  NS_IMETHOD End(bool *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIContentSignatureVerifier, NS_ICONTENTSIGNATUREVERIFIER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSICONTENTSIGNATUREVERIFIER \
  NS_IMETHOD VerifyContentSignature(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName, bool *_retval) override; \
  NS_IMETHOD CreateContext(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName) override; \
  NS_IMETHOD CreateContextWithoutCertChain(nsIContentSignatureReceiverCallback *aCallback, const nsACString & aContentSignatureHeader, const nsACString & aName) override; \
  NS_IMETHOD Update(const nsACString & aData) override; \
  NS_IMETHOD End(bool *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSICONTENTSIGNATUREVERIFIER \
  NS_METHOD VerifyContentSignature(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName, bool *_retval); \
  NS_METHOD CreateContext(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName); \
  NS_METHOD CreateContextWithoutCertChain(nsIContentSignatureReceiverCallback *aCallback, const nsACString & aContentSignatureHeader, const nsACString & aName); \
  NS_METHOD Update(const nsACString & aData); \
  NS_METHOD End(bool *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSICONTENTSIGNATUREVERIFIER(_to) \
  NS_IMETHOD VerifyContentSignature(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName, bool *_retval) override { return _to VerifyContentSignature(aData, aContentSignatureHeader, aCertificateChain, aName, _retval); } \
  NS_IMETHOD CreateContext(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName) override { return _to CreateContext(aData, aContentSignatureHeader, aCertificateChain, aName); } \
  NS_IMETHOD CreateContextWithoutCertChain(nsIContentSignatureReceiverCallback *aCallback, const nsACString & aContentSignatureHeader, const nsACString & aName) override { return _to CreateContextWithoutCertChain(aCallback, aContentSignatureHeader, aName); } \
  NS_IMETHOD Update(const nsACString & aData) override { return _to Update(aData); } \
  NS_IMETHOD End(bool *_retval) override { return _to End(_retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSICONTENTSIGNATUREVERIFIER(_to) \
  NS_IMETHOD VerifyContentSignature(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->VerifyContentSignature(aData, aContentSignatureHeader, aCertificateChain, aName, _retval); } \
  NS_IMETHOD CreateContext(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CreateContext(aData, aContentSignatureHeader, aCertificateChain, aName); } \
  NS_IMETHOD CreateContextWithoutCertChain(nsIContentSignatureReceiverCallback *aCallback, const nsACString & aContentSignatureHeader, const nsACString & aName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CreateContextWithoutCertChain(aCallback, aContentSignatureHeader, aName); } \
  NS_IMETHOD Update(const nsACString & aData) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Update(aData); } \
  NS_IMETHOD End(bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->End(_retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsContentSignatureVerifier : public nsIContentSignatureVerifier
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSICONTENTSIGNATUREVERIFIER

  nsContentSignatureVerifier();

private:
  ~nsContentSignatureVerifier();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsContentSignatureVerifier, nsIContentSignatureVerifier)

nsContentSignatureVerifier::nsContentSignatureVerifier()
{
  /* member initializers and constructor code */
}

nsContentSignatureVerifier::~nsContentSignatureVerifier()
{
  /* destructor code */
}

/* boolean verifyContentSignature (in ACString aData, in ACString aContentSignatureHeader, in ACString aCertificateChain, in ACString aName); */
NS_IMETHODIMP nsContentSignatureVerifier::VerifyContentSignature(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void createContext (in ACString aData, in ACString aContentSignatureHeader, in ACString aCertificateChain, in ACString aName); */
NS_IMETHODIMP nsContentSignatureVerifier::CreateContext(const nsACString & aData, const nsACString & aContentSignatureHeader, const nsACString & aCertificateChain, const nsACString & aName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void createContextWithoutCertChain (in nsIContentSignatureReceiverCallback aCallback, in ACString aContentSignatureHeader, in ACString aName); */
NS_IMETHODIMP nsContentSignatureVerifier::CreateContextWithoutCertChain(nsIContentSignatureReceiverCallback *aCallback, const nsACString & aContentSignatureHeader, const nsACString & aName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void update (in ACString aData); */
NS_IMETHODIMP nsContentSignatureVerifier::Update(const nsACString & aData)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean end (); */
NS_IMETHODIMP nsContentSignatureVerifier::End(bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIContentSignatureReceiverCallback */
#define NS_ICONTENTSIGNATURERECEIVERCALLBACK_IID_STR "1eb90707-df59-48b7-9d42-d8bf630ae744"

#define NS_ICONTENTSIGNATURERECEIVERCALLBACK_IID \
  {0x1eb90707, 0xdf59, 0x48b7, \
    { 0x9d, 0x42, 0xd8, 0xbf, 0x63, 0x0a, 0xe7, 0x44 }}

class NS_NO_VTABLE nsIContentSignatureReceiverCallback : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ICONTENTSIGNATURERECEIVERCALLBACK_IID)

  /* void contextCreated (in boolean successful); */
  NS_IMETHOD ContextCreated(bool successful) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIContentSignatureReceiverCallback, NS_ICONTENTSIGNATURERECEIVERCALLBACK_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSICONTENTSIGNATURERECEIVERCALLBACK \
  NS_IMETHOD ContextCreated(bool successful) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSICONTENTSIGNATURERECEIVERCALLBACK \
  NS_METHOD ContextCreated(bool successful); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSICONTENTSIGNATURERECEIVERCALLBACK(_to) \
  NS_IMETHOD ContextCreated(bool successful) override { return _to ContextCreated(successful); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSICONTENTSIGNATURERECEIVERCALLBACK(_to) \
  NS_IMETHOD ContextCreated(bool successful) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ContextCreated(successful); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsContentSignatureReceiverCallback : public nsIContentSignatureReceiverCallback
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSICONTENTSIGNATURERECEIVERCALLBACK

  nsContentSignatureReceiverCallback();

private:
  ~nsContentSignatureReceiverCallback();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsContentSignatureReceiverCallback, nsIContentSignatureReceiverCallback)

nsContentSignatureReceiverCallback::nsContentSignatureReceiverCallback()
{
  /* member initializers and constructor code */
}

nsContentSignatureReceiverCallback::~nsContentSignatureReceiverCallback()
{
  /* destructor code */
}

/* void contextCreated (in boolean successful); */
NS_IMETHODIMP nsContentSignatureReceiverCallback::ContextCreated(bool successful)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIContentSignatureVerifier_h__ */
