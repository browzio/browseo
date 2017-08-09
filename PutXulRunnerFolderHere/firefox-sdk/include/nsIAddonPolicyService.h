/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIAddonPolicyService.idl
 */

#ifndef __gen_nsIAddonPolicyService_h__
#define __gen_nsIAddonPolicyService_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

#ifndef __gen_nsIURI_h__
#include "nsIURI.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIAddonPolicyService */
#define NS_IADDONPOLICYSERVICE_IID_STR "8a034ef9-9d14-4c5d-8319-06c1ab574baa"

#define NS_IADDONPOLICYSERVICE_IID \
  {0x8a034ef9, 0x9d14, 0x4c5d, \
    { 0x83, 0x19, 0x06, 0xc1, 0xab, 0x57, 0x4b, 0xaa }}

class NS_NO_VTABLE nsIAddonPolicyService : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IADDONPOLICYSERVICE_IID)

  /* readonly attribute AString baseCSP; */
  NS_IMETHOD GetBaseCSP(nsAString & aBaseCSP) = 0;

  /* readonly attribute AString defaultCSP; */
  NS_IMETHOD GetDefaultCSP(nsAString & aDefaultCSP) = 0;

  /* AString getAddonCSP (in AString aAddonId); */
  NS_IMETHOD GetAddonCSP(const nsAString & aAddonId, nsAString & _retval) = 0;

  /* ACString getGeneratedBackgroundPageUrl (in ACString aAddonId); */
  NS_IMETHOD GetGeneratedBackgroundPageUrl(const nsACString & aAddonId, nsACString & _retval) = 0;

  /* boolean addonHasPermission (in AString aAddonId, in AString aPerm); */
  NS_IMETHOD AddonHasPermission(const nsAString & aAddonId, const nsAString & aPerm, bool *_retval) = 0;

  /* boolean addonMayLoadURI (in AString aAddonId, in nsIURI aURI); */
  NS_IMETHOD AddonMayLoadURI(const nsAString & aAddonId, nsIURI *aURI, bool *_retval) = 0;

  /* boolean extensionURILoadableByAnyone (in nsIURI aURI); */
  NS_IMETHOD ExtensionURILoadableByAnyone(nsIURI *aURI, bool *_retval) = 0;

  /* AString extensionURIToAddonId (in nsIURI aURI); */
  NS_IMETHOD ExtensionURIToAddonId(nsIURI *aURI, nsAString & _retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAddonPolicyService, NS_IADDONPOLICYSERVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIADDONPOLICYSERVICE \
  NS_IMETHOD GetBaseCSP(nsAString & aBaseCSP) override; \
  NS_IMETHOD GetDefaultCSP(nsAString & aDefaultCSP) override; \
  NS_IMETHOD GetAddonCSP(const nsAString & aAddonId, nsAString & _retval) override; \
  NS_IMETHOD GetGeneratedBackgroundPageUrl(const nsACString & aAddonId, nsACString & _retval) override; \
  NS_IMETHOD AddonHasPermission(const nsAString & aAddonId, const nsAString & aPerm, bool *_retval) override; \
  NS_IMETHOD AddonMayLoadURI(const nsAString & aAddonId, nsIURI *aURI, bool *_retval) override; \
  NS_IMETHOD ExtensionURILoadableByAnyone(nsIURI *aURI, bool *_retval) override; \
  NS_IMETHOD ExtensionURIToAddonId(nsIURI *aURI, nsAString & _retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIADDONPOLICYSERVICE \
  NS_METHOD GetBaseCSP(nsAString & aBaseCSP); \
  NS_METHOD GetDefaultCSP(nsAString & aDefaultCSP); \
  NS_METHOD GetAddonCSP(const nsAString & aAddonId, nsAString & _retval); \
  NS_METHOD GetGeneratedBackgroundPageUrl(const nsACString & aAddonId, nsACString & _retval); \
  NS_METHOD AddonHasPermission(const nsAString & aAddonId, const nsAString & aPerm, bool *_retval); \
  NS_METHOD AddonMayLoadURI(const nsAString & aAddonId, nsIURI *aURI, bool *_retval); \
  NS_METHOD ExtensionURILoadableByAnyone(nsIURI *aURI, bool *_retval); \
  NS_METHOD ExtensionURIToAddonId(nsIURI *aURI, nsAString & _retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIADDONPOLICYSERVICE(_to) \
  NS_IMETHOD GetBaseCSP(nsAString & aBaseCSP) override { return _to GetBaseCSP(aBaseCSP); } \
  NS_IMETHOD GetDefaultCSP(nsAString & aDefaultCSP) override { return _to GetDefaultCSP(aDefaultCSP); } \
  NS_IMETHOD GetAddonCSP(const nsAString & aAddonId, nsAString & _retval) override { return _to GetAddonCSP(aAddonId, _retval); } \
  NS_IMETHOD GetGeneratedBackgroundPageUrl(const nsACString & aAddonId, nsACString & _retval) override { return _to GetGeneratedBackgroundPageUrl(aAddonId, _retval); } \
  NS_IMETHOD AddonHasPermission(const nsAString & aAddonId, const nsAString & aPerm, bool *_retval) override { return _to AddonHasPermission(aAddonId, aPerm, _retval); } \
  NS_IMETHOD AddonMayLoadURI(const nsAString & aAddonId, nsIURI *aURI, bool *_retval) override { return _to AddonMayLoadURI(aAddonId, aURI, _retval); } \
  NS_IMETHOD ExtensionURILoadableByAnyone(nsIURI *aURI, bool *_retval) override { return _to ExtensionURILoadableByAnyone(aURI, _retval); } \
  NS_IMETHOD ExtensionURIToAddonId(nsIURI *aURI, nsAString & _retval) override { return _to ExtensionURIToAddonId(aURI, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIADDONPOLICYSERVICE(_to) \
  NS_IMETHOD GetBaseCSP(nsAString & aBaseCSP) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetBaseCSP(aBaseCSP); } \
  NS_IMETHOD GetDefaultCSP(nsAString & aDefaultCSP) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDefaultCSP(aDefaultCSP); } \
  NS_IMETHOD GetAddonCSP(const nsAString & aAddonId, nsAString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAddonCSP(aAddonId, _retval); } \
  NS_IMETHOD GetGeneratedBackgroundPageUrl(const nsACString & aAddonId, nsACString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetGeneratedBackgroundPageUrl(aAddonId, _retval); } \
  NS_IMETHOD AddonHasPermission(const nsAString & aAddonId, const nsAString & aPerm, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->AddonHasPermission(aAddonId, aPerm, _retval); } \
  NS_IMETHOD AddonMayLoadURI(const nsAString & aAddonId, nsIURI *aURI, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->AddonMayLoadURI(aAddonId, aURI, _retval); } \
  NS_IMETHOD ExtensionURILoadableByAnyone(nsIURI *aURI, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ExtensionURILoadableByAnyone(aURI, _retval); } \
  NS_IMETHOD ExtensionURIToAddonId(nsIURI *aURI, nsAString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ExtensionURIToAddonId(aURI, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAddonPolicyService : public nsIAddonPolicyService
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIADDONPOLICYSERVICE

  nsAddonPolicyService();

private:
  ~nsAddonPolicyService();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAddonPolicyService, nsIAddonPolicyService)

nsAddonPolicyService::nsAddonPolicyService()
{
  /* member initializers and constructor code */
}

nsAddonPolicyService::~nsAddonPolicyService()
{
  /* destructor code */
}

/* readonly attribute AString baseCSP; */
NS_IMETHODIMP nsAddonPolicyService::GetBaseCSP(nsAString & aBaseCSP)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString defaultCSP; */
NS_IMETHODIMP nsAddonPolicyService::GetDefaultCSP(nsAString & aDefaultCSP)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* AString getAddonCSP (in AString aAddonId); */
NS_IMETHODIMP nsAddonPolicyService::GetAddonCSP(const nsAString & aAddonId, nsAString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* ACString getGeneratedBackgroundPageUrl (in ACString aAddonId); */
NS_IMETHODIMP nsAddonPolicyService::GetGeneratedBackgroundPageUrl(const nsACString & aAddonId, nsACString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean addonHasPermission (in AString aAddonId, in AString aPerm); */
NS_IMETHODIMP nsAddonPolicyService::AddonHasPermission(const nsAString & aAddonId, const nsAString & aPerm, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean addonMayLoadURI (in AString aAddonId, in nsIURI aURI); */
NS_IMETHODIMP nsAddonPolicyService::AddonMayLoadURI(const nsAString & aAddonId, nsIURI *aURI, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean extensionURILoadableByAnyone (in nsIURI aURI); */
NS_IMETHODIMP nsAddonPolicyService::ExtensionURILoadableByAnyone(nsIURI *aURI, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* AString extensionURIToAddonId (in nsIURI aURI); */
NS_IMETHODIMP nsAddonPolicyService::ExtensionURIToAddonId(nsIURI *aURI, nsAString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIAddonContentPolicy */
#define NS_IADDONCONTENTPOLICY_IID_STR "7a4fe60b-9131-45f5-83f3-dc63b5d71a5d"

#define NS_IADDONCONTENTPOLICY_IID \
  {0x7a4fe60b, 0x9131, 0x45f5, \
    { 0x83, 0xf3, 0xdc, 0x63, 0xb5, 0xd7, 0x1a, 0x5d }}

class NS_NO_VTABLE nsIAddonContentPolicy : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IADDONCONTENTPOLICY_IID)

  /* AString validateAddonCSP (in AString aPolicyString); */
  NS_IMETHOD ValidateAddonCSP(const nsAString & aPolicyString, nsAString & _retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAddonContentPolicy, NS_IADDONCONTENTPOLICY_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIADDONCONTENTPOLICY \
  NS_IMETHOD ValidateAddonCSP(const nsAString & aPolicyString, nsAString & _retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIADDONCONTENTPOLICY \
  NS_METHOD ValidateAddonCSP(const nsAString & aPolicyString, nsAString & _retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIADDONCONTENTPOLICY(_to) \
  NS_IMETHOD ValidateAddonCSP(const nsAString & aPolicyString, nsAString & _retval) override { return _to ValidateAddonCSP(aPolicyString, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIADDONCONTENTPOLICY(_to) \
  NS_IMETHOD ValidateAddonCSP(const nsAString & aPolicyString, nsAString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ValidateAddonCSP(aPolicyString, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAddonContentPolicy : public nsIAddonContentPolicy
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIADDONCONTENTPOLICY

  nsAddonContentPolicy();

private:
  ~nsAddonContentPolicy();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAddonContentPolicy, nsIAddonContentPolicy)

nsAddonContentPolicy::nsAddonContentPolicy()
{
  /* member initializers and constructor code */
}

nsAddonContentPolicy::~nsAddonContentPolicy()
{
  /* destructor code */
}

/* AString validateAddonCSP (in AString aPolicyString); */
NS_IMETHODIMP nsAddonContentPolicy::ValidateAddonCSP(const nsAString & aPolicyString, nsAString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIAddonPolicyService_h__ */
