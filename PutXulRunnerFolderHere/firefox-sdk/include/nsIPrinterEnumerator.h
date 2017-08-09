/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPrinterEnumerator.idl
 */

#ifndef __gen_nsIPrinterEnumerator_h__
#define __gen_nsIPrinterEnumerator_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

#ifndef __gen_nsIPrintSettings_h__
#include "nsIPrintSettings.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIStringEnumerator; /* forward declaration */


/* starting interface:    nsIPrinterEnumerator */
#define NS_IPRINTERENUMERATOR_IID_STR "5e738fff-404c-4c94-9189-e8f2cce93e94"

#define NS_IPRINTERENUMERATOR_IID \
  {0x5e738fff, 0x404c, 0x4c94, \
    { 0x91, 0x89, 0xe8, 0xf2, 0xcc, 0xe9, 0x3e, 0x94 }}

class NS_NO_VTABLE nsIPrinterEnumerator : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRINTERENUMERATOR_IID)

  /* readonly attribute wstring defaultPrinterName; */
  NS_IMETHOD GetDefaultPrinterName(char16_t * *aDefaultPrinterName) = 0;

  /* void initPrintSettingsFromPrinter (in wstring aPrinterName, in nsIPrintSettings aPrintSettings); */
  NS_IMETHOD InitPrintSettingsFromPrinter(const char16_t * aPrinterName, nsIPrintSettings *aPrintSettings) = 0;

  /* readonly attribute nsIStringEnumerator printerNameList; */
  NS_IMETHOD GetPrinterNameList(nsIStringEnumerator * *aPrinterNameList) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPrinterEnumerator, NS_IPRINTERENUMERATOR_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRINTERENUMERATOR \
  NS_IMETHOD GetDefaultPrinterName(char16_t * *aDefaultPrinterName) override; \
  NS_IMETHOD InitPrintSettingsFromPrinter(const char16_t * aPrinterName, nsIPrintSettings *aPrintSettings) override; \
  NS_IMETHOD GetPrinterNameList(nsIStringEnumerator * *aPrinterNameList) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRINTERENUMERATOR \
  NS_METHOD GetDefaultPrinterName(char16_t * *aDefaultPrinterName); \
  NS_METHOD InitPrintSettingsFromPrinter(const char16_t * aPrinterName, nsIPrintSettings *aPrintSettings); \
  NS_METHOD GetPrinterNameList(nsIStringEnumerator * *aPrinterNameList); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRINTERENUMERATOR(_to) \
  NS_IMETHOD GetDefaultPrinterName(char16_t * *aDefaultPrinterName) override { return _to GetDefaultPrinterName(aDefaultPrinterName); } \
  NS_IMETHOD InitPrintSettingsFromPrinter(const char16_t * aPrinterName, nsIPrintSettings *aPrintSettings) override { return _to InitPrintSettingsFromPrinter(aPrinterName, aPrintSettings); } \
  NS_IMETHOD GetPrinterNameList(nsIStringEnumerator * *aPrinterNameList) override { return _to GetPrinterNameList(aPrinterNameList); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRINTERENUMERATOR(_to) \
  NS_IMETHOD GetDefaultPrinterName(char16_t * *aDefaultPrinterName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDefaultPrinterName(aDefaultPrinterName); } \
  NS_IMETHOD InitPrintSettingsFromPrinter(const char16_t * aPrinterName, nsIPrintSettings *aPrintSettings) override { return !_to ? NS_ERROR_NULL_POINTER : _to->InitPrintSettingsFromPrinter(aPrinterName, aPrintSettings); } \
  NS_IMETHOD GetPrinterNameList(nsIStringEnumerator * *aPrinterNameList) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPrinterNameList(aPrinterNameList); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPrinterEnumerator : public nsIPrinterEnumerator
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRINTERENUMERATOR

  nsPrinterEnumerator();

private:
  ~nsPrinterEnumerator();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPrinterEnumerator, nsIPrinterEnumerator)

nsPrinterEnumerator::nsPrinterEnumerator()
{
  /* member initializers and constructor code */
}

nsPrinterEnumerator::~nsPrinterEnumerator()
{
  /* destructor code */
}

/* readonly attribute wstring defaultPrinterName; */
NS_IMETHODIMP nsPrinterEnumerator::GetDefaultPrinterName(char16_t * *aDefaultPrinterName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void initPrintSettingsFromPrinter (in wstring aPrinterName, in nsIPrintSettings aPrintSettings); */
NS_IMETHODIMP nsPrinterEnumerator::InitPrintSettingsFromPrinter(const char16_t * aPrinterName, nsIPrintSettings *aPrintSettings)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIStringEnumerator printerNameList; */
NS_IMETHODIMP nsPrinterEnumerator::GetPrinterNameList(nsIStringEnumerator * *aPrinterNameList)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPrinterEnumerator_h__ */
