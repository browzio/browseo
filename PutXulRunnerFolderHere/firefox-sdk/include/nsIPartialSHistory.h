/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPartialSHistory.idl
 */

#ifndef __gen_nsIPartialSHistory_h__
#define __gen_nsIPartialSHistory_h__


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


/* starting interface:    nsIPartialSHistory */
#define NS_IPARTIALSHISTORY_IID_STR "5cd75e28-838c-4a0a-972e-6005f736ef7a"

#define NS_IPARTIALSHISTORY_IID \
  {0x5cd75e28, 0x838c, 0x4a0a, \
    { 0x97, 0x2e, 0x60, 0x05, 0xf7, 0x36, 0xef, 0x7a }}

class NS_NO_VTABLE nsIPartialSHistory : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPARTIALSHISTORY_IID)

  /* [infallible] readonly attribute unsigned long count; */
  NS_IMETHOD GetCount(uint32_t *aCount) = 0;
  inline uint32_t GetCount()
  {
    uint32_t result;
    mozilla::DebugOnly<nsresult> rv = GetCount(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute unsigned long globalIndexOffset; */
  NS_IMETHOD GetGlobalIndexOffset(uint32_t *aGlobalIndexOffset) = 0;
  inline uint32_t GetGlobalIndexOffset()
  {
    uint32_t result;
    mozilla::DebugOnly<nsresult> rv = GetGlobalIndexOffset(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* readonly attribute nsIFrameLoader ownerFrameLoader; */
  NS_IMETHOD GetOwnerFrameLoader(nsIFrameLoader * *aOwnerFrameLoader) = 0;

  /* void onAttachGroupedSessionHistory (in unsigned long aOffset); */
  NS_IMETHOD OnAttachGroupedSessionHistory(uint32_t aOffset) = 0;

  /* void onSessionHistoryChange (in unsigned long aCount); */
  NS_IMETHOD OnSessionHistoryChange(uint32_t aCount) = 0;

  /* void onActive (in unsigned long aGlobalLength, in unsigned long aTargetLocalIndex); */
  NS_IMETHOD OnActive(uint32_t aGlobalLength, uint32_t aTargetLocalIndex) = 0;

  /* void onDeactive (); */
  NS_IMETHOD OnDeactive(void) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPartialSHistory, NS_IPARTIALSHISTORY_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPARTIALSHISTORY \
  using nsIPartialSHistory::GetCount; \
  NS_IMETHOD GetCount(uint32_t *aCount) override; \
  using nsIPartialSHistory::GetGlobalIndexOffset; \
  NS_IMETHOD GetGlobalIndexOffset(uint32_t *aGlobalIndexOffset) override; \
  NS_IMETHOD GetOwnerFrameLoader(nsIFrameLoader * *aOwnerFrameLoader) override; \
  NS_IMETHOD OnAttachGroupedSessionHistory(uint32_t aOffset) override; \
  NS_IMETHOD OnSessionHistoryChange(uint32_t aCount) override; \
  NS_IMETHOD OnActive(uint32_t aGlobalLength, uint32_t aTargetLocalIndex) override; \
  NS_IMETHOD OnDeactive(void) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPARTIALSHISTORY \
  using nsIPartialSHistory::GetCount; \
  NS_METHOD GetCount(uint32_t *aCount); \
  using nsIPartialSHistory::GetGlobalIndexOffset; \
  NS_METHOD GetGlobalIndexOffset(uint32_t *aGlobalIndexOffset); \
  NS_METHOD GetOwnerFrameLoader(nsIFrameLoader * *aOwnerFrameLoader); \
  NS_METHOD OnAttachGroupedSessionHistory(uint32_t aOffset); \
  NS_METHOD OnSessionHistoryChange(uint32_t aCount); \
  NS_METHOD OnActive(uint32_t aGlobalLength, uint32_t aTargetLocalIndex); \
  NS_METHOD OnDeactive(void); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPARTIALSHISTORY(_to) \
  using nsIPartialSHistory::GetCount; \
  NS_IMETHOD GetCount(uint32_t *aCount) override { return _to GetCount(aCount); } \
  using nsIPartialSHistory::GetGlobalIndexOffset; \
  NS_IMETHOD GetGlobalIndexOffset(uint32_t *aGlobalIndexOffset) override { return _to GetGlobalIndexOffset(aGlobalIndexOffset); } \
  NS_IMETHOD GetOwnerFrameLoader(nsIFrameLoader * *aOwnerFrameLoader) override { return _to GetOwnerFrameLoader(aOwnerFrameLoader); } \
  NS_IMETHOD OnAttachGroupedSessionHistory(uint32_t aOffset) override { return _to OnAttachGroupedSessionHistory(aOffset); } \
  NS_IMETHOD OnSessionHistoryChange(uint32_t aCount) override { return _to OnSessionHistoryChange(aCount); } \
  NS_IMETHOD OnActive(uint32_t aGlobalLength, uint32_t aTargetLocalIndex) override { return _to OnActive(aGlobalLength, aTargetLocalIndex); } \
  NS_IMETHOD OnDeactive(void) override { return _to OnDeactive(); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPARTIALSHISTORY(_to) \
  NS_IMETHOD GetCount(uint32_t *aCount) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCount(aCount); } \
  NS_IMETHOD GetGlobalIndexOffset(uint32_t *aGlobalIndexOffset) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetGlobalIndexOffset(aGlobalIndexOffset); } \
  NS_IMETHOD GetOwnerFrameLoader(nsIFrameLoader * *aOwnerFrameLoader) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetOwnerFrameLoader(aOwnerFrameLoader); } \
  NS_IMETHOD OnAttachGroupedSessionHistory(uint32_t aOffset) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnAttachGroupedSessionHistory(aOffset); } \
  NS_IMETHOD OnSessionHistoryChange(uint32_t aCount) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnSessionHistoryChange(aCount); } \
  NS_IMETHOD OnActive(uint32_t aGlobalLength, uint32_t aTargetLocalIndex) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnActive(aGlobalLength, aTargetLocalIndex); } \
  NS_IMETHOD OnDeactive(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnDeactive(); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPartialSHistory : public nsIPartialSHistory
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPARTIALSHISTORY

  nsPartialSHistory();

private:
  ~nsPartialSHistory();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPartialSHistory, nsIPartialSHistory)

nsPartialSHistory::nsPartialSHistory()
{
  /* member initializers and constructor code */
}

nsPartialSHistory::~nsPartialSHistory()
{
  /* destructor code */
}

/* [infallible] readonly attribute unsigned long count; */
NS_IMETHODIMP nsPartialSHistory::GetCount(uint32_t *aCount)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute unsigned long globalIndexOffset; */
NS_IMETHODIMP nsPartialSHistory::GetGlobalIndexOffset(uint32_t *aGlobalIndexOffset)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIFrameLoader ownerFrameLoader; */
NS_IMETHODIMP nsPartialSHistory::GetOwnerFrameLoader(nsIFrameLoader * *aOwnerFrameLoader)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onAttachGroupedSessionHistory (in unsigned long aOffset); */
NS_IMETHODIMP nsPartialSHistory::OnAttachGroupedSessionHistory(uint32_t aOffset)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onSessionHistoryChange (in unsigned long aCount); */
NS_IMETHODIMP nsPartialSHistory::OnSessionHistoryChange(uint32_t aCount)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onActive (in unsigned long aGlobalLength, in unsigned long aTargetLocalIndex); */
NS_IMETHODIMP nsPartialSHistory::OnActive(uint32_t aGlobalLength, uint32_t aTargetLocalIndex)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onDeactive (); */
NS_IMETHODIMP nsPartialSHistory::OnDeactive()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPartialSHistory_h__ */
