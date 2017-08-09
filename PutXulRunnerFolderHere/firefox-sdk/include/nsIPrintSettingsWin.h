/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPrintSettingsWin.idl
 */

#ifndef __gen_nsIPrintSettingsWin_h__
#define __gen_nsIPrintSettingsWin_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
#include "windows.h"

/* starting interface:    nsIPrintSettingsWin */
#define NS_IPRINTSETTINGSWIN_IID_STR "c63eed41-6ac5-459e-8a64-033eb9ad770a"

#define NS_IPRINTSETTINGSWIN_IID \
  {0xc63eed41, 0x6ac5, 0x459e, \
    { 0x8a, 0x64, 0x03, 0x3e, 0xb9, 0xad, 0x77, 0x0a }}

class NS_NO_VTABLE nsIPrintSettingsWin : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRINTSETTINGSWIN_IID)

  /* [noscript] attribute wstring deviceName; */
  NS_IMETHOD GetDeviceName(char16_t * *aDeviceName) = 0;
  NS_IMETHOD SetDeviceName(const char16_t * aDeviceName) = 0;

  /* [noscript] attribute wstring driverName; */
  NS_IMETHOD GetDriverName(char16_t * *aDriverName) = 0;
  NS_IMETHOD SetDriverName(const char16_t * aDriverName) = 0;

  /* [noscript] attribute nsDevMode devMode; */
  NS_IMETHOD GetDevMode(DEVMODEW **aDevMode) = 0;
  NS_IMETHOD SetDevMode(DEVMODEW *aDevMode) = 0;

  /* [noscript] attribute double printableWidthInInches; */
  NS_IMETHOD GetPrintableWidthInInches(double *aPrintableWidthInInches) = 0;
  NS_IMETHOD SetPrintableWidthInInches(double aPrintableWidthInInches) = 0;

  /* [noscript] attribute double printableHeightInInches; */
  NS_IMETHOD GetPrintableHeightInInches(double *aPrintableHeightInInches) = 0;
  NS_IMETHOD SetPrintableHeightInInches(double aPrintableHeightInInches) = 0;

  /* [notxpcom] void copyFromNative (in nsHdc hdc, in nsDevMode devMode); */
  NS_IMETHOD_(void) CopyFromNative(HDC hdc, DEVMODEW *devMode) = 0;

  /* [notxpcom] void copyToNative (in nsDevMode devMode); */
  NS_IMETHOD_(void) CopyToNative(DEVMODEW *devMode) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPrintSettingsWin, NS_IPRINTSETTINGSWIN_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRINTSETTINGSWIN \
  NS_IMETHOD GetDeviceName(char16_t * *aDeviceName) override; \
  NS_IMETHOD SetDeviceName(const char16_t * aDeviceName) override; \
  NS_IMETHOD GetDriverName(char16_t * *aDriverName) override; \
  NS_IMETHOD SetDriverName(const char16_t * aDriverName) override; \
  NS_IMETHOD GetDevMode(DEVMODEW **aDevMode) override; \
  NS_IMETHOD SetDevMode(DEVMODEW *aDevMode) override; \
  NS_IMETHOD GetPrintableWidthInInches(double *aPrintableWidthInInches) override; \
  NS_IMETHOD SetPrintableWidthInInches(double aPrintableWidthInInches) override; \
  NS_IMETHOD GetPrintableHeightInInches(double *aPrintableHeightInInches) override; \
  NS_IMETHOD SetPrintableHeightInInches(double aPrintableHeightInInches) override; \
  NS_IMETHOD_(void) CopyFromNative(HDC hdc, DEVMODEW *devMode) override; \
  NS_IMETHOD_(void) CopyToNative(DEVMODEW *devMode) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRINTSETTINGSWIN \
  NS_METHOD GetDeviceName(char16_t * *aDeviceName); \
  NS_METHOD SetDeviceName(const char16_t * aDeviceName); \
  NS_METHOD GetDriverName(char16_t * *aDriverName); \
  NS_METHOD SetDriverName(const char16_t * aDriverName); \
  NS_METHOD GetDevMode(DEVMODEW **aDevMode); \
  NS_METHOD SetDevMode(DEVMODEW *aDevMode); \
  NS_METHOD GetPrintableWidthInInches(double *aPrintableWidthInInches); \
  NS_METHOD SetPrintableWidthInInches(double aPrintableWidthInInches); \
  NS_METHOD GetPrintableHeightInInches(double *aPrintableHeightInInches); \
  NS_METHOD SetPrintableHeightInInches(double aPrintableHeightInInches); \
  NS_METHOD_(void) CopyFromNative(HDC hdc, DEVMODEW *devMode); \
  NS_METHOD_(void) CopyToNative(DEVMODEW *devMode); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRINTSETTINGSWIN(_to) \
  NS_IMETHOD GetDeviceName(char16_t * *aDeviceName) override { return _to GetDeviceName(aDeviceName); } \
  NS_IMETHOD SetDeviceName(const char16_t * aDeviceName) override { return _to SetDeviceName(aDeviceName); } \
  NS_IMETHOD GetDriverName(char16_t * *aDriverName) override { return _to GetDriverName(aDriverName); } \
  NS_IMETHOD SetDriverName(const char16_t * aDriverName) override { return _to SetDriverName(aDriverName); } \
  NS_IMETHOD GetDevMode(DEVMODEW **aDevMode) override { return _to GetDevMode(aDevMode); } \
  NS_IMETHOD SetDevMode(DEVMODEW *aDevMode) override { return _to SetDevMode(aDevMode); } \
  NS_IMETHOD GetPrintableWidthInInches(double *aPrintableWidthInInches) override { return _to GetPrintableWidthInInches(aPrintableWidthInInches); } \
  NS_IMETHOD SetPrintableWidthInInches(double aPrintableWidthInInches) override { return _to SetPrintableWidthInInches(aPrintableWidthInInches); } \
  NS_IMETHOD GetPrintableHeightInInches(double *aPrintableHeightInInches) override { return _to GetPrintableHeightInInches(aPrintableHeightInInches); } \
  NS_IMETHOD SetPrintableHeightInInches(double aPrintableHeightInInches) override { return _to SetPrintableHeightInInches(aPrintableHeightInInches); } \
  NS_IMETHOD_(void) CopyFromNative(HDC hdc, DEVMODEW *devMode) override { return _to CopyFromNative(hdc, devMode); } \
  NS_IMETHOD_(void) CopyToNative(DEVMODEW *devMode) override { return _to CopyToNative(devMode); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRINTSETTINGSWIN(_to) \
  NS_IMETHOD GetDeviceName(char16_t * *aDeviceName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDeviceName(aDeviceName); } \
  NS_IMETHOD SetDeviceName(const char16_t * aDeviceName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetDeviceName(aDeviceName); } \
  NS_IMETHOD GetDriverName(char16_t * *aDriverName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDriverName(aDriverName); } \
  NS_IMETHOD SetDriverName(const char16_t * aDriverName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetDriverName(aDriverName); } \
  NS_IMETHOD GetDevMode(DEVMODEW **aDevMode) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDevMode(aDevMode); } \
  NS_IMETHOD SetDevMode(DEVMODEW *aDevMode) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetDevMode(aDevMode); } \
  NS_IMETHOD GetPrintableWidthInInches(double *aPrintableWidthInInches) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPrintableWidthInInches(aPrintableWidthInInches); } \
  NS_IMETHOD SetPrintableWidthInInches(double aPrintableWidthInInches) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetPrintableWidthInInches(aPrintableWidthInInches); } \
  NS_IMETHOD GetPrintableHeightInInches(double *aPrintableHeightInInches) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPrintableHeightInInches(aPrintableHeightInInches); } \
  NS_IMETHOD SetPrintableHeightInInches(double aPrintableHeightInInches) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetPrintableHeightInInches(aPrintableHeightInInches); } \
  NS_IMETHOD_(void) CopyFromNative(HDC hdc, DEVMODEW *devMode) override; \
  NS_IMETHOD_(void) CopyToNative(DEVMODEW *devMode) override; 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPrintSettingsWin : public nsIPrintSettingsWin
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRINTSETTINGSWIN

  nsPrintSettingsWin();

private:
  ~nsPrintSettingsWin();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPrintSettingsWin, nsIPrintSettingsWin)

nsPrintSettingsWin::nsPrintSettingsWin()
{
  /* member initializers and constructor code */
}

nsPrintSettingsWin::~nsPrintSettingsWin()
{
  /* destructor code */
}

/* [noscript] attribute wstring deviceName; */
NS_IMETHODIMP nsPrintSettingsWin::GetDeviceName(char16_t * *aDeviceName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsPrintSettingsWin::SetDeviceName(const char16_t * aDeviceName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] attribute wstring driverName; */
NS_IMETHODIMP nsPrintSettingsWin::GetDriverName(char16_t * *aDriverName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsPrintSettingsWin::SetDriverName(const char16_t * aDriverName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] attribute nsDevMode devMode; */
NS_IMETHODIMP nsPrintSettingsWin::GetDevMode(DEVMODEW **aDevMode)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsPrintSettingsWin::SetDevMode(DEVMODEW *aDevMode)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] attribute double printableWidthInInches; */
NS_IMETHODIMP nsPrintSettingsWin::GetPrintableWidthInInches(double *aPrintableWidthInInches)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsPrintSettingsWin::SetPrintableWidthInInches(double aPrintableWidthInInches)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript] attribute double printableHeightInInches; */
NS_IMETHODIMP nsPrintSettingsWin::GetPrintableHeightInInches(double *aPrintableHeightInInches)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsPrintSettingsWin::SetPrintableHeightInInches(double aPrintableHeightInInches)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [notxpcom] void copyFromNative (in nsHdc hdc, in nsDevMode devMode); */
NS_IMETHODIMP_(void) nsPrintSettingsWin::CopyFromNative(HDC hdc, DEVMODEW *devMode)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [notxpcom] void copyToNative (in nsDevMode devMode); */
NS_IMETHODIMP_(void) nsPrintSettingsWin::CopyToNative(DEVMODEW *devMode)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPrintSettingsWin_h__ */
