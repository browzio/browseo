/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsINSSU2FToken.idl
 */

#ifndef __gen_nsINSSU2FToken_h__
#define __gen_nsINSSU2FToken_h__


#ifndef __gen_nsIU2FToken_h__
#include "nsIU2FToken.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsINSSU2FToken */
#define NS_INSSU2FTOKEN_IID_STR "d9104a00-140b-4f86-a4b0-4998878ef4e6"

#define NS_INSSU2FTOKEN_IID \
  {0xd9104a00, 0x140b, 0x4f86, \
    { 0xa4, 0xb0, 0x49, 0x98, 0x87, 0x8e, 0xf4, 0xe6 }}

class NS_NO_VTABLE nsINSSU2FToken : public nsIU2FToken {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_INSSU2FTOKEN_IID)

  /* void init (); */
  NS_IMETHOD Init(void) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsINSSU2FToken, NS_INSSU2FTOKEN_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSINSSU2FTOKEN \
  NS_IMETHOD Init(void) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSINSSU2FTOKEN \
  NS_METHOD Init(void); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSINSSU2FTOKEN(_to) \
  NS_IMETHOD Init(void) override { return _to Init(); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSINSSU2FTOKEN(_to) \
  NS_IMETHOD Init(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Init(); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsNSSU2FToken : public nsINSSU2FToken
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSINSSU2FTOKEN

  nsNSSU2FToken();

private:
  ~nsNSSU2FToken();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsNSSU2FToken, nsINSSU2FToken)

nsNSSU2FToken::nsNSSU2FToken()
{
  /* member initializers and constructor code */
}

nsNSSU2FToken::~nsNSSU2FToken()
{
  /* destructor code */
}

/* void init (); */
NS_IMETHODIMP nsNSSU2FToken::Init()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif

#define NS_NSSU2FTOKEN_CONTRACTID  "@mozilla.org/dom/u2f/nss-u2f-token;1"

#endif /* __gen_nsINSSU2FToken_h__ */
