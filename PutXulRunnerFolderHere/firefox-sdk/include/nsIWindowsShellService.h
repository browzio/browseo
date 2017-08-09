/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIWindowsShellService.idl
 */

#ifndef __gen_nsIWindowsShellService_h__
#define __gen_nsIWindowsShellService_h__


#ifndef __gen_nsIShellService_h__
#include "nsIShellService.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIWindowsShellService */
#define NS_IWINDOWSSHELLSERVICE_IID_STR "f8a26b94-49e5-4441-8fbc-315e0b4f22ef"

#define NS_IWINDOWSSHELLSERVICE_IID \
  {0xf8a26b94, 0x49e5, 0x4441, \
    { 0x8f, 0xbc, 0x31, 0x5e, 0x0b, 0x4f, 0x22, 0xef }}

class NS_NO_VTABLE nsIWindowsShellService : public nsIShellService {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IWINDOWSSHELLSERVICE_IID)

  /* void shortcutMaintenance (); */
  NS_IMETHOD ShortcutMaintenance(void) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIWindowsShellService, NS_IWINDOWSSHELLSERVICE_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIWINDOWSSHELLSERVICE \
  NS_IMETHOD ShortcutMaintenance(void) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIWINDOWSSHELLSERVICE \
  NS_METHOD ShortcutMaintenance(void); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIWINDOWSSHELLSERVICE(_to) \
  NS_IMETHOD ShortcutMaintenance(void) override { return _to ShortcutMaintenance(); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIWINDOWSSHELLSERVICE(_to) \
  NS_IMETHOD ShortcutMaintenance(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ShortcutMaintenance(); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsWindowsShellService : public nsIWindowsShellService
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIWINDOWSSHELLSERVICE

  nsWindowsShellService();

private:
  ~nsWindowsShellService();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsWindowsShellService, nsIWindowsShellService)

nsWindowsShellService::nsWindowsShellService()
{
  /* member initializers and constructor code */
}

nsWindowsShellService::~nsWindowsShellService()
{
  /* destructor code */
}

/* void shortcutMaintenance (); */
NS_IMETHODIMP nsWindowsShellService::ShortcutMaintenance()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIWindowsShellService_h__ */
