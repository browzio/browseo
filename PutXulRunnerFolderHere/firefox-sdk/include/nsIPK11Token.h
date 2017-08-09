/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPK11Token.idl
 */

#ifndef __gen_nsIPK11Token_h__
#define __gen_nsIPK11Token_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIPK11Token */
#define NS_IPK11TOKEN_IID_STR "51191434-1dd2-11b2-a17c-e49c4e99a4e3"

#define NS_IPK11TOKEN_IID \
  {0x51191434, 0x1dd2, 0x11b2, \
    { 0xa1, 0x7c, 0xe4, 0x9c, 0x4e, 0x99, 0xa4, 0xe3 }}

class NS_NO_VTABLE nsIPK11Token : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPK11TOKEN_IID)

  enum {
    ASK_EVERY_TIME = -1,
    ASK_FIRST_TIME = 0,
    ASK_EXPIRE_TIME = 1
  };

  /* readonly attribute AUTF8String tokenName; */
  NS_IMETHOD GetTokenName(nsACString & aTokenName) = 0;

  /* readonly attribute AUTF8String tokenLabel; */
  NS_IMETHOD GetTokenLabel(nsACString & aTokenLabel) = 0;

  /* readonly attribute AUTF8String tokenManID; */
  NS_IMETHOD GetTokenManID(nsACString & aTokenManID) = 0;

  /* readonly attribute AUTF8String tokenHWVersion; */
  NS_IMETHOD GetTokenHWVersion(nsACString & aTokenHWVersion) = 0;

  /* readonly attribute AUTF8String tokenFWVersion; */
  NS_IMETHOD GetTokenFWVersion(nsACString & aTokenFWVersion) = 0;

  /* readonly attribute AUTF8String tokenSerialNumber; */
  NS_IMETHOD GetTokenSerialNumber(nsACString & aTokenSerialNumber) = 0;

  /* boolean isLoggedIn (); */
  NS_IMETHOD IsLoggedIn(bool *_retval) = 0;

  /* void login (in boolean force); */
  NS_IMETHOD Login(bool force) = 0;

  /* void logoutSimple (); */
  NS_IMETHOD LogoutSimple(void) = 0;

  /* void logoutAndDropAuthenticatedResources (); */
  NS_IMETHOD LogoutAndDropAuthenticatedResources(void) = 0;

  /* void reset (); */
  NS_IMETHOD Reset(void) = 0;

  /* readonly attribute long minimumPasswordLength; */
  NS_IMETHOD GetMinimumPasswordLength(int32_t *aMinimumPasswordLength) = 0;

  /* readonly attribute boolean needsUserInit; */
  NS_IMETHOD GetNeedsUserInit(bool *aNeedsUserInit) = 0;

  /* boolean checkPassword (in AUTF8String password); */
  NS_IMETHOD CheckPassword(const nsACString & password, bool *_retval) = 0;

  /* void initPassword (in AUTF8String initialPassword); */
  NS_IMETHOD InitPassword(const nsACString & initialPassword) = 0;

  /* void changePassword (in AUTF8String oldPassword, in AUTF8String newPassword); */
  NS_IMETHOD ChangePassword(const nsACString & oldPassword, const nsACString & newPassword) = 0;

  /* long getAskPasswordTimes (); */
  NS_IMETHOD GetAskPasswordTimes(int32_t *_retval) = 0;

  /* long getAskPasswordTimeout (); */
  NS_IMETHOD GetAskPasswordTimeout(int32_t *_retval) = 0;

  /* void setAskPasswordDefaults ([const] in long askTimes, [const] in long timeout); */
  NS_IMETHOD SetAskPasswordDefaults(const int32_t askTimes, const int32_t timeout) = 0;

  /* boolean isHardwareToken (); */
  NS_IMETHOD IsHardwareToken(bool *_retval) = 0;

  /* boolean needsLogin (); */
  NS_IMETHOD NeedsLogin(bool *_retval) = 0;

  /* boolean isFriendly (); */
  NS_IMETHOD IsFriendly(bool *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPK11Token, NS_IPK11TOKEN_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPK11TOKEN \
  NS_IMETHOD GetTokenName(nsACString & aTokenName) override; \
  NS_IMETHOD GetTokenLabel(nsACString & aTokenLabel) override; \
  NS_IMETHOD GetTokenManID(nsACString & aTokenManID) override; \
  NS_IMETHOD GetTokenHWVersion(nsACString & aTokenHWVersion) override; \
  NS_IMETHOD GetTokenFWVersion(nsACString & aTokenFWVersion) override; \
  NS_IMETHOD GetTokenSerialNumber(nsACString & aTokenSerialNumber) override; \
  NS_IMETHOD IsLoggedIn(bool *_retval) override; \
  NS_IMETHOD Login(bool force) override; \
  NS_IMETHOD LogoutSimple(void) override; \
  NS_IMETHOD LogoutAndDropAuthenticatedResources(void) override; \
  NS_IMETHOD Reset(void) override; \
  NS_IMETHOD GetMinimumPasswordLength(int32_t *aMinimumPasswordLength) override; \
  NS_IMETHOD GetNeedsUserInit(bool *aNeedsUserInit) override; \
  NS_IMETHOD CheckPassword(const nsACString & password, bool *_retval) override; \
  NS_IMETHOD InitPassword(const nsACString & initialPassword) override; \
  NS_IMETHOD ChangePassword(const nsACString & oldPassword, const nsACString & newPassword) override; \
  NS_IMETHOD GetAskPasswordTimes(int32_t *_retval) override; \
  NS_IMETHOD GetAskPasswordTimeout(int32_t *_retval) override; \
  NS_IMETHOD SetAskPasswordDefaults(const int32_t askTimes, const int32_t timeout) override; \
  NS_IMETHOD IsHardwareToken(bool *_retval) override; \
  NS_IMETHOD NeedsLogin(bool *_retval) override; \
  NS_IMETHOD IsFriendly(bool *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPK11TOKEN \
  NS_METHOD GetTokenName(nsACString & aTokenName); \
  NS_METHOD GetTokenLabel(nsACString & aTokenLabel); \
  NS_METHOD GetTokenManID(nsACString & aTokenManID); \
  NS_METHOD GetTokenHWVersion(nsACString & aTokenHWVersion); \
  NS_METHOD GetTokenFWVersion(nsACString & aTokenFWVersion); \
  NS_METHOD GetTokenSerialNumber(nsACString & aTokenSerialNumber); \
  NS_METHOD IsLoggedIn(bool *_retval); \
  NS_METHOD Login(bool force); \
  NS_METHOD LogoutSimple(void); \
  NS_METHOD LogoutAndDropAuthenticatedResources(void); \
  NS_METHOD Reset(void); \
  NS_METHOD GetMinimumPasswordLength(int32_t *aMinimumPasswordLength); \
  NS_METHOD GetNeedsUserInit(bool *aNeedsUserInit); \
  NS_METHOD CheckPassword(const nsACString & password, bool *_retval); \
  NS_METHOD InitPassword(const nsACString & initialPassword); \
  NS_METHOD ChangePassword(const nsACString & oldPassword, const nsACString & newPassword); \
  NS_METHOD GetAskPasswordTimes(int32_t *_retval); \
  NS_METHOD GetAskPasswordTimeout(int32_t *_retval); \
  NS_METHOD SetAskPasswordDefaults(const int32_t askTimes, const int32_t timeout); \
  NS_METHOD IsHardwareToken(bool *_retval); \
  NS_METHOD NeedsLogin(bool *_retval); \
  NS_METHOD IsFriendly(bool *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPK11TOKEN(_to) \
  NS_IMETHOD GetTokenName(nsACString & aTokenName) override { return _to GetTokenName(aTokenName); } \
  NS_IMETHOD GetTokenLabel(nsACString & aTokenLabel) override { return _to GetTokenLabel(aTokenLabel); } \
  NS_IMETHOD GetTokenManID(nsACString & aTokenManID) override { return _to GetTokenManID(aTokenManID); } \
  NS_IMETHOD GetTokenHWVersion(nsACString & aTokenHWVersion) override { return _to GetTokenHWVersion(aTokenHWVersion); } \
  NS_IMETHOD GetTokenFWVersion(nsACString & aTokenFWVersion) override { return _to GetTokenFWVersion(aTokenFWVersion); } \
  NS_IMETHOD GetTokenSerialNumber(nsACString & aTokenSerialNumber) override { return _to GetTokenSerialNumber(aTokenSerialNumber); } \
  NS_IMETHOD IsLoggedIn(bool *_retval) override { return _to IsLoggedIn(_retval); } \
  NS_IMETHOD Login(bool force) override { return _to Login(force); } \
  NS_IMETHOD LogoutSimple(void) override { return _to LogoutSimple(); } \
  NS_IMETHOD LogoutAndDropAuthenticatedResources(void) override { return _to LogoutAndDropAuthenticatedResources(); } \
  NS_IMETHOD Reset(void) override { return _to Reset(); } \
  NS_IMETHOD GetMinimumPasswordLength(int32_t *aMinimumPasswordLength) override { return _to GetMinimumPasswordLength(aMinimumPasswordLength); } \
  NS_IMETHOD GetNeedsUserInit(bool *aNeedsUserInit) override { return _to GetNeedsUserInit(aNeedsUserInit); } \
  NS_IMETHOD CheckPassword(const nsACString & password, bool *_retval) override { return _to CheckPassword(password, _retval); } \
  NS_IMETHOD InitPassword(const nsACString & initialPassword) override { return _to InitPassword(initialPassword); } \
  NS_IMETHOD ChangePassword(const nsACString & oldPassword, const nsACString & newPassword) override { return _to ChangePassword(oldPassword, newPassword); } \
  NS_IMETHOD GetAskPasswordTimes(int32_t *_retval) override { return _to GetAskPasswordTimes(_retval); } \
  NS_IMETHOD GetAskPasswordTimeout(int32_t *_retval) override { return _to GetAskPasswordTimeout(_retval); } \
  NS_IMETHOD SetAskPasswordDefaults(const int32_t askTimes, const int32_t timeout) override { return _to SetAskPasswordDefaults(askTimes, timeout); } \
  NS_IMETHOD IsHardwareToken(bool *_retval) override { return _to IsHardwareToken(_retval); } \
  NS_IMETHOD NeedsLogin(bool *_retval) override { return _to NeedsLogin(_retval); } \
  NS_IMETHOD IsFriendly(bool *_retval) override { return _to IsFriendly(_retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPK11TOKEN(_to) \
  NS_IMETHOD GetTokenName(nsACString & aTokenName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTokenName(aTokenName); } \
  NS_IMETHOD GetTokenLabel(nsACString & aTokenLabel) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTokenLabel(aTokenLabel); } \
  NS_IMETHOD GetTokenManID(nsACString & aTokenManID) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTokenManID(aTokenManID); } \
  NS_IMETHOD GetTokenHWVersion(nsACString & aTokenHWVersion) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTokenHWVersion(aTokenHWVersion); } \
  NS_IMETHOD GetTokenFWVersion(nsACString & aTokenFWVersion) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTokenFWVersion(aTokenFWVersion); } \
  NS_IMETHOD GetTokenSerialNumber(nsACString & aTokenSerialNumber) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTokenSerialNumber(aTokenSerialNumber); } \
  NS_IMETHOD IsLoggedIn(bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->IsLoggedIn(_retval); } \
  NS_IMETHOD Login(bool force) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Login(force); } \
  NS_IMETHOD LogoutSimple(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->LogoutSimple(); } \
  NS_IMETHOD LogoutAndDropAuthenticatedResources(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->LogoutAndDropAuthenticatedResources(); } \
  NS_IMETHOD Reset(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Reset(); } \
  NS_IMETHOD GetMinimumPasswordLength(int32_t *aMinimumPasswordLength) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetMinimumPasswordLength(aMinimumPasswordLength); } \
  NS_IMETHOD GetNeedsUserInit(bool *aNeedsUserInit) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetNeedsUserInit(aNeedsUserInit); } \
  NS_IMETHOD CheckPassword(const nsACString & password, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CheckPassword(password, _retval); } \
  NS_IMETHOD InitPassword(const nsACString & initialPassword) override { return !_to ? NS_ERROR_NULL_POINTER : _to->InitPassword(initialPassword); } \
  NS_IMETHOD ChangePassword(const nsACString & oldPassword, const nsACString & newPassword) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ChangePassword(oldPassword, newPassword); } \
  NS_IMETHOD GetAskPasswordTimes(int32_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAskPasswordTimes(_retval); } \
  NS_IMETHOD GetAskPasswordTimeout(int32_t *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAskPasswordTimeout(_retval); } \
  NS_IMETHOD SetAskPasswordDefaults(const int32_t askTimes, const int32_t timeout) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetAskPasswordDefaults(askTimes, timeout); } \
  NS_IMETHOD IsHardwareToken(bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->IsHardwareToken(_retval); } \
  NS_IMETHOD NeedsLogin(bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NeedsLogin(_retval); } \
  NS_IMETHOD IsFriendly(bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->IsFriendly(_retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPK11Token : public nsIPK11Token
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPK11TOKEN

  nsPK11Token();

private:
  ~nsPK11Token();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPK11Token, nsIPK11Token)

nsPK11Token::nsPK11Token()
{
  /* member initializers and constructor code */
}

nsPK11Token::~nsPK11Token()
{
  /* destructor code */
}

/* readonly attribute AUTF8String tokenName; */
NS_IMETHODIMP nsPK11Token::GetTokenName(nsACString & aTokenName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String tokenLabel; */
NS_IMETHODIMP nsPK11Token::GetTokenLabel(nsACString & aTokenLabel)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String tokenManID; */
NS_IMETHODIMP nsPK11Token::GetTokenManID(nsACString & aTokenManID)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String tokenHWVersion; */
NS_IMETHODIMP nsPK11Token::GetTokenHWVersion(nsACString & aTokenHWVersion)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String tokenFWVersion; */
NS_IMETHODIMP nsPK11Token::GetTokenFWVersion(nsACString & aTokenFWVersion)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AUTF8String tokenSerialNumber; */
NS_IMETHODIMP nsPK11Token::GetTokenSerialNumber(nsACString & aTokenSerialNumber)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean isLoggedIn (); */
NS_IMETHODIMP nsPK11Token::IsLoggedIn(bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void login (in boolean force); */
NS_IMETHODIMP nsPK11Token::Login(bool force)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void logoutSimple (); */
NS_IMETHODIMP nsPK11Token::LogoutSimple()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void logoutAndDropAuthenticatedResources (); */
NS_IMETHODIMP nsPK11Token::LogoutAndDropAuthenticatedResources()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void reset (); */
NS_IMETHODIMP nsPK11Token::Reset()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute long minimumPasswordLength; */
NS_IMETHODIMP nsPK11Token::GetMinimumPasswordLength(int32_t *aMinimumPasswordLength)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute boolean needsUserInit; */
NS_IMETHODIMP nsPK11Token::GetNeedsUserInit(bool *aNeedsUserInit)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean checkPassword (in AUTF8String password); */
NS_IMETHODIMP nsPK11Token::CheckPassword(const nsACString & password, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void initPassword (in AUTF8String initialPassword); */
NS_IMETHODIMP nsPK11Token::InitPassword(const nsACString & initialPassword)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void changePassword (in AUTF8String oldPassword, in AUTF8String newPassword); */
NS_IMETHODIMP nsPK11Token::ChangePassword(const nsACString & oldPassword, const nsACString & newPassword)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* long getAskPasswordTimes (); */
NS_IMETHODIMP nsPK11Token::GetAskPasswordTimes(int32_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* long getAskPasswordTimeout (); */
NS_IMETHODIMP nsPK11Token::GetAskPasswordTimeout(int32_t *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void setAskPasswordDefaults ([const] in long askTimes, [const] in long timeout); */
NS_IMETHODIMP nsPK11Token::SetAskPasswordDefaults(const int32_t askTimes, const int32_t timeout)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean isHardwareToken (); */
NS_IMETHODIMP nsPK11Token::IsHardwareToken(bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean needsLogin (); */
NS_IMETHODIMP nsPK11Token::NeedsLogin(bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean isFriendly (); */
NS_IMETHODIMP nsPK11Token::IsFriendly(bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPK11Token_h__ */
