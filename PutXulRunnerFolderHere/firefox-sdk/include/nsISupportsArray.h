/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsISupportsArray.idl
 */

#ifndef __gen_nsISupportsArray_h__
#define __gen_nsISupportsArray_h__


#ifndef __gen_nsICollection_h__
#include "nsICollection.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

class nsIBidirectionalEnumerator;
class nsISupportsArray;
 
#define NS_SUPPORTSARRAY_CID                         \
{ /* bda17d50-0d6b-11d3-9331-00104ba0fd40 */         \
    0xbda17d50,                                      \
    0x0d6b,                                          \
    0x11d3,                                          \
    {0x93, 0x31, 0x00, 0x10, 0x4b, 0xa0, 0xfd, 0x40} \
}
#define NS_SUPPORTSARRAY_CONTRACTID "@mozilla.org/supports-array;1"
 

/* starting interface:    nsISupportsArray */
#define NS_ISUPPORTSARRAY_IID_STR "241addc8-3608-4e73-8083-2fd6fa09eba2"

#define NS_ISUPPORTSARRAY_IID \
  {0x241addc8, 0x3608, 0x4e73, \
    { 0x80, 0x83, 0x2f, 0xd6, 0xfa, 0x09, 0xeb, 0xa2 }}

class NS_NO_VTABLE MOZ_DEPRECATED nsISupportsArray : public nsICollection {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ISUPPORTSARRAY_IID)

  /* [notxpcom] long IndexOf ([const] in nsISupports aPossibleElement); */
  NS_IMETHOD_(int32_t) IndexOf(const nsISupports *aPossibleElement) = 0;

  /* long GetIndexOf (in nsISupports aPossibleElement); */
  NS_IMETHOD GetIndexOf(nsISupports *aPossibleElement, int32_t *_retval) = 0;

  /* [notxpcom] boolean InsertElementAt (in nsISupports aElement, in unsigned long aIndex); */
  NS_IMETHOD_(bool) InsertElementAt(nsISupports *aElement, uint32_t aIndex) = 0;

  /* [notxpcom] boolean ReplaceElementAt (in nsISupports aElement, in unsigned long aIndex); */
  NS_IMETHOD_(bool) ReplaceElementAt(nsISupports *aElement, uint32_t aIndex) = 0;

  /* [notxpcom] boolean RemoveElementAt (in unsigned long aIndex); */
  NS_IMETHOD_(bool) RemoveElementAt(uint32_t aIndex) = 0;

  /* void DeleteElementAt (in unsigned long aIndex); */
  NS_IMETHOD DeleteElementAt(uint32_t aIndex) = 0;

  /* nsISupportsArray clone (); */
  NS_IMETHOD Clone(nsISupportsArray * *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsISupportsArray, NS_ISUPPORTSARRAY_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSISUPPORTSARRAY \
  NS_IMETHOD_(int32_t) IndexOf(const nsISupports *aPossibleElement) override; \
  NS_IMETHOD GetIndexOf(nsISupports *aPossibleElement, int32_t *_retval) override; \
  NS_IMETHOD_(bool) InsertElementAt(nsISupports *aElement, uint32_t aIndex) override; \
  NS_IMETHOD_(bool) ReplaceElementAt(nsISupports *aElement, uint32_t aIndex) override; \
  NS_IMETHOD_(bool) RemoveElementAt(uint32_t aIndex) override; \
  NS_IMETHOD DeleteElementAt(uint32_t aIndex) override; \
  NS_IMETHOD Clone(nsISupportsArray * *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSISUPPORTSARRAY \
  NS_METHOD_(int32_t) IndexOf(const nsISupports *aPossibleElement); \
  NS_METHOD GetIndexOf(nsISupports *aPossibleElement, int32_t *_retval); \
  NS_METHOD_(bool) InsertElementAt(nsISupports *aElement, uint32_t aIndex); \
  NS_METHOD_(bool) ReplaceElementAt(nsISupports *aElement, uint32_t aIndex); \
  NS_METHOD_(bool) RemoveElementAt(uint32_t aIndex); \
  NS_METHOD DeleteElementAt(uint32_t aIndex); \
  NS_METHOD Clone(nsISupportsArray * *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSISUPPORTSARRAY(_to) \
  NS_IMETHOD_(int32_t) IndexOf(const nsISupports *aPossibleElement) override { return _to IndexOf(aPossibleElement); } \
  NS_IMETHOD GetIndexOf(nsISupports *aPossibleElement, int32_t *_retval) override { return _to GetIndexOf(aPossibleElement, _retval); } \
  NS_IMETHOD_(bool) InsertElementAt(nsISupports *aElement, uint32_t aIndex) override { return _to InsertElementAt(aElement, aIndex); } \
  NS_IMETHOD_(bool) ReplaceElementAt(nsISupports *aElement, uint32_t aIndex) override { return _to ReplaceElementAt(aElement, aIndex); } \
  NS_IMETHOD_(bool) RemoveElementAt(uint32_t aIndex) override { return _to RemoveElementAt(aIndex); } \
  NS_IMETHOD DeleteElementAt(uint32_t aIndex) override { return _to DeleteElementAt(aIndex); } \
  NS_IMETHOD Clone(nsISupportsArray * *_retval) override { return _to Clone(_retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSISUPPORTSARRAY(_to) \
  NS_IMETHOD_(int32_t) IndexOf(const nsISupports *aPossibleElement) override; \
  NS_IMETHOD GetIndexOf(nsISupports *aPossibleElement, int32_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIndexOf(aPossibleElement, _retval); } \
  NS_IMETHOD_(bool) InsertElementAt(nsISupports *aElement, uint32_t aIndex) override; \
  NS_IMETHOD_(bool) ReplaceElementAt(nsISupports *aElement, uint32_t aIndex) override; \
  NS_IMETHOD_(bool) RemoveElementAt(uint32_t aIndex) override; \
  NS_IMETHOD DeleteElementAt(uint32_t aIndex) override { return !_to ? NS_ERROR_NULL_POINTER : _to->DeleteElementAt(aIndex); } \
  NS_IMETHOD Clone(nsISupportsArray * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Clone(_retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsSupportsArray : public nsISupportsArray
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSISUPPORTSARRAY

  nsSupportsArray();

private:
  ~nsSupportsArray();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsSupportsArray, nsISupportsArray)

nsSupportsArray::nsSupportsArray()
{
  /* member initializers and constructor code */
}

nsSupportsArray::~nsSupportsArray()
{
  /* destructor code */
}

/* [notxpcom] long IndexOf ([const] in nsISupports aPossibleElement); */
NS_IMETHODIMP_(int32_t) nsSupportsArray::IndexOf(const nsISupports *aPossibleElement)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* long GetIndexOf (in nsISupports aPossibleElement); */
NS_IMETHODIMP nsSupportsArray::GetIndexOf(nsISupports *aPossibleElement, int32_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [notxpcom] boolean InsertElementAt (in nsISupports aElement, in unsigned long aIndex); */
NS_IMETHODIMP_(bool) nsSupportsArray::InsertElementAt(nsISupports *aElement, uint32_t aIndex)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [notxpcom] boolean ReplaceElementAt (in nsISupports aElement, in unsigned long aIndex); */
NS_IMETHODIMP_(bool) nsSupportsArray::ReplaceElementAt(nsISupports *aElement, uint32_t aIndex)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [notxpcom] boolean RemoveElementAt (in unsigned long aIndex); */
NS_IMETHODIMP_(bool) nsSupportsArray::RemoveElementAt(uint32_t aIndex)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void DeleteElementAt (in unsigned long aIndex); */
NS_IMETHODIMP nsSupportsArray::DeleteElementAt(uint32_t aIndex)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsISupportsArray clone (); */
NS_IMETHODIMP nsSupportsArray::Clone(nsISupportsArray * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


// Construct and return a default implementation of nsISupportsArray:
extern MOZ_MUST_USE nsresult
NS_NewISupportsArray(nsISupportsArray** aInstancePtrResult);

#endif /* __gen_nsISupportsArray_h__ */
