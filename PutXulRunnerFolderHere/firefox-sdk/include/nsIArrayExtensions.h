/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIArrayExtensions.idl
 */

#ifndef __gen_nsIArrayExtensions_h__
#define __gen_nsIArrayExtensions_h__


#ifndef __gen_nsIArray_h__
#include "nsIArray.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIArrayExtensions */
#define NS_IARRAYEXTENSIONS_IID_STR "261d442e-050c-453d-8aaa-b3f23bcc528b"

#define NS_IARRAYEXTENSIONS_IID \
  {0x261d442e, 0x050c, 0x453d, \
    { 0x8a, 0xaa, 0xb3, 0xf2, 0x3b, 0xcc, 0x52, 0x8b }}

class NS_NO_VTABLE nsIArrayExtensions : public nsIArray {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IARRAYEXTENSIONS_IID)

  /* uint32_t Count (); */
  NS_IMETHOD Count(uint32_t *_retval) = 0;

  /* nsISupports GetElementAt (in uint32_t index); */
  NS_IMETHOD GetElementAt(uint32_t index, nsISupports * *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIArrayExtensions, NS_IARRAYEXTENSIONS_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIARRAYEXTENSIONS \
  NS_IMETHOD Count(uint32_t *_retval) override; \
  NS_IMETHOD GetElementAt(uint32_t index, nsISupports * *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIARRAYEXTENSIONS \
  NS_METHOD Count(uint32_t *_retval); \
  NS_METHOD GetElementAt(uint32_t index, nsISupports * *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIARRAYEXTENSIONS(_to) \
  NS_IMETHOD Count(uint32_t *_retval) override { return _to Count(_retval); } \
  NS_IMETHOD GetElementAt(uint32_t index, nsISupports * *_retval) override { return _to GetElementAt(index, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIARRAYEXTENSIONS(_to) \
  NS_IMETHOD Count(uint32_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Count(_retval); } \
  NS_IMETHOD GetElementAt(uint32_t index, nsISupports * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetElementAt(index, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsArrayExtensions : public nsIArrayExtensions
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIARRAYEXTENSIONS

  nsArrayExtensions();

private:
  ~nsArrayExtensions();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsArrayExtensions, nsIArrayExtensions)

nsArrayExtensions::nsArrayExtensions()
{
  /* member initializers and constructor code */
}

nsArrayExtensions::~nsArrayExtensions()
{
  /* destructor code */
}

/* uint32_t Count (); */
NS_IMETHODIMP nsArrayExtensions::Count(uint32_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsISupports GetElementAt (in uint32_t index); */
NS_IMETHODIMP nsArrayExtensions::GetElementAt(uint32_t index, nsISupports * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIArrayExtensions_h__ */
