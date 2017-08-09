/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIGroupedSHistory.idl
 */

#ifndef __gen_nsIGroupedSHistory_h__
#define __gen_nsIGroupedSHistory_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

#include "mozilla/Assertions.h"
#include "mozilla/DebugOnly.h"

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIFrameLoader; /* forward declaration */

class nsIPartialSHistory; /* forward declaration */


/* starting interface:    nsIGroupedSHistory */
#define NS_IGROUPEDSHISTORY_IID_STR "813e498d-73a8-449a-be09-6187e62c5352"

#define NS_IGROUPEDSHISTORY_IID \
  {0x813e498d, 0x73a8, 0x449a, \
    { 0xbe, 0x09, 0x61, 0x87, 0xe6, 0x2c, 0x53, 0x52 }}

class NS_NO_VTABLE nsIGroupedSHistory : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IGROUPEDSHISTORY_IID)

  /* [infallible] readonly attribute unsigned long count; */
  NS_IMETHOD GetCount(uint32_t *aCount) = 0;
  inline uint32_t GetCount()
  {
    uint32_t result;
    mozilla::DebugOnly<nsresult> rv = GetCount(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* void appendPartialSessionHistory (in nsIPartialSHistory aPartialHistory); */
  NS_IMETHOD AppendPartialSessionHistory(nsIPartialSHistory *aPartialHistory) = 0;

  /* void onPartialSessionHistoryChange (in nsIPartialSHistory aPartialHistory); */
  NS_IMETHOD OnPartialSessionHistoryChange(nsIPartialSHistory *aPartialHistory) = 0;

  /* void gotoIndex (in unsigned long aGlobalIndex, out nsIFrameLoader aTargetLoaderToSwap); */
  NS_IMETHOD GotoIndex(uint32_t aGlobalIndex, nsIFrameLoader * *aTargetLoaderToSwap) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIGroupedSHistory, NS_IGROUPEDSHISTORY_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIGROUPEDSHISTORY \
  using nsIGroupedSHistory::GetCount; \
  NS_IMETHOD GetCount(uint32_t *aCount) override; \
  NS_IMETHOD AppendPartialSessionHistory(nsIPartialSHistory *aPartialHistory) override; \
  NS_IMETHOD OnPartialSessionHistoryChange(nsIPartialSHistory *aPartialHistory) override; \
  NS_IMETHOD GotoIndex(uint32_t aGlobalIndex, nsIFrameLoader * *aTargetLoaderToSwap) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIGROUPEDSHISTORY \
  using nsIGroupedSHistory::GetCount; \
  NS_METHOD GetCount(uint32_t *aCount); \
  NS_METHOD AppendPartialSessionHistory(nsIPartialSHistory *aPartialHistory); \
  NS_METHOD OnPartialSessionHistoryChange(nsIPartialSHistory *aPartialHistory); \
  NS_METHOD GotoIndex(uint32_t aGlobalIndex, nsIFrameLoader * *aTargetLoaderToSwap); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIGROUPEDSHISTORY(_to) \
  using nsIGroupedSHistory::GetCount; \
  NS_IMETHOD GetCount(uint32_t *aCount) override { return _to GetCount(aCount); } \
  NS_IMETHOD AppendPartialSessionHistory(nsIPartialSHistory *aPartialHistory) override { return _to AppendPartialSessionHistory(aPartialHistory); } \
  NS_IMETHOD OnPartialSessionHistoryChange(nsIPartialSHistory *aPartialHistory) override { return _to OnPartialSessionHistoryChange(aPartialHistory); } \
  NS_IMETHOD GotoIndex(uint32_t aGlobalIndex, nsIFrameLoader * *aTargetLoaderToSwap) override { return _to GotoIndex(aGlobalIndex, aTargetLoaderToSwap); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIGROUPEDSHISTORY(_to) \
  NS_IMETHOD GetCount(uint32_t *aCount) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCount(aCount); } \
  NS_IMETHOD AppendPartialSessionHistory(nsIPartialSHistory *aPartialHistory) override { return !_to ? NS_ERROR_NULL_POINTER : _to->AppendPartialSessionHistory(aPartialHistory); } \
  NS_IMETHOD OnPartialSessionHistoryChange(nsIPartialSHistory *aPartialHistory) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnPartialSessionHistoryChange(aPartialHistory); } \
  NS_IMETHOD GotoIndex(uint32_t aGlobalIndex, nsIFrameLoader * *aTargetLoaderToSwap) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GotoIndex(aGlobalIndex, aTargetLoaderToSwap); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsGroupedSHistory : public nsIGroupedSHistory
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIGROUPEDSHISTORY

  nsGroupedSHistory();

private:
  ~nsGroupedSHistory();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsGroupedSHistory, nsIGroupedSHistory)

nsGroupedSHistory::nsGroupedSHistory()
{
  /* member initializers and constructor code */
}

nsGroupedSHistory::~nsGroupedSHistory()
{
  /* destructor code */
}

/* [infallible] readonly attribute unsigned long count; */
NS_IMETHODIMP nsGroupedSHistory::GetCount(uint32_t *aCount)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void appendPartialSessionHistory (in nsIPartialSHistory aPartialHistory); */
NS_IMETHODIMP nsGroupedSHistory::AppendPartialSessionHistory(nsIPartialSHistory *aPartialHistory)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onPartialSessionHistoryChange (in nsIPartialSHistory aPartialHistory); */
NS_IMETHODIMP nsGroupedSHistory::OnPartialSessionHistoryChange(nsIPartialSHistory *aPartialHistory)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void gotoIndex (in unsigned long aGlobalIndex, out nsIFrameLoader aTargetLoaderToSwap); */
NS_IMETHODIMP nsGroupedSHistory::GotoIndex(uint32_t aGlobalIndex, nsIFrameLoader * *aTargetLoaderToSwap)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIGroupedSHistory_h__ */
