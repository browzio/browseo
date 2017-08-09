/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsINamedPipeService.idl
 */

#ifndef __gen_nsINamedPipeService_h__
#define __gen_nsINamedPipeService_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

#ifndef __gen_nsrootidl_h__
#include "nsrootidl.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsINamedPipeDataObserver */
#define NS_INAMEDPIPEDATAOBSERVER_IID_STR "de4f460b-94fd-442c-9002-1637beb2185a"

#define NS_INAMEDPIPEDATAOBSERVER_IID \
  {0xde4f460b, 0x94fd, 0x442c, \
    { 0x90, 0x02, 0x16, 0x37, 0xbe, 0xb2, 0x18, 0x5a }}

class NS_NO_VTABLE nsINamedPipeDataObserver : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_INAMEDPIPEDATAOBSERVER_IID)

  /* void onDataAvailable (in unsigned long aBytesTransferred, in voidPtr aOverlapped); */
  NS_IMETHOD OnDataAvailable(uint32_t aBytesTransferred, void *aOverlapped) = 0;

  /* void onError (in unsigned long aError, in voidPtr aOverlapped); */
  NS_IMETHOD OnError(uint32_t aError, void *aOverlapped) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsINamedPipeDataObserver, NS_INAMEDPIPEDATAOBSERVER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSINAMEDPIPEDATAOBSERVER \
  NS_IMETHOD OnDataAvailable(uint32_t aBytesTransferred, void *aOverlapped) override; \
  NS_IMETHOD OnError(uint32_t aError, void *aOverlapped) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSINAMEDPIPEDATAOBSERVER \
  NS_METHOD OnDataAvailable(uint32_t aBytesTransferred, void *aOverlapped); \
  NS_METHOD OnError(uint32_t aError, void *aOverlapped); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSINAMEDPIPEDATAOBSERVER(_to) \
  NS_IMETHOD OnDataAvailable(uint32_t aBytesTransferred, void *aOverlapped) override { return _to OnDataAvailable(aBytesTransferred, aOverlapped); } \
  NS_IMETHOD OnError(uint32_t aError, void *aOverlapped) override { return _to OnError(aError, aOverlapped); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSINAMEDPIPEDATAOBSERVER(_to) \
  NS_IMETHOD OnDataAvailable(uint32_t aBytesTransferred, void *aOverlapped) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnDataAvailable(aBytesTransferred, aOverlapped); } \
  NS_IMETHOD OnError(uint32_t aError, void *aOverlapped) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnError(aError, aOverlapped); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsNamedPipeDataObserver : public nsINamedPipeDataObserver
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSINAMEDPIPEDATAOBSERVER

  nsNamedPipeDataObserver();

private:
  ~nsNamedPipeDataObserver();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsNamedPipeDataObserver, nsINamedPipeDataObserver)

nsNamedPipeDataObserver::nsNamedPipeDataObserver()
{
  /* member initializers and constructor code */
}

nsNamedPipeDataObserver::~nsNamedPipeDataObserver()
{
  /* destructor code */
}

/* void onDataAvailable (in unsigned long aBytesTransferred, in voidPtr aOverlapped); */
NS_IMETHODIMP nsNamedPipeDataObserver::OnDataAvailable(uint32_t aBytesTransferred, void *aOverlapped)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onError (in unsigned long aError, in voidPtr aOverlapped); */
NS_IMETHODIMP nsNamedPipeDataObserver::OnError(uint32_t aError, void *aOverlapped)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsINamedPipeService */
#define NS_INAMEDPIPESERVICE_IID_STR "1bf19133-5625-4ac8-836a-80b1c215f72b"

#define NS_INAMEDPIPESERVICE_IID \
  {0x1bf19133, 0x5625, 0x4ac8, \
    { 0x83, 0x6a, 0x80, 0xb1, 0xc2, 0x15, 0xf7, 0x2b }}

class NS_NO_VTABLE nsINamedPipeService : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_INAMEDPIPESERVICE_IID)

  /* void addDataObserver (in voidPtr aHandle, in nsINamedPipeDataObserver aObserver); */
  NS_IMETHOD AddDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver) = 0;

  /* void removeDataObserver (in voidPtr aHandle, in nsINamedPipeDataObserver aObserver); */
  NS_IMETHOD RemoveDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver) = 0;

  /* boolean isOnCurrentThread (); */
  NS_IMETHOD IsOnCurrentThread(bool *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsINamedPipeService, NS_INAMEDPIPESERVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSINAMEDPIPESERVICE \
  NS_IMETHOD AddDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver) override; \
  NS_IMETHOD RemoveDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver) override; \
  NS_IMETHOD IsOnCurrentThread(bool *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSINAMEDPIPESERVICE \
  NS_METHOD AddDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver); \
  NS_METHOD RemoveDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver); \
  NS_METHOD IsOnCurrentThread(bool *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSINAMEDPIPESERVICE(_to) \
  NS_IMETHOD AddDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver) override { return _to AddDataObserver(aHandle, aObserver); } \
  NS_IMETHOD RemoveDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver) override { return _to RemoveDataObserver(aHandle, aObserver); } \
  NS_IMETHOD IsOnCurrentThread(bool *_retval) override { return _to IsOnCurrentThread(_retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSINAMEDPIPESERVICE(_to) \
  NS_IMETHOD AddDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver) override { return !_to ? NS_ERROR_NULL_POINTER : _to->AddDataObserver(aHandle, aObserver); } \
  NS_IMETHOD RemoveDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RemoveDataObserver(aHandle, aObserver); } \
  NS_IMETHOD IsOnCurrentThread(bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->IsOnCurrentThread(_retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsNamedPipeService : public nsINamedPipeService
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSINAMEDPIPESERVICE

  nsNamedPipeService();

private:
  ~nsNamedPipeService();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsNamedPipeService, nsINamedPipeService)

nsNamedPipeService::nsNamedPipeService()
{
  /* member initializers and constructor code */
}

nsNamedPipeService::~nsNamedPipeService()
{
  /* destructor code */
}

/* void addDataObserver (in voidPtr aHandle, in nsINamedPipeDataObserver aObserver); */
NS_IMETHODIMP nsNamedPipeService::AddDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void removeDataObserver (in voidPtr aHandle, in nsINamedPipeDataObserver aObserver); */
NS_IMETHODIMP nsNamedPipeService::RemoveDataObserver(void *aHandle, nsINamedPipeDataObserver *aObserver)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean isOnCurrentThread (); */
NS_IMETHODIMP nsNamedPipeService::IsOnCurrentThread(bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsINamedPipeService_h__ */
