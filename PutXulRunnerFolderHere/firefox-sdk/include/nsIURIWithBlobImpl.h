/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIURIWithBlobImpl.idl
 */

#ifndef __gen_nsIURIWithBlobImpl_h__
#define __gen_nsIURIWithBlobImpl_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIURI; /* forward declaration */


/* starting interface:    nsIURIWithBlobImpl */
#define NS_IURIWITHBLOBIMPL_IID_STR "331b41d3-3506-4ab5-bef9-aab41e3202a3"

#define NS_IURIWITHBLOBIMPL_IID \
  {0x331b41d3, 0x3506, 0x4ab5, \
    { 0xbe, 0xf9, 0xaa, 0xb4, 0x1e, 0x32, 0x02, 0xa3 }}

class NS_NO_VTABLE nsIURIWithBlobImpl : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IURIWITHBLOBIMPL_IID)

  /* readonly attribute nsISupports blobImpl; */
  NS_IMETHOD GetBlobImpl(nsISupports * *aBlobImpl) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIURIWithBlobImpl, NS_IURIWITHBLOBIMPL_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIURIWITHBLOBIMPL \
  NS_IMETHOD GetBlobImpl(nsISupports * *aBlobImpl) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIURIWITHBLOBIMPL \
  NS_METHOD GetBlobImpl(nsISupports * *aBlobImpl); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIURIWITHBLOBIMPL(_to) \
  NS_IMETHOD GetBlobImpl(nsISupports * *aBlobImpl) override { return _to GetBlobImpl(aBlobImpl); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIURIWITHBLOBIMPL(_to) \
  NS_IMETHOD GetBlobImpl(nsISupports * *aBlobImpl) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetBlobImpl(aBlobImpl); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsURIWithBlobImpl : public nsIURIWithBlobImpl
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIURIWITHBLOBIMPL

  nsURIWithBlobImpl();

private:
  ~nsURIWithBlobImpl();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsURIWithBlobImpl, nsIURIWithBlobImpl)

nsURIWithBlobImpl::nsURIWithBlobImpl()
{
  /* member initializers and constructor code */
}

nsURIWithBlobImpl::~nsURIWithBlobImpl()
{
  /* destructor code */
}

/* readonly attribute nsISupports blobImpl; */
NS_IMETHODIMP nsURIWithBlobImpl::GetBlobImpl(nsISupports * *aBlobImpl)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIURIWithBlobImpl_h__ */
