/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsITaskbarPreviewController.idl
 */

#ifndef __gen_nsITaskbarPreviewController_h__
#define __gen_nsITaskbarPreviewController_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIDocShell; /* forward declaration */

class nsITaskbarPreview; /* forward declaration */

class nsITaskbarPreviewButton; /* forward declaration */


/* starting interface:    nsITaskbarPreviewCallback */
#define NS_ITASKBARPREVIEWCALLBACK_IID_STR "f3744696-320d-4804-9c27-6a84c29acaa6"

#define NS_ITASKBARPREVIEWCALLBACK_IID \
  {0xf3744696, 0x320d, 0x4804, \
    { 0x9c, 0x27, 0x6a, 0x84, 0xc2, 0x9a, 0xca, 0xa6 }}

class NS_NO_VTABLE nsITaskbarPreviewCallback : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ITASKBARPREVIEWCALLBACK_IID)

  /* void done (in nsISupports aCanvas, in boolean aDrawBorder); */
  NS_IMETHOD Done(nsISupports *aCanvas, bool aDrawBorder) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsITaskbarPreviewCallback, NS_ITASKBARPREVIEWCALLBACK_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSITASKBARPREVIEWCALLBACK \
  NS_IMETHOD Done(nsISupports *aCanvas, bool aDrawBorder) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSITASKBARPREVIEWCALLBACK \
  NS_METHOD Done(nsISupports *aCanvas, bool aDrawBorder); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSITASKBARPREVIEWCALLBACK(_to) \
  NS_IMETHOD Done(nsISupports *aCanvas, bool aDrawBorder) override { return _to Done(aCanvas, aDrawBorder); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSITASKBARPREVIEWCALLBACK(_to) \
  NS_IMETHOD Done(nsISupports *aCanvas, bool aDrawBorder) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Done(aCanvas, aDrawBorder); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsTaskbarPreviewCallback : public nsITaskbarPreviewCallback
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSITASKBARPREVIEWCALLBACK

  nsTaskbarPreviewCallback();

private:
  ~nsTaskbarPreviewCallback();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsTaskbarPreviewCallback, nsITaskbarPreviewCallback)

nsTaskbarPreviewCallback::nsTaskbarPreviewCallback()
{
  /* member initializers and constructor code */
}

nsTaskbarPreviewCallback::~nsTaskbarPreviewCallback()
{
  /* destructor code */
}

/* void done (in nsISupports aCanvas, in boolean aDrawBorder); */
NS_IMETHODIMP nsTaskbarPreviewCallback::Done(nsISupports *aCanvas, bool aDrawBorder)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsITaskbarPreviewController */
#define NS_ITASKBARPREVIEWCONTROLLER_IID_STR "8b427646-e446-4941-ae0b-c1122a173a68"

#define NS_ITASKBARPREVIEWCONTROLLER_IID \
  {0x8b427646, 0xe446, 0x4941, \
    { 0xae, 0x0b, 0xc1, 0x12, 0x2a, 0x17, 0x3a, 0x68 }}

class NS_NO_VTABLE nsITaskbarPreviewController : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ITASKBARPREVIEWCONTROLLER_IID)

  /* readonly attribute unsigned long width; */
  NS_IMETHOD GetWidth(uint32_t *aWidth) = 0;

  /* readonly attribute unsigned long height; */
  NS_IMETHOD GetHeight(uint32_t *aHeight) = 0;

  /* readonly attribute float thumbnailAspectRatio; */
  NS_IMETHOD GetThumbnailAspectRatio(float *aThumbnailAspectRatio) = 0;

  /* [deprecated] boolean drawPreview (in nsISupports ctx); */
  NS_IMETHOD DrawPreview(nsISupports *ctx, bool *_retval) = 0;

  /* [deprecated] boolean drawThumbnail (in nsISupports ctx, in unsigned long width, in unsigned long height); */
  NS_IMETHOD DrawThumbnail(nsISupports *ctx, uint32_t width, uint32_t height, bool *_retval) = 0;

  /* void requestPreview (in nsITaskbarPreviewCallback aCallback); */
  NS_IMETHOD RequestPreview(nsITaskbarPreviewCallback *aCallback) = 0;

  /* void requestThumbnail (in nsITaskbarPreviewCallback aCallback, in unsigned long width, in unsigned long height); */
  NS_IMETHOD RequestThumbnail(nsITaskbarPreviewCallback *aCallback, uint32_t width, uint32_t height) = 0;

  /* void onClose (); */
  NS_IMETHOD OnClose(void) = 0;

  /* boolean onActivate (); */
  NS_IMETHOD OnActivate(bool *_retval) = 0;

  /* void onClick (in nsITaskbarPreviewButton button); */
  NS_IMETHOD OnClick(nsITaskbarPreviewButton *button) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsITaskbarPreviewController, NS_ITASKBARPREVIEWCONTROLLER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSITASKBARPREVIEWCONTROLLER \
  NS_IMETHOD GetWidth(uint32_t *aWidth) override; \
  NS_IMETHOD GetHeight(uint32_t *aHeight) override; \
  NS_IMETHOD GetThumbnailAspectRatio(float *aThumbnailAspectRatio) override; \
  NS_IMETHOD DrawPreview(nsISupports *ctx, bool *_retval) override; \
  NS_IMETHOD DrawThumbnail(nsISupports *ctx, uint32_t width, uint32_t height, bool *_retval) override; \
  NS_IMETHOD RequestPreview(nsITaskbarPreviewCallback *aCallback) override; \
  NS_IMETHOD RequestThumbnail(nsITaskbarPreviewCallback *aCallback, uint32_t width, uint32_t height) override; \
  NS_IMETHOD OnClose(void) override; \
  NS_IMETHOD OnActivate(bool *_retval) override; \
  NS_IMETHOD OnClick(nsITaskbarPreviewButton *button) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSITASKBARPREVIEWCONTROLLER \
  NS_METHOD GetWidth(uint32_t *aWidth); \
  NS_METHOD GetHeight(uint32_t *aHeight); \
  NS_METHOD GetThumbnailAspectRatio(float *aThumbnailAspectRatio); \
  NS_METHOD DrawPreview(nsISupports *ctx, bool *_retval); \
  NS_METHOD DrawThumbnail(nsISupports *ctx, uint32_t width, uint32_t height, bool *_retval); \
  NS_METHOD RequestPreview(nsITaskbarPreviewCallback *aCallback); \
  NS_METHOD RequestThumbnail(nsITaskbarPreviewCallback *aCallback, uint32_t width, uint32_t height); \
  NS_METHOD OnClose(void); \
  NS_METHOD OnActivate(bool *_retval); \
  NS_METHOD OnClick(nsITaskbarPreviewButton *button); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSITASKBARPREVIEWCONTROLLER(_to) \
  NS_IMETHOD GetWidth(uint32_t *aWidth) override { return _to GetWidth(aWidth); } \
  NS_IMETHOD GetHeight(uint32_t *aHeight) override { return _to GetHeight(aHeight); } \
  NS_IMETHOD GetThumbnailAspectRatio(float *aThumbnailAspectRatio) override { return _to GetThumbnailAspectRatio(aThumbnailAspectRatio); } \
  NS_IMETHOD DrawPreview(nsISupports *ctx, bool *_retval) override { return _to DrawPreview(ctx, _retval); } \
  NS_IMETHOD DrawThumbnail(nsISupports *ctx, uint32_t width, uint32_t height, bool *_retval) override { return _to DrawThumbnail(ctx, width, height, _retval); } \
  NS_IMETHOD RequestPreview(nsITaskbarPreviewCallback *aCallback) override { return _to RequestPreview(aCallback); } \
  NS_IMETHOD RequestThumbnail(nsITaskbarPreviewCallback *aCallback, uint32_t width, uint32_t height) override { return _to RequestThumbnail(aCallback, width, height); } \
  NS_IMETHOD OnClose(void) override { return _to OnClose(); } \
  NS_IMETHOD OnActivate(bool *_retval) override { return _to OnActivate(_retval); } \
  NS_IMETHOD OnClick(nsITaskbarPreviewButton *button) override { return _to OnClick(button); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSITASKBARPREVIEWCONTROLLER(_to) \
  NS_IMETHOD GetWidth(uint32_t *aWidth) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetWidth(aWidth); } \
  NS_IMETHOD GetHeight(uint32_t *aHeight) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetHeight(aHeight); } \
  NS_IMETHOD GetThumbnailAspectRatio(float *aThumbnailAspectRatio) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetThumbnailAspectRatio(aThumbnailAspectRatio); } \
  NS_IMETHOD DrawPreview(nsISupports *ctx, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->DrawPreview(ctx, _retval); } \
  NS_IMETHOD DrawThumbnail(nsISupports *ctx, uint32_t width, uint32_t height, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->DrawThumbnail(ctx, width, height, _retval); } \
  NS_IMETHOD RequestPreview(nsITaskbarPreviewCallback *aCallback) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RequestPreview(aCallback); } \
  NS_IMETHOD RequestThumbnail(nsITaskbarPreviewCallback *aCallback, uint32_t width, uint32_t height) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RequestThumbnail(aCallback, width, height); } \
  NS_IMETHOD OnClose(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnClose(); } \
  NS_IMETHOD OnActivate(bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnActivate(_retval); } \
  NS_IMETHOD OnClick(nsITaskbarPreviewButton *button) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnClick(button); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsTaskbarPreviewController : public nsITaskbarPreviewController
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSITASKBARPREVIEWCONTROLLER

  nsTaskbarPreviewController();

private:
  ~nsTaskbarPreviewController();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsTaskbarPreviewController, nsITaskbarPreviewController)

nsTaskbarPreviewController::nsTaskbarPreviewController()
{
  /* member initializers and constructor code */
}

nsTaskbarPreviewController::~nsTaskbarPreviewController()
{
  /* destructor code */
}

/* readonly attribute unsigned long width; */
NS_IMETHODIMP nsTaskbarPreviewController::GetWidth(uint32_t *aWidth)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute unsigned long height; */
NS_IMETHODIMP nsTaskbarPreviewController::GetHeight(uint32_t *aHeight)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute float thumbnailAspectRatio; */
NS_IMETHODIMP nsTaskbarPreviewController::GetThumbnailAspectRatio(float *aThumbnailAspectRatio)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [deprecated] boolean drawPreview (in nsISupports ctx); */
NS_IMETHODIMP nsTaskbarPreviewController::DrawPreview(nsISupports *ctx, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [deprecated] boolean drawThumbnail (in nsISupports ctx, in unsigned long width, in unsigned long height); */
NS_IMETHODIMP nsTaskbarPreviewController::DrawThumbnail(nsISupports *ctx, uint32_t width, uint32_t height, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void requestPreview (in nsITaskbarPreviewCallback aCallback); */
NS_IMETHODIMP nsTaskbarPreviewController::RequestPreview(nsITaskbarPreviewCallback *aCallback)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void requestThumbnail (in nsITaskbarPreviewCallback aCallback, in unsigned long width, in unsigned long height); */
NS_IMETHODIMP nsTaskbarPreviewController::RequestThumbnail(nsITaskbarPreviewCallback *aCallback, uint32_t width, uint32_t height)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onClose (); */
NS_IMETHODIMP nsTaskbarPreviewController::OnClose()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean onActivate (); */
NS_IMETHODIMP nsTaskbarPreviewController::OnActivate(bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onClick (in nsITaskbarPreviewButton button); */
NS_IMETHODIMP nsTaskbarPreviewController::OnClick(nsITaskbarPreviewButton *button)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsITaskbarPreviewController_h__ */
