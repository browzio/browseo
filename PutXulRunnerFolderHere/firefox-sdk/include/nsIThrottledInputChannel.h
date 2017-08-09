/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIThrottledInputChannel.idl
 */

#ifndef __gen_nsIThrottledInputChannel_h__
#define __gen_nsIThrottledInputChannel_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIInputStream; /* forward declaration */

class nsIAsyncInputStream; /* forward declaration */


/* starting interface:    nsIInputChannelThrottleQueue */
#define NS_IINPUTCHANNELTHROTTLEQUEUE_IID_STR "6b4b96fe-3c67-4587-af7b-58b6b17da411"

#define NS_IINPUTCHANNELTHROTTLEQUEUE_IID \
  {0x6b4b96fe, 0x3c67, 0x4587, \
    { 0xaf, 0x7b, 0x58, 0xb6, 0xb1, 0x7d, 0xa4, 0x11 }}

class NS_NO_VTABLE nsIInputChannelThrottleQueue : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IINPUTCHANNELTHROTTLEQUEUE_IID)

  /* void init (in unsigned long aMeanBytesPerSecond, in unsigned long aMaxBytesPerSecond); */
  NS_IMETHOD Init(uint32_t aMeanBytesPerSecond, uint32_t aMaxBytesPerSecond) = 0;

  /* unsigned long available (in unsigned long aRemaining); */
  NS_IMETHOD Available(uint32_t aRemaining, uint32_t *_retval) = 0;

  /* void recordRead (in unsigned long aBytesRead); */
  NS_IMETHOD RecordRead(uint32_t aBytesRead) = 0;

  /* unsigned long long bytesProcessed (); */
  NS_IMETHOD BytesProcessed(uint64_t *_retval) = 0;

  /* nsIAsyncInputStream wrapStream (in nsIInputStream aInputStream); */
  NS_IMETHOD WrapStream(nsIInputStream *aInputStream, nsIAsyncInputStream * *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIInputChannelThrottleQueue, NS_IINPUTCHANNELTHROTTLEQUEUE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIINPUTCHANNELTHROTTLEQUEUE \
  NS_IMETHOD Init(uint32_t aMeanBytesPerSecond, uint32_t aMaxBytesPerSecond) override; \
  NS_IMETHOD Available(uint32_t aRemaining, uint32_t *_retval) override; \
  NS_IMETHOD RecordRead(uint32_t aBytesRead) override; \
  NS_IMETHOD BytesProcessed(uint64_t *_retval) override; \
  NS_IMETHOD WrapStream(nsIInputStream *aInputStream, nsIAsyncInputStream * *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIINPUTCHANNELTHROTTLEQUEUE \
  NS_METHOD Init(uint32_t aMeanBytesPerSecond, uint32_t aMaxBytesPerSecond); \
  NS_METHOD Available(uint32_t aRemaining, uint32_t *_retval); \
  NS_METHOD RecordRead(uint32_t aBytesRead); \
  NS_METHOD BytesProcessed(uint64_t *_retval); \
  NS_METHOD WrapStream(nsIInputStream *aInputStream, nsIAsyncInputStream * *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIINPUTCHANNELTHROTTLEQUEUE(_to) \
  NS_IMETHOD Init(uint32_t aMeanBytesPerSecond, uint32_t aMaxBytesPerSecond) override { return _to Init(aMeanBytesPerSecond, aMaxBytesPerSecond); } \
  NS_IMETHOD Available(uint32_t aRemaining, uint32_t *_retval) override { return _to Available(aRemaining, _retval); } \
  NS_IMETHOD RecordRead(uint32_t aBytesRead) override { return _to RecordRead(aBytesRead); } \
  NS_IMETHOD BytesProcessed(uint64_t *_retval) override { return _to BytesProcessed(_retval); } \
  NS_IMETHOD WrapStream(nsIInputStream *aInputStream, nsIAsyncInputStream * *_retval) override { return _to WrapStream(aInputStream, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIINPUTCHANNELTHROTTLEQUEUE(_to) \
  NS_IMETHOD Init(uint32_t aMeanBytesPerSecond, uint32_t aMaxBytesPerSecond) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Init(aMeanBytesPerSecond, aMaxBytesPerSecond); } \
  NS_IMETHOD Available(uint32_t aRemaining, uint32_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Available(aRemaining, _retval); } \
  NS_IMETHOD RecordRead(uint32_t aBytesRead) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RecordRead(aBytesRead); } \
  NS_IMETHOD BytesProcessed(uint64_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->BytesProcessed(_retval); } \
  NS_IMETHOD WrapStream(nsIInputStream *aInputStream, nsIAsyncInputStream * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->WrapStream(aInputStream, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsInputChannelThrottleQueue : public nsIInputChannelThrottleQueue
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIINPUTCHANNELTHROTTLEQUEUE

  nsInputChannelThrottleQueue();

private:
  ~nsInputChannelThrottleQueue();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsInputChannelThrottleQueue, nsIInputChannelThrottleQueue)

nsInputChannelThrottleQueue::nsInputChannelThrottleQueue()
{
  /* member initializers and constructor code */
}

nsInputChannelThrottleQueue::~nsInputChannelThrottleQueue()
{
  /* destructor code */
}

/* void init (in unsigned long aMeanBytesPerSecond, in unsigned long aMaxBytesPerSecond); */
NS_IMETHODIMP nsInputChannelThrottleQueue::Init(uint32_t aMeanBytesPerSecond, uint32_t aMaxBytesPerSecond)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* unsigned long available (in unsigned long aRemaining); */
NS_IMETHODIMP nsInputChannelThrottleQueue::Available(uint32_t aRemaining, uint32_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void recordRead (in unsigned long aBytesRead); */
NS_IMETHODIMP nsInputChannelThrottleQueue::RecordRead(uint32_t aBytesRead)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* unsigned long long bytesProcessed (); */
NS_IMETHODIMP nsInputChannelThrottleQueue::BytesProcessed(uint64_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsIAsyncInputStream wrapStream (in nsIInputStream aInputStream); */
NS_IMETHODIMP nsInputChannelThrottleQueue::WrapStream(nsIInputStream *aInputStream, nsIAsyncInputStream * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIThrottledInputChannel */
#define NS_ITHROTTLEDINPUTCHANNEL_IID_STR "0a32a100-c031-45b6-9e8b-0444c7d4a143"

#define NS_ITHROTTLEDINPUTCHANNEL_IID \
  {0x0a32a100, 0xc031, 0x45b6, \
    { 0x9e, 0x8b, 0x04, 0x44, 0xc7, 0xd4, 0xa1, 0x43 }}

class NS_NO_VTABLE nsIThrottledInputChannel : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ITHROTTLEDINPUTCHANNEL_IID)

  /* attribute nsIInputChannelThrottleQueue throttleQueue; */
  NS_IMETHOD GetThrottleQueue(nsIInputChannelThrottleQueue * *aThrottleQueue) = 0;
  NS_IMETHOD SetThrottleQueue(nsIInputChannelThrottleQueue *aThrottleQueue) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIThrottledInputChannel, NS_ITHROTTLEDINPUTCHANNEL_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSITHROTTLEDINPUTCHANNEL \
  NS_IMETHOD GetThrottleQueue(nsIInputChannelThrottleQueue * *aThrottleQueue) override; \
  NS_IMETHOD SetThrottleQueue(nsIInputChannelThrottleQueue *aThrottleQueue) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSITHROTTLEDINPUTCHANNEL \
  NS_METHOD GetThrottleQueue(nsIInputChannelThrottleQueue * *aThrottleQueue); \
  NS_METHOD SetThrottleQueue(nsIInputChannelThrottleQueue *aThrottleQueue); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSITHROTTLEDINPUTCHANNEL(_to) \
  NS_IMETHOD GetThrottleQueue(nsIInputChannelThrottleQueue * *aThrottleQueue) override { return _to GetThrottleQueue(aThrottleQueue); } \
  NS_IMETHOD SetThrottleQueue(nsIInputChannelThrottleQueue *aThrottleQueue) override { return _to SetThrottleQueue(aThrottleQueue); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSITHROTTLEDINPUTCHANNEL(_to) \
  NS_IMETHOD GetThrottleQueue(nsIInputChannelThrottleQueue * *aThrottleQueue) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetThrottleQueue(aThrottleQueue); } \
  NS_IMETHOD SetThrottleQueue(nsIInputChannelThrottleQueue *aThrottleQueue) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetThrottleQueue(aThrottleQueue); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsThrottledInputChannel : public nsIThrottledInputChannel
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSITHROTTLEDINPUTCHANNEL

  nsThrottledInputChannel();

private:
  ~nsThrottledInputChannel();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsThrottledInputChannel, nsIThrottledInputChannel)

nsThrottledInputChannel::nsThrottledInputChannel()
{
  /* member initializers and constructor code */
}

nsThrottledInputChannel::~nsThrottledInputChannel()
{
  /* destructor code */
}

/* attribute nsIInputChannelThrottleQueue throttleQueue; */
NS_IMETHODIMP nsThrottledInputChannel::GetThrottleQueue(nsIInputChannelThrottleQueue * *aThrottleQueue)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsThrottledInputChannel::SetThrottleQueue(nsIInputChannelThrottleQueue *aThrottleQueue)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIThrottledInputChannel_h__ */
