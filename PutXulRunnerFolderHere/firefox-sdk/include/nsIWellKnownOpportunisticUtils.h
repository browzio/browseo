/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIWellKnownOpportunisticUtils.idl
 */

#ifndef __gen_nsIWellKnownOpportunisticUtils_h__
#define __gen_nsIWellKnownOpportunisticUtils_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
#define NS_WELLKNOWNOPPORTUNISTICUTILS_CONTRACTID "@mozilla.org/network/well-known-opportunistic-utils;1"

/* starting interface:    nsIWellKnownOpportunisticUtils */
#define NS_IWELLKNOWNOPPORTUNISTICUTILS_IID_STR "b4f96c89-5238-450c-8bda-e12c26f1d150"

#define NS_IWELLKNOWNOPPORTUNISTICUTILS_IID \
  {0xb4f96c89, 0x5238, 0x450c, \
    { 0x8b, 0xda, 0xe1, 0x2c, 0x26, 0xf1, 0xd1, 0x50 }}

class NS_NO_VTABLE nsIWellKnownOpportunisticUtils : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IWELLKNOWNOPPORTUNISTICUTILS_IID)

  /* void verify (in ACString aJSON, in ACString aOrigin, in long aAlternatePort); */
  NS_IMETHOD Verify(const nsACString & aJSON, const nsACString & aOrigin, int32_t aAlternatePort) = 0;

  /* readonly attribute bool valid; */
  NS_IMETHOD GetValid(bool *aValid) = 0;

  /* readonly attribute bool mixed; */
  NS_IMETHOD GetMixed(bool *aMixed) = 0;

  /* readonly attribute long lifetime; */
  NS_IMETHOD GetLifetime(int32_t *aLifetime) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIWellKnownOpportunisticUtils, NS_IWELLKNOWNOPPORTUNISTICUTILS_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIWELLKNOWNOPPORTUNISTICUTILS \
  NS_IMETHOD Verify(const nsACString & aJSON, const nsACString & aOrigin, int32_t aAlternatePort) override; \
  NS_IMETHOD GetValid(bool *aValid) override; \
  NS_IMETHOD GetMixed(bool *aMixed) override; \
  NS_IMETHOD GetLifetime(int32_t *aLifetime) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIWELLKNOWNOPPORTUNISTICUTILS \
  NS_METHOD Verify(const nsACString & aJSON, const nsACString & aOrigin, int32_t aAlternatePort); \
  NS_METHOD GetValid(bool *aValid); \
  NS_METHOD GetMixed(bool *aMixed); \
  NS_METHOD GetLifetime(int32_t *aLifetime); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIWELLKNOWNOPPORTUNISTICUTILS(_to) \
  NS_IMETHOD Verify(const nsACString & aJSON, const nsACString & aOrigin, int32_t aAlternatePort) override { return _to Verify(aJSON, aOrigin, aAlternatePort); } \
  NS_IMETHOD GetValid(bool *aValid) override { return _to GetValid(aValid); } \
  NS_IMETHOD GetMixed(bool *aMixed) override { return _to GetMixed(aMixed); } \
  NS_IMETHOD GetLifetime(int32_t *aLifetime) override { return _to GetLifetime(aLifetime); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIWELLKNOWNOPPORTUNISTICUTILS(_to) \
  NS_IMETHOD Verify(const nsACString & aJSON, const nsACString & aOrigin, int32_t aAlternatePort) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Verify(aJSON, aOrigin, aAlternatePort); } \
  NS_IMETHOD GetValid(bool *aValid) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetValid(aValid); } \
  NS_IMETHOD GetMixed(bool *aMixed) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetMixed(aMixed); } \
  NS_IMETHOD GetLifetime(int32_t *aLifetime) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLifetime(aLifetime); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsWellKnownOpportunisticUtils : public nsIWellKnownOpportunisticUtils
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIWELLKNOWNOPPORTUNISTICUTILS

  nsWellKnownOpportunisticUtils();

private:
  ~nsWellKnownOpportunisticUtils();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsWellKnownOpportunisticUtils, nsIWellKnownOpportunisticUtils)

nsWellKnownOpportunisticUtils::nsWellKnownOpportunisticUtils()
{
  /* member initializers and constructor code */
}

nsWellKnownOpportunisticUtils::~nsWellKnownOpportunisticUtils()
{
  /* destructor code */
}

/* void verify (in ACString aJSON, in ACString aOrigin, in long aAlternatePort); */
NS_IMETHODIMP nsWellKnownOpportunisticUtils::Verify(const nsACString & aJSON, const nsACString & aOrigin, int32_t aAlternatePort)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute bool valid; */
NS_IMETHODIMP nsWellKnownOpportunisticUtils::GetValid(bool *aValid)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute bool mixed; */
NS_IMETHODIMP nsWellKnownOpportunisticUtils::GetMixed(bool *aMixed)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute long lifetime; */
NS_IMETHODIMP nsWellKnownOpportunisticUtils::GetLifetime(int32_t *aLifetime)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIWellKnownOpportunisticUtils_h__ */
