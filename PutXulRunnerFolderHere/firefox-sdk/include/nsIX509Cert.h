/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIX509Cert.idl
 */

#ifndef __gen_nsIX509Cert_h__
#define __gen_nsIX509Cert_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIArray; /* forward declaration */

class nsIX509CertValidity; /* forward declaration */

class nsIASN1Object; /* forward declaration */

class nsICertVerificationListener; /* forward declaration */

 /* forward declaration */
 typedef struct CERTCertificateStr CERTCertificate;

/* starting interface:    nsIX509Cert */
#define NS_IX509CERT_IID_STR "bdc3979a-5422-4cd5-8589-696b6e96ea83"

#define NS_IX509CERT_IID \
  {0xbdc3979a, 0x5422, 0x4cd5, \
    { 0x85, 0x89, 0x69, 0x6b, 0x6e, 0x96, 0xea, 0x83 }}

class NS_NO_VTABLE nsIX509Cert : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IX509CERT_IID)

  /* readonly attribute AString nickname; */
  NS_IMETHOD GetNickname(nsAString & aNickname) = 0;

  /* readonly attribute AString emailAddress; */
  NS_IMETHOD GetEmailAddress(nsAString & aEmailAddress) = 0;

  /* readonly attribute bool isBuiltInRoot; */
  NS_IMETHOD GetIsBuiltInRoot(bool *aIsBuiltInRoot) = 0;

  /* void getEmailAddresses (out unsigned long length, [array, size_is (length), retval] out wstring addresses); */
  NS_IMETHOD GetEmailAddresses(uint32_t *length, char16_t * **addresses) = 0;

  /* boolean containsEmailAddress (in AString aEmailAddress); */
  NS_IMETHOD ContainsEmailAddress(const nsAString & aEmailAddress, bool *_retval) = 0;

  /* readonly attribute AString subjectName; */
  NS_IMETHOD GetSubjectName(nsAString & aSubjectName) = 0;

  /* readonly attribute AString commonName; */
  NS_IMETHOD GetCommonName(nsAString & aCommonName) = 0;

  /* readonly attribute AString organization; */
  NS_IMETHOD GetOrganization(nsAString & aOrganization) = 0;

  /* readonly attribute AString organizationalUnit; */
  NS_IMETHOD GetOrganizationalUnit(nsAString & aOrganizationalUnit) = 0;

  /* readonly attribute AString sha256Fingerprint; */
  NS_IMETHOD GetSha256Fingerprint(nsAString & aSha256Fingerprint) = 0;

  /* readonly attribute AString sha1Fingerprint; */
  NS_IMETHOD GetSha1Fingerprint(nsAString & aSha1Fingerprint) = 0;

  /* readonly attribute AString tokenName; */
  NS_IMETHOD GetTokenName(nsAString & aTokenName) = 0;

  /* readonly attribute AString issuerName; */
  NS_IMETHOD GetIssuerName(nsAString & aIssuerName) = 0;

  /* readonly attribute AString serialNumber; */
  NS_IMETHOD GetSerialNumber(nsAString & aSerialNumber) = 0;

  /* readonly attribute AString issuerCommonName; */
  NS_IMETHOD GetIssuerCommonName(nsAString & aIssuerCommonName) = 0;

  /* readonly attribute AString issuerOrganization; */
  NS_IMETHOD GetIssuerOrganization(nsAString & aIssuerOrganization) = 0;

  /* readonly attribute AString issuerOrganizationUnit; */
  NS_IMETHOD GetIssuerOrganizationUnit(nsAString & aIssuerOrganizationUnit) = 0;

  /* readonly attribute nsIX509Cert issuer; */
  NS_IMETHOD GetIssuer(nsIX509Cert * *aIssuer) = 0;

  /* readonly attribute nsIX509CertValidity validity; */
  NS_IMETHOD GetValidity(nsIX509CertValidity * *aValidity) = 0;

  /* readonly attribute ACString dbKey; */
  NS_IMETHOD GetDbKey(nsACString & aDbKey) = 0;

  /* readonly attribute AString windowTitle; */
  NS_IMETHOD GetWindowTitle(nsAString & aWindowTitle) = 0;

  enum {
    UNKNOWN_CERT = 0U,
    CA_CERT = 1U,
    USER_CERT = 2U,
    EMAIL_CERT = 4U,
    SERVER_CERT = 8U,
    ANY_CERT = 65535U
  };

  /* readonly attribute unsigned long certType; */
  NS_IMETHOD GetCertType(uint32_t *aCertType) = 0;

  /* readonly attribute boolean isSelfSigned; */
  NS_IMETHOD GetIsSelfSigned(bool *aIsSelfSigned) = 0;

  enum {
    CMS_CHAIN_MODE_CertOnly = 1U,
    CMS_CHAIN_MODE_CertChain = 2U,
    CMS_CHAIN_MODE_CertChainWithRoot = 3U
  };

  /* nsIArray getChain (); */
  NS_IMETHOD GetChain(nsIArray * *_retval) = 0;

  /* readonly attribute AString keyUsages; */
  NS_IMETHOD GetKeyUsages(nsAString & aKeyUsages) = 0;

  /* readonly attribute nsIASN1Object ASN1Structure; */
  NS_IMETHOD GetASN1Structure(nsIASN1Object * *aASN1Structure) = 0;

  /* void getRawDER (out unsigned long length, [array, size_is (length), retval] out octet data); */
  NS_IMETHOD GetRawDER(uint32_t *length, uint8_t **data) = 0;

  /* boolean equals (in nsIX509Cert other); */
  NS_IMETHOD Equals(nsIX509Cert *other, bool *_retval) = 0;

  /* readonly attribute ACString sha256SubjectPublicKeyInfoDigest; */
  NS_IMETHOD GetSha256SubjectPublicKeyInfoDigest(nsACString & aSha256SubjectPublicKeyInfoDigest) = 0;

  /* void exportAsCMS (in unsigned long chainMode, out unsigned long length, [array, size_is (length), retval] out octet data); */
  NS_IMETHOD ExportAsCMS(uint32_t chainMode, uint32_t *length, uint8_t **data) = 0;

  /* [noscript,notxpcom] CERTCertificatePtr getCert (); */
  NS_IMETHOD_(CERTCertificate *) GetCert(void) = 0;

  /* void getAllTokenNames (out unsigned long length, [array, size_is (length), retval] out wstring tokenNames); */
  NS_IMETHOD GetAllTokenNames(uint32_t *length, char16_t * **tokenNames) = 0;

  /* void markForPermDeletion (); */
  NS_IMETHOD MarkForPermDeletion(void) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIX509Cert, NS_IX509CERT_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIX509CERT \
  NS_IMETHOD GetNickname(nsAString & aNickname) override; \
  NS_IMETHOD GetEmailAddress(nsAString & aEmailAddress) override; \
  NS_IMETHOD GetIsBuiltInRoot(bool *aIsBuiltInRoot) override; \
  NS_IMETHOD GetEmailAddresses(uint32_t *length, char16_t * **addresses) override; \
  NS_IMETHOD ContainsEmailAddress(const nsAString & aEmailAddress, bool *_retval) override; \
  NS_IMETHOD GetSubjectName(nsAString & aSubjectName) override; \
  NS_IMETHOD GetCommonName(nsAString & aCommonName) override; \
  NS_IMETHOD GetOrganization(nsAString & aOrganization) override; \
  NS_IMETHOD GetOrganizationalUnit(nsAString & aOrganizationalUnit) override; \
  NS_IMETHOD GetSha256Fingerprint(nsAString & aSha256Fingerprint) override; \
  NS_IMETHOD GetSha1Fingerprint(nsAString & aSha1Fingerprint) override; \
  NS_IMETHOD GetTokenName(nsAString & aTokenName) override; \
  NS_IMETHOD GetIssuerName(nsAString & aIssuerName) override; \
  NS_IMETHOD GetSerialNumber(nsAString & aSerialNumber) override; \
  NS_IMETHOD GetIssuerCommonName(nsAString & aIssuerCommonName) override; \
  NS_IMETHOD GetIssuerOrganization(nsAString & aIssuerOrganization) override; \
  NS_IMETHOD GetIssuerOrganizationUnit(nsAString & aIssuerOrganizationUnit) override; \
  NS_IMETHOD GetIssuer(nsIX509Cert * *aIssuer) override; \
  NS_IMETHOD GetValidity(nsIX509CertValidity * *aValidity) override; \
  NS_IMETHOD GetDbKey(nsACString & aDbKey) override; \
  NS_IMETHOD GetWindowTitle(nsAString & aWindowTitle) override; \
  NS_IMETHOD GetCertType(uint32_t *aCertType) override; \
  NS_IMETHOD GetIsSelfSigned(bool *aIsSelfSigned) override; \
  NS_IMETHOD GetChain(nsIArray * *_retval) override; \
  NS_IMETHOD GetKeyUsages(nsAString & aKeyUsages) override; \
  NS_IMETHOD GetASN1Structure(nsIASN1Object * *aASN1Structure) override; \
  NS_IMETHOD GetRawDER(uint32_t *length, uint8_t **data) override; \
  NS_IMETHOD Equals(nsIX509Cert *other, bool *_retval) override; \
  NS_IMETHOD GetSha256SubjectPublicKeyInfoDigest(nsACString & aSha256SubjectPublicKeyInfoDigest) override; \
  NS_IMETHOD ExportAsCMS(uint32_t chainMode, uint32_t *length, uint8_t **data) override; \
  NS_IMETHOD_(CERTCertificate *) GetCert(void) override; \
  NS_IMETHOD GetAllTokenNames(uint32_t *length, char16_t * **tokenNames) override; \
  NS_IMETHOD MarkForPermDeletion(void) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIX509CERT \
  NS_METHOD GetNickname(nsAString & aNickname); \
  NS_METHOD GetEmailAddress(nsAString & aEmailAddress); \
  NS_METHOD GetIsBuiltInRoot(bool *aIsBuiltInRoot); \
  NS_METHOD GetEmailAddresses(uint32_t *length, char16_t * **addresses); \
  NS_METHOD ContainsEmailAddress(const nsAString & aEmailAddress, bool *_retval); \
  NS_METHOD GetSubjectName(nsAString & aSubjectName); \
  NS_METHOD GetCommonName(nsAString & aCommonName); \
  NS_METHOD GetOrganization(nsAString & aOrganization); \
  NS_METHOD GetOrganizationalUnit(nsAString & aOrganizationalUnit); \
  NS_METHOD GetSha256Fingerprint(nsAString & aSha256Fingerprint); \
  NS_METHOD GetSha1Fingerprint(nsAString & aSha1Fingerprint); \
  NS_METHOD GetTokenName(nsAString & aTokenName); \
  NS_METHOD GetIssuerName(nsAString & aIssuerName); \
  NS_METHOD GetSerialNumber(nsAString & aSerialNumber); \
  NS_METHOD GetIssuerCommonName(nsAString & aIssuerCommonName); \
  NS_METHOD GetIssuerOrganization(nsAString & aIssuerOrganization); \
  NS_METHOD GetIssuerOrganizationUnit(nsAString & aIssuerOrganizationUnit); \
  NS_METHOD GetIssuer(nsIX509Cert * *aIssuer); \
  NS_METHOD GetValidity(nsIX509CertValidity * *aValidity); \
  NS_METHOD GetDbKey(nsACString & aDbKey); \
  NS_METHOD GetWindowTitle(nsAString & aWindowTitle); \
  NS_METHOD GetCertType(uint32_t *aCertType); \
  NS_METHOD GetIsSelfSigned(bool *aIsSelfSigned); \
  NS_METHOD GetChain(nsIArray * *_retval); \
  NS_METHOD GetKeyUsages(nsAString & aKeyUsages); \
  NS_METHOD GetASN1Structure(nsIASN1Object * *aASN1Structure); \
  NS_METHOD GetRawDER(uint32_t *length, uint8_t **data); \
  NS_METHOD Equals(nsIX509Cert *other, bool *_retval); \
  NS_METHOD GetSha256SubjectPublicKeyInfoDigest(nsACString & aSha256SubjectPublicKeyInfoDigest); \
  NS_METHOD ExportAsCMS(uint32_t chainMode, uint32_t *length, uint8_t **data); \
  NS_METHOD_(CERTCertificate *) GetCert(void); \
  NS_METHOD GetAllTokenNames(uint32_t *length, char16_t * **tokenNames); \
  NS_METHOD MarkForPermDeletion(void); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIX509CERT(_to) \
  NS_IMETHOD GetNickname(nsAString & aNickname) override { return _to GetNickname(aNickname); } \
  NS_IMETHOD GetEmailAddress(nsAString & aEmailAddress) override { return _to GetEmailAddress(aEmailAddress); } \
  NS_IMETHOD GetIsBuiltInRoot(bool *aIsBuiltInRoot) override { return _to GetIsBuiltInRoot(aIsBuiltInRoot); } \
  NS_IMETHOD GetEmailAddresses(uint32_t *length, char16_t * **addresses) override { return _to GetEmailAddresses(length, addresses); } \
  NS_IMETHOD ContainsEmailAddress(const nsAString & aEmailAddress, bool *_retval) override { return _to ContainsEmailAddress(aEmailAddress, _retval); } \
  NS_IMETHOD GetSubjectName(nsAString & aSubjectName) override { return _to GetSubjectName(aSubjectName); } \
  NS_IMETHOD GetCommonName(nsAString & aCommonName) override { return _to GetCommonName(aCommonName); } \
  NS_IMETHOD GetOrganization(nsAString & aOrganization) override { return _to GetOrganization(aOrganization); } \
  NS_IMETHOD GetOrganizationalUnit(nsAString & aOrganizationalUnit) override { return _to GetOrganizationalUnit(aOrganizationalUnit); } \
  NS_IMETHOD GetSha256Fingerprint(nsAString & aSha256Fingerprint) override { return _to GetSha256Fingerprint(aSha256Fingerprint); } \
  NS_IMETHOD GetSha1Fingerprint(nsAString & aSha1Fingerprint) override { return _to GetSha1Fingerprint(aSha1Fingerprint); } \
  NS_IMETHOD GetTokenName(nsAString & aTokenName) override { return _to GetTokenName(aTokenName); } \
  NS_IMETHOD GetIssuerName(nsAString & aIssuerName) override { return _to GetIssuerName(aIssuerName); } \
  NS_IMETHOD GetSerialNumber(nsAString & aSerialNumber) override { return _to GetSerialNumber(aSerialNumber); } \
  NS_IMETHOD GetIssuerCommonName(nsAString & aIssuerCommonName) override { return _to GetIssuerCommonName(aIssuerCommonName); } \
  NS_IMETHOD GetIssuerOrganization(nsAString & aIssuerOrganization) override { return _to GetIssuerOrganization(aIssuerOrganization); } \
  NS_IMETHOD GetIssuerOrganizationUnit(nsAString & aIssuerOrganizationUnit) override { return _to GetIssuerOrganizationUnit(aIssuerOrganizationUnit); } \
  NS_IMETHOD GetIssuer(nsIX509Cert * *aIssuer) override { return _to GetIssuer(aIssuer); } \
  NS_IMETHOD GetValidity(nsIX509CertValidity * *aValidity) override { return _to GetValidity(aValidity); } \
  NS_IMETHOD GetDbKey(nsACString & aDbKey) override { return _to GetDbKey(aDbKey); } \
  NS_IMETHOD GetWindowTitle(nsAString & aWindowTitle) override { return _to GetWindowTitle(aWindowTitle); } \
  NS_IMETHOD GetCertType(uint32_t *aCertType) override { return _to GetCertType(aCertType); } \
  NS_IMETHOD GetIsSelfSigned(bool *aIsSelfSigned) override { return _to GetIsSelfSigned(aIsSelfSigned); } \
  NS_IMETHOD GetChain(nsIArray * *_retval) override { return _to GetChain(_retval); } \
  NS_IMETHOD GetKeyUsages(nsAString & aKeyUsages) override { return _to GetKeyUsages(aKeyUsages); } \
  NS_IMETHOD GetASN1Structure(nsIASN1Object * *aASN1Structure) override { return _to GetASN1Structure(aASN1Structure); } \
  NS_IMETHOD GetRawDER(uint32_t *length, uint8_t **data) override { return _to GetRawDER(length, data); } \
  NS_IMETHOD Equals(nsIX509Cert *other, bool *_retval) override { return _to Equals(other, _retval); } \
  NS_IMETHOD GetSha256SubjectPublicKeyInfoDigest(nsACString & aSha256SubjectPublicKeyInfoDigest) override { return _to GetSha256SubjectPublicKeyInfoDigest(aSha256SubjectPublicKeyInfoDigest); } \
  NS_IMETHOD ExportAsCMS(uint32_t chainMode, uint32_t *length, uint8_t **data) override { return _to ExportAsCMS(chainMode, length, data); } \
  NS_IMETHOD_(CERTCertificate *) GetCert(void) override { return _to GetCert(); } \
  NS_IMETHOD GetAllTokenNames(uint32_t *length, char16_t * **tokenNames) override { return _to GetAllTokenNames(length, tokenNames); } \
  NS_IMETHOD MarkForPermDeletion(void) override { return _to MarkForPermDeletion(); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIX509CERT(_to) \
  NS_IMETHOD GetNickname(nsAString & aNickname) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetNickname(aNickname); } \
  NS_IMETHOD GetEmailAddress(nsAString & aEmailAddress) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetEmailAddress(aEmailAddress); } \
  NS_IMETHOD GetIsBuiltInRoot(bool *aIsBuiltInRoot) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIsBuiltInRoot(aIsBuiltInRoot); } \
  NS_IMETHOD GetEmailAddresses(uint32_t *length, char16_t * **addresses) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetEmailAddresses(length, addresses); } \
  NS_IMETHOD ContainsEmailAddress(const nsAString & aEmailAddress, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ContainsEmailAddress(aEmailAddress, _retval); } \
  NS_IMETHOD GetSubjectName(nsAString & aSubjectName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetSubjectName(aSubjectName); } \
  NS_IMETHOD GetCommonName(nsAString & aCommonName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCommonName(aCommonName); } \
  NS_IMETHOD GetOrganization(nsAString & aOrganization) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetOrganization(aOrganization); } \
  NS_IMETHOD GetOrganizationalUnit(nsAString & aOrganizationalUnit) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetOrganizationalUnit(aOrganizationalUnit); } \
  NS_IMETHOD GetSha256Fingerprint(nsAString & aSha256Fingerprint) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetSha256Fingerprint(aSha256Fingerprint); } \
  NS_IMETHOD GetSha1Fingerprint(nsAString & aSha1Fingerprint) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetSha1Fingerprint(aSha1Fingerprint); } \
  NS_IMETHOD GetTokenName(nsAString & aTokenName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTokenName(aTokenName); } \
  NS_IMETHOD GetIssuerName(nsAString & aIssuerName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIssuerName(aIssuerName); } \
  NS_IMETHOD GetSerialNumber(nsAString & aSerialNumber) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetSerialNumber(aSerialNumber); } \
  NS_IMETHOD GetIssuerCommonName(nsAString & aIssuerCommonName) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIssuerCommonName(aIssuerCommonName); } \
  NS_IMETHOD GetIssuerOrganization(nsAString & aIssuerOrganization) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIssuerOrganization(aIssuerOrganization); } \
  NS_IMETHOD GetIssuerOrganizationUnit(nsAString & aIssuerOrganizationUnit) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIssuerOrganizationUnit(aIssuerOrganizationUnit); } \
  NS_IMETHOD GetIssuer(nsIX509Cert * *aIssuer) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIssuer(aIssuer); } \
  NS_IMETHOD GetValidity(nsIX509CertValidity * *aValidity) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetValidity(aValidity); } \
  NS_IMETHOD GetDbKey(nsACString & aDbKey) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDbKey(aDbKey); } \
  NS_IMETHOD GetWindowTitle(nsAString & aWindowTitle) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetWindowTitle(aWindowTitle); } \
  NS_IMETHOD GetCertType(uint32_t *aCertType) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCertType(aCertType); } \
  NS_IMETHOD GetIsSelfSigned(bool *aIsSelfSigned) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIsSelfSigned(aIsSelfSigned); } \
  NS_IMETHOD GetChain(nsIArray * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetChain(_retval); } \
  NS_IMETHOD GetKeyUsages(nsAString & aKeyUsages) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetKeyUsages(aKeyUsages); } \
  NS_IMETHOD GetASN1Structure(nsIASN1Object * *aASN1Structure) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetASN1Structure(aASN1Structure); } \
  NS_IMETHOD GetRawDER(uint32_t *length, uint8_t **data) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetRawDER(length, data); } \
  NS_IMETHOD Equals(nsIX509Cert *other, bool *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Equals(other, _retval); } \
  NS_IMETHOD GetSha256SubjectPublicKeyInfoDigest(nsACString & aSha256SubjectPublicKeyInfoDigest) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetSha256SubjectPublicKeyInfoDigest(aSha256SubjectPublicKeyInfoDigest); } \
  NS_IMETHOD ExportAsCMS(uint32_t chainMode, uint32_t *length, uint8_t **data) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ExportAsCMS(chainMode, length, data); } \
  NS_IMETHOD_(CERTCertificate *) GetCert(void) override; \
  NS_IMETHOD GetAllTokenNames(uint32_t *length, char16_t * **tokenNames) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAllTokenNames(length, tokenNames); } \
  NS_IMETHOD MarkForPermDeletion(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->MarkForPermDeletion(); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsX509Cert : public nsIX509Cert
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIX509CERT

  nsX509Cert();

private:
  ~nsX509Cert();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsX509Cert, nsIX509Cert)

nsX509Cert::nsX509Cert()
{
  /* member initializers and constructor code */
}

nsX509Cert::~nsX509Cert()
{
  /* destructor code */
}

/* readonly attribute AString nickname; */
NS_IMETHODIMP nsX509Cert::GetNickname(nsAString & aNickname)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString emailAddress; */
NS_IMETHODIMP nsX509Cert::GetEmailAddress(nsAString & aEmailAddress)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute bool isBuiltInRoot; */
NS_IMETHODIMP nsX509Cert::GetIsBuiltInRoot(bool *aIsBuiltInRoot)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void getEmailAddresses (out unsigned long length, [array, size_is (length), retval] out wstring addresses); */
NS_IMETHODIMP nsX509Cert::GetEmailAddresses(uint32_t *length, char16_t * **addresses)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean containsEmailAddress (in AString aEmailAddress); */
NS_IMETHODIMP nsX509Cert::ContainsEmailAddress(const nsAString & aEmailAddress, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString subjectName; */
NS_IMETHODIMP nsX509Cert::GetSubjectName(nsAString & aSubjectName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString commonName; */
NS_IMETHODIMP nsX509Cert::GetCommonName(nsAString & aCommonName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString organization; */
NS_IMETHODIMP nsX509Cert::GetOrganization(nsAString & aOrganization)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString organizationalUnit; */
NS_IMETHODIMP nsX509Cert::GetOrganizationalUnit(nsAString & aOrganizationalUnit)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString sha256Fingerprint; */
NS_IMETHODIMP nsX509Cert::GetSha256Fingerprint(nsAString & aSha256Fingerprint)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString sha1Fingerprint; */
NS_IMETHODIMP nsX509Cert::GetSha1Fingerprint(nsAString & aSha1Fingerprint)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString tokenName; */
NS_IMETHODIMP nsX509Cert::GetTokenName(nsAString & aTokenName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString issuerName; */
NS_IMETHODIMP nsX509Cert::GetIssuerName(nsAString & aIssuerName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString serialNumber; */
NS_IMETHODIMP nsX509Cert::GetSerialNumber(nsAString & aSerialNumber)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString issuerCommonName; */
NS_IMETHODIMP nsX509Cert::GetIssuerCommonName(nsAString & aIssuerCommonName)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString issuerOrganization; */
NS_IMETHODIMP nsX509Cert::GetIssuerOrganization(nsAString & aIssuerOrganization)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString issuerOrganizationUnit; */
NS_IMETHODIMP nsX509Cert::GetIssuerOrganizationUnit(nsAString & aIssuerOrganizationUnit)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIX509Cert issuer; */
NS_IMETHODIMP nsX509Cert::GetIssuer(nsIX509Cert * *aIssuer)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIX509CertValidity validity; */
NS_IMETHODIMP nsX509Cert::GetValidity(nsIX509CertValidity * *aValidity)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute ACString dbKey; */
NS_IMETHODIMP nsX509Cert::GetDbKey(nsACString & aDbKey)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString windowTitle; */
NS_IMETHODIMP nsX509Cert::GetWindowTitle(nsAString & aWindowTitle)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute unsigned long certType; */
NS_IMETHODIMP nsX509Cert::GetCertType(uint32_t *aCertType)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute boolean isSelfSigned; */
NS_IMETHODIMP nsX509Cert::GetIsSelfSigned(bool *aIsSelfSigned)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* nsIArray getChain (); */
NS_IMETHODIMP nsX509Cert::GetChain(nsIArray * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute AString keyUsages; */
NS_IMETHODIMP nsX509Cert::GetKeyUsages(nsAString & aKeyUsages)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIASN1Object ASN1Structure; */
NS_IMETHODIMP nsX509Cert::GetASN1Structure(nsIASN1Object * *aASN1Structure)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void getRawDER (out unsigned long length, [array, size_is (length), retval] out octet data); */
NS_IMETHODIMP nsX509Cert::GetRawDER(uint32_t *length, uint8_t **data)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* boolean equals (in nsIX509Cert other); */
NS_IMETHODIMP nsX509Cert::Equals(nsIX509Cert *other, bool *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute ACString sha256SubjectPublicKeyInfoDigest; */
NS_IMETHODIMP nsX509Cert::GetSha256SubjectPublicKeyInfoDigest(nsACString & aSha256SubjectPublicKeyInfoDigest)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void exportAsCMS (in unsigned long chainMode, out unsigned long length, [array, size_is (length), retval] out octet data); */
NS_IMETHODIMP nsX509Cert::ExportAsCMS(uint32_t chainMode, uint32_t *length, uint8_t **data)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript,notxpcom] CERTCertificatePtr getCert (); */
NS_IMETHODIMP_(CERTCertificate *) nsX509Cert::GetCert()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void getAllTokenNames (out unsigned long length, [array, size_is (length), retval] out wstring tokenNames); */
NS_IMETHODIMP nsX509Cert::GetAllTokenNames(uint32_t *length, char16_t * **tokenNames)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void markForPermDeletion (); */
NS_IMETHODIMP nsX509Cert::MarkForPermDeletion()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIX509Cert_h__ */
