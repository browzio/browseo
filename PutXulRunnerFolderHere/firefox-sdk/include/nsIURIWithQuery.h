/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIURIWithQuery.idl
 */

#ifndef __gen_nsIURIWithQuery_h__
#define __gen_nsIURIWithQuery_h__


#ifndef __gen_nsIURI_h__
#include "nsIURI.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIURIWithQuery */
#define NS_IURIWITHQUERY_IID_STR "367510ee-8556-435a-8f99-b5fd357e08cc"

#define NS_IURIWITHQUERY_IID \
  {0x367510ee, 0x8556, 0x435a, \
    { 0x8f, 0x99, 0xb5, 0xfd, 0x35, 0x7e, 0x08, 0xcc }}

class NS_NO_VTABLE nsIURIWithQuery : public nsIURI {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IURIWITHQUERY_IID)

  /* attribute AUTF8String filePath; */
  NS_IMETHOD GetFilePath(nsACString & aFilePath) = 0;
  NS_IMETHOD SetFilePath(const nsACString & aFilePath) = 0;

  /* attribute AUTF8String query; */
  NS_IMETHOD GetQuery(nsACString & aQuery) = 0;
  NS_IMETHOD SetQuery(const nsACString & aQuery) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIURIWithQuery, NS_IURIWITHQUERY_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIURIWITHQUERY \
  NS_IMETHOD GetFilePath(nsACString & aFilePath) override; \
  NS_IMETHOD SetFilePath(const nsACString & aFilePath) override; \
  NS_IMETHOD GetQuery(nsACString & aQuery) override; \
  NS_IMETHOD SetQuery(const nsACString & aQuery) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIURIWITHQUERY \
  NS_METHOD GetFilePath(nsACString & aFilePath); \
  NS_METHOD SetFilePath(const nsACString & aFilePath); \
  NS_METHOD GetQuery(nsACString & aQuery); \
  NS_METHOD SetQuery(const nsACString & aQuery); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIURIWITHQUERY(_to) \
  NS_IMETHOD GetFilePath(nsACString & aFilePath) override { return _to GetFilePath(aFilePath); } \
  NS_IMETHOD SetFilePath(const nsACString & aFilePath) override { return _to SetFilePath(aFilePath); } \
  NS_IMETHOD GetQuery(nsACString & aQuery) override { return _to GetQuery(aQuery); } \
  NS_IMETHOD SetQuery(const nsACString & aQuery) override { return _to SetQuery(aQuery); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIURIWITHQUERY(_to) \
  NS_IMETHOD GetFilePath(nsACString & aFilePath) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetFilePath(aFilePath); } \
  NS_IMETHOD SetFilePath(const nsACString & aFilePath) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetFilePath(aFilePath); } \
  NS_IMETHOD GetQuery(nsACString & aQuery) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetQuery(aQuery); } \
  NS_IMETHOD SetQuery(const nsACString & aQuery) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetQuery(aQuery); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsURIWithQuery : public nsIURIWithQuery
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIURIWITHQUERY

  nsURIWithQuery();

private:
  ~nsURIWithQuery();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsURIWithQuery, nsIURIWithQuery)

nsURIWithQuery::nsURIWithQuery()
{
  /* member initializers and constructor code */
}

nsURIWithQuery::~nsURIWithQuery()
{
  /* destructor code */
}

/* attribute AUTF8String filePath; */
NS_IMETHODIMP nsURIWithQuery::GetFilePath(nsACString & aFilePath)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsURIWithQuery::SetFilePath(const nsACString & aFilePath)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* attribute AUTF8String query; */
NS_IMETHODIMP nsURIWithQuery::GetQuery(nsACString & aQuery)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsURIWithQuery::SetQuery(const nsACString & aQuery)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIURIWithQuery_h__ */
