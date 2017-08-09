/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIImageLoadingContent.idl
 */

#ifndef __gen_nsIImageLoadingContent_h__
#define __gen_nsIImageLoadingContent_h__


#ifndef __gen_imgINotificationObserver_h__
#include "imgINotificationObserver.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
#include "mozilla/Maybe.h"
#include "Visibility.h"
class imgIRequest; /* forward declaration */

class nsIChannel; /* forward declaration */

class nsIStreamListener; /* forward declaration */

class nsIURI; /* forward declaration */

class nsIDocument; /* forward declaration */

class nsIFrame; /* forward declaration */


/* starting interface:    nsIImageLoadingContent */
#define NS_IIMAGELOADINGCONTENT_IID_STR "0357123d-9224-4d12-a47e-868c32689777"

#define NS_IIMAGELOADINGCONTENT_IID \
  {0x0357123d, 0x9224, 0x4d12, \
    { 0xa4, 0x7e, 0x86, 0x8c, 0x32, 0x68, 0x97, 0x77 }}

class NS_NO_VTABLE nsIImageLoadingContent : public imgINotificationObserver {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IIMAGELOADINGCONTENT_IID)

  enum {
    UNKNOWN_REQUEST = -1,
    CURRENT_REQUEST = 0,
    PENDING_REQUEST = 1
  };

  /* attribute boolean loadingEnabled; */
  NS_IMETHOD GetLoadingEnabled(bool *aLoadingEnabled) = 0;
  NS_IMETHOD SetLoadingEnabled(bool aLoadingEnabled) = 0;

  /* readonly attribute short imageBlockingStatus; */
  NS_IMETHOD GetImageBlockingStatus(int16_t *aImageBlockingStatus) = 0;

  /* void addObserver (in imgINotificationObserver aObserver); */
  NS_IMETHOD AddObserver(imgINotificationObserver *aObserver) = 0;

  /* void removeObserver (in imgINotificationObserver aObserver); */
  NS_IMETHOD RemoveObserver(imgINotificationObserver *aObserver) = 0;

  /* imgIRequest getRequest (in long aRequestType); */
  NS_IMETHOD GetRequest(int32_t aRequestType, imgIRequest * *_retval) = 0;

  /* [noscript,notxpcom] boolean currentRequestHasSize (); */
  NS_IMETHOD_(bool) CurrentRequestHasSize(void) = 0;

  /* [notxpcom] void frameCreated (in nsIFrame aFrame); */
  NS_IMETHOD_(void) FrameCreated(nsIFrame *aFrame) = 0;

  /* [notxpcom] void frameDestroyed (in nsIFrame aFrame); */
  NS_IMETHOD_(void) FrameDestroyed(nsIFrame *aFrame) = 0;

  /* long getRequestType (in imgIRequest aRequest); */
  NS_IMETHOD GetRequestType(imgIRequest *aRequest, int32_t *_retval) = 0;

  /* readonly attribute nsIURI currentURI; */
  NS_IMETHOD GetCurrentURI(nsIURI * *aCurrentURI) = 0;

  /* nsIStreamListener loadImageWithChannel (in nsIChannel aChannel); */
  NS_IMETHOD LoadImageWithChannel(nsIChannel *aChannel, nsIStreamListener * *_retval) = 0;

  /* [optional_argc] void forceReload ([optional] in boolean aNotify); */
  NS_IMETHOD ForceReload(bool aNotify, uint8_t _argc) = 0;

  /* void forceImageState (in boolean aForce, in unsigned long long aState); */
  NS_IMETHOD ForceImageState(bool aForce, uint64_t aState) = 0;

  /* readonly attribute unsigned long naturalWidth; */
  NS_IMETHOD GetNaturalWidth(uint32_t *aNaturalWidth) = 0;

  /* readonly attribute unsigned long naturalHeight; */
  NS_IMETHOD GetNaturalHeight(uint32_t *aNaturalHeight) = 0;

  /* [noscript,notxpcom] void onVisibilityChange (in Visibility aNewVisibility, in MaybeOnNonvisible aNonvisibleAction); */
  NS_IMETHOD_(void) OnVisibilityChange(mozilla::Visibility aNewVisibility, const mozilla::Maybe<mozilla::OnNonvisible> & aNonvisibleAction) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIImageLoadingContent, NS_IIMAGELOADINGCONTENT_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIIMAGELOADINGCONTENT \
  NS_IMETHOD GetLoadingEnabled(bool *aLoadingEnabled) override; \
  NS_IMETHOD SetLoadingEnabled(bool aLoadingEnabled) override; \
  NS_IMETHOD GetImageBlockingStatus(int16_t *aImageBlockingStatus) override; \
  NS_IMETHOD AddObserver(imgINotificationObserver *aObserver) override; \
  NS_IMETHOD RemoveObserver(imgINotificationObserver *aObserver) override; \
  NS_IMETHOD GetRequest(int32_t aRequestType, imgIRequest * *_retval) override; \
  NS_IMETHOD_(bool) CurrentRequestHasSize(void) override; \
  NS_IMETHOD_(void) FrameCreated(nsIFrame *aFrame) override; \
  NS_IMETHOD_(void) FrameDestroyed(nsIFrame *aFrame) override; \
  NS_IMETHOD GetRequestType(imgIRequest *aRequest, int32_t *_retval) override; \
  NS_IMETHOD GetCurrentURI(nsIURI * *aCurrentURI) override; \
  NS_IMETHOD LoadImageWithChannel(nsIChannel *aChannel, nsIStreamListener * *_retval) override; \
  NS_IMETHOD ForceReload(bool aNotify, uint8_t _argc) override; \
  NS_IMETHOD ForceImageState(bool aForce, uint64_t aState) override; \
  NS_IMETHOD GetNaturalWidth(uint32_t *aNaturalWidth) override; \
  NS_IMETHOD GetNaturalHeight(uint32_t *aNaturalHeight) override; \
  NS_IMETHOD_(void) OnVisibilityChange(mozilla::Visibility aNewVisibility, const mozilla::Maybe<mozilla::OnNonvisible> & aNonvisibleAction) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIIMAGELOADINGCONTENT \
  NS_METHOD GetLoadingEnabled(bool *aLoadingEnabled); \
  NS_METHOD SetLoadingEnabled(bool aLoadingEnabled); \
  NS_METHOD GetImageBlockingStatus(int16_t *aImageBlockingStatus); \
  NS_METHOD AddObserver(imgINotificationObserver *aObserver); \
  NS_METHOD RemoveObserver(imgINotificationObserver *aObserver); \
  NS_METHOD GetRequest(int32_t aRequestType, imgIRequest * *_retval); \
  NS_METHOD_(bool) CurrentRequestHasSize(void); \
  NS_METHOD_(void) FrameCreated(nsIFrame *aFrame); \
  NS_METHOD_(void) FrameDestroyed(nsIFrame *aFrame); \
  NS_METHOD GetRequestType(imgIRequest *aRequest, int32_t *_retval); \
  NS_METHOD GetCurrentURI(nsIURI * *aCurrentURI); \
  NS_METHOD LoadImageWithChannel(nsIChannel *aChannel, nsIStreamListener * *_retval); \
  NS_METHOD ForceReload(bool aNotify, uint8_t _argc); \
  NS_METHOD ForceImageState(bool aForce, uint64_t aState); \
  NS_METHOD GetNaturalWidth(uint32_t *aNaturalWidth); \
  NS_METHOD GetNaturalHeight(uint32_t *aNaturalHeight); \
  NS_METHOD_(void) OnVisibilityChange(mozilla::Visibility aNewVisibility, const mozilla::Maybe<mozilla::OnNonvisible> & aNonvisibleAction); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIIMAGELOADINGCONTENT(_to) \
  NS_IMETHOD GetLoadingEnabled(bool *aLoadingEnabled) override { return _to GetLoadingEnabled(aLoadingEnabled); } \
  NS_IMETHOD SetLoadingEnabled(bool aLoadingEnabled) override { return _to SetLoadingEnabled(aLoadingEnabled); } \
  NS_IMETHOD GetImageBlockingStatus(int16_t *aImageBlockingStatus) override { return _to GetImageBlockingStatus(aImageBlockingStatus); } \
  NS_IMETHOD AddObserver(imgINotificationObserver *aObserver) override { return _to AddObserver(aObserver); } \
  NS_IMETHOD RemoveObserver(imgINotificationObserver *aObserver) override { return _to RemoveObserver(aObserver); } \
  NS_IMETHOD GetRequest(int32_t aRequestType, imgIRequest * *_retval) override { return _to GetRequest(aRequestType, _retval); } \
  NS_IMETHOD_(bool) CurrentRequestHasSize(void) override { return _to CurrentRequestHasSize(); } \
  NS_IMETHOD_(void) FrameCreated(nsIFrame *aFrame) override { return _to FrameCreated(aFrame); } \
  NS_IMETHOD_(void) FrameDestroyed(nsIFrame *aFrame) override { return _to FrameDestroyed(aFrame); } \
  NS_IMETHOD GetRequestType(imgIRequest *aRequest, int32_t *_retval) override { return _to GetRequestType(aRequest, _retval); } \
  NS_IMETHOD GetCurrentURI(nsIURI * *aCurrentURI) override { return _to GetCurrentURI(aCurrentURI); } \
  NS_IMETHOD LoadImageWithChannel(nsIChannel *aChannel, nsIStreamListener * *_retval) override { return _to LoadImageWithChannel(aChannel, _retval); } \
  NS_IMETHOD ForceReload(bool aNotify, uint8_t _argc) override { return _to ForceReload(aNotify, _argc); } \
  NS_IMETHOD ForceImageState(bool aForce, uint64_t aState) override { return _to ForceImageState(aForce, aState); } \
  NS_IMETHOD GetNaturalWidth(uint32_t *aNaturalWidth) override { return _to GetNaturalWidth(aNaturalWidth); } \
  NS_IMETHOD GetNaturalHeight(uint32_t *aNaturalHeight) override { return _to GetNaturalHeight(aNaturalHeight); } \
  NS_IMETHOD_(void) OnVisibilityChange(mozilla::Visibility aNewVisibility, const mozilla::Maybe<mozilla::OnNonvisible> & aNonvisibleAction) override { return _to OnVisibilityChange(aNewVisibility, aNonvisibleAction); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIIMAGELOADINGCONTENT(_to) \
  NS_IMETHOD GetLoadingEnabled(bool *aLoadingEnabled) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLoadingEnabled(aLoadingEnabled); } \
  NS_IMETHOD SetLoadingEnabled(bool aLoadingEnabled) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetLoadingEnabled(aLoadingEnabled); } \
  NS_IMETHOD GetImageBlockingStatus(int16_t *aImageBlockingStatus) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetImageBlockingStatus(aImageBlockingStatus); } \
  NS_IMETHOD AddObserver(imgINotificationObserver *aObserver) override { return !_to ? NS_ERROR_NULL_POINTER : _to->AddObserver(aObserver); } \
  NS_IMETHOD RemoveObserver(imgINotificationObserver *aObserver) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RemoveObserver(aObserver); } \
  NS_IMETHOD GetRequest(int32_t aRequestType, imgIRequest * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetRequest(aRequestType, _retval); } \
  NS_IMETHOD_(bool) CurrentRequestHasSize(void) override; \
  NS_IMETHOD_(void) FrameCreated(nsIFrame *aFrame) override; \
  NS_IMETHOD_(void) FrameDestroyed(nsIFrame *aFrame) override; \
  NS_IMETHOD GetRequestType(imgIRequest *aRequest, int32_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetRequestType(aRequest, _retval); } \
  NS_IMETHOD GetCurrentURI(nsIURI * *aCurrentURI) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCurrentURI(aCurrentURI); } \
  NS_IMETHOD LoadImageWithChannel(nsIChannel *aChannel, nsIStreamListener * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->LoadImageWithChannel(aChannel, _retval); } \
  NS_IMETHOD ForceReload(bool aNotify, uint8_t _argc) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ForceReload(aNotify, _argc); } \
  NS_IMETHOD ForceImageState(bool aForce, uint64_t aState) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ForceImageState(aForce, aState); } \
  NS_IMETHOD GetNaturalWidth(uint32_t *aNaturalWidth) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetNaturalWidth(aNaturalWidth); } \
  NS_IMETHOD GetNaturalHeight(uint32_t *aNaturalHeight) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetNaturalHeight(aNaturalHeight); } \
  NS_IMETHOD_(void) OnVisibilityChange(mozilla::Visibility aNewVisibility, const mozilla::Maybe<mozilla::OnNonvisible> & aNonvisibleAction) override; 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsImageLoadingContent : public nsIImageLoadingContent
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIIMAGELOADINGCONTENT

  nsImageLoadingContent();

private:
  ~nsImageLoadingContent();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsImageLoadingContent, nsIImageLoadingContent)

nsImageLoadingContent::nsImageLoadingContent()
{
  /* member initializers and constructor code */
}

nsImageLoadingContent::~nsImageLoadingContent()
{
  /* destructor code */
}

/* attribute boolean loadingEnabled; */
NS_IMETHODIMP nsImageLoadingContent::GetLoadingEnabled(bool *aLoadingEnabled)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsImageLoadingContent::SetLoadingEnabled(bool aLoadingEnabled)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute short imageBlockingStatus; */
NS_IMETHODIMP nsImageLoadingContent::GetImageBlockingStatus(int16_t *aImageBlockingStatus)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void addObserver (in imgINotificationObserver aObserver); */
NS_IMETHODIMP nsImageLoadingContent::AddObserver(imgINotificationObserver *aObserver)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void removeObserver (in imgINotificationObserver aObserver); */
NS_IMETHODIMP nsImageLoadingContent::RemoveObserver(imgINotificationObserver *aObserver)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* imgIRequest getRequest (in long aRequestType); */
NS_IMETHODIMP nsImageLoadingContent::GetRequest(int32_t aRequestType, imgIRequest * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript,notxpcom] boolean currentRequestHasSize (); */
NS_IMETHODIMP_(bool) nsImageLoadingContent::CurrentRequestHasSize()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [notxpcom] void frameCreated (in nsIFrame aFrame); */
NS_IMETHODIMP_(void) nsImageLoadingContent::FrameCreated(nsIFrame *aFrame)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [notxpcom] void frameDestroyed (in nsIFrame aFrame); */
NS_IMETHODIMP_(void) nsImageLoadingContent::FrameDestroyed(nsIFrame *aFrame)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* long getRequestType (in imgIRequest aRequest); */
NS_IMETHODIMP nsImageLoadingContent::GetRequestType(imgIRequest *aRequest, int32_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIURI currentURI; */
NS_IMETHODIMP nsImageLoadingContent::GetCurrentURI(nsIURI * *aCurrentURI)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsIStreamListener loadImageWithChannel (in nsIChannel aChannel); */
NS_IMETHODIMP nsImageLoadingContent::LoadImageWithChannel(nsIChannel *aChannel, nsIStreamListener * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [optional_argc] void forceReload ([optional] in boolean aNotify); */
NS_IMETHODIMP nsImageLoadingContent::ForceReload(bool aNotify, uint8_t _argc)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void forceImageState (in boolean aForce, in unsigned long long aState); */
NS_IMETHODIMP nsImageLoadingContent::ForceImageState(bool aForce, uint64_t aState)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute unsigned long naturalWidth; */
NS_IMETHODIMP nsImageLoadingContent::GetNaturalWidth(uint32_t *aNaturalWidth)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute unsigned long naturalHeight; */
NS_IMETHODIMP nsImageLoadingContent::GetNaturalHeight(uint32_t *aNaturalHeight)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript,notxpcom] void onVisibilityChange (in Visibility aNewVisibility, in MaybeOnNonvisible aNonvisibleAction); */
NS_IMETHODIMP_(void) nsImageLoadingContent::OnVisibilityChange(mozilla::Visibility aNewVisibility, const mozilla::Maybe<mozilla::OnNonvisible> & aNonvisibleAction)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIImageLoadingContent_h__ */
