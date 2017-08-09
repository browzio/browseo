/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIAccessibilityService.idl
 */

#ifndef __gen_nsIAccessibilityService_h__
#define __gen_nsIAccessibilityService_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIDOMNode; /* forward declaration */

class nsIAccessible; /* forward declaration */

class nsIWeakReference; /* forward declaration */

class nsIPresShell; /* forward declaration */

class nsIAccessiblePivot; /* forward declaration */


/* starting interface:    nsIAccessibilityService */
#define NS_IACCESSIBILITYSERVICE_IID_STR "9a6f80fe-25cc-405c-9f8f-25869bc9f94e"

#define NS_IACCESSIBILITYSERVICE_IID \
  {0x9a6f80fe, 0x25cc, 0x405c, \
    { 0x9f, 0x8f, 0x25, 0x86, 0x9b, 0xc9, 0xf9, 0x4e }}

class NS_NO_VTABLE nsIAccessibilityService : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IACCESSIBILITYSERVICE_IID)

  /* nsIAccessible getApplicationAccessible (); */
  NS_IMETHOD GetApplicationAccessible(nsIAccessible * *_retval) = 0;

  /* nsIAccessible getAccessibleFor (in nsIDOMNode aNode); */
  NS_IMETHOD GetAccessibleFor(nsIDOMNode *aNode, nsIAccessible * *_retval) = 0;

  /* AString getStringRole (in unsigned long aRole); */
  NS_IMETHOD GetStringRole(uint32_t aRole, nsAString & _retval) = 0;

  /* nsISupports getStringStates (in unsigned long aStates, in unsigned long aExtraStates); */
  NS_IMETHOD GetStringStates(uint32_t aStates, uint32_t aExtraStates, nsISupports * *_retval) = 0;

  /* AString getStringEventType (in unsigned long aEventType); */
  NS_IMETHOD GetStringEventType(uint32_t aEventType, nsAString & _retval) = 0;

  /* AString getStringRelationType (in unsigned long aRelationType); */
  NS_IMETHOD GetStringRelationType(uint32_t aRelationType, nsAString & _retval) = 0;

  /* nsIAccessible getAccessibleFromCache (in nsIDOMNode aNode); */
  NS_IMETHOD GetAccessibleFromCache(nsIDOMNode *aNode, nsIAccessible * *_retval) = 0;

  /* nsIAccessiblePivot createAccessiblePivot (in nsIAccessible aRoot); */
  NS_IMETHOD CreateAccessiblePivot(nsIAccessible *aRoot, nsIAccessiblePivot * *_retval) = 0;

  /* void setLogging (in ACString aModules); */
  NS_IMETHOD SetLogging(const nsACString & aModules) = 0;

  /* boolean isLogged (in AString aModule); */
  NS_IMETHOD IsLogged(const nsAString & aModule, bool *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAccessibilityService, NS_IACCESSIBILITYSERVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIACCESSIBILITYSERVICE \
  NS_IMETHOD GetApplicationAccessible(nsIAccessible * *_retval) override; \
  NS_IMETHOD GetAccessibleFor(nsIDOMNode *aNode, nsIAccessible * *_retval) override; \
  NS_IMETHOD GetStringRole(uint32_t aRole, nsAString & _retval) override; \
  NS_IMETHOD GetStringStates(uint32_t aStates, uint32_t aExtraStates, nsISupports * *_retval) override; \
  NS_IMETHOD GetStringEventType(uint32_t aEventType, nsAString & _retval) override; \
  NS_IMETHOD GetStringRelationType(uint32_t aRelationType, nsAString & _retval) override; \
  NS_IMETHOD GetAccessibleFromCache(nsIDOMNode *aNode, nsIAccessible * *_retval) override; \
  NS_IMETHOD CreateAccessiblePivot(nsIAccessible *aRoot, nsIAccessiblePivot * *_retval) override; \
  NS_IMETHOD SetLogging(const nsACString & aModules) override; \
  NS_IMETHOD IsLogged(const nsAString & aModule, bool *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIACCESSIBILITYSERVICE \
  NS_METHOD GetApplicationAccessible(nsIAccessible * *_retval); \
  NS_METHOD GetAccessibleFor(nsIDOMNode *aNode, nsIAccessible * *_retval); \
  NS_METHOD GetStringRole(uint32_t aRole, nsAString & _retval); \
  NS_METHOD GetStringStates(uint32_t aStates, uint32_t aExtraStates, nsISupports * *_retval); \
  NS_METHOD GetStringEventType(uint32_t aEventType, nsAString & _retval); \
  NS_METHOD GetStringRelationType(uint32_t aRelationType, nsAString & _retval); \
  NS_METHOD GetAccessibleFromCache(nsIDOMNode *aNode, nsIAccessible * *_retval); \
  NS_METHOD CreateAccessiblePivot(nsIAccessible *aRoot, nsIAccessiblePivot * *_retval); \
  NS_METHOD SetLogging(const nsACString & aModules); \
  NS_METHOD IsLogged(const nsAString & aModule, bool *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIACCESSIBILITYSERVICE(_to) \
  NS_IMETHOD GetApplicationAccessible(nsIAccessible * *_retval) override { return _to GetApplicationAccessible(_retval); } \
  NS_IMETHOD GetAccessibleFor(nsIDOMNode *aNode, nsIAccessible * *_retval) override { return _to GetAccessibleFor(aNode, _retval); } \
  NS_IMETHOD GetStringRole(uint32_t aRole, nsAString & _retval) override { return _to GetStringRole(aRole, _retval); } \
  NS_IMETHOD GetStringStates(uint32_t aStates, uint32_t aExtraStates, nsISupports * *_retval) override { return _to GetStringStates(aStates, aExtraStates, _retval); } \
  NS_IMETHOD GetStringEventType(uint32_t aEventType, nsAString & _retval) override { return _to GetStringEventType(aEventType, _retval); } \
  NS_IMETHOD GetStringRelationType(uint32_t aRelationType, nsAString & _retval) override { return _to GetStringRelationType(aRelationType, _retval); } \
  NS_IMETHOD GetAccessibleFromCache(nsIDOMNode *aNode, nsIAccessible * *_retval) override { return _to GetAccessibleFromCache(aNode, _retval); } \
  NS_IMETHOD CreateAccessiblePivot(nsIAccessible *aRoot, nsIAccessiblePivot * *_retval) override { return _to CreateAccessiblePivot(aRoot, _retval); } \
  NS_IMETHOD SetLogging(const nsACString & aModules) override { return _to SetLogging(aModules); } \
  NS_IMETHOD IsLogged(const nsAString & aModule, bool *_retval) override { return _to IsLogged(aModule, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIACCESSIBILITYSERVICE(_to) \
  NS_IMETHOD GetApplicationAccessible(nsIAccessible * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetApplicationAccessible(_retval); } \
  NS_IMETHOD GetAccessibleFor(nsIDOMNode *aNode, nsIAccessible * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAccessibleFor(aNode, _retval); } \
  NS_IMETHOD GetStringRole(uint32_t aRole, nsAString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetStringRole(aRole, _retval); } \
  NS_IMETHOD GetStringStates(uint32_t aStates, uint32_t aExtraStates, nsISupports * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetStringStates(aStates, aExtraStates, _retval); } \
  NS_IMETHOD GetStringEventType(uint32_t aEventType, nsAString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetStringEventType(aEventType, _retval); } \
  NS_IMETHOD GetStringRelationType(uint32_t aRelationType, nsAString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetStringRelationType(aRelationType, _retval); } \
  NS_IMETHOD GetAccessibleFromCache(nsIDOMNode *aNode, nsIAccessible * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAccessibleFromCache(aNode, _retval); } \
  NS_IMETHOD CreateAccessiblePivot(nsIAccessible *aRoot, nsIAccessiblePivot * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CreateAccessiblePivot(aRoot, _retval); } \
  NS_IMETHOD SetLogging(const nsACString & aModules) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetLogging(aModules); } \
  NS_IMETHOD IsLogged(const nsAString & aModule, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->IsLogged(aModule, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAccessibilityService : public nsIAccessibilityService
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIACCESSIBILITYSERVICE

  nsAccessibilityService();

private:
  ~nsAccessibilityService();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAccessibilityService, nsIAccessibilityService)

nsAccessibilityService::nsAccessibilityService()
{
  /* member initializers and constructor code */
}

nsAccessibilityService::~nsAccessibilityService()
{
  /* destructor code */
}

/* nsIAccessible getApplicationAccessible (); */
NS_IMETHODIMP nsAccessibilityService::GetApplicationAccessible(nsIAccessible * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsIAccessible getAccessibleFor (in nsIDOMNode aNode); */
NS_IMETHODIMP nsAccessibilityService::GetAccessibleFor(nsIDOMNode *aNode, nsIAccessible * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* AString getStringRole (in unsigned long aRole); */
NS_IMETHODIMP nsAccessibilityService::GetStringRole(uint32_t aRole, nsAString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsISupports getStringStates (in unsigned long aStates, in unsigned long aExtraStates); */
NS_IMETHODIMP nsAccessibilityService::GetStringStates(uint32_t aStates, uint32_t aExtraStates, nsISupports * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* AString getStringEventType (in unsigned long aEventType); */
NS_IMETHODIMP nsAccessibilityService::GetStringEventType(uint32_t aEventType, nsAString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* AString getStringRelationType (in unsigned long aRelationType); */
NS_IMETHODIMP nsAccessibilityService::GetStringRelationType(uint32_t aRelationType, nsAString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsIAccessible getAccessibleFromCache (in nsIDOMNode aNode); */
NS_IMETHODIMP nsAccessibilityService::GetAccessibleFromCache(nsIDOMNode *aNode, nsIAccessible * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsIAccessiblePivot createAccessiblePivot (in nsIAccessible aRoot); */
NS_IMETHODIMP nsAccessibilityService::CreateAccessiblePivot(nsIAccessible *aRoot, nsIAccessiblePivot * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void setLogging (in ACString aModules); */
NS_IMETHODIMP nsAccessibilityService::SetLogging(const nsACString & aModules)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean isLogged (in AString aModule); */
NS_IMETHODIMP nsAccessibilityService::IsLogged(const nsAString & aModule, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIAccessibleRetrieval */
#define NS_IACCESSIBLERETRIEVAL_IID_STR "d85e0cbe-47ce-490c-8488-f821dd2be0c2"

#define NS_IACCESSIBLERETRIEVAL_IID \
  {0xd85e0cbe, 0x47ce, 0x490c, \
    { 0x84, 0x88, 0xf8, 0x21, 0xdd, 0x2b, 0xe0, 0xc2 }}

class NS_NO_VTABLE nsIAccessibleRetrieval : public nsIAccessibilityService {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IACCESSIBLERETRIEVAL_IID)

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAccessibleRetrieval, NS_IACCESSIBLERETRIEVAL_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIACCESSIBLERETRIEVAL \
  /* no methods! */

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIACCESSIBLERETRIEVAL \
  /* no methods! */

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIACCESSIBLERETRIEVAL(_to) \
  /* no methods! */

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIACCESSIBLERETRIEVAL(_to) \
  /* no methods! */

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAccessibleRetrieval : public nsIAccessibleRetrieval
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIACCESSIBLERETRIEVAL

  nsAccessibleRetrieval();

private:
  ~nsAccessibleRetrieval();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAccessibleRetrieval, nsIAccessibleRetrieval)

nsAccessibleRetrieval::nsAccessibleRetrieval()
{
  /* member initializers and constructor code */
}

nsAccessibleRetrieval::~nsAccessibleRetrieval()
{
  /* destructor code */
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIAccessibilityService_h__ */
