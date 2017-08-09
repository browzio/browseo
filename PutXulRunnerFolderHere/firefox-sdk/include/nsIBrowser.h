/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIBrowser.idl
 */

#ifndef __gen_nsIBrowser_h__
#define __gen_nsIBrowser_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIDOMElement; /* forward declaration */


/* starting interface:    nsIBrowser */
#define NS_IBROWSER_IID_STR "14e5a0cb-e223-4202-95e8-fe53275193ea"

#define NS_IBROWSER_IID \
  {0x14e5a0cb, 0xe223, 0x4202, \
    { 0x95, 0xe8, 0xfe, 0x53, 0x27, 0x51, 0x93, 0xea }}

class NS_NO_VTABLE nsIBrowser : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IBROWSER_IID)

  /* readonly attribute nsIDOMElement relatedBrowser; */
  NS_IMETHOD GetRelatedBrowser(nsIDOMElement * *aRelatedBrowser) = 0;

  /* void dropLinks (in unsigned long linksCount, [array, size_is (linksCount)] in wstring links); */
  NS_IMETHOD DropLinks(uint32_t linksCount, const char16_t * *links) = 0;

  /* void swapBrowsers (in nsIBrowser aOtherBrowser); */
  NS_IMETHOD SwapBrowsers(nsIBrowser *aOtherBrowser) = 0;

  /* void closeBrowser (); */
  NS_IMETHOD CloseBrowser(void) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIBrowser, NS_IBROWSER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIBROWSER \
  NS_IMETHOD GetRelatedBrowser(nsIDOMElement * *aRelatedBrowser) override; \
  NS_IMETHOD DropLinks(uint32_t linksCount, const char16_t * *links) override; \
  NS_IMETHOD SwapBrowsers(nsIBrowser *aOtherBrowser) override; \
  NS_IMETHOD CloseBrowser(void) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIBROWSER \
  NS_METHOD GetRelatedBrowser(nsIDOMElement * *aRelatedBrowser); \
  NS_METHOD DropLinks(uint32_t linksCount, const char16_t * *links); \
  NS_METHOD SwapBrowsers(nsIBrowser *aOtherBrowser); \
  NS_METHOD CloseBrowser(void); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIBROWSER(_to) \
  NS_IMETHOD GetRelatedBrowser(nsIDOMElement * *aRelatedBrowser) override { return _to GetRelatedBrowser(aRelatedBrowser); } \
  NS_IMETHOD DropLinks(uint32_t linksCount, const char16_t * *links) override { return _to DropLinks(linksCount, links); } \
  NS_IMETHOD SwapBrowsers(nsIBrowser *aOtherBrowser) override { return _to SwapBrowsers(aOtherBrowser); } \
  NS_IMETHOD CloseBrowser(void) override { return _to CloseBrowser(); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIBROWSER(_to) \
  NS_IMETHOD GetRelatedBrowser(nsIDOMElement * *aRelatedBrowser) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetRelatedBrowser(aRelatedBrowser); } \
  NS_IMETHOD DropLinks(uint32_t linksCount, const char16_t * *links) override { return !_to ? NS_ERROR_NULL_POINTER : _to->DropLinks(linksCount, links); } \
  NS_IMETHOD SwapBrowsers(nsIBrowser *aOtherBrowser) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SwapBrowsers(aOtherBrowser); } \
  NS_IMETHOD CloseBrowser(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CloseBrowser(); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsBrowser : public nsIBrowser
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIBROWSER

  nsBrowser();

private:
  ~nsBrowser();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsBrowser, nsIBrowser)

nsBrowser::nsBrowser()
{
  /* member initializers and constructor code */
}

nsBrowser::~nsBrowser()
{
  /* destructor code */
}

/* readonly attribute nsIDOMElement relatedBrowser; */
NS_IMETHODIMP nsBrowser::GetRelatedBrowser(nsIDOMElement * *aRelatedBrowser)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void dropLinks (in unsigned long linksCount, [array, size_is (linksCount)] in wstring links); */
NS_IMETHODIMP nsBrowser::DropLinks(uint32_t linksCount, const char16_t * *links)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void swapBrowsers (in nsIBrowser aOtherBrowser); */
NS_IMETHODIMP nsBrowser::SwapBrowsers(nsIBrowser *aOtherBrowser)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void closeBrowser (); */
NS_IMETHODIMP nsBrowser::CloseBrowser()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIBrowser_h__ */
