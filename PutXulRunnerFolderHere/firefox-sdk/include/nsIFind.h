/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIFind.idl
 */

#ifndef __gen_nsIFind_h__
#define __gen_nsIFind_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIDOMRange; /* forward declaration */

class nsIWordBreaker; /* forward declaration */


/* starting interface:    nsIFind */
#define NS_IFIND_IID_STR "40aba110-2a56-4678-be90-e2c17a9ae7d7"

#define NS_IFIND_IID \
  {0x40aba110, 0x2a56, 0x4678, \
    { 0xbe, 0x90, 0xe2, 0xc1, 0x7a, 0x9a, 0xe7, 0xd7 }}

class NS_NO_VTABLE nsIFind : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IFIND_IID)

  /* attribute boolean findBackwards; */
  NS_IMETHOD GetFindBackwards(bool *aFindBackwards) = 0;
  NS_IMETHOD SetFindBackwards(bool aFindBackwards) = 0;

  /* attribute boolean caseSensitive; */
  NS_IMETHOD GetCaseSensitive(bool *aCaseSensitive) = 0;
  NS_IMETHOD SetCaseSensitive(bool aCaseSensitive) = 0;

  /* attribute boolean entireWord; */
  NS_IMETHOD GetEntireWord(bool *aEntireWord) = 0;
  NS_IMETHOD SetEntireWord(bool aEntireWord) = 0;

  /* nsIDOMRange Find (in wstring aPatText, in nsIDOMRange aSearchRange, in nsIDOMRange aStartPoint, in nsIDOMRange aEndPoint); */
  NS_IMETHOD Find(const char16_t * aPatText, nsIDOMRange *aSearchRange, nsIDOMRange *aStartPoint, nsIDOMRange *aEndPoint, nsIDOMRange * *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIFind, NS_IFIND_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIFIND \
  NS_IMETHOD GetFindBackwards(bool *aFindBackwards) override; \
  NS_IMETHOD SetFindBackwards(bool aFindBackwards) override; \
  NS_IMETHOD GetCaseSensitive(bool *aCaseSensitive) override; \
  NS_IMETHOD SetCaseSensitive(bool aCaseSensitive) override; \
  NS_IMETHOD GetEntireWord(bool *aEntireWord) override; \
  NS_IMETHOD SetEntireWord(bool aEntireWord) override; \
  NS_IMETHOD Find(const char16_t * aPatText, nsIDOMRange *aSearchRange, nsIDOMRange *aStartPoint, nsIDOMRange *aEndPoint, nsIDOMRange * *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIFIND \
  NS_METHOD GetFindBackwards(bool *aFindBackwards); \
  NS_METHOD SetFindBackwards(bool aFindBackwards); \
  NS_METHOD GetCaseSensitive(bool *aCaseSensitive); \
  NS_METHOD SetCaseSensitive(bool aCaseSensitive); \
  NS_METHOD GetEntireWord(bool *aEntireWord); \
  NS_METHOD SetEntireWord(bool aEntireWord); \
  NS_METHOD Find(const char16_t * aPatText, nsIDOMRange *aSearchRange, nsIDOMRange *aStartPoint, nsIDOMRange *aEndPoint, nsIDOMRange * *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIFIND(_to) \
  NS_IMETHOD GetFindBackwards(bool *aFindBackwards) override { return _to GetFindBackwards(aFindBackwards); } \
  NS_IMETHOD SetFindBackwards(bool aFindBackwards) override { return _to SetFindBackwards(aFindBackwards); } \
  NS_IMETHOD GetCaseSensitive(bool *aCaseSensitive) override { return _to GetCaseSensitive(aCaseSensitive); } \
  NS_IMETHOD SetCaseSensitive(bool aCaseSensitive) override { return _to SetCaseSensitive(aCaseSensitive); } \
  NS_IMETHOD GetEntireWord(bool *aEntireWord) override { return _to GetEntireWord(aEntireWord); } \
  NS_IMETHOD SetEntireWord(bool aEntireWord) override { return _to SetEntireWord(aEntireWord); } \
  NS_IMETHOD Find(const char16_t * aPatText, nsIDOMRange *aSearchRange, nsIDOMRange *aStartPoint, nsIDOMRange *aEndPoint, nsIDOMRange * *_retval) override { return _to Find(aPatText, aSearchRange, aStartPoint, aEndPoint, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIFIND(_to) \
  NS_IMETHOD GetFindBackwards(bool *aFindBackwards) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetFindBackwards(aFindBackwards); } \
  NS_IMETHOD SetFindBackwards(bool aFindBackwards) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetFindBackwards(aFindBackwards); } \
  NS_IMETHOD GetCaseSensitive(bool *aCaseSensitive) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCaseSensitive(aCaseSensitive); } \
  NS_IMETHOD SetCaseSensitive(bool aCaseSensitive) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetCaseSensitive(aCaseSensitive); } \
  NS_IMETHOD GetEntireWord(bool *aEntireWord) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetEntireWord(aEntireWord); } \
  NS_IMETHOD SetEntireWord(bool aEntireWord) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetEntireWord(aEntireWord); } \
  NS_IMETHOD Find(const char16_t * aPatText, nsIDOMRange *aSearchRange, nsIDOMRange *aStartPoint, nsIDOMRange *aEndPoint, nsIDOMRange * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Find(aPatText, aSearchRange, aStartPoint, aEndPoint, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsFind : public nsIFind
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIFIND

  nsFind();

private:
  ~nsFind();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsFind, nsIFind)

nsFind::nsFind()
{
  /* member initializers and constructor code */
}

nsFind::~nsFind()
{
  /* destructor code */
}

/* attribute boolean findBackwards; */
NS_IMETHODIMP nsFind::GetFindBackwards(bool *aFindBackwards)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsFind::SetFindBackwards(bool aFindBackwards)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* attribute boolean caseSensitive; */
NS_IMETHODIMP nsFind::GetCaseSensitive(bool *aCaseSensitive)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsFind::SetCaseSensitive(bool aCaseSensitive)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* attribute boolean entireWord; */
NS_IMETHODIMP nsFind::GetEntireWord(bool *aEntireWord)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsFind::SetEntireWord(bool aEntireWord)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsIDOMRange Find (in wstring aPatText, in nsIDOMRange aSearchRange, in nsIDOMRange aStartPoint, in nsIDOMRange aEndPoint); */
NS_IMETHODIMP nsFind::Find(const char16_t * aPatText, nsIDOMRange *aSearchRange, nsIDOMRange *aStartPoint, nsIDOMRange *aEndPoint, nsIDOMRange * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIFind_h__ */
