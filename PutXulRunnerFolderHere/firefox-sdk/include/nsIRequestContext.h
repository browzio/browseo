/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIRequestContext.idl
 */

#ifndef __gen_nsIRequestContext_h__
#define __gen_nsIRequestContext_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
// Forward-declare mozilla::net::SpdyPushCache
namespace mozilla {
namespace net {
class SpdyPushCache;
}
}

/* starting interface:    nsIRequestContext */
#define NS_IREQUESTCONTEXT_IID_STR "658e3e6e-8633-4b1a-8d66-fa9f72293e63"

#define NS_IREQUESTCONTEXT_IID \
  {0x658e3e6e, 0x8633, 0x4b1a, \
    { 0x8d, 0x66, 0xfa, 0x9f, 0x72, 0x29, 0x3e, 0x63 }}

class NS_NO_VTABLE nsIRequestContext : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IREQUESTCONTEXT_IID)

  /* [noscript] readonly attribute nsID ID; */
  NS_IMETHOD GetID(nsID *aID) = 0;

  /* readonly attribute unsigned long blockingTransactionCount; */
  NS_IMETHOD GetBlockingTransactionCount(uint32_t *aBlockingTransactionCount) = 0;

  /* void addBlockingTransaction (); */
  NS_IMETHOD AddBlockingTransaction(void) = 0;

  /* unsigned long removeBlockingTransaction (); */
  NS_IMETHOD RemoveBlockingTransaction(uint32_t *_retval) = 0;

  /* [noscript] attribute SpdyPushCachePtr spdyPushCache; */
  NS_IMETHOD GetSpdyPushCache(mozilla::net::SpdyPushCache **aSpdyPushCache) = 0;
  NS_IMETHOD SetSpdyPushCache(mozilla::net::SpdyPushCache *aSpdyPushCache) = 0;

  /* [noscript] attribute ACString userAgentOverride; */
  NS_IMETHOD GetUserAgentOverride(nsACString & aUserAgentOverride) = 0;
  NS_IMETHOD SetUserAgentOverride(const nsACString & aUserAgentOverride) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIRequestContext, NS_IREQUESTCONTEXT_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIREQUESTCONTEXT \
  NS_IMETHOD GetID(nsID *aID) override; \
  NS_IMETHOD GetBlockingTransactionCount(uint32_t *aBlockingTransactionCount) override; \
  NS_IMETHOD AddBlockingTransaction(void) override; \
  NS_IMETHOD RemoveBlockingTransaction(uint32_t *_retval) override; \
  NS_IMETHOD GetSpdyPushCache(mozilla::net::SpdyPushCache **aSpdyPushCache) override; \
  NS_IMETHOD SetSpdyPushCache(mozilla::net::SpdyPushCache *aSpdyPushCache) override; \
  NS_IMETHOD GetUserAgentOverride(nsACString & aUserAgentOverride) override; \
  NS_IMETHOD SetUserAgentOverride(const nsACString & aUserAgentOverride) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIREQUESTCONTEXT \
  NS_METHOD GetID(nsID *aID); \
  NS_METHOD GetBlockingTransactionCount(uint32_t *aBlockingTransactionCount); \
  NS_METHOD AddBlockingTransaction(void); \
  NS_METHOD RemoveBlockingTransaction(uint32_t *_retval); \
  NS_METHOD GetSpdyPushCache(mozilla::net::SpdyPushCache **aSpdyPushCache); \
  NS_METHOD SetSpdyPushCache(mozilla::net::SpdyPushCache *aSpdyPushCache); \
  NS_METHOD GetUserAgentOverride(nsACString & aUserAgentOverride); \
  NS_METHOD SetUserAgentOverride(const nsACString & aUserAgentOverride); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIREQUESTCONTEXT(_to) \
  NS_IMETHOD GetID(nsID *aID) override { return _to GetID(aID); } \
  NS_IMETHOD GetBlockingTransactionCount(uint32_t *aBlockingTransactionCount) override { return _to GetBlockingTransactionCount(aBlockingTransactionCount); } \
  NS_IMETHOD AddBlockingTransaction(void) override { return _to AddBlockingTransaction(); } \
  NS_IMETHOD RemoveBlockingTransaction(uint32_t *_retval) override { return _to RemoveBlockingTransaction(_retval); } \
  NS_IMETHOD GetSpdyPushCache(mozilla::net::SpdyPushCache **aSpdyPushCache) override { return _to GetSpdyPushCache(aSpdyPushCache); } \
  NS_IMETHOD SetSpdyPushCache(mozilla::net::SpdyPushCache *aSpdyPushCache) override { return _to SetSpdyPushCache(aSpdyPushCache); } \
  NS_IMETHOD GetUserAgentOverride(nsACString & aUserAgentOverride) override { return _to GetUserAgentOverride(aUserAgentOverride); } \
  NS_IMETHOD SetUserAgentOverride(const nsACString & aUserAgentOverride) override { return _to SetUserAgentOverride(aUserAgentOverride); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIREQUESTCONTEXT(_to) \
  NS_IMETHOD GetID(nsID *aID) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetID(aID); } \
  NS_IMETHOD GetBlockingTransactionCount(uint32_t *aBlockingTransactionCount) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetBlockingTransactionCount(aBlockingTransactionCount); } \
  NS_IMETHOD AddBlockingTransaction(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->AddBlockingTransaction(); } \
  NS_IMETHOD RemoveBlockingTransaction(uint32_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RemoveBlockingTransaction(_retval); } \
  NS_IMETHOD GetSpdyPushCache(mozilla::net::SpdyPushCache **aSpdyPushCache) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetSpdyPushCache(aSpdyPushCache); } \
  NS_IMETHOD SetSpdyPushCache(mozilla::net::SpdyPushCache *aSpdyPushCache) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetSpdyPushCache(aSpdyPushCache); } \
  NS_IMETHOD GetUserAgentOverride(nsACString & aUserAgentOverride) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetUserAgentOverride(aUserAgentOverride); } \
  NS_IMETHOD SetUserAgentOverride(const nsACString & aUserAgentOverride) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetUserAgentOverride(aUserAgentOverride); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsRequestContext : public nsIRequestContext
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIREQUESTCONTEXT

  nsRequestContext();

private:
  ~nsRequestContext();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsRequestContext, nsIRequestContext)

nsRequestContext::nsRequestContext()
{
  /* member initializers and constructor code */
}

nsRequestContext::~nsRequestContext()
{
  /* destructor code */
}

/* [noscript] readonly attribute nsID ID; */
NS_IMETHODIMP nsRequestContext::GetID(nsID *aID)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute unsigned long blockingTransactionCount; */
NS_IMETHODIMP nsRequestContext::GetBlockingTransactionCount(uint32_t *aBlockingTransactionCount)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void addBlockingTransaction (); */
NS_IMETHODIMP nsRequestContext::AddBlockingTransaction()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* unsigned long removeBlockingTransaction (); */
NS_IMETHODIMP nsRequestContext::RemoveBlockingTransaction(uint32_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] attribute SpdyPushCachePtr spdyPushCache; */
NS_IMETHODIMP nsRequestContext::GetSpdyPushCache(mozilla::net::SpdyPushCache **aSpdyPushCache)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsRequestContext::SetSpdyPushCache(mozilla::net::SpdyPushCache *aSpdyPushCache)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] attribute ACString userAgentOverride; */
NS_IMETHODIMP nsRequestContext::GetUserAgentOverride(nsACString & aUserAgentOverride)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsRequestContext::SetUserAgentOverride(const nsACString & aUserAgentOverride)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIRequestContextService */
#define NS_IREQUESTCONTEXTSERVICE_IID_STR "7fcbf4da-d828-4acc-b144-e5435198f727"

#define NS_IREQUESTCONTEXTSERVICE_IID \
  {0x7fcbf4da, 0xd828, 0x4acc, \
    { 0xb1, 0x44, 0xe5, 0x43, 0x51, 0x98, 0xf7, 0x27 }}

class NS_NO_VTABLE nsIRequestContextService : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IREQUESTCONTEXTSERVICE_IID)

  /* nsIRequestContext getRequestContext (in nsIDRef id); */
  NS_IMETHOD GetRequestContext(const nsID & id, nsIRequestContext * *_retval) = 0;

  /* nsID newRequestContextID (); */
  NS_IMETHOD NewRequestContextID(nsID *_retval) = 0;

  /* void removeRequestContext (in nsIDRef id); */
  NS_IMETHOD RemoveRequestContext(const nsID & id) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIRequestContextService, NS_IREQUESTCONTEXTSERVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIREQUESTCONTEXTSERVICE \
  NS_IMETHOD GetRequestContext(const nsID & id, nsIRequestContext * *_retval) override; \
  NS_IMETHOD NewRequestContextID(nsID *_retval) override; \
  NS_IMETHOD RemoveRequestContext(const nsID & id) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIREQUESTCONTEXTSERVICE \
  NS_METHOD GetRequestContext(const nsID & id, nsIRequestContext * *_retval); \
  NS_METHOD NewRequestContextID(nsID *_retval); \
  NS_METHOD RemoveRequestContext(const nsID & id); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIREQUESTCONTEXTSERVICE(_to) \
  NS_IMETHOD GetRequestContext(const nsID & id, nsIRequestContext * *_retval) override { return _to GetRequestContext(id, _retval); } \
  NS_IMETHOD NewRequestContextID(nsID *_retval) override { return _to NewRequestContextID(_retval); } \
  NS_IMETHOD RemoveRequestContext(const nsID & id) override { return _to RemoveRequestContext(id); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIREQUESTCONTEXTSERVICE(_to) \
  NS_IMETHOD GetRequestContext(const nsID & id, nsIRequestContext * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetRequestContext(id, _retval); } \
  NS_IMETHOD NewRequestContextID(nsID *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NewRequestContextID(_retval); } \
  NS_IMETHOD RemoveRequestContext(const nsID & id) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RemoveRequestContext(id); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsRequestContextService : public nsIRequestContextService
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIREQUESTCONTEXTSERVICE

  nsRequestContextService();

private:
  ~nsRequestContextService();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsRequestContextService, nsIRequestContextService)

nsRequestContextService::nsRequestContextService()
{
  /* member initializers and constructor code */
}

nsRequestContextService::~nsRequestContextService()
{
  /* destructor code */
}

/* nsIRequestContext getRequestContext (in nsIDRef id); */
NS_IMETHODIMP nsRequestContextService::GetRequestContext(const nsID & id, nsIRequestContext * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsID newRequestContextID (); */
NS_IMETHODIMP nsRequestContextService::NewRequestContextID(nsID *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void removeRequestContext (in nsIDRef id); */
NS_IMETHODIMP nsRequestContextService::RemoveRequestContext(const nsID & id)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIRequestContext_h__ */
