/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIU2FToken.idl
 */

#ifndef __gen_nsIU2FToken_h__
#define __gen_nsIU2FToken_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIArray; /* forward declaration */


/* starting interface:    nsIU2FToken */
#define NS_IU2FTOKEN_IID_STR "5778242f-1f42-47a2-b514-fa1adde2d904"

#define NS_IU2FTOKEN_IID \
  {0x5778242f, 0x1f42, 0x47a2, \
    { 0xb5, 0x14, 0xfa, 0x1a, 0xdd, 0xe2, 0xd9, 0x04 }}

class NS_NO_VTABLE nsIU2FToken : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IU2FTOKEN_IID)

  /* void isCompatibleVersion (in AString version, [retval] out boolean result); */
  NS_IMETHOD IsCompatibleVersion(const nsAString & version, bool *result) = 0;

  /* void isRegistered ([array, size_is (keyHandleLen)] in octet keyHandle, in uint32_t keyHandleLen, [retval] out boolean result); */
  NS_IMETHOD IsRegistered(uint8_t *keyHandle, uint32_t keyHandleLen, bool *result) = 0;

  /* void register ([array, size_is (applicationLen)] in octet application, in uint32_t applicationLen, [array, size_is (challengeLen)] in octet challenge, in uint32_t challengeLen, [array, size_is (registrationLen)] out octet registration, out uint32_t registrationLen); */
  NS_IMETHOD Register(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t **registration, uint32_t *registrationLen) = 0;

  /* void sign ([array, size_is (applicationLen)] in octet application, in uint32_t applicationLen, [array, size_is (challengeLen)] in octet challenge, in uint32_t challengeLen, [array, size_is (keyHandleLen)] in octet keyHandle, in uint32_t keyHandleLen, [array, size_is (signatureLen)] out octet signature, out uint32_t signatureLen); */
  NS_IMETHOD Sign(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t *keyHandle, uint32_t keyHandleLen, uint8_t **signature, uint32_t *signatureLen) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIU2FToken, NS_IU2FTOKEN_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIU2FTOKEN \
  NS_IMETHOD IsCompatibleVersion(const nsAString & version, bool *result) override; \
  NS_IMETHOD IsRegistered(uint8_t *keyHandle, uint32_t keyHandleLen, bool *result) override; \
  NS_IMETHOD Register(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t **registration, uint32_t *registrationLen) override; \
  NS_IMETHOD Sign(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t *keyHandle, uint32_t keyHandleLen, uint8_t **signature, uint32_t *signatureLen) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIU2FTOKEN \
  NS_METHOD IsCompatibleVersion(const nsAString & version, bool *result); \
  NS_METHOD IsRegistered(uint8_t *keyHandle, uint32_t keyHandleLen, bool *result); \
  NS_METHOD Register(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t **registration, uint32_t *registrationLen); \
  NS_METHOD Sign(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t *keyHandle, uint32_t keyHandleLen, uint8_t **signature, uint32_t *signatureLen); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIU2FTOKEN(_to) \
  NS_IMETHOD IsCompatibleVersion(const nsAString & version, bool *result) override { return _to IsCompatibleVersion(version, result); } \
  NS_IMETHOD IsRegistered(uint8_t *keyHandle, uint32_t keyHandleLen, bool *result) override { return _to IsRegistered(keyHandle, keyHandleLen, result); } \
  NS_IMETHOD Register(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t **registration, uint32_t *registrationLen) override { return _to Register(application, applicationLen, challenge, challengeLen, registration, registrationLen); } \
  NS_IMETHOD Sign(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t *keyHandle, uint32_t keyHandleLen, uint8_t **signature, uint32_t *signatureLen) override { return _to Sign(application, applicationLen, challenge, challengeLen, keyHandle, keyHandleLen, signature, signatureLen); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIU2FTOKEN(_to) \
  NS_IMETHOD IsCompatibleVersion(const nsAString & version, bool *result) override { return !_to ? NS_ERROR_NULL_POINTER : _to->IsCompatibleVersion(version, result); } \
  NS_IMETHOD IsRegistered(uint8_t *keyHandle, uint32_t keyHandleLen, bool *result) override { return !_to ? NS_ERROR_NULL_POINTER : _to->IsRegistered(keyHandle, keyHandleLen, result); } \
  NS_IMETHOD Register(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t **registration, uint32_t *registrationLen) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Register(application, applicationLen, challenge, challengeLen, registration, registrationLen); } \
  NS_IMETHOD Sign(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t *keyHandle, uint32_t keyHandleLen, uint8_t **signature, uint32_t *signatureLen) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Sign(application, applicationLen, challenge, challengeLen, keyHandle, keyHandleLen, signature, signatureLen); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsU2FToken : public nsIU2FToken
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIU2FTOKEN

  nsU2FToken();

private:
  ~nsU2FToken();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsU2FToken, nsIU2FToken)

nsU2FToken::nsU2FToken()
{
  /* member initializers and constructor code */
}

nsU2FToken::~nsU2FToken()
{
  /* destructor code */
}

/* void isCompatibleVersion (in AString version, [retval] out boolean result); */
NS_IMETHODIMP nsU2FToken::IsCompatibleVersion(const nsAString & version, bool *result)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void isRegistered ([array, size_is (keyHandleLen)] in octet keyHandle, in uint32_t keyHandleLen, [retval] out boolean result); */
NS_IMETHODIMP nsU2FToken::IsRegistered(uint8_t *keyHandle, uint32_t keyHandleLen, bool *result)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void register ([array, size_is (applicationLen)] in octet application, in uint32_t applicationLen, [array, size_is (challengeLen)] in octet challenge, in uint32_t challengeLen, [array, size_is (registrationLen)] out octet registration, out uint32_t registrationLen); */
NS_IMETHODIMP nsU2FToken::Register(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t **registration, uint32_t *registrationLen)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void sign ([array, size_is (applicationLen)] in octet application, in uint32_t applicationLen, [array, size_is (challengeLen)] in octet challenge, in uint32_t challengeLen, [array, size_is (keyHandleLen)] in octet keyHandle, in uint32_t keyHandleLen, [array, size_is (signatureLen)] out octet signature, out uint32_t signatureLen); */
NS_IMETHODIMP nsU2FToken::Sign(uint8_t *application, uint32_t applicationLen, uint8_t *challenge, uint32_t challengeLen, uint8_t *keyHandle, uint32_t keyHandleLen, uint8_t **signature, uint32_t *signatureLen)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIU2FToken_h__ */
