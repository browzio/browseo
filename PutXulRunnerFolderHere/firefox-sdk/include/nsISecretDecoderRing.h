/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsISecretDecoderRing.idl
 */

#ifndef __gen_nsISecretDecoderRing_h__
#define __gen_nsISecretDecoderRing_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsISecretDecoderRing */
#define NS_ISECRETDECODERRING_IID_STR "0ec80360-075c-11d4-9fd4-00c04f1b83d8"

#define NS_ISECRETDECODERRING_IID \
  {0x0ec80360, 0x075c, 0x11d4, \
    { 0x9f, 0xd4, 0x00, 0xc0, 0x4f, 0x1b, 0x83, 0xd8 }}

class NS_NO_VTABLE nsISecretDecoderRing : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ISECRETDECODERRING_IID)

  /* ACString encryptString (in ACString text); */
  NS_IMETHOD EncryptString(const nsACString & text, nsACString & _retval) = 0;

  /* ACString decryptString (in ACString encryptedBase64Text); */
  NS_IMETHOD DecryptString(const nsACString & encryptedBase64Text, nsACString & _retval) = 0;

  /* void changePassword (); */
  NS_IMETHOD ChangePassword(void) = 0;

  /* void logout (); */
  NS_IMETHOD Logout(void) = 0;

  /* void logoutAndTeardown (); */
  NS_IMETHOD LogoutAndTeardown(void) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsISecretDecoderRing, NS_ISECRETDECODERRING_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSISECRETDECODERRING \
  NS_IMETHOD EncryptString(const nsACString & text, nsACString & _retval) override; \
  NS_IMETHOD DecryptString(const nsACString & encryptedBase64Text, nsACString & _retval) override; \
  NS_IMETHOD ChangePassword(void) override; \
  NS_IMETHOD Logout(void) override; \
  NS_IMETHOD LogoutAndTeardown(void) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSISECRETDECODERRING \
  NS_METHOD EncryptString(const nsACString & text, nsACString & _retval); \
  NS_METHOD DecryptString(const nsACString & encryptedBase64Text, nsACString & _retval); \
  NS_METHOD ChangePassword(void); \
  NS_METHOD Logout(void); \
  NS_METHOD LogoutAndTeardown(void); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSISECRETDECODERRING(_to) \
  NS_IMETHOD EncryptString(const nsACString & text, nsACString & _retval) override { return _to EncryptString(text, _retval); } \
  NS_IMETHOD DecryptString(const nsACString & encryptedBase64Text, nsACString & _retval) override { return _to DecryptString(encryptedBase64Text, _retval); } \
  NS_IMETHOD ChangePassword(void) override { return _to ChangePassword(); } \
  NS_IMETHOD Logout(void) override { return _to Logout(); } \
  NS_IMETHOD LogoutAndTeardown(void) override { return _to LogoutAndTeardown(); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSISECRETDECODERRING(_to) \
  NS_IMETHOD EncryptString(const nsACString & text, nsACString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->EncryptString(text, _retval); } \
  NS_IMETHOD DecryptString(const nsACString & encryptedBase64Text, nsACString & _retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->DecryptString(encryptedBase64Text, _retval); } \
  NS_IMETHOD ChangePassword(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ChangePassword(); } \
  NS_IMETHOD Logout(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Logout(); } \
  NS_IMETHOD LogoutAndTeardown(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->LogoutAndTeardown(); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsSecretDecoderRing : public nsISecretDecoderRing
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSISECRETDECODERRING

  nsSecretDecoderRing();

private:
  ~nsSecretDecoderRing();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsSecretDecoderRing, nsISecretDecoderRing)

nsSecretDecoderRing::nsSecretDecoderRing()
{
  /* member initializers and constructor code */
}

nsSecretDecoderRing::~nsSecretDecoderRing()
{
  /* destructor code */
}

/* ACString encryptString (in ACString text); */
NS_IMETHODIMP nsSecretDecoderRing::EncryptString(const nsACString & text, nsACString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* ACString decryptString (in ACString encryptedBase64Text); */
NS_IMETHODIMP nsSecretDecoderRing::DecryptString(const nsACString & encryptedBase64Text, nsACString & _retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void changePassword (); */
NS_IMETHODIMP nsSecretDecoderRing::ChangePassword()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void logout (); */
NS_IMETHODIMP nsSecretDecoderRing::Logout()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void logoutAndTeardown (); */
NS_IMETHODIMP nsSecretDecoderRing::LogoutAndTeardown()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsISecretDecoderRing_h__ */
