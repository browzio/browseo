/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIDOMCSSKeyframeRule.idl
 */

#ifndef __gen_nsIDOMCSSKeyframeRule_h__
#define __gen_nsIDOMCSSKeyframeRule_h__


#ifndef __gen_nsIDOMCSSRule_h__
#include "nsIDOMCSSRule.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIDOMCSSKeyframeRule */
#define NS_IDOMCSSKEYFRAMERULE_IID_STR "a281a8b4-eaa2-49a8-8b97-acc2814a57c9"

#define NS_IDOMCSSKEYFRAMERULE_IID \
  {0xa281a8b4, 0xeaa2, 0x49a8, \
    { 0x8b, 0x97, 0xac, 0xc2, 0x81, 0x4a, 0x57, 0xc9 }}

class NS_NO_VTABLE nsIDOMCSSKeyframeRule : public nsIDOMCSSRule {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IDOMCSSKEYFRAMERULE_IID)

  /* attribute DOMString keyText; */
  NS_IMETHOD GetKeyText(nsAString & aKeyText) = 0;
  NS_IMETHOD SetKeyText(const nsAString & aKeyText) = 0;

  /* readonly attribute nsIDOMCSSStyleDeclaration style; */
  NS_IMETHOD GetStyle(nsIDOMCSSStyleDeclaration * *aStyle) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIDOMCSSKeyframeRule, NS_IDOMCSSKEYFRAMERULE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIDOMCSSKEYFRAMERULE \
  NS_IMETHOD GetKeyText(nsAString & aKeyText) override; \
  NS_IMETHOD SetKeyText(const nsAString & aKeyText) override; \
  NS_IMETHOD GetStyle(nsIDOMCSSStyleDeclaration * *aStyle) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIDOMCSSKEYFRAMERULE \
  NS_METHOD GetKeyText(nsAString & aKeyText); \
  NS_METHOD SetKeyText(const nsAString & aKeyText); \
  NS_METHOD GetStyle(nsIDOMCSSStyleDeclaration * *aStyle); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIDOMCSSKEYFRAMERULE(_to) \
  NS_IMETHOD GetKeyText(nsAString & aKeyText) override { return _to GetKeyText(aKeyText); } \
  NS_IMETHOD SetKeyText(const nsAString & aKeyText) override { return _to SetKeyText(aKeyText); } \
  NS_IMETHOD GetStyle(nsIDOMCSSStyleDeclaration * *aStyle) override { return _to GetStyle(aStyle); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIDOMCSSKEYFRAMERULE(_to) \
  NS_IMETHOD GetKeyText(nsAString & aKeyText) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetKeyText(aKeyText); } \
  NS_IMETHOD SetKeyText(const nsAString & aKeyText) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetKeyText(aKeyText); } \
  NS_IMETHOD GetStyle(nsIDOMCSSStyleDeclaration * *aStyle) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetStyle(aStyle); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsDOMCSSKeyframeRule : public nsIDOMCSSKeyframeRule
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIDOMCSSKEYFRAMERULE

  nsDOMCSSKeyframeRule();

private:
  ~nsDOMCSSKeyframeRule();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsDOMCSSKeyframeRule, nsIDOMCSSKeyframeRule)

nsDOMCSSKeyframeRule::nsDOMCSSKeyframeRule()
{
  /* member initializers and constructor code */
}

nsDOMCSSKeyframeRule::~nsDOMCSSKeyframeRule()
{
  /* destructor code */
}

/* attribute DOMString keyText; */
NS_IMETHODIMP nsDOMCSSKeyframeRule::GetKeyText(nsAString & aKeyText)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsDOMCSSKeyframeRule::SetKeyText(const nsAString & aKeyText)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIDOMCSSStyleDeclaration style; */
NS_IMETHODIMP nsDOMCSSKeyframeRule::GetStyle(nsIDOMCSSStyleDeclaration * *aStyle)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIDOMCSSKeyframeRule_h__ */
