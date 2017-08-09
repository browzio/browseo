/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsITabParent.idl
 */

#ifndef __gen_nsITabParent_h__
#define __gen_nsITabParent_h__


#ifndef __gen_domstubs_h__
#include "domstubs.h"
#endif

#include "mozilla/Assertions.h"
#include "mozilla/DebugOnly.h"

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsITabParent */
#define NS_ITABPARENT_IID_STR "8e49f7b0-1f98-4939-bf91-e9c39cd56434"

#define NS_ITABPARENT_IID \
  {0x8e49f7b0, 0x1f98, 0x4939, \
    { 0xbf, 0x91, 0xe9, 0xc3, 0x9c, 0xd5, 0x64, 0x34 }}

class NS_NO_VTABLE nsITabParent : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ITABPARENT_IID)

  /* void getChildProcessOffset (out int32_t aCssX, out int32_t aCssY); */
  NS_IMETHOD GetChildProcessOffset(int32_t *aCssX, int32_t *aCssY) = 0;

  /* readonly attribute boolean useAsyncPanZoom; */
  NS_IMETHOD GetUseAsyncPanZoom(bool *aUseAsyncPanZoom) = 0;

  /* attribute boolean docShellIsActive; */
  NS_IMETHOD GetDocShellIsActive(bool *aDocShellIsActive) = 0;
  NS_IMETHOD SetDocShellIsActive(bool aDocShellIsActive) = 0;

  /* [infallible] readonly attribute boolean isPrerendered; */
  NS_IMETHOD GetIsPrerendered(bool *aIsPrerendered) = 0;
  inline bool GetIsPrerendered()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetIsPrerendered(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* void preserveLayers (in boolean aPreserveLayers); */
  NS_IMETHOD PreserveLayers(bool aPreserveLayers) = 0;

  /* void suppressDisplayport (in bool aEnabled); */
  NS_IMETHOD SuppressDisplayport(bool aEnabled) = 0;

  /* readonly attribute uint64_t tabId; */
  NS_IMETHOD GetTabId(uint64_t *aTabId) = 0;

  /* readonly attribute int32_t osPid; */
  NS_IMETHOD GetOsPid(int32_t *aOsPid) = 0;

  /* void navigateByKey (in bool aForward, in bool aForDocumentNavigation); */
  NS_IMETHOD NavigateByKey(bool aForward, bool aForDocumentNavigation) = 0;

  /* readonly attribute boolean hasContentOpener; */
  NS_IMETHOD GetHasContentOpener(bool *aHasContentOpener) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsITabParent, NS_ITABPARENT_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSITABPARENT \
  NS_IMETHOD GetChildProcessOffset(int32_t *aCssX, int32_t *aCssY) override; \
  NS_IMETHOD GetUseAsyncPanZoom(bool *aUseAsyncPanZoom) override; \
  NS_IMETHOD GetDocShellIsActive(bool *aDocShellIsActive) override; \
  NS_IMETHOD SetDocShellIsActive(bool aDocShellIsActive) override; \
  using nsITabParent::GetIsPrerendered; \
  NS_IMETHOD GetIsPrerendered(bool *aIsPrerendered) override; \
  NS_IMETHOD PreserveLayers(bool aPreserveLayers) override; \
  NS_IMETHOD SuppressDisplayport(bool aEnabled) override; \
  NS_IMETHOD GetTabId(uint64_t *aTabId) override; \
  NS_IMETHOD GetOsPid(int32_t *aOsPid) override; \
  NS_IMETHOD NavigateByKey(bool aForward, bool aForDocumentNavigation) override; \
  NS_IMETHOD GetHasContentOpener(bool *aHasContentOpener) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSITABPARENT \
  NS_METHOD GetChildProcessOffset(int32_t *aCssX, int32_t *aCssY); \
  NS_METHOD GetUseAsyncPanZoom(bool *aUseAsyncPanZoom); \
  NS_METHOD GetDocShellIsActive(bool *aDocShellIsActive); \
  NS_METHOD SetDocShellIsActive(bool aDocShellIsActive); \
  using nsITabParent::GetIsPrerendered; \
  NS_METHOD GetIsPrerendered(bool *aIsPrerendered); \
  NS_METHOD PreserveLayers(bool aPreserveLayers); \
  NS_METHOD SuppressDisplayport(bool aEnabled); \
  NS_METHOD GetTabId(uint64_t *aTabId); \
  NS_METHOD GetOsPid(int32_t *aOsPid); \
  NS_METHOD NavigateByKey(bool aForward, bool aForDocumentNavigation); \
  NS_METHOD GetHasContentOpener(bool *aHasContentOpener); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSITABPARENT(_to) \
  NS_IMETHOD GetChildProcessOffset(int32_t *aCssX, int32_t *aCssY) override { return _to GetChildProcessOffset(aCssX, aCssY); } \
  NS_IMETHOD GetUseAsyncPanZoom(bool *aUseAsyncPanZoom) override { return _to GetUseAsyncPanZoom(aUseAsyncPanZoom); } \
  NS_IMETHOD GetDocShellIsActive(bool *aDocShellIsActive) override { return _to GetDocShellIsActive(aDocShellIsActive); } \
  NS_IMETHOD SetDocShellIsActive(bool aDocShellIsActive) override { return _to SetDocShellIsActive(aDocShellIsActive); } \
  using nsITabParent::GetIsPrerendered; \
  NS_IMETHOD GetIsPrerendered(bool *aIsPrerendered) override { return _to GetIsPrerendered(aIsPrerendered); } \
  NS_IMETHOD PreserveLayers(bool aPreserveLayers) override { return _to PreserveLayers(aPreserveLayers); } \
  NS_IMETHOD SuppressDisplayport(bool aEnabled) override { return _to SuppressDisplayport(aEnabled); } \
  NS_IMETHOD GetTabId(uint64_t *aTabId) override { return _to GetTabId(aTabId); } \
  NS_IMETHOD GetOsPid(int32_t *aOsPid) override { return _to GetOsPid(aOsPid); } \
  NS_IMETHOD NavigateByKey(bool aForward, bool aForDocumentNavigation) override { return _to NavigateByKey(aForward, aForDocumentNavigation); } \
  NS_IMETHOD GetHasContentOpener(bool *aHasContentOpener) override { return _to GetHasContentOpener(aHasContentOpener); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSITABPARENT(_to) \
  NS_IMETHOD GetChildProcessOffset(int32_t *aCssX, int32_t *aCssY) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetChildProcessOffset(aCssX, aCssY); } \
  NS_IMETHOD GetUseAsyncPanZoom(bool *aUseAsyncPanZoom) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetUseAsyncPanZoom(aUseAsyncPanZoom); } \
  NS_IMETHOD GetDocShellIsActive(bool *aDocShellIsActive) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDocShellIsActive(aDocShellIsActive); } \
  NS_IMETHOD SetDocShellIsActive(bool aDocShellIsActive) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetDocShellIsActive(aDocShellIsActive); } \
  NS_IMETHOD GetIsPrerendered(bool *aIsPrerendered) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIsPrerendered(aIsPrerendered); } \
  NS_IMETHOD PreserveLayers(bool aPreserveLayers) override { return !_to ? NS_ERROR_NULL_POINTER : _to->PreserveLayers(aPreserveLayers); } \
  NS_IMETHOD SuppressDisplayport(bool aEnabled) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SuppressDisplayport(aEnabled); } \
  NS_IMETHOD GetTabId(uint64_t *aTabId) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTabId(aTabId); } \
  NS_IMETHOD GetOsPid(int32_t *aOsPid) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetOsPid(aOsPid); } \
  NS_IMETHOD NavigateByKey(bool aForward, bool aForDocumentNavigation) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NavigateByKey(aForward, aForDocumentNavigation); } \
  NS_IMETHOD GetHasContentOpener(bool *aHasContentOpener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetHasContentOpener(aHasContentOpener); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsTabParent : public nsITabParent
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSITABPARENT

  nsTabParent();

private:
  ~nsTabParent();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsTabParent, nsITabParent)

nsTabParent::nsTabParent()
{
  /* member initializers and constructor code */
}

nsTabParent::~nsTabParent()
{
  /* destructor code */
}

/* void getChildProcessOffset (out int32_t aCssX, out int32_t aCssY); */
NS_IMETHODIMP nsTabParent::GetChildProcessOffset(int32_t *aCssX, int32_t *aCssY)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute boolean useAsyncPanZoom; */
NS_IMETHODIMP nsTabParent::GetUseAsyncPanZoom(bool *aUseAsyncPanZoom)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* attribute boolean docShellIsActive; */
NS_IMETHODIMP nsTabParent::GetDocShellIsActive(bool *aDocShellIsActive)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsTabParent::SetDocShellIsActive(bool aDocShellIsActive)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean isPrerendered; */
NS_IMETHODIMP nsTabParent::GetIsPrerendered(bool *aIsPrerendered)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void preserveLayers (in boolean aPreserveLayers); */
NS_IMETHODIMP nsTabParent::PreserveLayers(bool aPreserveLayers)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void suppressDisplayport (in bool aEnabled); */
NS_IMETHODIMP nsTabParent::SuppressDisplayport(bool aEnabled)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute uint64_t tabId; */
NS_IMETHODIMP nsTabParent::GetTabId(uint64_t *aTabId)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute int32_t osPid; */
NS_IMETHODIMP nsTabParent::GetOsPid(int32_t *aOsPid)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void navigateByKey (in bool aForward, in bool aForDocumentNavigation); */
NS_IMETHODIMP nsTabParent::NavigateByKey(bool aForward, bool aForDocumentNavigation)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute boolean hasContentOpener; */
NS_IMETHODIMP nsTabParent::GetHasContentOpener(bool *aHasContentOpener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsITabParent_h__ */
