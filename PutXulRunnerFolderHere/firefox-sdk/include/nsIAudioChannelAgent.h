/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIAudioChannelAgent.idl
 */

#ifndef __gen_nsIAudioChannelAgent_h__
#define __gen_nsIAudioChannelAgent_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class mozIDOMWindow; /* forward declaration */

typedef uint32_t  nsSuspendedTypes;


/* starting interface:    nsISuspendedTypes */
#define NS_ISUSPENDEDTYPES_IID_STR "2822a840-f009-11e5-a837-0800200c9a66"

#define NS_ISUSPENDEDTYPES_IID \
  {0x2822a840, 0xf009, 0x11e5, \
    { 0xa8, 0x37, 0x08, 0x00, 0x20, 0x0c, 0x9a, 0x66 }}

class NS_NO_VTABLE nsISuspendedTypes : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_ISUSPENDEDTYPES_IID)

  enum {
    NONE_SUSPENDED = 0U,
    SUSPENDED_PAUSE = 1U,
    SUSPENDED_BLOCK = 2U,
    SUSPENDED_PAUSE_DISPOSABLE = 3U,
    SUSPENDED_STOP_DISPOSABLE = 4U
  };

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsISuspendedTypes, NS_ISUSPENDEDTYPES_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSISUSPENDEDTYPES \

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSISUSPENDEDTYPES \

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSISUSPENDEDTYPES(_to) \

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSISUSPENDEDTYPES(_to) \

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsSuspendedTypes : public nsISuspendedTypes
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSISUSPENDEDTYPES

  nsSuspendedTypes();

private:
  ~nsSuspendedTypes();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsSuspendedTypes, nsISuspendedTypes)

nsSuspendedTypes::nsSuspendedTypes()
{
  /* member initializers and constructor code */
}

nsSuspendedTypes::~nsSuspendedTypes()
{
  /* destructor code */
}

/* End of implementation class template. */
#endif

namespace mozilla {
namespace dom {
// It's defined in dom/audiochannel/AudioChannelService.h.
class AudioPlaybackConfig;
}
}

/* starting interface:    nsIAudioChannelAgentCallback */
#define NS_IAUDIOCHANNELAGENTCALLBACK_IID_STR "15c05894-408e-4798-b527-a8c32d9c5f8c"

#define NS_IAUDIOCHANNELAGENTCALLBACK_IID \
  {0x15c05894, 0x408e, 0x4798, \
    { 0xb5, 0x27, 0xa8, 0xc3, 0x2d, 0x9c, 0x5f, 0x8c }}

class NS_NO_VTABLE nsIAudioChannelAgentCallback : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IAUDIOCHANNELAGENTCALLBACK_IID)

  /* void windowVolumeChanged (in float aVolume, in bool aMuted); */
  NS_IMETHOD WindowVolumeChanged(float aVolume, bool aMuted) = 0;

  /* void windowSuspendChanged (in uint32_t aSuspend); */
  NS_IMETHOD WindowSuspendChanged(uint32_t aSuspend) = 0;

  /* void windowAudioCaptureChanged (in bool aCapture); */
  NS_IMETHOD WindowAudioCaptureChanged(bool aCapture) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAudioChannelAgentCallback, NS_IAUDIOCHANNELAGENTCALLBACK_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIAUDIOCHANNELAGENTCALLBACK \
  NS_IMETHOD WindowVolumeChanged(float aVolume, bool aMuted) override; \
  NS_IMETHOD WindowSuspendChanged(uint32_t aSuspend) override; \
  NS_IMETHOD WindowAudioCaptureChanged(bool aCapture) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIAUDIOCHANNELAGENTCALLBACK \
  NS_METHOD WindowVolumeChanged(float aVolume, bool aMuted); \
  NS_METHOD WindowSuspendChanged(uint32_t aSuspend); \
  NS_METHOD WindowAudioCaptureChanged(bool aCapture); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIAUDIOCHANNELAGENTCALLBACK(_to) \
  NS_IMETHOD WindowVolumeChanged(float aVolume, bool aMuted) override { return _to WindowVolumeChanged(aVolume, aMuted); } \
  NS_IMETHOD WindowSuspendChanged(uint32_t aSuspend) override { return _to WindowSuspendChanged(aSuspend); } \
  NS_IMETHOD WindowAudioCaptureChanged(bool aCapture) override { return _to WindowAudioCaptureChanged(aCapture); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIAUDIOCHANNELAGENTCALLBACK(_to) \
  NS_IMETHOD WindowVolumeChanged(float aVolume, bool aMuted) override { return !_to ? NS_ERROR_NULL_POINTER : _to->WindowVolumeChanged(aVolume, aMuted); } \
  NS_IMETHOD WindowSuspendChanged(uint32_t aSuspend) override { return !_to ? NS_ERROR_NULL_POINTER : _to->WindowSuspendChanged(aSuspend); } \
  NS_IMETHOD WindowAudioCaptureChanged(bool aCapture) override { return !_to ? NS_ERROR_NULL_POINTER : _to->WindowAudioCaptureChanged(aCapture); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAudioChannelAgentCallback : public nsIAudioChannelAgentCallback
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIAUDIOCHANNELAGENTCALLBACK

  nsAudioChannelAgentCallback();

private:
  ~nsAudioChannelAgentCallback();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAudioChannelAgentCallback, nsIAudioChannelAgentCallback)

nsAudioChannelAgentCallback::nsAudioChannelAgentCallback()
{
  /* member initializers and constructor code */
}

nsAudioChannelAgentCallback::~nsAudioChannelAgentCallback()
{
  /* destructor code */
}

/* void windowVolumeChanged (in float aVolume, in bool aMuted); */
NS_IMETHODIMP nsAudioChannelAgentCallback::WindowVolumeChanged(float aVolume, bool aMuted)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void windowSuspendChanged (in uint32_t aSuspend); */
NS_IMETHODIMP nsAudioChannelAgentCallback::WindowSuspendChanged(uint32_t aSuspend)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void windowAudioCaptureChanged (in bool aCapture); */
NS_IMETHODIMP nsAudioChannelAgentCallback::WindowAudioCaptureChanged(bool aCapture)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIAudioChannelAgent */
#define NS_IAUDIOCHANNELAGENT_IID_STR "ab7e21c0-970c-11e5-a837-0800200c9a66"

#define NS_IAUDIOCHANNELAGENT_IID \
  {0xab7e21c0, 0x970c, 0x11e5, \
    { 0xa8, 0x37, 0x08, 0x00, 0x20, 0x0c, 0x9a, 0x66 }}

class nsIAudioChannelAgent : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IAUDIOCHANNELAGENT_IID)

  enum {
    AUDIO_AGENT_CHANNEL_NORMAL = 0,
    AUDIO_AGENT_CHANNEL_CONTENT = 1,
    AUDIO_AGENT_CHANNEL_NOTIFICATION = 2,
    AUDIO_AGENT_CHANNEL_ALARM = 3,
    AUDIO_AGENT_CHANNEL_TELEPHONY = 4,
    AUDIO_AGENT_CHANNEL_RINGER = 5,
    AUDIO_AGENT_CHANNEL_PUBLICNOTIFICATION = 6,
    AUDIO_AGENT_CHANNEL_SYSTEM = 7,
    AUDIO_AGENT_CHANNEL_ERROR = 1000,
    AUDIO_AGENT_STATE_NORMAL = 0,
    AUDIO_AGENT_STATE_MUTED = 1,
    AUDIO_AGENT_STATE_FADED = 2
  };

  /* readonly attribute long audioChannelType; */
  NS_IMETHOD GetAudioChannelType(int32_t *aAudioChannelType) = 0;

   inline int32_t AudioChannelType() {
    int32_t channel;
    return NS_SUCCEEDED(GetAudioChannelType(&channel)) ? channel : AUDIO_AGENT_CHANNEL_ERROR;
  }
    /* void init (in mozIDOMWindow window, in long channelType, in nsIAudioChannelAgentCallback callback); */
  NS_IMETHOD Init(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback) = 0;

  /* void initWithWeakCallback (in mozIDOMWindow window, in long channelType, in nsIAudioChannelAgentCallback callback); */
  NS_IMETHOD InitWithWeakCallback(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback) = 0;

  /* void notifyStartedPlaying (in AudioPlaybackConfig config, in uint8_t audible); */
  NS_IMETHOD NotifyStartedPlaying(mozilla::dom::AudioPlaybackConfig *config, uint8_t audible) = 0;

  /* void notifyStoppedPlaying (); */
  NS_IMETHOD NotifyStoppedPlaying(void) = 0;

  /* void notifyStartedAudible (in uint8_t audible, in uint32_t reason); */
  NS_IMETHOD NotifyStartedAudible(uint8_t audible, uint32_t reason) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIAudioChannelAgent, NS_IAUDIOCHANNELAGENT_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIAUDIOCHANNELAGENT \
  NS_IMETHOD GetAudioChannelType(int32_t *aAudioChannelType) override; \
  NS_IMETHOD Init(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback) override; \
  NS_IMETHOD InitWithWeakCallback(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback) override; \
  NS_IMETHOD NotifyStartedPlaying(mozilla::dom::AudioPlaybackConfig *config, uint8_t audible) override; \
  NS_IMETHOD NotifyStoppedPlaying(void) override; \
  NS_IMETHOD NotifyStartedAudible(uint8_t audible, uint32_t reason) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIAUDIOCHANNELAGENT \
  NS_METHOD GetAudioChannelType(int32_t *aAudioChannelType); \
  NS_METHOD Init(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback); \
  NS_METHOD InitWithWeakCallback(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback); \
  NS_METHOD NotifyStartedPlaying(mozilla::dom::AudioPlaybackConfig *config, uint8_t audible); \
  NS_METHOD NotifyStoppedPlaying(void); \
  NS_METHOD NotifyStartedAudible(uint8_t audible, uint32_t reason); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIAUDIOCHANNELAGENT(_to) \
  NS_IMETHOD GetAudioChannelType(int32_t *aAudioChannelType) override { return _to GetAudioChannelType(aAudioChannelType); } \
  NS_IMETHOD Init(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback) override { return _to Init(window, channelType, callback); } \
  NS_IMETHOD InitWithWeakCallback(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback) override { return _to InitWithWeakCallback(window, channelType, callback); } \
  NS_IMETHOD NotifyStartedPlaying(mozilla::dom::AudioPlaybackConfig *config, uint8_t audible) override { return _to NotifyStartedPlaying(config, audible); } \
  NS_IMETHOD NotifyStoppedPlaying(void) override { return _to NotifyStoppedPlaying(); } \
  NS_IMETHOD NotifyStartedAudible(uint8_t audible, uint32_t reason) override { return _to NotifyStartedAudible(audible, reason); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIAUDIOCHANNELAGENT(_to) \
  NS_IMETHOD GetAudioChannelType(int32_t *aAudioChannelType) override { return !_to ? NS_ERROR_NULL_POINTER : _to->GetAudioChannelType(aAudioChannelType); } \
  NS_IMETHOD Init(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Init(window, channelType, callback); } \
  NS_IMETHOD InitWithWeakCallback(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback) override { return !_to ? NS_ERROR_NULL_POINTER : _to->InitWithWeakCallback(window, channelType, callback); } \
  NS_IMETHOD NotifyStartedPlaying(mozilla::dom::AudioPlaybackConfig *config, uint8_t audible) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NotifyStartedPlaying(config, audible); } \
  NS_IMETHOD NotifyStoppedPlaying(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NotifyStoppedPlaying(); } \
  NS_IMETHOD NotifyStartedAudible(uint8_t audible, uint32_t reason) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NotifyStartedAudible(audible, reason); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsAudioChannelAgent : public nsIAudioChannelAgent
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIAUDIOCHANNELAGENT

  nsAudioChannelAgent();

private:
  ~nsAudioChannelAgent();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsAudioChannelAgent, nsIAudioChannelAgent)

nsAudioChannelAgent::nsAudioChannelAgent()
{
  /* member initializers and constructor code */
}

nsAudioChannelAgent::~nsAudioChannelAgent()
{
  /* destructor code */
}

/* readonly attribute long audioChannelType; */
NS_IMETHODIMP nsAudioChannelAgent::GetAudioChannelType(int32_t *aAudioChannelType)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void init (in mozIDOMWindow window, in long channelType, in nsIAudioChannelAgentCallback callback); */
NS_IMETHODIMP nsAudioChannelAgent::Init(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void initWithWeakCallback (in mozIDOMWindow window, in long channelType, in nsIAudioChannelAgentCallback callback); */
NS_IMETHODIMP nsAudioChannelAgent::InitWithWeakCallback(mozIDOMWindow *window, int32_t channelType, nsIAudioChannelAgentCallback *callback)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void notifyStartedPlaying (in AudioPlaybackConfig config, in uint8_t audible); */
NS_IMETHODIMP nsAudioChannelAgent::NotifyStartedPlaying(mozilla::dom::AudioPlaybackConfig *config, uint8_t audible)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void notifyStoppedPlaying (); */
NS_IMETHODIMP nsAudioChannelAgent::NotifyStoppedPlaying()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void notifyStartedAudible (in uint8_t audible, in uint32_t reason); */
NS_IMETHODIMP nsAudioChannelAgent::NotifyStartedAudible(uint8_t audible, uint32_t reason)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIAudioChannelAgent_h__ */
