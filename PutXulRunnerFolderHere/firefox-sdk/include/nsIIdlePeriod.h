/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIIdlePeriod.idl
 */

#ifndef __gen_nsIIdlePeriod_h__
#define __gen_nsIIdlePeriod_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
namespace mozilla {
class TimeStamp;
}

/* starting interface:    nsIIdlePeriod */
#define NS_IIDLEPERIOD_IID_STR "21dd35a2-eae9-4bd8-b470-0dfa35a0e3b9"

#define NS_IIDLEPERIOD_IID \
  {0x21dd35a2, 0xeae9, 0x4bd8, \
    { 0xb4, 0x70, 0x0d, 0xfa, 0x35, 0xa0, 0xe3, 0xb9 }}

class NS_NO_VTABLE nsIIdlePeriod : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IIDLEPERIOD_IID)

  /* TimeStamp getIdlePeriodHint (); */
  NS_IMETHOD GetIdlePeriodHint(mozilla::TimeStamp *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIIdlePeriod, NS_IIDLEPERIOD_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIIDLEPERIOD \
  NS_IMETHOD GetIdlePeriodHint(mozilla::TimeStamp *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIIDLEPERIOD \
  NS_METHOD GetIdlePeriodHint(mozilla::TimeStamp *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIIDLEPERIOD(_to) \
  NS_IMETHOD GetIdlePeriodHint(mozilla::TimeStamp *_retval) override { return _to GetIdlePeriodHint(_retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIIDLEPERIOD(_to) \
  NS_IMETHOD GetIdlePeriodHint(mozilla::TimeStamp *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIdlePeriodHint(_retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsIdlePeriod : public nsIIdlePeriod
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIIDLEPERIOD

  nsIdlePeriod();

private:
  ~nsIdlePeriod();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsIdlePeriod, nsIIdlePeriod)

nsIdlePeriod::nsIdlePeriod()
{
  /* member initializers and constructor code */
}

nsIdlePeriod::~nsIdlePeriod()
{
  /* destructor code */
}

/* TimeStamp getIdlePeriodHint (); */
NS_IMETHODIMP nsIdlePeriod::GetIdlePeriodHint(mozilla::TimeStamp *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIIdlePeriod_h__ */
