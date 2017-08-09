/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIDroppedLinkHandler.idl
 */

#ifndef __gen_nsIDroppedLinkHandler_h__
#define __gen_nsIDroppedLinkHandler_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIDOMDragEvent; /* forward declaration */


/* starting interface:    nsIDroppedLinkItem */
#define NS_IDROPPEDLINKITEM_IID_STR "69e14f91-2e09-4ca6-a511-a715c99a2804"

#define NS_IDROPPEDLINKITEM_IID \
  {0x69e14f91, 0x2e09, 0x4ca6, \
    { 0xa5, 0x11, 0xa7, 0x15, 0xc9, 0x9a, 0x28, 0x04 }}

class NS_NO_VTABLE nsIDroppedLinkItem : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IDROPPEDLINKITEM_IID)

  /* readonly attribute DOMString url; */
  NS_IMETHOD GetUrl(nsAString & aUrl) = 0;

  /* readonly attribute DOMString name; */
  NS_IMETHOD GetName(nsAString & aName) = 0;

  /* readonly attribute DOMString type; */
  NS_IMETHOD GetType(nsAString & aType) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIDroppedLinkItem, NS_IDROPPEDLINKITEM_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIDROPPEDLINKITEM \
  NS_IMETHOD GetUrl(nsAString & aUrl) override; \
  NS_IMETHOD GetName(nsAString & aName) override; \
  NS_IMETHOD GetType(nsAString & aType) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIDROPPEDLINKITEM \
  NS_METHOD GetUrl(nsAString & aUrl); \
  NS_METHOD GetName(nsAString & aName); \
  NS_METHOD GetType(nsAString & aType); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIDROPPEDLINKITEM(_to) \
  NS_IMETHOD GetUrl(nsAString & aUrl) override { return _to GetUrl(aUrl); } \
  NS_IMETHOD GetName(nsAString & aName) override { return _to GetName(aName); } \
  NS_IMETHOD GetType(nsAString & aType) override { return _to GetType(aType); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIDROPPEDLINKITEM(_to) \
  NS_IMETHOD GetUrl(nsAString & aUrl) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetUrl(aUrl); } \
  NS_IMETHOD GetName(nsAString & aName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetName(aName); } \
  NS_IMETHOD GetType(nsAString & aType) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetType(aType); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsDroppedLinkItem : public nsIDroppedLinkItem
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIDROPPEDLINKITEM

  nsDroppedLinkItem();

private:
  ~nsDroppedLinkItem();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsDroppedLinkItem, nsIDroppedLinkItem)

nsDroppedLinkItem::nsDroppedLinkItem()
{
  /* member initializers and constructor code */
}

nsDroppedLinkItem::~nsDroppedLinkItem()
{
  /* destructor code */
}

/* readonly attribute DOMString url; */
NS_IMETHODIMP nsDroppedLinkItem::GetUrl(nsAString & aUrl)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute DOMString name; */
NS_IMETHODIMP nsDroppedLinkItem::GetName(nsAString & aName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute DOMString type; */
NS_IMETHODIMP nsDroppedLinkItem::GetType(nsAString & aType)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIDroppedLinkHandler */
#define NS_IDROPPEDLINKHANDLER_IID_STR "21b5c25a-28a9-47bd-8431-fa9116305ded"

#define NS_IDROPPEDLINKHANDLER_IID \
  {0x21b5c25a, 0x28a9, 0x47bd, \
    { 0x84, 0x31, 0xfa, 0x91, 0x16, 0x30, 0x5d, 0xed }}

class NS_NO_VTABLE nsIDroppedLinkHandler : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IDROPPEDLINKHANDLER_IID)

  /* boolean canDropLink (in nsIDOMDragEvent aEvent, in boolean aAllowSameDocument); */
  NS_IMETHOD CanDropLink(nsIDOMDragEvent *aEvent, bool aAllowSameDocument, bool *_retval) = 0;

  /* AString dropLink (in nsIDOMDragEvent aEvent, out AString aName, [optional] in boolean aDisallowInherit); */
  NS_IMETHOD DropLink(nsIDOMDragEvent *aEvent, nsAString & aName, bool aDisallowInherit, nsAString & _retval) = 0;

  /* void dropLinks (in nsIDOMDragEvent aEvent, [optional] in boolean aDisallowInherit, [optional] out unsigned long aCount, [array, size_is (aCount), retval] out nsIDroppedLinkItem aLinks); */
  NS_IMETHOD DropLinks(nsIDOMDragEvent *aEvent, bool aDisallowInherit, uint32_t *aCount, nsIDroppedLinkItem * **aLinks) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIDroppedLinkHandler, NS_IDROPPEDLINKHANDLER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIDROPPEDLINKHANDLER \
  NS_IMETHOD CanDropLink(nsIDOMDragEvent *aEvent, bool aAllowSameDocument, bool *_retval) override; \
  NS_IMETHOD DropLink(nsIDOMDragEvent *aEvent, nsAString & aName, bool aDisallowInherit, nsAString & _retval) override; \
  NS_IMETHOD DropLinks(nsIDOMDragEvent *aEvent, bool aDisallowInherit, uint32_t *aCount, nsIDroppedLinkItem * **aLinks) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIDROPPEDLINKHANDLER \
  NS_METHOD CanDropLink(nsIDOMDragEvent *aEvent, bool aAllowSameDocument, bool *_retval); \
  NS_METHOD DropLink(nsIDOMDragEvent *aEvent, nsAString & aName, bool aDisallowInherit, nsAString & _retval); \
  NS_METHOD DropLinks(nsIDOMDragEvent *aEvent, bool aDisallowInherit, uint32_t *aCount, nsIDroppedLinkItem * **aLinks); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIDROPPEDLINKHANDLER(_to) \
  NS_IMETHOD CanDropLink(nsIDOMDragEvent *aEvent, bool aAllowSameDocument, bool *_retval) override { return _to CanDropLink(aEvent, aAllowSameDocument, _retval); } \
  NS_IMETHOD DropLink(nsIDOMDragEvent *aEvent, nsAString & aName, bool aDisallowInherit, nsAString & _retval) override { return _to DropLink(aEvent, aName, aDisallowInherit, _retval); } \
  NS_IMETHOD DropLinks(nsIDOMDragEvent *aEvent, bool aDisallowInherit, uint32_t *aCount, nsIDroppedLinkItem * **aLinks) override { return _to DropLinks(aEvent, aDisallowInherit, aCount, aLinks); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIDROPPEDLINKHANDLER(_to) \
  NS_IMETHOD CanDropLink(nsIDOMDragEvent *aEvent, bool aAllowSameDocument, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CanDropLink(aEvent, aAllowSameDocument, _retval); } \
  NS_IMETHOD DropLink(nsIDOMDragEvent *aEvent, nsAString & aName, bool aDisallowInherit, nsAString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->DropLink(aEvent, aName, aDisallowInherit, _retval); } \
  NS_IMETHOD DropLinks(nsIDOMDragEvent *aEvent, bool aDisallowInherit, uint32_t *aCount, nsIDroppedLinkItem * **aLinks) override { return !_to ? NS_ERROR_NULL_POINTER : _to->DropLinks(aEvent, aDisallowInherit, aCount, aLinks); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsDroppedLinkHandler : public nsIDroppedLinkHandler
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIDROPPEDLINKHANDLER

  nsDroppedLinkHandler();

private:
  ~nsDroppedLinkHandler();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsDroppedLinkHandler, nsIDroppedLinkHandler)

nsDroppedLinkHandler::nsDroppedLinkHandler()
{
  /* member initializers and constructor code */
}

nsDroppedLinkHandler::~nsDroppedLinkHandler()
{
  /* destructor code */
}

/* boolean canDropLink (in nsIDOMDragEvent aEvent, in boolean aAllowSameDocument); */
NS_IMETHODIMP nsDroppedLinkHandler::CanDropLink(nsIDOMDragEvent *aEvent, bool aAllowSameDocument, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* AString dropLink (in nsIDOMDragEvent aEvent, out AString aName, [optional] in boolean aDisallowInherit); */
NS_IMETHODIMP nsDroppedLinkHandler::DropLink(nsIDOMDragEvent *aEvent, nsAString & aName, bool aDisallowInherit, nsAString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void dropLinks (in nsIDOMDragEvent aEvent, [optional] in boolean aDisallowInherit, [optional] out unsigned long aCount, [array, size_is (aCount), retval] out nsIDroppedLinkItem aLinks); */
NS_IMETHODIMP nsDroppedLinkHandler::DropLinks(nsIDOMDragEvent *aEvent, bool aDisallowInherit, uint32_t *aCount, nsIDroppedLinkItem * **aLinks)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIDroppedLinkHandler_h__ */
