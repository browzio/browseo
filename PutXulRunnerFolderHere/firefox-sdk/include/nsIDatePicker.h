/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIDatePicker.idl
 */

#ifndef __gen_nsIDatePicker_h__
#define __gen_nsIDatePicker_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class mozIDOMWindowProxy; /* forward declaration */


/* starting interface:    nsIDatePickerShownCallback */
#define NS_IDATEPICKERSHOWNCALLBACK_IID_STR "13388a28-1b0b-4218-a31b-588f7a4ec26c"

#define NS_IDATEPICKERSHOWNCALLBACK_IID \
  {0x13388a28, 0x1b0b, 0x4218, \
    { 0xa3, 0x1b, 0x58, 0x8f, 0x7a, 0x4e, 0xc2, 0x6c }}

class NS_NO_VTABLE nsIDatePickerShownCallback : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IDATEPICKERSHOWNCALLBACK_IID)

  /* void cancel (); */
  NS_IMETHOD Cancel(void) = 0;

  /* void done (in AString date); */
  NS_IMETHOD Done(const nsAString & date) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIDatePickerShownCallback, NS_IDATEPICKERSHOWNCALLBACK_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIDATEPICKERSHOWNCALLBACK \
  NS_IMETHOD Cancel(void) override; \
  NS_IMETHOD Done(const nsAString & date) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIDATEPICKERSHOWNCALLBACK \
  NS_METHOD Cancel(void); \
  NS_METHOD Done(const nsAString & date); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIDATEPICKERSHOWNCALLBACK(_to) \
  NS_IMETHOD Cancel(void) override { return _to Cancel(); } \
  NS_IMETHOD Done(const nsAString & date) override { return _to Done(date); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIDATEPICKERSHOWNCALLBACK(_to) \
  NS_IMETHOD Cancel(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Cancel(); } \
  NS_IMETHOD Done(const nsAString & date) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Done(date); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsDatePickerShownCallback : public nsIDatePickerShownCallback
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIDATEPICKERSHOWNCALLBACK

  nsDatePickerShownCallback();

private:
  ~nsDatePickerShownCallback();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsDatePickerShownCallback, nsIDatePickerShownCallback)

nsDatePickerShownCallback::nsDatePickerShownCallback()
{
  /* member initializers and constructor code */
}

nsDatePickerShownCallback::~nsDatePickerShownCallback()
{
  /* destructor code */
}

/* void cancel (); */
NS_IMETHODIMP nsDatePickerShownCallback::Cancel()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void done (in AString date); */
NS_IMETHODIMP nsDatePickerShownCallback::Done(const nsAString & date)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIDatePicker */
#define NS_IDATEPICKER_IID_STR "7becfc64-966b-4d53-87d2-9161f36bd3b3"

#define NS_IDATEPICKER_IID \
  {0x7becfc64, 0x966b, 0x4d53, \
    { 0x87, 0xd2, 0x91, 0x61, 0xf3, 0x6b, 0xd3, 0xb3 }}

class NS_NO_VTABLE nsIDatePicker : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IDATEPICKER_IID)

  /* void init (in mozIDOMWindowProxy parent, in AString title, in AString initialDate); */
  NS_IMETHOD Init(mozIDOMWindowProxy *parent, const nsAString & title, const nsAString & initialDate) = 0;

  /* void open (in nsIDatePickerShownCallback callback); */
  NS_IMETHOD Open(nsIDatePickerShownCallback *callback) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIDatePicker, NS_IDATEPICKER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIDATEPICKER \
  NS_IMETHOD Init(mozIDOMWindowProxy *parent, const nsAString & title, const nsAString & initialDate) override; \
  NS_IMETHOD Open(nsIDatePickerShownCallback *callback) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIDATEPICKER \
  NS_METHOD Init(mozIDOMWindowProxy *parent, const nsAString & title, const nsAString & initialDate); \
  NS_METHOD Open(nsIDatePickerShownCallback *callback); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIDATEPICKER(_to) \
  NS_IMETHOD Init(mozIDOMWindowProxy *parent, const nsAString & title, const nsAString & initialDate) override { return _to Init(parent, title, initialDate); } \
  NS_IMETHOD Open(nsIDatePickerShownCallback *callback) override { return _to Open(callback); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIDATEPICKER(_to) \
  NS_IMETHOD Init(mozIDOMWindowProxy *parent, const nsAString & title, const nsAString & initialDate) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Init(parent, title, initialDate); } \
  NS_IMETHOD Open(nsIDatePickerShownCallback *callback) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Open(callback); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsDatePicker : public nsIDatePicker
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIDATEPICKER

  nsDatePicker();

private:
  ~nsDatePicker();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsDatePicker, nsIDatePicker)

nsDatePicker::nsDatePicker()
{
  /* member initializers and constructor code */
}

nsDatePicker::~nsDatePicker()
{
  /* destructor code */
}

/* void init (in mozIDOMWindowProxy parent, in AString title, in AString initialDate); */
NS_IMETHODIMP nsDatePicker::Init(mozIDOMWindowProxy *parent, const nsAString & title, const nsAString & initialDate)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void open (in nsIDatePickerShownCallback callback); */
NS_IMETHODIMP nsDatePicker::Open(nsIDatePickerShownCallback *callback)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIDatePicker_h__ */
