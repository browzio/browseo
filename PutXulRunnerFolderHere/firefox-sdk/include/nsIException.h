/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIException.idl
 */

#ifndef __gen_nsIException_h__
#define __gen_nsIException_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

#include "js/Value.h"

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIStackFrame */
#define NS_ISTACKFRAME_IID_STR "28bfb2a2-5ea6-4738-918b-049dc4d51f0b"

#define NS_ISTACKFRAME_IID \
  {0x28bfb2a2, 0x5ea6, 0x4738, \
    { 0x91, 0x8b, 0x04, 0x9d, 0xc4, 0xd5, 0x1f, 0x0b }}

class NS_NO_VTABLE nsIStackFrame : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ISTACKFRAME_IID)

  /* readonly attribute uint32_t language; */
  NS_IMETHOD GetLanguage(uint32_t *aLanguage) = 0;

  /* readonly attribute AUTF8String languageName; */
  NS_IMETHOD GetLanguageName(nsACString & aLanguageName) = 0;

  /* [implicit_jscontext] readonly attribute AString filename; */
  NS_IMETHOD GetFilename(JSContext* cx, nsAString & aFilename) = 0;

  /* [implicit_jscontext] readonly attribute AString name; */
  NS_IMETHOD GetName(JSContext* cx, nsAString & aName) = 0;

  /* [implicit_jscontext] readonly attribute int32_t lineNumber; */
  NS_IMETHOD GetLineNumber(JSContext* cx, int32_t *aLineNumber) = 0;

  /* [implicit_jscontext] readonly attribute int32_t columnNumber; */
  NS_IMETHOD GetColumnNumber(JSContext* cx, int32_t *aColumnNumber) = 0;

  /* readonly attribute AUTF8String sourceLine; */
  NS_IMETHOD GetSourceLine(nsACString & aSourceLine) = 0;

  /* [implicit_jscontext] readonly attribute AString asyncCause; */
  NS_IMETHOD GetAsyncCause(JSContext* cx, nsAString & aAsyncCause) = 0;

  /* [implicit_jscontext] readonly attribute nsIStackFrame asyncCaller; */
  NS_IMETHOD GetAsyncCaller(JSContext* cx, nsIStackFrame * *aAsyncCaller) = 0;

  /* [implicit_jscontext] readonly attribute nsIStackFrame caller; */
  NS_IMETHOD GetCaller(JSContext* cx, nsIStackFrame * *aCaller) = 0;

  /* [implicit_jscontext] readonly attribute AString formattedStack; */
  NS_IMETHOD GetFormattedStack(JSContext* cx, nsAString & aFormattedStack) = 0;

  /* readonly attribute jsval nativeSavedFrame; */
  NS_IMETHOD GetNativeSavedFrame(JS::MutableHandleValue aNativeSavedFrame) = 0;

  /* [implicit_jscontext] AUTF8String toString (); */
  NS_IMETHOD ToString(JSContext* cx, nsACString & _retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIStackFrame, NS_ISTACKFRAME_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSISTACKFRAME \
  NS_IMETHOD GetLanguage(uint32_t *aLanguage) override; \
  NS_IMETHOD GetLanguageName(nsACString & aLanguageName) override; \
  NS_IMETHOD GetFilename(JSContext* cx, nsAString & aFilename) override; \
  NS_IMETHOD GetName(JSContext* cx, nsAString & aName) override; \
  NS_IMETHOD GetLineNumber(JSContext* cx, int32_t *aLineNumber) override; \
  NS_IMETHOD GetColumnNumber(JSContext* cx, int32_t *aColumnNumber) override; \
  NS_IMETHOD GetSourceLine(nsACString & aSourceLine) override; \
  NS_IMETHOD GetAsyncCause(JSContext* cx, nsAString & aAsyncCause) override; \
  NS_IMETHOD GetAsyncCaller(JSContext* cx, nsIStackFrame * *aAsyncCaller) override; \
  NS_IMETHOD GetCaller(JSContext* cx, nsIStackFrame * *aCaller) override; \
  NS_IMETHOD GetFormattedStack(JSContext* cx, nsAString & aFormattedStack) override; \
  NS_IMETHOD GetNativeSavedFrame(JS::MutableHandleValue aNativeSavedFrame) override; \
  NS_IMETHOD ToString(JSContext* cx, nsACString & _retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSISTACKFRAME \
  NS_METHOD GetLanguage(uint32_t *aLanguage); \
  NS_METHOD GetLanguageName(nsACString & aLanguageName); \
  NS_METHOD GetFilename(JSContext* cx, nsAString & aFilename); \
  NS_METHOD GetName(JSContext* cx, nsAString & aName); \
  NS_METHOD GetLineNumber(JSContext* cx, int32_t *aLineNumber); \
  NS_METHOD GetColumnNumber(JSContext* cx, int32_t *aColumnNumber); \
  NS_METHOD GetSourceLine(nsACString & aSourceLine); \
  NS_METHOD GetAsyncCause(JSContext* cx, nsAString & aAsyncCause); \
  NS_METHOD GetAsyncCaller(JSContext* cx, nsIStackFrame * *aAsyncCaller); \
  NS_METHOD GetCaller(JSContext* cx, nsIStackFrame * *aCaller); \
  NS_METHOD GetFormattedStack(JSContext* cx, nsAString & aFormattedStack); \
  NS_METHOD GetNativeSavedFrame(JS::MutableHandleValue aNativeSavedFrame); \
  NS_METHOD ToString(JSContext* cx, nsACString & _retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSISTACKFRAME(_to) \
  NS_IMETHOD GetLanguage(uint32_t *aLanguage) override { return _to GetLanguage(aLanguage); } \
  NS_IMETHOD GetLanguageName(nsACString & aLanguageName) override { return _to GetLanguageName(aLanguageName); } \
  NS_IMETHOD GetFilename(JSContext* cx, nsAString & aFilename) override { return _to GetFilename(cx, aFilename); } \
  NS_IMETHOD GetName(JSContext* cx, nsAString & aName) override { return _to GetName(cx, aName); } \
  NS_IMETHOD GetLineNumber(JSContext* cx, int32_t *aLineNumber) override { return _to GetLineNumber(cx, aLineNumber); } \
  NS_IMETHOD GetColumnNumber(JSContext* cx, int32_t *aColumnNumber) override { return _to GetColumnNumber(cx, aColumnNumber); } \
  NS_IMETHOD GetSourceLine(nsACString & aSourceLine) override { return _to GetSourceLine(aSourceLine); } \
  NS_IMETHOD GetAsyncCause(JSContext* cx, nsAString & aAsyncCause) override { return _to GetAsyncCause(cx, aAsyncCause); } \
  NS_IMETHOD GetAsyncCaller(JSContext* cx, nsIStackFrame * *aAsyncCaller) override { return _to GetAsyncCaller(cx, aAsyncCaller); } \
  NS_IMETHOD GetCaller(JSContext* cx, nsIStackFrame * *aCaller) override { return _to GetCaller(cx, aCaller); } \
  NS_IMETHOD GetFormattedStack(JSContext* cx, nsAString & aFormattedStack) override { return _to GetFormattedStack(cx, aFormattedStack); } \
  NS_IMETHOD GetNativeSavedFrame(JS::MutableHandleValue aNativeSavedFrame) override { return _to GetNativeSavedFrame(aNativeSavedFrame); } \
  NS_IMETHOD ToString(JSContext* cx, nsACString & _retval) override { return _to ToString(cx, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSISTACKFRAME(_to) \
  NS_IMETHOD GetLanguage(uint32_t *aLanguage) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLanguage(aLanguage); } \
  NS_IMETHOD GetLanguageName(nsACString & aLanguageName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLanguageName(aLanguageName); } \
  NS_IMETHOD GetFilename(JSContext* cx, nsAString & aFilename) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetFilename(cx, aFilename); } \
  NS_IMETHOD GetName(JSContext* cx, nsAString & aName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetName(cx, aName); } \
  NS_IMETHOD GetLineNumber(JSContext* cx, int32_t *aLineNumber) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLineNumber(cx, aLineNumber); } \
  NS_IMETHOD GetColumnNumber(JSContext* cx, int32_t *aColumnNumber) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetColumnNumber(cx, aColumnNumber); } \
  NS_IMETHOD GetSourceLine(nsACString & aSourceLine) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetSourceLine(aSourceLine); } \
  NS_IMETHOD GetAsyncCause(JSContext* cx, nsAString & aAsyncCause) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAsyncCause(cx, aAsyncCause); } \
  NS_IMETHOD GetAsyncCaller(JSContext* cx, nsIStackFrame * *aAsyncCaller) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAsyncCaller(cx, aAsyncCaller); } \
  NS_IMETHOD GetCaller(JSContext* cx, nsIStackFrame * *aCaller) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCaller(cx, aCaller); } \
  NS_IMETHOD GetFormattedStack(JSContext* cx, nsAString & aFormattedStack) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetFormattedStack(cx, aFormattedStack); } \
  NS_IMETHOD GetNativeSavedFrame(JS::MutableHandleValue aNativeSavedFrame) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetNativeSavedFrame(aNativeSavedFrame); } \
  NS_IMETHOD ToString(JSContext* cx, nsACString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ToString(cx, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsStackFrame : public nsIStackFrame
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSISTACKFRAME

  nsStackFrame();

private:
  ~nsStackFrame();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsStackFrame, nsIStackFrame)

nsStackFrame::nsStackFrame()
{
  /* member initializers and constructor code */
}

nsStackFrame::~nsStackFrame()
{
  /* destructor code */
}

/* readonly attribute uint32_t language; */
NS_IMETHODIMP nsStackFrame::GetLanguage(uint32_t *aLanguage)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String languageName; */
NS_IMETHODIMP nsStackFrame::GetLanguageName(nsACString & aLanguageName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute AString filename; */
NS_IMETHODIMP nsStackFrame::GetFilename(JSContext* cx, nsAString & aFilename)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute AString name; */
NS_IMETHODIMP nsStackFrame::GetName(JSContext* cx, nsAString & aName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute int32_t lineNumber; */
NS_IMETHODIMP nsStackFrame::GetLineNumber(JSContext* cx, int32_t *aLineNumber)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute int32_t columnNumber; */
NS_IMETHODIMP nsStackFrame::GetColumnNumber(JSContext* cx, int32_t *aColumnNumber)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String sourceLine; */
NS_IMETHODIMP nsStackFrame::GetSourceLine(nsACString & aSourceLine)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute AString asyncCause; */
NS_IMETHODIMP nsStackFrame::GetAsyncCause(JSContext* cx, nsAString & aAsyncCause)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute nsIStackFrame asyncCaller; */
NS_IMETHODIMP nsStackFrame::GetAsyncCaller(JSContext* cx, nsIStackFrame * *aAsyncCaller)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute nsIStackFrame caller; */
NS_IMETHODIMP nsStackFrame::GetCaller(JSContext* cx, nsIStackFrame * *aCaller)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute AString formattedStack; */
NS_IMETHODIMP nsStackFrame::GetFormattedStack(JSContext* cx, nsAString & aFormattedStack)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute jsval nativeSavedFrame; */
NS_IMETHODIMP nsStackFrame::GetNativeSavedFrame(JS::MutableHandleValue aNativeSavedFrame)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] AUTF8String toString (); */
NS_IMETHODIMP nsStackFrame::ToString(JSContext* cx, nsACString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIException */
#define NS_IEXCEPTION_IID_STR "4371b5bf-6845-487f-8d9d-3f1e4a9badd2"

#define NS_IEXCEPTION_IID \
  {0x4371b5bf, 0x6845, 0x487f, \
    { 0x8d, 0x9d, 0x3f, 0x1e, 0x4a, 0x9b, 0xad, 0xd2 }}

class NS_NO_VTABLE nsIException : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IEXCEPTION_IID)

  /* [binaryname(MessageMoz)] readonly attribute AUTF8String message; */
  NS_IMETHOD GetMessageMoz(nsACString & aMessage) = 0;

  /* readonly attribute nsresult result; */
  NS_IMETHOD GetResult(nsresult *aResult) = 0;

  /* readonly attribute AUTF8String name; */
  NS_IMETHOD GetName(nsACString & aName) = 0;

  /* [implicit_jscontext] readonly attribute AString filename; */
  NS_IMETHOD GetFilename(JSContext* cx, nsAString & aFilename) = 0;

  /* [implicit_jscontext] readonly attribute uint32_t lineNumber; */
  NS_IMETHOD GetLineNumber(JSContext* cx, uint32_t *aLineNumber) = 0;

  /* readonly attribute uint32_t columnNumber; */
  NS_IMETHOD GetColumnNumber(uint32_t *aColumnNumber) = 0;

  /* readonly attribute nsIStackFrame location; */
  NS_IMETHOD GetLocation(nsIStackFrame * *aLocation) = 0;

  /* readonly attribute nsISupports data; */
  NS_IMETHOD GetData(nsISupports * *aData) = 0;

  /* [implicit_jscontext] AUTF8String toString (); */
  NS_IMETHOD ToString(JSContext* cx, nsACString & _retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIException, NS_IEXCEPTION_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIEXCEPTION \
  NS_IMETHOD GetMessageMoz(nsACString & aMessage) override; \
  NS_IMETHOD GetResult(nsresult *aResult) override; \
  NS_IMETHOD GetName(nsACString & aName) override; \
  NS_IMETHOD GetFilename(JSContext* cx, nsAString & aFilename) override; \
  NS_IMETHOD GetLineNumber(JSContext* cx, uint32_t *aLineNumber) override; \
  NS_IMETHOD GetColumnNumber(uint32_t *aColumnNumber) override; \
  NS_IMETHOD GetLocation(nsIStackFrame * *aLocation) override; \
  NS_IMETHOD GetData(nsISupports * *aData) override; \
  NS_IMETHOD ToString(JSContext* cx, nsACString & _retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIEXCEPTION \
  NS_METHOD GetMessageMoz(nsACString & aMessage); \
  NS_METHOD GetResult(nsresult *aResult); \
  NS_METHOD GetName(nsACString & aName); \
  NS_METHOD GetFilename(JSContext* cx, nsAString & aFilename); \
  NS_METHOD GetLineNumber(JSContext* cx, uint32_t *aLineNumber); \
  NS_METHOD GetColumnNumber(uint32_t *aColumnNumber); \
  NS_METHOD GetLocation(nsIStackFrame * *aLocation); \
  NS_METHOD GetData(nsISupports * *aData); \
  NS_METHOD ToString(JSContext* cx, nsACString & _retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIEXCEPTION(_to) \
  NS_IMETHOD GetMessageMoz(nsACString & aMessage) override { return _to GetMessageMoz(aMessage); } \
  NS_IMETHOD GetResult(nsresult *aResult) override { return _to GetResult(aResult); } \
  NS_IMETHOD GetName(nsACString & aName) override { return _to GetName(aName); } \
  NS_IMETHOD GetFilename(JSContext* cx, nsAString & aFilename) override { return _to GetFilename(cx, aFilename); } \
  NS_IMETHOD GetLineNumber(JSContext* cx, uint32_t *aLineNumber) override { return _to GetLineNumber(cx, aLineNumber); } \
  NS_IMETHOD GetColumnNumber(uint32_t *aColumnNumber) override { return _to GetColumnNumber(aColumnNumber); } \
  NS_IMETHOD GetLocation(nsIStackFrame * *aLocation) override { return _to GetLocation(aLocation); } \
  NS_IMETHOD GetData(nsISupports * *aData) override { return _to GetData(aData); } \
  NS_IMETHOD ToString(JSContext* cx, nsACString & _retval) override { return _to ToString(cx, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIEXCEPTION(_to) \
  NS_IMETHOD GetMessageMoz(nsACString & aMessage) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetMessageMoz(aMessage); } \
  NS_IMETHOD GetResult(nsresult *aResult) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetResult(aResult); } \
  NS_IMETHOD GetName(nsACString & aName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetName(aName); } \
  NS_IMETHOD GetFilename(JSContext* cx, nsAString & aFilename) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetFilename(cx, aFilename); } \
  NS_IMETHOD GetLineNumber(JSContext* cx, uint32_t *aLineNumber) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLineNumber(cx, aLineNumber); } \
  NS_IMETHOD GetColumnNumber(uint32_t *aColumnNumber) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetColumnNumber(aColumnNumber); } \
  NS_IMETHOD GetLocation(nsIStackFrame * *aLocation) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLocation(aLocation); } \
  NS_IMETHOD GetData(nsISupports * *aData) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetData(aData); } \
  NS_IMETHOD ToString(JSContext* cx, nsACString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ToString(cx, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsException : public nsIException
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIEXCEPTION

  nsException();

private:
  ~nsException();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsException, nsIException)

nsException::nsException()
{
  /* member initializers and constructor code */
}

nsException::~nsException()
{
  /* destructor code */
}

/* [binaryname(MessageMoz)] readonly attribute AUTF8String message; */
NS_IMETHODIMP nsException::GetMessageMoz(nsACString & aMessage)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsresult result; */
NS_IMETHODIMP nsException::GetResult(nsresult *aResult)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String name; */
NS_IMETHODIMP nsException::GetName(nsACString & aName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute AString filename; */
NS_IMETHODIMP nsException::GetFilename(JSContext* cx, nsAString & aFilename)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute uint32_t lineNumber; */
NS_IMETHODIMP nsException::GetLineNumber(JSContext* cx, uint32_t *aLineNumber)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute uint32_t columnNumber; */
NS_IMETHODIMP nsException::GetColumnNumber(uint32_t *aColumnNumber)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIStackFrame location; */
NS_IMETHODIMP nsException::GetLocation(nsIStackFrame * *aLocation)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsISupports data; */
NS_IMETHODIMP nsException::GetData(nsISupports * *aData)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] AUTF8String toString (); */
NS_IMETHODIMP nsException::ToString(JSContext* cx, nsACString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIException_h__ */
