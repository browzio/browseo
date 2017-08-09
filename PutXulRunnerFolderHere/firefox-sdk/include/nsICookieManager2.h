/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsICookieManager2.idl
 */

#ifndef __gen_nsICookieManager2_h__
#define __gen_nsICookieManager2_h__


#ifndef __gen_nsICookieManager_h__
#include "nsICookieManager.h"
#endif

#include "js/Value.h"

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsICookie2; /* forward declaration */

class nsIFile; /* forward declaration */


/* starting interface:    nsICookieManager2 */
#define NS_ICOOKIEMANAGER2_IID_STR "daf0caa7-b431-4b4d-ba51-08c179bb9dfe"

#define NS_ICOOKIEMANAGER2_IID \
  {0xdaf0caa7, 0xb431, 0x4b4d, \
    { 0xba, 0x51, 0x08, 0xc1, 0x79, 0xbb, 0x9d, 0xfe }}

class NS_NO_VTABLE nsICookieManager2 : public nsICookieManager {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ICOOKIEMANAGER2_IID)

  /* [implicit_jscontext,optional_argc] void add (in AUTF8String aHost, in AUTF8String aPath, in ACString aName, in ACString aValue, in boolean aIsSecure, in boolean aIsHttpOnly, in boolean aIsSession, in int64_t aExpiry, [optional] in jsval aOriginAttributes); */
  NS_IMETHOD Add(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc) = 0;

  /* [notxpcom] nsresult addNative (in AUTF8String aHost, in AUTF8String aPath, in ACString aName, in ACString aValue, in boolean aIsSecure, in boolean aIsHttpOnly, in boolean aIsSession, in int64_t aExpiry, in NeckoOriginAttributesPtr aOriginAttributes); */
  NS_IMETHOD_(nsresult) AddNative(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, mozilla::NeckoOriginAttributes *aOriginAttributes) = 0;

  /* [implicit_jscontext,optional_argc] boolean cookieExists (in nsICookie2 aCookie, [optional] in jsval aOriginAttributes); */
  NS_IMETHOD CookieExists(nsICookie2 *aCookie, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, bool *_retval) = 0;

  /* [notxpcom] nsresult cookieExistsNative (in nsICookie2 aCookie, in NeckoOriginAttributesPtr aOriginAttributes, out boolean aExists); */
  NS_IMETHOD_(nsresult) CookieExistsNative(nsICookie2 *aCookie, mozilla::NeckoOriginAttributes *aOriginAttributes, bool *aExists) = 0;

  /* unsigned long countCookiesFromHost (in AUTF8String aHost); */
  NS_IMETHOD CountCookiesFromHost(const nsACString & aHost, uint32_t *_retval) = 0;

  /* [implicit_jscontext,optional_argc] nsISimpleEnumerator getCookiesFromHost (in AUTF8String aHost, [optional] in jsval aOriginAttributes); */
  NS_IMETHOD GetCookiesFromHost(const nsACString & aHost, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, nsISimpleEnumerator * *_retval) = 0;

  /* void importCookies (in nsIFile aCookieFile); */
  NS_IMETHOD ImportCookies(nsIFile *aCookieFile) = 0;

  /* nsISimpleEnumerator getCookiesWithOriginAttributes (in DOMString aPattern, [optional] in AUTF8String aHost); */
  NS_IMETHOD GetCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost, nsISimpleEnumerator * *_retval) = 0;

  /* void removeCookiesWithOriginAttributes (in DOMString aPattern, [optional] in AUTF8String aHost); */
  NS_IMETHOD RemoveCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsICookieManager2, NS_ICOOKIEMANAGER2_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSICOOKIEMANAGER2 \
  NS_IMETHOD Add(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc) override; \
  NS_IMETHOD_(nsresult) AddNative(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, mozilla::NeckoOriginAttributes *aOriginAttributes) override; \
  NS_IMETHOD CookieExists(nsICookie2 *aCookie, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, bool *_retval) override; \
  NS_IMETHOD_(nsresult) CookieExistsNative(nsICookie2 *aCookie, mozilla::NeckoOriginAttributes *aOriginAttributes, bool *aExists) override; \
  NS_IMETHOD CountCookiesFromHost(const nsACString & aHost, uint32_t *_retval) override; \
  NS_IMETHOD GetCookiesFromHost(const nsACString & aHost, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, nsISimpleEnumerator * *_retval) override; \
  NS_IMETHOD ImportCookies(nsIFile *aCookieFile) override; \
  NS_IMETHOD GetCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost, nsISimpleEnumerator * *_retval) override; \
  NS_IMETHOD RemoveCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSICOOKIEMANAGER2 \
  NS_METHOD Add(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc); \
  NS_METHOD_(nsresult) AddNative(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, mozilla::NeckoOriginAttributes *aOriginAttributes); \
  NS_METHOD CookieExists(nsICookie2 *aCookie, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, bool *_retval); \
  NS_METHOD_(nsresult) CookieExistsNative(nsICookie2 *aCookie, mozilla::NeckoOriginAttributes *aOriginAttributes, bool *aExists); \
  NS_METHOD CountCookiesFromHost(const nsACString & aHost, uint32_t *_retval); \
  NS_METHOD GetCookiesFromHost(const nsACString & aHost, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, nsISimpleEnumerator * *_retval); \
  NS_METHOD ImportCookies(nsIFile *aCookieFile); \
  NS_METHOD GetCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost, nsISimpleEnumerator * *_retval); \
  NS_METHOD RemoveCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSICOOKIEMANAGER2(_to) \
  NS_IMETHOD Add(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc) override { return _to Add(aHost, aPath, aName, aValue, aIsSecure, aIsHttpOnly, aIsSession, aExpiry, aOriginAttributes, cx, _argc); } \
  NS_IMETHOD_(nsresult) AddNative(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, mozilla::NeckoOriginAttributes *aOriginAttributes) override { return _to AddNative(aHost, aPath, aName, aValue, aIsSecure, aIsHttpOnly, aIsSession, aExpiry, aOriginAttributes); } \
  NS_IMETHOD CookieExists(nsICookie2 *aCookie, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, bool *_retval) override { return _to CookieExists(aCookie, aOriginAttributes, cx, _argc, _retval); } \
  NS_IMETHOD_(nsresult) CookieExistsNative(nsICookie2 *aCookie, mozilla::NeckoOriginAttributes *aOriginAttributes, bool *aExists) override { return _to CookieExistsNative(aCookie, aOriginAttributes, aExists); } \
  NS_IMETHOD CountCookiesFromHost(const nsACString & aHost, uint32_t *_retval) override { return _to CountCookiesFromHost(aHost, _retval); } \
  NS_IMETHOD GetCookiesFromHost(const nsACString & aHost, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, nsISimpleEnumerator * *_retval) override { return _to GetCookiesFromHost(aHost, aOriginAttributes, cx, _argc, _retval); } \
  NS_IMETHOD ImportCookies(nsIFile *aCookieFile) override { return _to ImportCookies(aCookieFile); } \
  NS_IMETHOD GetCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost, nsISimpleEnumerator * *_retval) override { return _to GetCookiesWithOriginAttributes(aPattern, aHost, _retval); } \
  NS_IMETHOD RemoveCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost) override { return _to RemoveCookiesWithOriginAttributes(aPattern, aHost); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSICOOKIEMANAGER2(_to) \
  NS_IMETHOD Add(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Add(aHost, aPath, aName, aValue, aIsSecure, aIsHttpOnly, aIsSession, aExpiry, aOriginAttributes, cx, _argc); } \
  NS_IMETHOD_(nsresult) AddNative(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, mozilla::NeckoOriginAttributes *aOriginAttributes) override; \
  NS_IMETHOD CookieExists(nsICookie2 *aCookie, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CookieExists(aCookie, aOriginAttributes, cx, _argc, _retval); } \
  NS_IMETHOD_(nsresult) CookieExistsNative(nsICookie2 *aCookie, mozilla::NeckoOriginAttributes *aOriginAttributes, bool *aExists) override; \
  NS_IMETHOD CountCookiesFromHost(const nsACString & aHost, uint32_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CountCookiesFromHost(aHost, _retval); } \
  NS_IMETHOD GetCookiesFromHost(const nsACString & aHost, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, nsISimpleEnumerator * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCookiesFromHost(aHost, aOriginAttributes, cx, _argc, _retval); } \
  NS_IMETHOD ImportCookies(nsIFile *aCookieFile) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ImportCookies(aCookieFile); } \
  NS_IMETHOD GetCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost, nsISimpleEnumerator * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCookiesWithOriginAttributes(aPattern, aHost, _retval); } \
  NS_IMETHOD RemoveCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RemoveCookiesWithOriginAttributes(aPattern, aHost); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsCookieManager2 : public nsICookieManager2
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSICOOKIEMANAGER2

  nsCookieManager2();

private:
  ~nsCookieManager2();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsCookieManager2, nsICookieManager2)

nsCookieManager2::nsCookieManager2()
{
  /* member initializers and constructor code */
}

nsCookieManager2::~nsCookieManager2()
{
  /* destructor code */
}

/* [implicit_jscontext,optional_argc] void add (in AUTF8String aHost, in AUTF8String aPath, in ACString aName, in ACString aValue, in boolean aIsSecure, in boolean aIsHttpOnly, in boolean aIsSession, in int64_t aExpiry, [optional] in jsval aOriginAttributes); */
NS_IMETHODIMP nsCookieManager2::Add(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [notxpcom] nsresult addNative (in AUTF8String aHost, in AUTF8String aPath, in ACString aName, in ACString aValue, in boolean aIsSecure, in boolean aIsHttpOnly, in boolean aIsSession, in int64_t aExpiry, in NeckoOriginAttributesPtr aOriginAttributes); */
NS_IMETHODIMP_(nsresult) nsCookieManager2::AddNative(const nsACString & aHost, const nsACString & aPath, const nsACString & aName, const nsACString & aValue, bool aIsSecure, bool aIsHttpOnly, bool aIsSession, int64_t aExpiry, mozilla::NeckoOriginAttributes *aOriginAttributes)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext,optional_argc] boolean cookieExists (in nsICookie2 aCookie, [optional] in jsval aOriginAttributes); */
NS_IMETHODIMP nsCookieManager2::CookieExists(nsICookie2 *aCookie, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [notxpcom] nsresult cookieExistsNative (in nsICookie2 aCookie, in NeckoOriginAttributesPtr aOriginAttributes, out boolean aExists); */
NS_IMETHODIMP_(nsresult) nsCookieManager2::CookieExistsNative(nsICookie2 *aCookie, mozilla::NeckoOriginAttributes *aOriginAttributes, bool *aExists)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* unsigned long countCookiesFromHost (in AUTF8String aHost); */
NS_IMETHODIMP nsCookieManager2::CountCookiesFromHost(const nsACString & aHost, uint32_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext,optional_argc] nsISimpleEnumerator getCookiesFromHost (in AUTF8String aHost, [optional] in jsval aOriginAttributes); */
NS_IMETHODIMP nsCookieManager2::GetCookiesFromHost(const nsACString & aHost, JS::HandleValue aOriginAttributes, JSContext* cx, uint8_t _argc, nsISimpleEnumerator * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void importCookies (in nsIFile aCookieFile); */
NS_IMETHODIMP nsCookieManager2::ImportCookies(nsIFile *aCookieFile)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsISimpleEnumerator getCookiesWithOriginAttributes (in DOMString aPattern, [optional] in AUTF8String aHost); */
NS_IMETHODIMP nsCookieManager2::GetCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost, nsISimpleEnumerator * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void removeCookiesWithOriginAttributes (in DOMString aPattern, [optional] in AUTF8String aHost); */
NS_IMETHODIMP nsCookieManager2::RemoveCookiesWithOriginAttributes(const nsAString & aPattern, const nsACString & aHost)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsICookieManager2_h__ */
