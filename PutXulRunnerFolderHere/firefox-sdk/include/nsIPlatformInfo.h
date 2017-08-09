/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPlatformInfo.idl
 */

#ifndef __gen_nsIPlatformInfo_h__
#define __gen_nsIPlatformInfo_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIPlatformInfo */
#define NS_IPLATFORMINFO_IID_STR "ab6650cf-0806-4aea-b8f2-40fdae74f1cc"

#define NS_IPLATFORMINFO_IID \
  {0xab6650cf, 0x0806, 0x4aea, \
    { 0xb8, 0xf2, 0x40, 0xfd, 0xae, 0x74, 0xf1, 0xcc }}

class NS_NO_VTABLE nsIPlatformInfo : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPLATFORMINFO_IID)

  /* readonly attribute ACString platformVersion; */
  NS_IMETHOD GetPlatformVersion(nsACString & aPlatformVersion) = 0;

  /* readonly attribute ACString platformBuildID; */
  NS_IMETHOD GetPlatformBuildID(nsACString & aPlatformBuildID) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPlatformInfo, NS_IPLATFORMINFO_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPLATFORMINFO \
  NS_IMETHOD GetPlatformVersion(nsACString & aPlatformVersion) override; \
  NS_IMETHOD GetPlatformBuildID(nsACString & aPlatformBuildID) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPLATFORMINFO \
  NS_METHOD GetPlatformVersion(nsACString & aPlatformVersion); \
  NS_METHOD GetPlatformBuildID(nsACString & aPlatformBuildID); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPLATFORMINFO(_to) \
  NS_IMETHOD GetPlatformVersion(nsACString & aPlatformVersion) override { return _to GetPlatformVersion(aPlatformVersion); } \
  NS_IMETHOD GetPlatformBuildID(nsACString & aPlatformBuildID) override { return _to GetPlatformBuildID(aPlatformBuildID); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPLATFORMINFO(_to) \
  NS_IMETHOD GetPlatformVersion(nsACString & aPlatformVersion) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPlatformVersion(aPlatformVersion); } \
  NS_IMETHOD GetPlatformBuildID(nsACString & aPlatformBuildID) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPlatformBuildID(aPlatformBuildID); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPlatformInfo : public nsIPlatformInfo
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPLATFORMINFO

  nsPlatformInfo();

private:
  ~nsPlatformInfo();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPlatformInfo, nsIPlatformInfo)

nsPlatformInfo::nsPlatformInfo()
{
  /* member initializers and constructor code */
}

nsPlatformInfo::~nsPlatformInfo()
{
  /* destructor code */
}

/* readonly attribute ACString platformVersion; */
NS_IMETHODIMP nsPlatformInfo::GetPlatformVersion(nsACString & aPlatformVersion)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute ACString platformBuildID; */
NS_IMETHODIMP nsPlatformInfo::GetPlatformBuildID(nsACString & aPlatformBuildID)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPlatformInfo_h__ */
