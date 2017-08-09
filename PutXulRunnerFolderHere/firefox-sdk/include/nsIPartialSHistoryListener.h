/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPartialSHistoryListener.idl
 */

#ifndef __gen_nsIPartialSHistoryListener_h__
#define __gen_nsIPartialSHistoryListener_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIPartialSHistoryListener */
#define NS_IPARTIALSHISTORYLISTENER_IID_STR "be0cd2b6-6f03-4366-9fe2-184c914ff3df"

#define NS_IPARTIALSHISTORYLISTENER_IID \
  {0xbe0cd2b6, 0x6f03, 0x4366, \
    { 0x9f, 0xe2, 0x18, 0x4c, 0x91, 0x4f, 0xf3, 0xdf }}

class NS_NO_VTABLE nsIPartialSHistoryListener : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPARTIALSHISTORYLISTENER_IID)

  /* void onRequestCrossBrowserNavigation (in unsigned long aIndex); */
  NS_IMETHOD OnRequestCrossBrowserNavigation(uint32_t aIndex) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPartialSHistoryListener, NS_IPARTIALSHISTORYLISTENER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPARTIALSHISTORYLISTENER \
  NS_IMETHOD OnRequestCrossBrowserNavigation(uint32_t aIndex) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPARTIALSHISTORYLISTENER \
  NS_METHOD OnRequestCrossBrowserNavigation(uint32_t aIndex); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPARTIALSHISTORYLISTENER(_to) \
  NS_IMETHOD OnRequestCrossBrowserNavigation(uint32_t aIndex) override { return _to OnRequestCrossBrowserNavigation(aIndex); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPARTIALSHISTORYLISTENER(_to) \
  NS_IMETHOD OnRequestCrossBrowserNavigation(uint32_t aIndex) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnRequestCrossBrowserNavigation(aIndex); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPartialSHistoryListener : public nsIPartialSHistoryListener
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPARTIALSHISTORYLISTENER

  nsPartialSHistoryListener();

private:
  ~nsPartialSHistoryListener();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPartialSHistoryListener, nsIPartialSHistoryListener)

nsPartialSHistoryListener::nsPartialSHistoryListener()
{
  /* member initializers and constructor code */
}

nsPartialSHistoryListener::~nsPartialSHistoryListener()
{
  /* destructor code */
}

/* void onRequestCrossBrowserNavigation (in unsigned long aIndex); */
NS_IMETHODIMP nsPartialSHistoryListener::OnRequestCrossBrowserNavigation(uint32_t aIndex)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPartialSHistoryListener_h__ */
