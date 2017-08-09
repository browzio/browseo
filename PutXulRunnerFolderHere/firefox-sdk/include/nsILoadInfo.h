/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsILoadInfo.idl
 */

#ifndef __gen_nsILoadInfo_h__
#define __gen_nsILoadInfo_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

#ifndef __gen_nsIContentPolicy_h__
#include "nsIContentPolicy.h"
#endif

#include "js/Value.h"

#include "mozilla/Assertions.h"
#include "mozilla/DebugOnly.h"

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIDOMDocument; /* forward declaration */

class nsINode; /* forward declaration */

class nsIPrincipal; /* forward declaration */

#include "nsTArray.h"
#include "mozilla/BasePrincipal.h"
#include "mozilla/LoadTainting.h"
class nsCString;
typedef uint32_t  nsSecurityFlags;


/* starting interface:    nsILoadInfo */
#define NS_ILOADINFO_IID_STR "ddc65bf9-2f60-41ab-b22a-4f1ae9efcd36"

#define NS_ILOADINFO_IID \
  {0xddc65bf9, 0x2f60, 0x41ab, \
    { 0xb2, 0x2a, 0x4f, 0x1a, 0xe9, 0xef, 0xcd, 0x36 }}

class nsILoadInfo : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ILOADINFO_IID)

  enum {
    SEC_NORMAL = 0U,
    SEC_REQUIRE_SAME_ORIGIN_DATA_INHERITS = 1U,
    SEC_REQUIRE_SAME_ORIGIN_DATA_IS_BLOCKED = 2U,
    SEC_ALLOW_CROSS_ORIGIN_DATA_INHERITS = 4U,
    SEC_ALLOW_CROSS_ORIGIN_DATA_IS_NULL = 8U,
    SEC_REQUIRE_CORS_DATA_INHERITS = 16U,
    SEC_COOKIES_DEFAULT = 0U,
    SEC_COOKIES_INCLUDE = 32U,
    SEC_COOKIES_SAME_ORIGIN = 64U,
    SEC_COOKIES_OMIT = 96U,
    SEC_FORCE_INHERIT_PRINCIPAL = 128U,
    SEC_SANDBOXED = 256U,
    SEC_ABOUT_BLANK_INHERITS = 512U,
    SEC_ALLOW_CHROME = 1024U,
    SEC_DISALLOW_SCRIPT = 2048U,
    SEC_DONT_FOLLOW_REDIRECTS = 4096U,
    SEC_LOAD_ERROR_PAGE = 8192U,
    SEC_FORCE_INHERIT_PRINCIPAL_OVERRULE_OWNER = 16384U
  };

  /* readonly attribute nsIPrincipal loadingPrincipal; */
  NS_IMETHOD GetLoadingPrincipal(nsIPrincipal * *aLoadingPrincipal) = 0;

  /* [binaryname(LoadingPrincipal),noscript,nostdcall,notxpcom] nsIPrincipal binaryLoadingPrincipal (); */
  virtual nsIPrincipal * LoadingPrincipal(void) = 0;

  /* readonly attribute nsIPrincipal triggeringPrincipal; */
  NS_IMETHOD GetTriggeringPrincipal(nsIPrincipal * *aTriggeringPrincipal) = 0;

  /* [binaryname(TriggeringPrincipal),noscript,nostdcall,notxpcom] nsIPrincipal binaryTriggeringPrincipal (); */
  virtual nsIPrincipal * TriggeringPrincipal(void) = 0;

  /* attribute nsIPrincipal principalToInherit; */
  NS_IMETHOD GetPrincipalToInherit(nsIPrincipal * *aPrincipalToInherit) = 0;
  NS_IMETHOD SetPrincipalToInherit(nsIPrincipal *aPrincipalToInherit) = 0;

  /* [binaryname(PrincipalToInherit),noscript,nostdcall,notxpcom] nsIPrincipal binaryPrincipalToInherit (); */
  virtual nsIPrincipal * PrincipalToInherit(void) = 0;

  /* readonly attribute nsIDOMDocument loadingDocument; */
  NS_IMETHOD GetLoadingDocument(nsIDOMDocument * *aLoadingDocument) = 0;

  /* [binaryname(LoadingNode),noscript,nostdcall,notxpcom] nsINode binaryLoadingNode (); */
  virtual nsINode * LoadingNode(void) = 0;

  /* readonly attribute nsSecurityFlags securityFlags; */
  NS_IMETHOD GetSecurityFlags(nsSecurityFlags *aSecurityFlags) = 0;

   inline nsSecurityFlags GetSecurityFlags()
  {
    nsSecurityFlags result;
    mozilla::DebugOnly<nsresult> rv = GetSecurityFlags(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }
  /* [infallible] readonly attribute unsigned long securityMode; */
  NS_IMETHOD GetSecurityMode(uint32_t *aSecurityMode) = 0;
  inline uint32_t GetSecurityMode()
  {
    uint32_t result;
    mozilla::DebugOnly<nsresult> rv = GetSecurityMode(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute boolean isInThirdPartyContext; */
  NS_IMETHOD GetIsInThirdPartyContext(bool *aIsInThirdPartyContext) = 0;
  inline bool GetIsInThirdPartyContext()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetIsInThirdPartyContext(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute unsigned long cookiePolicy; */
  NS_IMETHOD GetCookiePolicy(uint32_t *aCookiePolicy) = 0;
  inline uint32_t GetCookiePolicy()
  {
    uint32_t result;
    mozilla::DebugOnly<nsresult> rv = GetCookiePolicy(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute boolean forceInheritPrincipal; */
  NS_IMETHOD GetForceInheritPrincipal(bool *aForceInheritPrincipal) = 0;
  inline bool GetForceInheritPrincipal()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetForceInheritPrincipal(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute boolean forceInheritPrincipalOverruleOwner; */
  NS_IMETHOD GetForceInheritPrincipalOverruleOwner(bool *aForceInheritPrincipalOverruleOwner) = 0;
  inline bool GetForceInheritPrincipalOverruleOwner()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetForceInheritPrincipalOverruleOwner(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute boolean loadingSandboxed; */
  NS_IMETHOD GetLoadingSandboxed(bool *aLoadingSandboxed) = 0;
  inline bool GetLoadingSandboxed()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetLoadingSandboxed(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute boolean aboutBlankInherits; */
  NS_IMETHOD GetAboutBlankInherits(bool *aAboutBlankInherits) = 0;
  inline bool GetAboutBlankInherits()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetAboutBlankInherits(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute boolean allowChrome; */
  NS_IMETHOD GetAllowChrome(bool *aAllowChrome) = 0;
  inline bool GetAllowChrome()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetAllowChrome(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute boolean disallowScript; */
  NS_IMETHOD GetDisallowScript(bool *aDisallowScript) = 0;
  inline bool GetDisallowScript()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetDisallowScript(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute boolean dontFollowRedirects; */
  NS_IMETHOD GetDontFollowRedirects(bool *aDontFollowRedirects) = 0;
  inline bool GetDontFollowRedirects()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetDontFollowRedirects(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute boolean loadErrorPage; */
  NS_IMETHOD GetLoadErrorPage(bool *aLoadErrorPage) = 0;
  inline bool GetLoadErrorPage()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetLoadErrorPage(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* readonly attribute nsContentPolicyType externalContentPolicyType; */
  NS_IMETHOD GetExternalContentPolicyType(nsContentPolicyType *aExternalContentPolicyType) = 0;

   inline nsContentPolicyType GetExternalContentPolicyType()
  {
    nsContentPolicyType result;
    mozilla::DebugOnly<nsresult> rv = GetExternalContentPolicyType(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }
  /* [noscript,notxpcom] nsContentPolicyType internalContentPolicyType (); */
  NS_IMETHOD_(nsContentPolicyType) InternalContentPolicyType(void) = 0;

  /* [infallible] readonly attribute boolean upgradeInsecureRequests; */
  NS_IMETHOD GetUpgradeInsecureRequests(bool *aUpgradeInsecureRequests) = 0;
  inline bool GetUpgradeInsecureRequests()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetUpgradeInsecureRequests(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] attribute boolean verifySignedContent; */
  NS_IMETHOD GetVerifySignedContent(bool *aVerifySignedContent) = 0;
  inline bool GetVerifySignedContent()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetVerifySignedContent(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }
  NS_IMETHOD SetVerifySignedContent(bool aVerifySignedContent) = 0;

  /* [infallible] attribute boolean enforceSRI; */
  NS_IMETHOD GetEnforceSRI(bool *aEnforceSRI) = 0;
  inline bool GetEnforceSRI()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetEnforceSRI(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }
  NS_IMETHOD SetEnforceSRI(bool aEnforceSRI) = 0;

  /* [infallible] readonly attribute boolean forceInheritPrincipalDropped; */
  NS_IMETHOD GetForceInheritPrincipalDropped(bool *aForceInheritPrincipalDropped) = 0;
  inline bool GetForceInheritPrincipalDropped()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetForceInheritPrincipalDropped(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute unsigned long long innerWindowID; */
  NS_IMETHOD GetInnerWindowID(uint64_t *aInnerWindowID) = 0;
  inline uint64_t GetInnerWindowID()
  {
    uint64_t result;
    mozilla::DebugOnly<nsresult> rv = GetInnerWindowID(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute unsigned long long outerWindowID; */
  NS_IMETHOD GetOuterWindowID(uint64_t *aOuterWindowID) = 0;
  inline uint64_t GetOuterWindowID()
  {
    uint64_t result;
    mozilla::DebugOnly<nsresult> rv = GetOuterWindowID(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute unsigned long long parentOuterWindowID; */
  NS_IMETHOD GetParentOuterWindowID(uint64_t *aParentOuterWindowID) = 0;
  inline uint64_t GetParentOuterWindowID()
  {
    uint64_t result;
    mozilla::DebugOnly<nsresult> rv = GetParentOuterWindowID(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute unsigned long long frameOuterWindowID; */
  NS_IMETHOD GetFrameOuterWindowID(uint64_t *aFrameOuterWindowID) = 0;
  inline uint64_t GetFrameOuterWindowID()
  {
    uint64_t result;
    mozilla::DebugOnly<nsresult> rv = GetFrameOuterWindowID(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* void resetPrincipalsToNullPrincipal (); */
  NS_IMETHOD ResetPrincipalsToNullPrincipal(void) = 0;

  /* [binaryname(ScriptableOriginAttributes),implicit_jscontext] attribute jsval originAttributes; */
  NS_IMETHOD GetScriptableOriginAttributes(JSContext* cx, JS::MutableHandleValue aOriginAttributes) = 0;
  NS_IMETHOD SetScriptableOriginAttributes(JSContext* cx, JS::HandleValue aOriginAttributes) = 0;

  /* [binaryname(GetOriginAttributes),noscript,nostdcall] NeckoOriginAttributes binaryGetOriginAttributes (); */
  virtual nsresult GetOriginAttributes(mozilla::NeckoOriginAttributes *_retval) = 0;

  /* [binaryname(SetOriginAttributes),noscript,nostdcall] void binarySetOriginAttributes (in const_OriginAttributesRef aOriginAttrs); */
  virtual nsresult SetOriginAttributes(const mozilla::NeckoOriginAttributes & aOriginAttrs) = 0;

   inline mozilla::NeckoOriginAttributes GetOriginAttributes()
  {
    mozilla::NeckoOriginAttributes result;
    mozilla::DebugOnly<nsresult> rv = GetOriginAttributes(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }
  /* [infallible] attribute boolean enforceSecurity; */
  NS_IMETHOD GetEnforceSecurity(bool *aEnforceSecurity) = 0;
  inline bool GetEnforceSecurity()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetEnforceSecurity(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }
  NS_IMETHOD SetEnforceSecurity(bool aEnforceSecurity) = 0;

  /* [infallible] attribute boolean initialSecurityCheckDone; */
  NS_IMETHOD GetInitialSecurityCheckDone(bool *aInitialSecurityCheckDone) = 0;
  inline bool GetInitialSecurityCheckDone()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetInitialSecurityCheckDone(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }
  NS_IMETHOD SetInitialSecurityCheckDone(bool aInitialSecurityCheckDone) = 0;

  /* void appendRedirectedPrincipal (in nsIPrincipal principal, in boolean isInternalRedirect); */
  NS_IMETHOD AppendRedirectedPrincipal(nsIPrincipal *principal, bool isInternalRedirect) = 0;

  /* [implicit_jscontext] readonly attribute jsval redirectChainIncludingInternalRedirects; */
  NS_IMETHOD GetRedirectChainIncludingInternalRedirects(JSContext* cx, JS::MutableHandleValue aRedirectChainIncludingInternalRedirects) = 0;

  /* [binaryname(RedirectChainIncludingInternalRedirects),noscript,nostdcall,notxpcom] const_nsIPrincipalArray binaryRedirectChainIncludingInternalRedirects (); */
  virtual const nsTArray<nsCOMPtr<nsIPrincipal>> & RedirectChainIncludingInternalRedirects(void) = 0;

  /* [implicit_jscontext] readonly attribute jsval redirectChain; */
  NS_IMETHOD GetRedirectChain(JSContext* cx, JS::MutableHandleValue aRedirectChain) = 0;

  /* [binaryname(RedirectChain),noscript,nostdcall,notxpcom] const_nsIPrincipalArray binaryRedirectChain (); */
  virtual const nsTArray<nsCOMPtr<nsIPrincipal>> & RedirectChain(void) = 0;

  /* [noscript,nostdcall,notxpcom] void setCorsPreflightInfo (in StringArrayRef unsafeHeaders, in boolean forcePreflight); */
  virtual void SetCorsPreflightInfo(const nsTArray<nsCString> & unsafeHeaders, bool forcePreflight) = 0;

  /* [binaryname(CorsUnsafeHeaders),noscript,nostdcall,notxpcom] StringArrayRef corsUnsafeHeaders (); */
  virtual const nsTArray<nsCString> & CorsUnsafeHeaders(void) = 0;

  /* [infallible] readonly attribute boolean forcePreflight; */
  NS_IMETHOD GetForcePreflight(bool *aForcePreflight) = 0;
  inline bool GetForcePreflight()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetForcePreflight(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible] readonly attribute boolean isPreflight; */
  NS_IMETHOD GetIsPreflight(bool *aIsPreflight) = 0;
  inline bool GetIsPreflight()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetIsPreflight(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible,noscript] readonly attribute boolean forceHSTSPriming; */
  NS_IMETHOD GetForceHSTSPriming(bool *aForceHSTSPriming) = 0;
  inline bool GetForceHSTSPriming()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetForceHSTSPriming(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [infallible,noscript] readonly attribute boolean mixedContentWouldBlock; */
  NS_IMETHOD GetMixedContentWouldBlock(bool *aMixedContentWouldBlock) = 0;
  inline bool GetMixedContentWouldBlock()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetMixedContentWouldBlock(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

  /* [noscript,nostdcall,notxpcom] void setHSTSPriming (in boolean mixeContentWouldBlock); */
  virtual void SetHSTSPriming(bool mixeContentWouldBlock) = 0;

  /* [noscript,nostdcall,notxpcom] void clearHSTSPriming (); */
  virtual void ClearHSTSPriming(void) = 0;

  enum {
    TAINTING_BASIC = 0U,
    TAINTING_CORS = 1U,
    TAINTING_OPAQUE = 2U
  };

  /* readonly attribute unsigned long tainting; */
  NS_IMETHOD GetTainting(uint32_t *aTainting) = 0;

  /* void maybeIncreaseTainting (in unsigned long aTainting); */
  NS_IMETHOD MaybeIncreaseTainting(uint32_t aTainting) = 0;

   static_assert(TAINTING_BASIC == static_cast<uint32_t>(mozilla::LoadTainting::Basic),
                "basic tainting enums should match");
  static_assert(TAINTING_CORS == static_cast<uint32_t>(mozilla::LoadTainting::CORS),
                "cors tainting enums should match");
  static_assert(TAINTING_OPAQUE == static_cast<uint32_t>(mozilla::LoadTainting::Opaque),
                "opaque tainting enums should match");
  mozilla::LoadTainting GetTainting()
  {
    uint32_t tainting = TAINTING_BASIC;
    MOZ_ALWAYS_SUCCEEDS(GetTainting(&tainting));
    return static_cast<mozilla::LoadTainting>(tainting);
  }
  void MaybeIncreaseTainting(mozilla::LoadTainting aTainting)
  {
    uint32_t tainting = static_cast<uint32_t>(aTainting);
    MOZ_ALWAYS_SUCCEEDS(MaybeIncreaseTainting(tainting));
  }
  /* [infallible] readonly attribute boolean isTopLevelLoad; */
  NS_IMETHOD GetIsTopLevelLoad(bool *aIsTopLevelLoad) = 0;
  inline bool GetIsTopLevelLoad()
  {
    bool result;
    mozilla::DebugOnly<nsresult> rv = GetIsTopLevelLoad(&result);
    MOZ_ASSERT(NS_SUCCEEDED(rv));
    return result;
  }

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsILoadInfo, NS_ILOADINFO_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSILOADINFO \
  NS_IMETHOD GetLoadingPrincipal(nsIPrincipal * *aLoadingPrincipal) override; \
  virtual nsIPrincipal * LoadingPrincipal(void) override; \
  NS_IMETHOD GetTriggeringPrincipal(nsIPrincipal * *aTriggeringPrincipal) override; \
  virtual nsIPrincipal * TriggeringPrincipal(void) override; \
  NS_IMETHOD GetPrincipalToInherit(nsIPrincipal * *aPrincipalToInherit) override; \
  NS_IMETHOD SetPrincipalToInherit(nsIPrincipal *aPrincipalToInherit) override; \
  virtual nsIPrincipal * PrincipalToInherit(void) override; \
  NS_IMETHOD GetLoadingDocument(nsIDOMDocument * *aLoadingDocument) override; \
  virtual nsINode * LoadingNode(void) override; \
  NS_IMETHOD GetSecurityFlags(nsSecurityFlags *aSecurityFlags) override; \
  using nsILoadInfo::GetSecurityMode; \
  NS_IMETHOD GetSecurityMode(uint32_t *aSecurityMode) override; \
  using nsILoadInfo::GetIsInThirdPartyContext; \
  NS_IMETHOD GetIsInThirdPartyContext(bool *aIsInThirdPartyContext) override; \
  using nsILoadInfo::GetCookiePolicy; \
  NS_IMETHOD GetCookiePolicy(uint32_t *aCookiePolicy) override; \
  using nsILoadInfo::GetForceInheritPrincipal; \
  NS_IMETHOD GetForceInheritPrincipal(bool *aForceInheritPrincipal) override; \
  using nsILoadInfo::GetForceInheritPrincipalOverruleOwner; \
  NS_IMETHOD GetForceInheritPrincipalOverruleOwner(bool *aForceInheritPrincipalOverruleOwner) override; \
  using nsILoadInfo::GetLoadingSandboxed; \
  NS_IMETHOD GetLoadingSandboxed(bool *aLoadingSandboxed) override; \
  using nsILoadInfo::GetAboutBlankInherits; \
  NS_IMETHOD GetAboutBlankInherits(bool *aAboutBlankInherits) override; \
  using nsILoadInfo::GetAllowChrome; \
  NS_IMETHOD GetAllowChrome(bool *aAllowChrome) override; \
  using nsILoadInfo::GetDisallowScript; \
  NS_IMETHOD GetDisallowScript(bool *aDisallowScript) override; \
  using nsILoadInfo::GetDontFollowRedirects; \
  NS_IMETHOD GetDontFollowRedirects(bool *aDontFollowRedirects) override; \
  using nsILoadInfo::GetLoadErrorPage; \
  NS_IMETHOD GetLoadErrorPage(bool *aLoadErrorPage) override; \
  NS_IMETHOD GetExternalContentPolicyType(nsContentPolicyType *aExternalContentPolicyType) override; \
  NS_IMETHOD_(nsContentPolicyType) InternalContentPolicyType(void) override; \
  using nsILoadInfo::GetUpgradeInsecureRequests; \
  NS_IMETHOD GetUpgradeInsecureRequests(bool *aUpgradeInsecureRequests) override; \
  using nsILoadInfo::GetVerifySignedContent; \
  NS_IMETHOD GetVerifySignedContent(bool *aVerifySignedContent) override; \
  NS_IMETHOD SetVerifySignedContent(bool aVerifySignedContent) override; \
  using nsILoadInfo::GetEnforceSRI; \
  NS_IMETHOD GetEnforceSRI(bool *aEnforceSRI) override; \
  NS_IMETHOD SetEnforceSRI(bool aEnforceSRI) override; \
  using nsILoadInfo::GetForceInheritPrincipalDropped; \
  NS_IMETHOD GetForceInheritPrincipalDropped(bool *aForceInheritPrincipalDropped) override; \
  using nsILoadInfo::GetInnerWindowID; \
  NS_IMETHOD GetInnerWindowID(uint64_t *aInnerWindowID) override; \
  using nsILoadInfo::GetOuterWindowID; \
  NS_IMETHOD GetOuterWindowID(uint64_t *aOuterWindowID) override; \
  using nsILoadInfo::GetParentOuterWindowID; \
  NS_IMETHOD GetParentOuterWindowID(uint64_t *aParentOuterWindowID) override; \
  using nsILoadInfo::GetFrameOuterWindowID; \
  NS_IMETHOD GetFrameOuterWindowID(uint64_t *aFrameOuterWindowID) override; \
  NS_IMETHOD ResetPrincipalsToNullPrincipal(void) override; \
  NS_IMETHOD GetScriptableOriginAttributes(JSContext* cx, JS::MutableHandleValue aOriginAttributes) override; \
  NS_IMETHOD SetScriptableOriginAttributes(JSContext* cx, JS::HandleValue aOriginAttributes) override; \
  virtual nsresult GetOriginAttributes(mozilla::NeckoOriginAttributes *_retval) override; \
  virtual nsresult SetOriginAttributes(const mozilla::NeckoOriginAttributes & aOriginAttrs) override; \
  using nsILoadInfo::GetEnforceSecurity; \
  NS_IMETHOD GetEnforceSecurity(bool *aEnforceSecurity) override; \
  NS_IMETHOD SetEnforceSecurity(bool aEnforceSecurity) override; \
  using nsILoadInfo::GetInitialSecurityCheckDone; \
  NS_IMETHOD GetInitialSecurityCheckDone(bool *aInitialSecurityCheckDone) override; \
  NS_IMETHOD SetInitialSecurityCheckDone(bool aInitialSecurityCheckDone) override; \
  NS_IMETHOD AppendRedirectedPrincipal(nsIPrincipal *principal, bool isInternalRedirect) override; \
  NS_IMETHOD GetRedirectChainIncludingInternalRedirects(JSContext* cx, JS::MutableHandleValue aRedirectChainIncludingInternalRedirects) override; \
  virtual const nsTArray<nsCOMPtr<nsIPrincipal>> & RedirectChainIncludingInternalRedirects(void) override; \
  NS_IMETHOD GetRedirectChain(JSContext* cx, JS::MutableHandleValue aRedirectChain) override; \
  virtual const nsTArray<nsCOMPtr<nsIPrincipal>> & RedirectChain(void) override; \
  virtual void SetCorsPreflightInfo(const nsTArray<nsCString> & unsafeHeaders, bool forcePreflight) override; \
  virtual const nsTArray<nsCString> & CorsUnsafeHeaders(void) override; \
  using nsILoadInfo::GetForcePreflight; \
  NS_IMETHOD GetForcePreflight(bool *aForcePreflight) override; \
  using nsILoadInfo::GetIsPreflight; \
  NS_IMETHOD GetIsPreflight(bool *aIsPreflight) override; \
  using nsILoadInfo::GetForceHSTSPriming; \
  NS_IMETHOD GetForceHSTSPriming(bool *aForceHSTSPriming) override; \
  using nsILoadInfo::GetMixedContentWouldBlock; \
  NS_IMETHOD GetMixedContentWouldBlock(bool *aMixedContentWouldBlock) override; \
  virtual void SetHSTSPriming(bool mixeContentWouldBlock) override; \
  virtual void ClearHSTSPriming(void) override; \
  NS_IMETHOD GetTainting(uint32_t *aTainting) override; \
  NS_IMETHOD MaybeIncreaseTainting(uint32_t aTainting) override; \
  using nsILoadInfo::GetIsTopLevelLoad; \
  NS_IMETHOD GetIsTopLevelLoad(bool *aIsTopLevelLoad) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSILOADINFO \
  NS_METHOD GetLoadingPrincipal(nsIPrincipal * *aLoadingPrincipal); \
  nsIPrincipal * LoadingPrincipal(void); \
  NS_METHOD GetTriggeringPrincipal(nsIPrincipal * *aTriggeringPrincipal); \
  nsIPrincipal * TriggeringPrincipal(void); \
  NS_METHOD GetPrincipalToInherit(nsIPrincipal * *aPrincipalToInherit); \
  NS_METHOD SetPrincipalToInherit(nsIPrincipal *aPrincipalToInherit); \
  nsIPrincipal * PrincipalToInherit(void); \
  NS_METHOD GetLoadingDocument(nsIDOMDocument * *aLoadingDocument); \
  nsINode * LoadingNode(void); \
  NS_METHOD GetSecurityFlags(nsSecurityFlags *aSecurityFlags); \
  using nsILoadInfo::GetSecurityMode; \
  NS_METHOD GetSecurityMode(uint32_t *aSecurityMode); \
  using nsILoadInfo::GetIsInThirdPartyContext; \
  NS_METHOD GetIsInThirdPartyContext(bool *aIsInThirdPartyContext); \
  using nsILoadInfo::GetCookiePolicy; \
  NS_METHOD GetCookiePolicy(uint32_t *aCookiePolicy); \
  using nsILoadInfo::GetForceInheritPrincipal; \
  NS_METHOD GetForceInheritPrincipal(bool *aForceInheritPrincipal); \
  using nsILoadInfo::GetForceInheritPrincipalOverruleOwner; \
  NS_METHOD GetForceInheritPrincipalOverruleOwner(bool *aForceInheritPrincipalOverruleOwner); \
  using nsILoadInfo::GetLoadingSandboxed; \
  NS_METHOD GetLoadingSandboxed(bool *aLoadingSandboxed); \
  using nsILoadInfo::GetAboutBlankInherits; \
  NS_METHOD GetAboutBlankInherits(bool *aAboutBlankInherits); \
  using nsILoadInfo::GetAllowChrome; \
  NS_METHOD GetAllowChrome(bool *aAllowChrome); \
  using nsILoadInfo::GetDisallowScript; \
  NS_METHOD GetDisallowScript(bool *aDisallowScript); \
  using nsILoadInfo::GetDontFollowRedirects; \
  NS_METHOD GetDontFollowRedirects(bool *aDontFollowRedirects); \
  using nsILoadInfo::GetLoadErrorPage; \
  NS_METHOD GetLoadErrorPage(bool *aLoadErrorPage); \
  NS_METHOD GetExternalContentPolicyType(nsContentPolicyType *aExternalContentPolicyType); \
  NS_METHOD_(nsContentPolicyType) InternalContentPolicyType(void); \
  using nsILoadInfo::GetUpgradeInsecureRequests; \
  NS_METHOD GetUpgradeInsecureRequests(bool *aUpgradeInsecureRequests); \
  using nsILoadInfo::GetVerifySignedContent; \
  NS_METHOD GetVerifySignedContent(bool *aVerifySignedContent); \
  NS_METHOD SetVerifySignedContent(bool aVerifySignedContent); \
  using nsILoadInfo::GetEnforceSRI; \
  NS_METHOD GetEnforceSRI(bool *aEnforceSRI); \
  NS_METHOD SetEnforceSRI(bool aEnforceSRI); \
  using nsILoadInfo::GetForceInheritPrincipalDropped; \
  NS_METHOD GetForceInheritPrincipalDropped(bool *aForceInheritPrincipalDropped); \
  using nsILoadInfo::GetInnerWindowID; \
  NS_METHOD GetInnerWindowID(uint64_t *aInnerWindowID); \
  using nsILoadInfo::GetOuterWindowID; \
  NS_METHOD GetOuterWindowID(uint64_t *aOuterWindowID); \
  using nsILoadInfo::GetParentOuterWindowID; \
  NS_METHOD GetParentOuterWindowID(uint64_t *aParentOuterWindowID); \
  using nsILoadInfo::GetFrameOuterWindowID; \
  NS_METHOD GetFrameOuterWindowID(uint64_t *aFrameOuterWindowID); \
  NS_METHOD ResetPrincipalsToNullPrincipal(void); \
  NS_METHOD GetScriptableOriginAttributes(JSContext* cx, JS::MutableHandleValue aOriginAttributes); \
  NS_METHOD SetScriptableOriginAttributes(JSContext* cx, JS::HandleValue aOriginAttributes); \
  nsresult GetOriginAttributes(mozilla::NeckoOriginAttributes *_retval); \
  nsresult SetOriginAttributes(const mozilla::NeckoOriginAttributes & aOriginAttrs); \
  using nsILoadInfo::GetEnforceSecurity; \
  NS_METHOD GetEnforceSecurity(bool *aEnforceSecurity); \
  NS_METHOD SetEnforceSecurity(bool aEnforceSecurity); \
  using nsILoadInfo::GetInitialSecurityCheckDone; \
  NS_METHOD GetInitialSecurityCheckDone(bool *aInitialSecurityCheckDone); \
  NS_METHOD SetInitialSecurityCheckDone(bool aInitialSecurityCheckDone); \
  NS_METHOD AppendRedirectedPrincipal(nsIPrincipal *principal, bool isInternalRedirect); \
  NS_METHOD GetRedirectChainIncludingInternalRedirects(JSContext* cx, JS::MutableHandleValue aRedirectChainIncludingInternalRedirects); \
  const nsTArray<nsCOMPtr<nsIPrincipal>> & RedirectChainIncludingInternalRedirects(void); \
  NS_METHOD GetRedirectChain(JSContext* cx, JS::MutableHandleValue aRedirectChain); \
  const nsTArray<nsCOMPtr<nsIPrincipal>> & RedirectChain(void); \
  void SetCorsPreflightInfo(const nsTArray<nsCString> & unsafeHeaders, bool forcePreflight); \
  const nsTArray<nsCString> & CorsUnsafeHeaders(void); \
  using nsILoadInfo::GetForcePreflight; \
  NS_METHOD GetForcePreflight(bool *aForcePreflight); \
  using nsILoadInfo::GetIsPreflight; \
  NS_METHOD GetIsPreflight(bool *aIsPreflight); \
  using nsILoadInfo::GetForceHSTSPriming; \
  NS_METHOD GetForceHSTSPriming(bool *aForceHSTSPriming); \
  using nsILoadInfo::GetMixedContentWouldBlock; \
  NS_METHOD GetMixedContentWouldBlock(bool *aMixedContentWouldBlock); \
  void SetHSTSPriming(bool mixeContentWouldBlock); \
  void ClearHSTSPriming(void); \
  NS_METHOD GetTainting(uint32_t *aTainting); \
  NS_METHOD MaybeIncreaseTainting(uint32_t aTainting); \
  using nsILoadInfo::GetIsTopLevelLoad; \
  NS_METHOD GetIsTopLevelLoad(bool *aIsTopLevelLoad); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSILOADINFO(_to) \
  NS_IMETHOD GetLoadingPrincipal(nsIPrincipal * *aLoadingPrincipal) override { return _to GetLoadingPrincipal(aLoadingPrincipal); } \
  virtual nsIPrincipal * LoadingPrincipal(void) override { return _to LoadingPrincipal(); } \
  NS_IMETHOD GetTriggeringPrincipal(nsIPrincipal * *aTriggeringPrincipal) override { return _to GetTriggeringPrincipal(aTriggeringPrincipal); } \
  virtual nsIPrincipal * TriggeringPrincipal(void) override { return _to TriggeringPrincipal(); } \
  NS_IMETHOD GetPrincipalToInherit(nsIPrincipal * *aPrincipalToInherit) override { return _to GetPrincipalToInherit(aPrincipalToInherit); } \
  NS_IMETHOD SetPrincipalToInherit(nsIPrincipal *aPrincipalToInherit) override { return _to SetPrincipalToInherit(aPrincipalToInherit); } \
  virtual nsIPrincipal * PrincipalToInherit(void) override { return _to PrincipalToInherit(); } \
  NS_IMETHOD GetLoadingDocument(nsIDOMDocument * *aLoadingDocument) override { return _to GetLoadingDocument(aLoadingDocument); } \
  virtual nsINode * LoadingNode(void) override { return _to LoadingNode(); } \
  NS_IMETHOD GetSecurityFlags(nsSecurityFlags *aSecurityFlags) override { return _to GetSecurityFlags(aSecurityFlags); } \
  using nsILoadInfo::GetSecurityMode; \
  NS_IMETHOD GetSecurityMode(uint32_t *aSecurityMode) override { return _to GetSecurityMode(aSecurityMode); } \
  using nsILoadInfo::GetIsInThirdPartyContext; \
  NS_IMETHOD GetIsInThirdPartyContext(bool *aIsInThirdPartyContext) override { return _to GetIsInThirdPartyContext(aIsInThirdPartyContext); } \
  using nsILoadInfo::GetCookiePolicy; \
  NS_IMETHOD GetCookiePolicy(uint32_t *aCookiePolicy) override { return _to GetCookiePolicy(aCookiePolicy); } \
  using nsILoadInfo::GetForceInheritPrincipal; \
  NS_IMETHOD GetForceInheritPrincipal(bool *aForceInheritPrincipal) override { return _to GetForceInheritPrincipal(aForceInheritPrincipal); } \
  using nsILoadInfo::GetForceInheritPrincipalOverruleOwner; \
  NS_IMETHOD GetForceInheritPrincipalOverruleOwner(bool *aForceInheritPrincipalOverruleOwner) override { return _to GetForceInheritPrincipalOverruleOwner(aForceInheritPrincipalOverruleOwner); } \
  using nsILoadInfo::GetLoadingSandboxed; \
  NS_IMETHOD GetLoadingSandboxed(bool *aLoadingSandboxed) override { return _to GetLoadingSandboxed(aLoadingSandboxed); } \
  using nsILoadInfo::GetAboutBlankInherits; \
  NS_IMETHOD GetAboutBlankInherits(bool *aAboutBlankInherits) override { return _to GetAboutBlankInherits(aAboutBlankInherits); } \
  using nsILoadInfo::GetAllowChrome; \
  NS_IMETHOD GetAllowChrome(bool *aAllowChrome) override { return _to GetAllowChrome(aAllowChrome); } \
  using nsILoadInfo::GetDisallowScript; \
  NS_IMETHOD GetDisallowScript(bool *aDisallowScript) override { return _to GetDisallowScript(aDisallowScript); } \
  using nsILoadInfo::GetDontFollowRedirects; \
  NS_IMETHOD GetDontFollowRedirects(bool *aDontFollowRedirects) override { return _to GetDontFollowRedirects(aDontFollowRedirects); } \
  using nsILoadInfo::GetLoadErrorPage; \
  NS_IMETHOD GetLoadErrorPage(bool *aLoadErrorPage) override { return _to GetLoadErrorPage(aLoadErrorPage); } \
  NS_IMETHOD GetExternalContentPolicyType(nsContentPolicyType *aExternalContentPolicyType) override { return _to GetExternalContentPolicyType(aExternalContentPolicyType); } \
  NS_IMETHOD_(nsContentPolicyType) InternalContentPolicyType(void) override { return _to InternalContentPolicyType(); } \
  using nsILoadInfo::GetUpgradeInsecureRequests; \
  NS_IMETHOD GetUpgradeInsecureRequests(bool *aUpgradeInsecureRequests) override { return _to GetUpgradeInsecureRequests(aUpgradeInsecureRequests); } \
  using nsILoadInfo::GetVerifySignedContent; \
  NS_IMETHOD GetVerifySignedContent(bool *aVerifySignedContent) override { return _to GetVerifySignedContent(aVerifySignedContent); } \
  NS_IMETHOD SetVerifySignedContent(bool aVerifySignedContent) override { return _to SetVerifySignedContent(aVerifySignedContent); } \
  using nsILoadInfo::GetEnforceSRI; \
  NS_IMETHOD GetEnforceSRI(bool *aEnforceSRI) override { return _to GetEnforceSRI(aEnforceSRI); } \
  NS_IMETHOD SetEnforceSRI(bool aEnforceSRI) override { return _to SetEnforceSRI(aEnforceSRI); } \
  using nsILoadInfo::GetForceInheritPrincipalDropped; \
  NS_IMETHOD GetForceInheritPrincipalDropped(bool *aForceInheritPrincipalDropped) override { return _to GetForceInheritPrincipalDropped(aForceInheritPrincipalDropped); } \
  using nsILoadInfo::GetInnerWindowID; \
  NS_IMETHOD GetInnerWindowID(uint64_t *aInnerWindowID) override { return _to GetInnerWindowID(aInnerWindowID); } \
  using nsILoadInfo::GetOuterWindowID; \
  NS_IMETHOD GetOuterWindowID(uint64_t *aOuterWindowID) override { return _to GetOuterWindowID(aOuterWindowID); } \
  using nsILoadInfo::GetParentOuterWindowID; \
  NS_IMETHOD GetParentOuterWindowID(uint64_t *aParentOuterWindowID) override { return _to GetParentOuterWindowID(aParentOuterWindowID); } \
  using nsILoadInfo::GetFrameOuterWindowID; \
  NS_IMETHOD GetFrameOuterWindowID(uint64_t *aFrameOuterWindowID) override { return _to GetFrameOuterWindowID(aFrameOuterWindowID); } \
  NS_IMETHOD ResetPrincipalsToNullPrincipal(void) override { return _to ResetPrincipalsToNullPrincipal(); } \
  NS_IMETHOD GetScriptableOriginAttributes(JSContext* cx, JS::MutableHandleValue aOriginAttributes) override { return _to GetScriptableOriginAttributes(cx, aOriginAttributes); } \
  NS_IMETHOD SetScriptableOriginAttributes(JSContext* cx, JS::HandleValue aOriginAttributes) override { return _to SetScriptableOriginAttributes(cx, aOriginAttributes); } \
  virtual nsresult GetOriginAttributes(mozilla::NeckoOriginAttributes *_retval) override { return _to GetOriginAttributes(_retval); } \
  virtual nsresult SetOriginAttributes(const mozilla::NeckoOriginAttributes & aOriginAttrs) override { return _to SetOriginAttributes(aOriginAttrs); } \
  using nsILoadInfo::GetEnforceSecurity; \
  NS_IMETHOD GetEnforceSecurity(bool *aEnforceSecurity) override { return _to GetEnforceSecurity(aEnforceSecurity); } \
  NS_IMETHOD SetEnforceSecurity(bool aEnforceSecurity) override { return _to SetEnforceSecurity(aEnforceSecurity); } \
  using nsILoadInfo::GetInitialSecurityCheckDone; \
  NS_IMETHOD GetInitialSecurityCheckDone(bool *aInitialSecurityCheckDone) override { return _to GetInitialSecurityCheckDone(aInitialSecurityCheckDone); } \
  NS_IMETHOD SetInitialSecurityCheckDone(bool aInitialSecurityCheckDone) override { return _to SetInitialSecurityCheckDone(aInitialSecurityCheckDone); } \
  NS_IMETHOD AppendRedirectedPrincipal(nsIPrincipal *principal, bool isInternalRedirect) override { return _to AppendRedirectedPrincipal(principal, isInternalRedirect); } \
  NS_IMETHOD GetRedirectChainIncludingInternalRedirects(JSContext* cx, JS::MutableHandleValue aRedirectChainIncludingInternalRedirects) override { return _to GetRedirectChainIncludingInternalRedirects(cx, aRedirectChainIncludingInternalRedirects); } \
  virtual const nsTArray<nsCOMPtr<nsIPrincipal>> & RedirectChainIncludingInternalRedirects(void) override { return _to RedirectChainIncludingInternalRedirects(); } \
  NS_IMETHOD GetRedirectChain(JSContext* cx, JS::MutableHandleValue aRedirectChain) override { return _to GetRedirectChain(cx, aRedirectChain); } \
  virtual const nsTArray<nsCOMPtr<nsIPrincipal>> & RedirectChain(void) override { return _to RedirectChain(); } \
  virtual void SetCorsPreflightInfo(const nsTArray<nsCString> & unsafeHeaders, bool forcePreflight) override { return _to SetCorsPreflightInfo(unsafeHeaders, forcePreflight); } \
  virtual const nsTArray<nsCString> & CorsUnsafeHeaders(void) override { return _to CorsUnsafeHeaders(); } \
  using nsILoadInfo::GetForcePreflight; \
  NS_IMETHOD GetForcePreflight(bool *aForcePreflight) override { return _to GetForcePreflight(aForcePreflight); } \
  using nsILoadInfo::GetIsPreflight; \
  NS_IMETHOD GetIsPreflight(bool *aIsPreflight) override { return _to GetIsPreflight(aIsPreflight); } \
  using nsILoadInfo::GetForceHSTSPriming; \
  NS_IMETHOD GetForceHSTSPriming(bool *aForceHSTSPriming) override { return _to GetForceHSTSPriming(aForceHSTSPriming); } \
  using nsILoadInfo::GetMixedContentWouldBlock; \
  NS_IMETHOD GetMixedContentWouldBlock(bool *aMixedContentWouldBlock) override { return _to GetMixedContentWouldBlock(aMixedContentWouldBlock); } \
  virtual void SetHSTSPriming(bool mixeContentWouldBlock) override { return _to SetHSTSPriming(mixeContentWouldBlock); } \
  virtual void ClearHSTSPriming(void) override { return _to ClearHSTSPriming(); } \
  NS_IMETHOD GetTainting(uint32_t *aTainting) override { return _to GetTainting(aTainting); } \
  NS_IMETHOD MaybeIncreaseTainting(uint32_t aTainting) override { return _to MaybeIncreaseTainting(aTainting); } \
  using nsILoadInfo::GetIsTopLevelLoad; \
  NS_IMETHOD GetIsTopLevelLoad(bool *aIsTopLevelLoad) override { return _to GetIsTopLevelLoad(aIsTopLevelLoad); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSILOADINFO(_to) \
  NS_IMETHOD GetLoadingPrincipal(nsIPrincipal * *aLoadingPrincipal) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLoadingPrincipal(aLoadingPrincipal); } \
  virtual nsIPrincipal * LoadingPrincipal(void) override; \
  NS_IMETHOD GetTriggeringPrincipal(nsIPrincipal * *aTriggeringPrincipal) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTriggeringPrincipal(aTriggeringPrincipal); } \
  virtual nsIPrincipal * TriggeringPrincipal(void) override; \
  NS_IMETHOD GetPrincipalToInherit(nsIPrincipal * *aPrincipalToInherit) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetPrincipalToInherit(aPrincipalToInherit); } \
  NS_IMETHOD SetPrincipalToInherit(nsIPrincipal *aPrincipalToInherit) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetPrincipalToInherit(aPrincipalToInherit); } \
  virtual nsIPrincipal * PrincipalToInherit(void) override; \
  NS_IMETHOD GetLoadingDocument(nsIDOMDocument * *aLoadingDocument) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLoadingDocument(aLoadingDocument); } \
  virtual nsINode * LoadingNode(void) override; \
  NS_IMETHOD GetSecurityFlags(nsSecurityFlags *aSecurityFlags) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetSecurityFlags(aSecurityFlags); } \
  NS_IMETHOD GetSecurityMode(uint32_t *aSecurityMode) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetSecurityMode(aSecurityMode); } \
  NS_IMETHOD GetIsInThirdPartyContext(bool *aIsInThirdPartyContext) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIsInThirdPartyContext(aIsInThirdPartyContext); } \
  NS_IMETHOD GetCookiePolicy(uint32_t *aCookiePolicy) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetCookiePolicy(aCookiePolicy); } \
  NS_IMETHOD GetForceInheritPrincipal(bool *aForceInheritPrincipal) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetForceInheritPrincipal(aForceInheritPrincipal); } \
  NS_IMETHOD GetForceInheritPrincipalOverruleOwner(bool *aForceInheritPrincipalOverruleOwner) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetForceInheritPrincipalOverruleOwner(aForceInheritPrincipalOverruleOwner); } \
  NS_IMETHOD GetLoadingSandboxed(bool *aLoadingSandboxed) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLoadingSandboxed(aLoadingSandboxed); } \
  NS_IMETHOD GetAboutBlankInherits(bool *aAboutBlankInherits) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAboutBlankInherits(aAboutBlankInherits); } \
  NS_IMETHOD GetAllowChrome(bool *aAllowChrome) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAllowChrome(aAllowChrome); } \
  NS_IMETHOD GetDisallowScript(bool *aDisallowScript) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDisallowScript(aDisallowScript); } \
  NS_IMETHOD GetDontFollowRedirects(bool *aDontFollowRedirects) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetDontFollowRedirects(aDontFollowRedirects); } \
  NS_IMETHOD GetLoadErrorPage(bool *aLoadErrorPage) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetLoadErrorPage(aLoadErrorPage); } \
  NS_IMETHOD GetExternalContentPolicyType(nsContentPolicyType *aExternalContentPolicyType) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetExternalContentPolicyType(aExternalContentPolicyType); } \
  NS_IMETHOD_(nsContentPolicyType) InternalContentPolicyType(void) override; \
  NS_IMETHOD GetUpgradeInsecureRequests(bool *aUpgradeInsecureRequests) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetUpgradeInsecureRequests(aUpgradeInsecureRequests); } \
  NS_IMETHOD GetVerifySignedContent(bool *aVerifySignedContent) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetVerifySignedContent(aVerifySignedContent); } \
  NS_IMETHOD SetVerifySignedContent(bool aVerifySignedContent) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetVerifySignedContent(aVerifySignedContent); } \
  NS_IMETHOD GetEnforceSRI(bool *aEnforceSRI) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetEnforceSRI(aEnforceSRI); } \
  NS_IMETHOD SetEnforceSRI(bool aEnforceSRI) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetEnforceSRI(aEnforceSRI); } \
  NS_IMETHOD GetForceInheritPrincipalDropped(bool *aForceInheritPrincipalDropped) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetForceInheritPrincipalDropped(aForceInheritPrincipalDropped); } \
  NS_IMETHOD GetInnerWindowID(uint64_t *aInnerWindowID) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetInnerWindowID(aInnerWindowID); } \
  NS_IMETHOD GetOuterWindowID(uint64_t *aOuterWindowID) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetOuterWindowID(aOuterWindowID); } \
  NS_IMETHOD GetParentOuterWindowID(uint64_t *aParentOuterWindowID) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetParentOuterWindowID(aParentOuterWindowID); } \
  NS_IMETHOD GetFrameOuterWindowID(uint64_t *aFrameOuterWindowID) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetFrameOuterWindowID(aFrameOuterWindowID); } \
  NS_IMETHOD ResetPrincipalsToNullPrincipal(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ResetPrincipalsToNullPrincipal(); } \
  NS_IMETHOD GetScriptableOriginAttributes(JSContext* cx, JS::MutableHandleValue aOriginAttributes) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetScriptableOriginAttributes(cx, aOriginAttributes); } \
  NS_IMETHOD SetScriptableOriginAttributes(JSContext* cx, JS::HandleValue aOriginAttributes) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetScriptableOriginAttributes(cx, aOriginAttributes); } \
  virtual nsresult GetOriginAttributes(mozilla::NeckoOriginAttributes *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetOriginAttributes(_retval); } \
  virtual nsresult SetOriginAttributes(const mozilla::NeckoOriginAttributes & aOriginAttrs) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetOriginAttributes(aOriginAttrs); } \
  NS_IMETHOD GetEnforceSecurity(bool *aEnforceSecurity) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetEnforceSecurity(aEnforceSecurity); } \
  NS_IMETHOD SetEnforceSecurity(bool aEnforceSecurity) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetEnforceSecurity(aEnforceSecurity); } \
  NS_IMETHOD GetInitialSecurityCheckDone(bool *aInitialSecurityCheckDone) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetInitialSecurityCheckDone(aInitialSecurityCheckDone); } \
  NS_IMETHOD SetInitialSecurityCheckDone(bool aInitialSecurityCheckDone) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetInitialSecurityCheckDone(aInitialSecurityCheckDone); } \
  NS_IMETHOD AppendRedirectedPrincipal(nsIPrincipal *principal, bool isInternalRedirect) override { return !_to ? NS_ERROR_NULL_POINTER : _to->AppendRedirectedPrincipal(principal, isInternalRedirect); } \
  NS_IMETHOD GetRedirectChainIncludingInternalRedirects(JSContext* cx, JS::MutableHandleValue aRedirectChainIncludingInternalRedirects) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetRedirectChainIncludingInternalRedirects(cx, aRedirectChainIncludingInternalRedirects); } \
  virtual const nsTArray<nsCOMPtr<nsIPrincipal>> & RedirectChainIncludingInternalRedirects(void) override; \
  NS_IMETHOD GetRedirectChain(JSContext* cx, JS::MutableHandleValue aRedirectChain) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetRedirectChain(cx, aRedirectChain); } \
  virtual const nsTArray<nsCOMPtr<nsIPrincipal>> & RedirectChain(void) override; \
  virtual void SetCorsPreflightInfo(const nsTArray<nsCString> & unsafeHeaders, bool forcePreflight) override; \
  virtual const nsTArray<nsCString> & CorsUnsafeHeaders(void) override; \
  NS_IMETHOD GetForcePreflight(bool *aForcePreflight) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetForcePreflight(aForcePreflight); } \
  NS_IMETHOD GetIsPreflight(bool *aIsPreflight) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIsPreflight(aIsPreflight); } \
  NS_IMETHOD GetForceHSTSPriming(bool *aForceHSTSPriming) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetForceHSTSPriming(aForceHSTSPriming); } \
  NS_IMETHOD GetMixedContentWouldBlock(bool *aMixedContentWouldBlock) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetMixedContentWouldBlock(aMixedContentWouldBlock); } \
  virtual void SetHSTSPriming(bool mixeContentWouldBlock) override; \
  virtual void ClearHSTSPriming(void) override; \
  NS_IMETHOD GetTainting(uint32_t *aTainting) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetTainting(aTainting); } \
  NS_IMETHOD MaybeIncreaseTainting(uint32_t aTainting) override { return !_to ? NS_ERROR_NULL_POINTER : _to->MaybeIncreaseTainting(aTainting); } \
  NS_IMETHOD GetIsTopLevelLoad(bool *aIsTopLevelLoad) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetIsTopLevelLoad(aIsTopLevelLoad); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsLoadInfo : public nsILoadInfo
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSILOADINFO

  nsLoadInfo();

private:
  ~nsLoadInfo();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsLoadInfo, nsILoadInfo)

nsLoadInfo::nsLoadInfo()
{
  /* member initializers and constructor code */
}

nsLoadInfo::~nsLoadInfo()
{
  /* destructor code */
}

/* readonly attribute nsIPrincipal loadingPrincipal; */
NS_IMETHODIMP nsLoadInfo::GetLoadingPrincipal(nsIPrincipal * *aLoadingPrincipal)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [binaryname(LoadingPrincipal),noscript,nostdcall,notxpcom] nsIPrincipal binaryLoadingPrincipal (); */
nsIPrincipal * nsLoadInfo::LoadingPrincipal()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIPrincipal triggeringPrincipal; */
NS_IMETHODIMP nsLoadInfo::GetTriggeringPrincipal(nsIPrincipal * *aTriggeringPrincipal)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [binaryname(TriggeringPrincipal),noscript,nostdcall,notxpcom] nsIPrincipal binaryTriggeringPrincipal (); */
nsIPrincipal * nsLoadInfo::TriggeringPrincipal()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* attribute nsIPrincipal principalToInherit; */
NS_IMETHODIMP nsLoadInfo::GetPrincipalToInherit(nsIPrincipal * *aPrincipalToInherit)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsLoadInfo::SetPrincipalToInherit(nsIPrincipal *aPrincipalToInherit)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [binaryname(PrincipalToInherit),noscript,nostdcall,notxpcom] nsIPrincipal binaryPrincipalToInherit (); */
nsIPrincipal * nsLoadInfo::PrincipalToInherit()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsIDOMDocument loadingDocument; */
NS_IMETHODIMP nsLoadInfo::GetLoadingDocument(nsIDOMDocument * *aLoadingDocument)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [binaryname(LoadingNode),noscript,nostdcall,notxpcom] nsINode binaryLoadingNode (); */
nsINode * nsLoadInfo::LoadingNode()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsSecurityFlags securityFlags; */
NS_IMETHODIMP nsLoadInfo::GetSecurityFlags(nsSecurityFlags *aSecurityFlags)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute unsigned long securityMode; */
NS_IMETHODIMP nsLoadInfo::GetSecurityMode(uint32_t *aSecurityMode)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean isInThirdPartyContext; */
NS_IMETHODIMP nsLoadInfo::GetIsInThirdPartyContext(bool *aIsInThirdPartyContext)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute unsigned long cookiePolicy; */
NS_IMETHODIMP nsLoadInfo::GetCookiePolicy(uint32_t *aCookiePolicy)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean forceInheritPrincipal; */
NS_IMETHODIMP nsLoadInfo::GetForceInheritPrincipal(bool *aForceInheritPrincipal)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean forceInheritPrincipalOverruleOwner; */
NS_IMETHODIMP nsLoadInfo::GetForceInheritPrincipalOverruleOwner(bool *aForceInheritPrincipalOverruleOwner)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean loadingSandboxed; */
NS_IMETHODIMP nsLoadInfo::GetLoadingSandboxed(bool *aLoadingSandboxed)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean aboutBlankInherits; */
NS_IMETHODIMP nsLoadInfo::GetAboutBlankInherits(bool *aAboutBlankInherits)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean allowChrome; */
NS_IMETHODIMP nsLoadInfo::GetAllowChrome(bool *aAllowChrome)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean disallowScript; */
NS_IMETHODIMP nsLoadInfo::GetDisallowScript(bool *aDisallowScript)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean dontFollowRedirects; */
NS_IMETHODIMP nsLoadInfo::GetDontFollowRedirects(bool *aDontFollowRedirects)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean loadErrorPage; */
NS_IMETHODIMP nsLoadInfo::GetLoadErrorPage(bool *aLoadErrorPage)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute nsContentPolicyType externalContentPolicyType; */
NS_IMETHODIMP nsLoadInfo::GetExternalContentPolicyType(nsContentPolicyType *aExternalContentPolicyType)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript,notxpcom] nsContentPolicyType internalContentPolicyType (); */
NS_IMETHODIMP_(nsContentPolicyType) nsLoadInfo::InternalContentPolicyType()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean upgradeInsecureRequests; */
NS_IMETHODIMP nsLoadInfo::GetUpgradeInsecureRequests(bool *aUpgradeInsecureRequests)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] attribute boolean verifySignedContent; */
NS_IMETHODIMP nsLoadInfo::GetVerifySignedContent(bool *aVerifySignedContent)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsLoadInfo::SetVerifySignedContent(bool aVerifySignedContent)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] attribute boolean enforceSRI; */
NS_IMETHODIMP nsLoadInfo::GetEnforceSRI(bool *aEnforceSRI)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsLoadInfo::SetEnforceSRI(bool aEnforceSRI)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean forceInheritPrincipalDropped; */
NS_IMETHODIMP nsLoadInfo::GetForceInheritPrincipalDropped(bool *aForceInheritPrincipalDropped)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute unsigned long long innerWindowID; */
NS_IMETHODIMP nsLoadInfo::GetInnerWindowID(uint64_t *aInnerWindowID)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute unsigned long long outerWindowID; */
NS_IMETHODIMP nsLoadInfo::GetOuterWindowID(uint64_t *aOuterWindowID)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute unsigned long long parentOuterWindowID; */
NS_IMETHODIMP nsLoadInfo::GetParentOuterWindowID(uint64_t *aParentOuterWindowID)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute unsigned long long frameOuterWindowID; */
NS_IMETHODIMP nsLoadInfo::GetFrameOuterWindowID(uint64_t *aFrameOuterWindowID)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void resetPrincipalsToNullPrincipal (); */
NS_IMETHODIMP nsLoadInfo::ResetPrincipalsToNullPrincipal()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [binaryname(ScriptableOriginAttributes),implicit_jscontext] attribute jsval originAttributes; */
NS_IMETHODIMP nsLoadInfo::GetScriptableOriginAttributes(JSContext* cx, JS::MutableHandleValue aOriginAttributes)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsLoadInfo::SetScriptableOriginAttributes(JSContext* cx, JS::HandleValue aOriginAttributes)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [binaryname(GetOriginAttributes),noscript,nostdcall] NeckoOriginAttributes binaryGetOriginAttributes (); */
nsresult nsLoadInfo::GetOriginAttributes(mozilla::NeckoOriginAttributes *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [binaryname(SetOriginAttributes),noscript,nostdcall] void binarySetOriginAttributes (in const_OriginAttributesRef aOriginAttrs); */
nsresult nsLoadInfo::SetOriginAttributes(const mozilla::NeckoOriginAttributes & aOriginAttrs)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] attribute boolean enforceSecurity; */
NS_IMETHODIMP nsLoadInfo::GetEnforceSecurity(bool *aEnforceSecurity)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsLoadInfo::SetEnforceSecurity(bool aEnforceSecurity)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] attribute boolean initialSecurityCheckDone; */
NS_IMETHODIMP nsLoadInfo::GetInitialSecurityCheckDone(bool *aInitialSecurityCheckDone)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}
NS_IMETHODIMP nsLoadInfo::SetInitialSecurityCheckDone(bool aInitialSecurityCheckDone)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void appendRedirectedPrincipal (in nsIPrincipal principal, in boolean isInternalRedirect); */
NS_IMETHODIMP nsLoadInfo::AppendRedirectedPrincipal(nsIPrincipal *principal, bool isInternalRedirect)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute jsval redirectChainIncludingInternalRedirects; */
NS_IMETHODIMP nsLoadInfo::GetRedirectChainIncludingInternalRedirects(JSContext* cx, JS::MutableHandleValue aRedirectChainIncludingInternalRedirects)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [binaryname(RedirectChainIncludingInternalRedirects),noscript,nostdcall,notxpcom] const_nsIPrincipalArray binaryRedirectChainIncludingInternalRedirects (); */
const nsTArray<nsCOMPtr<nsIPrincipal>> & nsLoadInfo::RedirectChainIncludingInternalRedirects()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [implicit_jscontext] readonly attribute jsval redirectChain; */
NS_IMETHODIMP nsLoadInfo::GetRedirectChain(JSContext* cx, JS::MutableHandleValue aRedirectChain)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [binaryname(RedirectChain),noscript,nostdcall,notxpcom] const_nsIPrincipalArray binaryRedirectChain (); */
const nsTArray<nsCOMPtr<nsIPrincipal>> & nsLoadInfo::RedirectChain()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript,nostdcall,notxpcom] void setCorsPreflightInfo (in StringArrayRef unsafeHeaders, in boolean forcePreflight); */
void nsLoadInfo::SetCorsPreflightInfo(const nsTArray<nsCString> & unsafeHeaders, bool forcePreflight)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [binaryname(CorsUnsafeHeaders),noscript,nostdcall,notxpcom] StringArrayRef corsUnsafeHeaders (); */
const nsTArray<nsCString> & nsLoadInfo::CorsUnsafeHeaders()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean forcePreflight; */
NS_IMETHODIMP nsLoadInfo::GetForcePreflight(bool *aForcePreflight)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean isPreflight; */
NS_IMETHODIMP nsLoadInfo::GetIsPreflight(bool *aIsPreflight)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible,noscript] readonly attribute boolean forceHSTSPriming; */
NS_IMETHODIMP nsLoadInfo::GetForceHSTSPriming(bool *aForceHSTSPriming)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible,noscript] readonly attribute boolean mixedContentWouldBlock; */
NS_IMETHODIMP nsLoadInfo::GetMixedContentWouldBlock(bool *aMixedContentWouldBlock)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript,nostdcall,notxpcom] void setHSTSPriming (in boolean mixeContentWouldBlock); */
void nsLoadInfo::SetHSTSPriming(bool mixeContentWouldBlock)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [noscript,nostdcall,notxpcom] void clearHSTSPriming (); */
void nsLoadInfo::ClearHSTSPriming()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* readonly attribute unsigned long tainting; */
NS_IMETHODIMP nsLoadInfo::GetTainting(uint32_t *aTainting)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void maybeIncreaseTainting (in unsigned long aTainting); */
NS_IMETHODIMP nsLoadInfo::MaybeIncreaseTainting(uint32_t aTainting)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* [infallible] readonly attribute boolean isTopLevelLoad; */
NS_IMETHODIMP nsLoadInfo::GetIsTopLevelLoad(bool *aIsTopLevelLoad)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsILoadInfo_h__ */
