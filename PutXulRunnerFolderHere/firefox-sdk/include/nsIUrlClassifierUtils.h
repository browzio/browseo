/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIUrlClassifierUtils.idl
 */

#ifndef __gen_nsIUrlClassifierUtils_h__
#define __gen_nsIUrlClassifierUtils_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIURI; /* forward declaration */


/* starting interface:    nsIUrlClassifierUtils */
#define NS_IURLCLASSIFIERUTILS_IID_STR "e4f0e59c-b922-48b0-a7b6-1735c1f96fed"

#define NS_IURLCLASSIFIERUTILS_IID \
  {0xe4f0e59c, 0xb922, 0x48b0, \
    { 0xa7, 0xb6, 0x17, 0x35, 0xc1, 0xf9, 0x6f, 0xed }}

class NS_NO_VTABLE nsIUrlClassifierUtils : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IURLCLASSIFIERUTILS_IID)

  /* ACString getKeyForURI (in nsIURI uri); */
  NS_IMETHOD GetKeyForURI(nsIURI *uri, nsACString & _retval) = 0;

  /* ACString getProvider (in ACString tableName); */
  NS_IMETHOD GetProvider(const nsACString & tableName, nsACString & _retval) = 0;

  /* ACString getProtocolVersion (in ACString provider); */
  NS_IMETHOD GetProtocolVersion(const nsACString & provider, nsACString & _retval) = 0;

  /* ACString convertThreatTypeToListNames (in uint32_t threatType); */
  NS_IMETHOD ConvertThreatTypeToListNames(uint32_t threatType, nsACString & _retval) = 0;

  /* uint32_t convertListNameToThreatType (in ACString listName); */
  NS_IMETHOD ConvertListNameToThreatType(const nsACString & listName, uint32_t *_retval) = 0;

  /* ACString makeUpdateRequestV4 ([array, size_is (aCount)] in string aListNames, [array, size_is (aCount)] in string aStatesBase64, in uint32_t aCount); */
  NS_IMETHOD MakeUpdateRequestV4(const char * *aListNames, const char * *aStatesBase64, uint32_t aCount, nsACString & _retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIUrlClassifierUtils, NS_IURLCLASSIFIERUTILS_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIURLCLASSIFIERUTILS \
  NS_IMETHOD GetKeyForURI(nsIURI *uri, nsACString & _retval) override; \
  NS_IMETHOD GetProvider(const nsACString & tableName, nsACString & _retval) override; \
  NS_IMETHOD GetProtocolVersion(const nsACString & provider, nsACString & _retval) override; \
  NS_IMETHOD ConvertThreatTypeToListNames(uint32_t threatType, nsACString & _retval) override; \
  NS_IMETHOD ConvertListNameToThreatType(const nsACString & listName, uint32_t *_retval) override; \
  NS_IMETHOD MakeUpdateRequestV4(const char * *aListNames, const char * *aStatesBase64, uint32_t aCount, nsACString & _retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIURLCLASSIFIERUTILS \
  NS_METHOD GetKeyForURI(nsIURI *uri, nsACString & _retval); \
  NS_METHOD GetProvider(const nsACString & tableName, nsACString & _retval); \
  NS_METHOD GetProtocolVersion(const nsACString & provider, nsACString & _retval); \
  NS_METHOD ConvertThreatTypeToListNames(uint32_t threatType, nsACString & _retval); \
  NS_METHOD ConvertListNameToThreatType(const nsACString & listName, uint32_t *_retval); \
  NS_METHOD MakeUpdateRequestV4(const char * *aListNames, const char * *aStatesBase64, uint32_t aCount, nsACString & _retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIURLCLASSIFIERUTILS(_to) \
  NS_IMETHOD GetKeyForURI(nsIURI *uri, nsACString & _retval) override { return _to GetKeyForURI(uri, _retval); } \
  NS_IMETHOD GetProvider(const nsACString & tableName, nsACString & _retval) override { return _to GetProvider(tableName, _retval); } \
  NS_IMETHOD GetProtocolVersion(const nsACString & provider, nsACString & _retval) override { return _to GetProtocolVersion(provider, _retval); } \
  NS_IMETHOD ConvertThreatTypeToListNames(uint32_t threatType, nsACString & _retval) override { return _to ConvertThreatTypeToListNames(threatType, _retval); } \
  NS_IMETHOD ConvertListNameToThreatType(const nsACString & listName, uint32_t *_retval) override { return _to ConvertListNameToThreatType(listName, _retval); } \
  NS_IMETHOD MakeUpdateRequestV4(const char * *aListNames, const char * *aStatesBase64, uint32_t aCount, nsACString & _retval) override { return _to MakeUpdateRequestV4(aListNames, aStatesBase64, aCount, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIURLCLASSIFIERUTILS(_to) \
  NS_IMETHOD GetKeyForURI(nsIURI *uri, nsACString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetKeyForURI(uri, _retval); } \
  NS_IMETHOD GetProvider(const nsACString & tableName, nsACString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetProvider(tableName, _retval); } \
  NS_IMETHOD GetProtocolVersion(const nsACString & provider, nsACString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetProtocolVersion(provider, _retval); } \
  NS_IMETHOD ConvertThreatTypeToListNames(uint32_t threatType, nsACString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ConvertThreatTypeToListNames(threatType, _retval); } \
  NS_IMETHOD ConvertListNameToThreatType(const nsACString & listName, uint32_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ConvertListNameToThreatType(listName, _retval); } \
  NS_IMETHOD MakeUpdateRequestV4(const char * *aListNames, const char * *aStatesBase64, uint32_t aCount, nsACString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->MakeUpdateRequestV4(aListNames, aStatesBase64, aCount, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsUrlClassifierUtils : public nsIUrlClassifierUtils
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIURLCLASSIFIERUTILS

  nsUrlClassifierUtils();

private:
  ~nsUrlClassifierUtils();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsUrlClassifierUtils, nsIUrlClassifierUtils)

nsUrlClassifierUtils::nsUrlClassifierUtils()
{
  /* member initializers and constructor code */
}

nsUrlClassifierUtils::~nsUrlClassifierUtils()
{
  /* destructor code */
}

/* ACString getKeyForURI (in nsIURI uri); */
NS_IMETHODIMP nsUrlClassifierUtils::GetKeyForURI(nsIURI *uri, nsACString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* ACString getProvider (in ACString tableName); */
NS_IMETHODIMP nsUrlClassifierUtils::GetProvider(const nsACString & tableName, nsACString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* ACString getProtocolVersion (in ACString provider); */
NS_IMETHODIMP nsUrlClassifierUtils::GetProtocolVersion(const nsACString & provider, nsACString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* ACString convertThreatTypeToListNames (in uint32_t threatType); */
NS_IMETHODIMP nsUrlClassifierUtils::ConvertThreatTypeToListNames(uint32_t threatType, nsACString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* uint32_t convertListNameToThreatType (in ACString listName); */
NS_IMETHODIMP nsUrlClassifierUtils::ConvertListNameToThreatType(const nsACString & listName, uint32_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* ACString makeUpdateRequestV4 ([array, size_is (aCount)] in string aListNames, [array, size_is (aCount)] in string aStatesBase64, in uint32_t aCount); */
NS_IMETHODIMP nsUrlClassifierUtils::MakeUpdateRequestV4(const char * *aListNames, const char * *aStatesBase64, uint32_t aCount, nsACString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIUrlClassifierUtils_h__ */
