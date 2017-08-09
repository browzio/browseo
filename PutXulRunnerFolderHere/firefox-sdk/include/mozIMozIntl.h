/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\mozIMozIntl.idl
 */

#ifndef __gen_mozIMozIntl_h__
#define __gen_mozIMozIntl_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

#include "js/Value.h"

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    mozIMozIntl */
#define MOZIMOZINTL_IID_STR "9f9bc42e-54f4-11e6-9aed-4b1429ac0ba0"

#define MOZIMOZINTL_IID \
  {0x9f9bc42e, 0x54f4, 0x11e6, \
    { 0x9a, 0xed, 0x4b, 0x14, 0x29, 0xac, 0x0b, 0xa0 }}

class NS_NO_VTABLE mozIMozIntl : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(MOZIMOZINTL_IID)

  /* [implicit_jscontext] void addGetCalendarInfo (in jsval intlObject); */
  NS_IMETHOD AddGetCalendarInfo(JS::HandleValue intlObject, JSContext* cx) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(mozIMozIntl, MOZIMOZINTL_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_MOZIMOZINTL \
  NS_IMETHOD AddGetCalendarInfo(JS::HandleValue intlObject, JSContext* cx) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_MOZIMOZINTL \
  NS_METHOD AddGetCalendarInfo(JS::HandleValue intlObject, JSContext* cx); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_MOZIMOZINTL(_to) \
  NS_IMETHOD AddGetCalendarInfo(JS::HandleValue intlObject, JSContext* cx) override { return _to AddGetCalendarInfo(intlObject, cx); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_MOZIMOZINTL(_to) \
  NS_IMETHOD AddGetCalendarInfo(JS::HandleValue intlObject, JSContext* cx) override { return !_to ? NS_ERROR_NULL_POINTER : _to->AddGetCalendarInfo(intlObject, cx); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class _MYCLASS_ : public mozIMozIntl
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_MOZIMOZINTL

  _MYCLASS_();

private:
  ~_MYCLASS_();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(_MYCLASS_, mozIMozIntl)

_MYCLASS_::_MYCLASS_()
{
  /* member initializers and constructor code */
}

_MYCLASS_::~_MYCLASS_()
{
  /* destructor code */
}

/* [implicit_jscontext] void addGetCalendarInfo (in jsval intlObject); */
NS_IMETHODIMP _MYCLASS_::AddGetCalendarInfo(JS::HandleValue intlObject, JSContext* cx)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_mozIMozIntl_h__ */
