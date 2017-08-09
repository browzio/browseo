/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsISHistoryListener.idl
 */

#ifndef __gen_nsISHistoryListener_h__
#define __gen_nsISHistoryListener_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIURI; /* forward declaration */


/* starting interface:    nsISHistoryListener */
#define NS_ISHISTORYLISTENER_IID_STR "125c0833-746a-400e-9b89-d2d18545c08a"

#define NS_ISHISTORYLISTENER_IID \
  {0x125c0833, 0x746a, 0x400e, \
    { 0x9b, 0x89, 0xd2, 0xd1, 0x85, 0x45, 0xc0, 0x8a }}

class NS_NO_VTABLE nsISHistoryListener : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ISHISTORYLISTENER_IID)

  /* void OnHistoryNewEntry (in nsIURI aNewURI, in long aOldIndex); */
  NS_IMETHOD OnHistoryNewEntry(nsIURI *aNewURI, int32_t aOldIndex) = 0;

  /* boolean OnHistoryGoBack (in nsIURI aBackURI); */
  NS_IMETHOD OnHistoryGoBack(nsIURI *aBackURI, bool *_retval) = 0;

  /* boolean OnHistoryGoForward (in nsIURI aForwardURI); */
  NS_IMETHOD OnHistoryGoForward(nsIURI *aForwardURI, bool *_retval) = 0;

  /* boolean OnHistoryReload (in nsIURI aReloadURI, in unsigned long aReloadFlags); */
  NS_IMETHOD OnHistoryReload(nsIURI *aReloadURI, uint32_t aReloadFlags, bool *_retval) = 0;

  /* boolean OnHistoryGotoIndex (in long aIndex, in nsIURI aGotoURI); */
  NS_IMETHOD OnHistoryGotoIndex(int32_t aIndex, nsIURI *aGotoURI, bool *_retval) = 0;

  /* boolean OnHistoryPurge (in long aNumEntries); */
  NS_IMETHOD OnHistoryPurge(int32_t aNumEntries, bool *_retval) = 0;

  /* void OnHistoryReplaceEntry (in long aIndex); */
  NS_IMETHOD OnHistoryReplaceEntry(int32_t aIndex) = 0;

  /* void OnLengthChange (in long aCount); */
  NS_IMETHOD OnLengthChange(int32_t aCount) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsISHistoryListener, NS_ISHISTORYLISTENER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSISHISTORYLISTENER \
  NS_IMETHOD OnHistoryNewEntry(nsIURI *aNewURI, int32_t aOldIndex) override; \
  NS_IMETHOD OnHistoryGoBack(nsIURI *aBackURI, bool *_retval) override; \
  NS_IMETHOD OnHistoryGoForward(nsIURI *aForwardURI, bool *_retval) override; \
  NS_IMETHOD OnHistoryReload(nsIURI *aReloadURI, uint32_t aReloadFlags, bool *_retval) override; \
  NS_IMETHOD OnHistoryGotoIndex(int32_t aIndex, nsIURI *aGotoURI, bool *_retval) override; \
  NS_IMETHOD OnHistoryPurge(int32_t aNumEntries, bool *_retval) override; \
  NS_IMETHOD OnHistoryReplaceEntry(int32_t aIndex) override; \
  NS_IMETHOD OnLengthChange(int32_t aCount) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSISHISTORYLISTENER \
  NS_METHOD OnHistoryNewEntry(nsIURI *aNewURI, int32_t aOldIndex); \
  NS_METHOD OnHistoryGoBack(nsIURI *aBackURI, bool *_retval); \
  NS_METHOD OnHistoryGoForward(nsIURI *aForwardURI, bool *_retval); \
  NS_METHOD OnHistoryReload(nsIURI *aReloadURI, uint32_t aReloadFlags, bool *_retval); \
  NS_METHOD OnHistoryGotoIndex(int32_t aIndex, nsIURI *aGotoURI, bool *_retval); \
  NS_METHOD OnHistoryPurge(int32_t aNumEntries, bool *_retval); \
  NS_METHOD OnHistoryReplaceEntry(int32_t aIndex); \
  NS_METHOD OnLengthChange(int32_t aCount); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSISHISTORYLISTENER(_to) \
  NS_IMETHOD OnHistoryNewEntry(nsIURI *aNewURI, int32_t aOldIndex) override { return _to OnHistoryNewEntry(aNewURI, aOldIndex); } \
  NS_IMETHOD OnHistoryGoBack(nsIURI *aBackURI, bool *_retval) override { return _to OnHistoryGoBack(aBackURI, _retval); } \
  NS_IMETHOD OnHistoryGoForward(nsIURI *aForwardURI, bool *_retval) override { return _to OnHistoryGoForward(aForwardURI, _retval); } \
  NS_IMETHOD OnHistoryReload(nsIURI *aReloadURI, uint32_t aReloadFlags, bool *_retval) override { return _to OnHistoryReload(aReloadURI, aReloadFlags, _retval); } \
  NS_IMETHOD OnHistoryGotoIndex(int32_t aIndex, nsIURI *aGotoURI, bool *_retval) override { return _to OnHistoryGotoIndex(aIndex, aGotoURI, _retval); } \
  NS_IMETHOD OnHistoryPurge(int32_t aNumEntries, bool *_retval) override { return _to OnHistoryPurge(aNumEntries, _retval); } \
  NS_IMETHOD OnHistoryReplaceEntry(int32_t aIndex) override { return _to OnHistoryReplaceEntry(aIndex); } \
  NS_IMETHOD OnLengthChange(int32_t aCount) override { return _to OnLengthChange(aCount); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSISHISTORYLISTENER(_to) \
  NS_IMETHOD OnHistoryNewEntry(nsIURI *aNewURI, int32_t aOldIndex) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnHistoryNewEntry(aNewURI, aOldIndex); } \
  NS_IMETHOD OnHistoryGoBack(nsIURI *aBackURI, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnHistoryGoBack(aBackURI, _retval); } \
  NS_IMETHOD OnHistoryGoForward(nsIURI *aForwardURI, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnHistoryGoForward(aForwardURI, _retval); } \
  NS_IMETHOD OnHistoryReload(nsIURI *aReloadURI, uint32_t aReloadFlags, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnHistoryReload(aReloadURI, aReloadFlags, _retval); } \
  NS_IMETHOD OnHistoryGotoIndex(int32_t aIndex, nsIURI *aGotoURI, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnHistoryGotoIndex(aIndex, aGotoURI, _retval); } \
  NS_IMETHOD OnHistoryPurge(int32_t aNumEntries, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnHistoryPurge(aNumEntries, _retval); } \
  NS_IMETHOD OnHistoryReplaceEntry(int32_t aIndex) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnHistoryReplaceEntry(aIndex); } \
  NS_IMETHOD OnLengthChange(int32_t aCount) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnLengthChange(aCount); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsSHistoryListener : public nsISHistoryListener
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSISHISTORYLISTENER

  nsSHistoryListener();

private:
  ~nsSHistoryListener();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsSHistoryListener, nsISHistoryListener)

nsSHistoryListener::nsSHistoryListener()
{
  /* member initializers and constructor code */
}

nsSHistoryListener::~nsSHistoryListener()
{
  /* destructor code */
}

/* void OnHistoryNewEntry (in nsIURI aNewURI, in long aOldIndex); */
NS_IMETHODIMP nsSHistoryListener::OnHistoryNewEntry(nsIURI *aNewURI, int32_t aOldIndex)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean OnHistoryGoBack (in nsIURI aBackURI); */
NS_IMETHODIMP nsSHistoryListener::OnHistoryGoBack(nsIURI *aBackURI, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean OnHistoryGoForward (in nsIURI aForwardURI); */
NS_IMETHODIMP nsSHistoryListener::OnHistoryGoForward(nsIURI *aForwardURI, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean OnHistoryReload (in nsIURI aReloadURI, in unsigned long aReloadFlags); */
NS_IMETHODIMP nsSHistoryListener::OnHistoryReload(nsIURI *aReloadURI, uint32_t aReloadFlags, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean OnHistoryGotoIndex (in long aIndex, in nsIURI aGotoURI); */
NS_IMETHODIMP nsSHistoryListener::OnHistoryGotoIndex(int32_t aIndex, nsIURI *aGotoURI, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean OnHistoryPurge (in long aNumEntries); */
NS_IMETHODIMP nsSHistoryListener::OnHistoryPurge(int32_t aNumEntries, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void OnHistoryReplaceEntry (in long aIndex); */
NS_IMETHODIMP nsSHistoryListener::OnHistoryReplaceEntry(int32_t aIndex)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void OnLengthChange (in long aCount); */
NS_IMETHODIMP nsSHistoryListener::OnLengthChange(int32_t aCount)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsISHistoryListener_h__ */
