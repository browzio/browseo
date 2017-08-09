/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIThread.idl
 */

#ifndef __gen_nsIThread_h__
#define __gen_nsIThread_h__


#ifndef __gen_nsIEventTarget_h__
#include "nsIEventTarget.h"
#endif

#ifndef __gen_nsIIdlePeriod_h__
#include "nsIIdlePeriod.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
#include "mozilla/AlreadyAddRefed.h"

/* starting interface:    nsIThread */
#define NS_ITHREAD_IID_STR "5801d193-29d1-4964-a6b7-70eb697ddf2b"

#define NS_ITHREAD_IID \
  {0x5801d193, 0x29d1, 0x4964, \
    { 0xa6, 0xb7, 0x70, 0xeb, 0x69, 0x7d, 0xdf, 0x2b }}

class NS_NO_VTABLE nsIThread : public nsIEventTarget {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ITHREAD_IID)

  /* [noscript] readonly attribute PRThread PRThread; */
  NS_IMETHOD GetPRThread(PRThread **aPRThread) = 0;

  /* [noscript] attribute boolean CanInvokeJS; */
  NS_IMETHOD GetCanInvokeJS(bool *aCanInvokeJS) = 0;
  NS_IMETHOD SetCanInvokeJS(bool aCanInvokeJS) = 0;

  /* void shutdown (); */
  NS_IMETHOD Shutdown(void) = 0;

  /* boolean hasPendingEvents (); */
  NS_IMETHOD HasPendingEvents(bool *_retval) = 0;

  /* boolean processNextEvent (in boolean mayWait); */
  NS_IMETHOD ProcessNextEvent(bool mayWait, bool *_retval) = 0;

  /* void asyncShutdown (); */
  NS_IMETHOD AsyncShutdown(void) = 0;

  /* [noscript] void registerIdlePeriod (in alreadyAddRefed_nsIIdlePeriod aIdlePeriod); */
  NS_IMETHOD RegisterIdlePeriod(already_AddRefed<nsIIdlePeriod> aIdlePeriod) = 0;

  /* [noscript] void idleDispatch (in alreadyAddRefed_nsIRunnable event); */
  NS_IMETHOD IdleDispatch(already_AddRefed<nsIRunnable> event) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIThread, NS_ITHREAD_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSITHREAD \
  NS_IMETHOD GetPRThread(PRThread **aPRThread) override; \
  NS_IMETHOD GetCanInvokeJS(bool *aCanInvokeJS) override; \
  NS_IMETHOD SetCanInvokeJS(bool aCanInvokeJS) override; \
  NS_IMETHOD Shutdown(void) override; \
  NS_IMETHOD HasPendingEvents(bool *_retval) override; \
  NS_IMETHOD ProcessNextEvent(bool mayWait, bool *_retval) override; \
  NS_IMETHOD AsyncShutdown(void) override; \
  NS_IMETHOD RegisterIdlePeriod(already_AddRefed<nsIIdlePeriod> aIdlePeriod) override; \
  NS_IMETHOD IdleDispatch(already_AddRefed<nsIRunnable> event) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSITHREAD \
  NS_METHOD GetPRThread(PRThread **aPRThread); \
  NS_METHOD GetCanInvokeJS(bool *aCanInvokeJS); \
  NS_METHOD SetCanInvokeJS(bool aCanInvokeJS); \
  NS_METHOD Shutdown(void); \
  NS_METHOD HasPendingEvents(bool *_retval); \
  NS_METHOD ProcessNextEvent(bool mayWait, bool *_retval); \
  NS_METHOD AsyncShutdown(void); \
  NS_METHOD RegisterIdlePeriod(already_AddRefed<nsIIdlePeriod> aIdlePeriod); \
  NS_METHOD IdleDispatch(already_AddRefed<nsIRunnable> event); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSITHREAD(_to) \
  NS_IMETHOD GetPRThread(PRThread **aPRThread) override { return _to GetPRThread(aPRThread); } \
  NS_IMETHOD GetCanInvokeJS(bool *aCanInvokeJS) override { return _to GetCanInvokeJS(aCanInvokeJS); } \
  NS_IMETHOD SetCanInvokeJS(bool aCanInvokeJS) override { return _to SetCanInvokeJS(aCanInvokeJS); } \
  NS_IMETHOD Shutdown(void) override { return _to Shutdown(); } \
  NS_IMETHOD HasPendingEvents(bool *_retval) override { return _to HasPendingEvents(_retval); } \
  NS_IMETHOD ProcessNextEvent(bool mayWait, bool *_retval) override { return _to ProcessNextEvent(mayWait, _retval); } \
  NS_IMETHOD AsyncShutdown(void) override { return _to AsyncShutdown(); } \
  NS_IMETHOD RegisterIdlePeriod(already_AddRefed<nsIIdlePeriod> aIdlePeriod) override { return _to RegisterIdlePeriod(aIdlePeriod); } \
  NS_IMETHOD IdleDispatch(already_AddRefed<nsIRunnable> event) override { return _to IdleDispatch(event); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSITHREAD(_to) \
  NS_IMETHOD GetPRThread(PRThread **aPRThread) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPRThread(aPRThread); } \
  NS_IMETHOD GetCanInvokeJS(bool *aCanInvokeJS) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCanInvokeJS(aCanInvokeJS); } \
  NS_IMETHOD SetCanInvokeJS(bool aCanInvokeJS) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetCanInvokeJS(aCanInvokeJS); } \
  NS_IMETHOD Shutdown(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Shutdown(); } \
  NS_IMETHOD HasPendingEvents(bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->HasPendingEvents(_retval); } \
  NS_IMETHOD ProcessNextEvent(bool mayWait, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ProcessNextEvent(mayWait, _retval); } \
  NS_IMETHOD AsyncShutdown(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->AsyncShutdown(); } \
  NS_IMETHOD RegisterIdlePeriod(already_AddRefed<nsIIdlePeriod> aIdlePeriod) override { return !_to ? NS_ERROR_NULL_POINTER : _to->RegisterIdlePeriod(aIdlePeriod); } \
  NS_IMETHOD IdleDispatch(already_AddRefed<nsIRunnable> event) override { return !_to ? NS_ERROR_NULL_POINTER : _to->IdleDispatch(event); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsThread : public nsIThread
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSITHREAD

  nsThread();

private:
  ~nsThread();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsThread, nsIThread)

nsThread::nsThread()
{
  /* member initializers and constructor code */
}

nsThread::~nsThread()
{
  /* destructor code */
}

/* [noscript] readonly attribute PRThread PRThread; */
NS_IMETHODIMP nsThread::GetPRThread(PRThread **aPRThread)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] attribute boolean CanInvokeJS; */
NS_IMETHODIMP nsThread::GetCanInvokeJS(bool *aCanInvokeJS)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsThread::SetCanInvokeJS(bool aCanInvokeJS)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void shutdown (); */
NS_IMETHODIMP nsThread::Shutdown()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean hasPendingEvents (); */
NS_IMETHODIMP nsThread::HasPendingEvents(bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean processNextEvent (in boolean mayWait); */
NS_IMETHODIMP nsThread::ProcessNextEvent(bool mayWait, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void asyncShutdown (); */
NS_IMETHODIMP nsThread::AsyncShutdown()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] void registerIdlePeriod (in alreadyAddRefed_nsIIdlePeriod aIdlePeriod); */
NS_IMETHODIMP nsThread::RegisterIdlePeriod(already_AddRefed<nsIIdlePeriod> aIdlePeriod)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] void idleDispatch (in alreadyAddRefed_nsIRunnable event); */
NS_IMETHODIMP nsThread::IdleDispatch(already_AddRefed<nsIRunnable> event)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIThread_h__ */
