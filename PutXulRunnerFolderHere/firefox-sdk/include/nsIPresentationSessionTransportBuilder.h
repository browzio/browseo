/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPresentationSessionTransportBuilder.idl
 */

#ifndef __gen_nsIPresentationSessionTransportBuilder_h__
#define __gen_nsIPresentationSessionTransportBuilder_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif
class nsIPresentationChannelDescription; /* forward declaration */

class nsISocketTransport; /* forward declaration */

class mozIDOMWindow; /* forward declaration */

class nsIPresentationControlChannel; /* forward declaration */

class nsIPresentationSessionTransport; /* forward declaration */


/* starting interface:    nsIPresentationSessionTransportBuilderListener */
#define NS_IPRESENTATIONSESSIONTRANSPORTBUILDERLISTENER_IID_STR "673f6de1-e253-41b8-9be8-b7ff161fa8dc"

#define NS_IPRESENTATIONSESSIONTRANSPORTBUILDERLISTENER_IID \
  {0x673f6de1, 0xe253, 0x41b8, \
    { 0x9b, 0xe8, 0xb7, 0xff, 0x16, 0x1f, 0xa8, 0xdc }}

class NS_NO_VTABLE nsIPresentationSessionTransportBuilderListener : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONSESSIONTRANSPORTBUILDERLISTENER_IID)

  /* void onSessionTransport (in nsIPresentationSessionTransport transport); */
  NS_IMETHOD OnSessionTransport(nsIPresentationSessionTransport *transport) = 0;

  /* void onError (in nsresult reason); */
  NS_IMETHOD OnError(nsresult reason) = 0;

  /* void sendOffer (in nsIPresentationChannelDescription offer); */
  NS_IMETHOD SendOffer(nsIPresentationChannelDescription *offer) = 0;

  /* void sendAnswer (in nsIPresentationChannelDescription answer); */
  NS_IMETHOD SendAnswer(nsIPresentationChannelDescription *answer) = 0;

  /* void sendIceCandidate (in DOMString candidate); */
  NS_IMETHOD SendIceCandidate(const nsAString & candidate) = 0;

  /* void close (in nsresult reason); */
  NS_IMETHOD Close(nsresult reason) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationSessionTransportBuilderListener, NS_IPRESENTATIONSESSIONTRANSPORTBUILDERLISTENER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONSESSIONTRANSPORTBUILDERLISTENER \
  NS_IMETHOD OnSessionTransport(nsIPresentationSessionTransport *transport) override; \
  NS_IMETHOD OnError(nsresult reason) override; \
  NS_IMETHOD SendOffer(nsIPresentationChannelDescription *offer) override; \
  NS_IMETHOD SendAnswer(nsIPresentationChannelDescription *answer) override; \
  NS_IMETHOD SendIceCandidate(const nsAString & candidate) override; \
  NS_IMETHOD Close(nsresult reason) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONSESSIONTRANSPORTBUILDERLISTENER \
  NS_METHOD OnSessionTransport(nsIPresentationSessionTransport *transport); \
  NS_METHOD OnError(nsresult reason); \
  NS_METHOD SendOffer(nsIPresentationChannelDescription *offer); \
  NS_METHOD SendAnswer(nsIPresentationChannelDescription *answer); \
  NS_METHOD SendIceCandidate(const nsAString & candidate); \
  NS_METHOD Close(nsresult reason); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONSESSIONTRANSPORTBUILDERLISTENER(_to) \
  NS_IMETHOD OnSessionTransport(nsIPresentationSessionTransport *transport) override { return _to OnSessionTransport(transport); } \
  NS_IMETHOD OnError(nsresult reason) override { return _to OnError(reason); } \
  NS_IMETHOD SendOffer(nsIPresentationChannelDescription *offer) override { return _to SendOffer(offer); } \
  NS_IMETHOD SendAnswer(nsIPresentationChannelDescription *answer) override { return _to SendAnswer(answer); } \
  NS_IMETHOD SendIceCandidate(const nsAString & candidate) override { return _to SendIceCandidate(candidate); } \
  NS_IMETHOD Close(nsresult reason) override { return _to Close(reason); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONSESSIONTRANSPORTBUILDERLISTENER(_to) \
  NS_IMETHOD OnSessionTransport(nsIPresentationSessionTransport *transport) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnSessionTransport(transport); } \
  NS_IMETHOD OnError(nsresult reason) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnError(reason); } \
  NS_IMETHOD SendOffer(nsIPresentationChannelDescription *offer) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SendOffer(offer); } \
  NS_IMETHOD SendAnswer(nsIPresentationChannelDescription *answer) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SendAnswer(answer); } \
  NS_IMETHOD SendIceCandidate(const nsAString & candidate) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SendIceCandidate(candidate); } \
  NS_IMETHOD Close(nsresult reason) override { return !_to ? NS_ERROR_NULL_POINTER : _to->Close(reason); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationSessionTransportBuilderListener : public nsIPresentationSessionTransportBuilderListener
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONSESSIONTRANSPORTBUILDERLISTENER

  nsPresentationSessionTransportBuilderListener();

private:
  ~nsPresentationSessionTransportBuilderListener();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationSessionTransportBuilderListener, nsIPresentationSessionTransportBuilderListener)

nsPresentationSessionTransportBuilderListener::nsPresentationSessionTransportBuilderListener()
{
  /* member initializers and constructor code */
}

nsPresentationSessionTransportBuilderListener::~nsPresentationSessionTransportBuilderListener()
{
  /* destructor code */
}

/* void onSessionTransport (in nsIPresentationSessionTransport transport); */
NS_IMETHODIMP nsPresentationSessionTransportBuilderListener::OnSessionTransport(nsIPresentationSessionTransport *transport)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onError (in nsresult reason); */
NS_IMETHODIMP nsPresentationSessionTransportBuilderListener::OnError(nsresult reason)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void sendOffer (in nsIPresentationChannelDescription offer); */
NS_IMETHODIMP nsPresentationSessionTransportBuilderListener::SendOffer(nsIPresentationChannelDescription *offer)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void sendAnswer (in nsIPresentationChannelDescription answer); */
NS_IMETHODIMP nsPresentationSessionTransportBuilderListener::SendAnswer(nsIPresentationChannelDescription *answer)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void sendIceCandidate (in DOMString candidate); */
NS_IMETHODIMP nsPresentationSessionTransportBuilderListener::SendIceCandidate(const nsAString & candidate)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void close (in nsresult reason); */
NS_IMETHODIMP nsPresentationSessionTransportBuilderListener::Close(nsresult reason)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIPresentationSessionTransportBuilder */
#define NS_IPRESENTATIONSESSIONTRANSPORTBUILDER_IID_STR "2fdbe67d-80f9-48dc-8237-5bef8fa19801"

#define NS_IPRESENTATIONSESSIONTRANSPORTBUILDER_IID \
  {0x2fdbe67d, 0x80f9, 0x48dc, \
    { 0x82, 0x37, 0x5b, 0xef, 0x8f, 0xa1, 0x98, 0x01 }}

class NS_NO_VTABLE nsIPresentationSessionTransportBuilder : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONSESSIONTRANSPORTBUILDER_IID)

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationSessionTransportBuilder, NS_IPRESENTATIONSESSIONTRANSPORTBUILDER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONSESSIONTRANSPORTBUILDER \
  /* no methods! */

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONSESSIONTRANSPORTBUILDER \
  /* no methods! */

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONSESSIONTRANSPORTBUILDER(_to) \
  /* no methods! */

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONSESSIONTRANSPORTBUILDER(_to) \
  /* no methods! */

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationSessionTransportBuilder : public nsIPresentationSessionTransportBuilder
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONSESSIONTRANSPORTBUILDER

  nsPresentationSessionTransportBuilder();

private:
  ~nsPresentationSessionTransportBuilder();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationSessionTransportBuilder, nsIPresentationSessionTransportBuilder)

nsPresentationSessionTransportBuilder::nsPresentationSessionTransportBuilder()
{
  /* member initializers and constructor code */
}

nsPresentationSessionTransportBuilder::~nsPresentationSessionTransportBuilder()
{
  /* destructor code */
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIPresentationTransportBuilderConstructor */
#define NS_IPRESENTATIONTRANSPORTBUILDERCONSTRUCTOR_IID_STR "706482b2-1b51-4bed-a21d-785a9cfcfac7"

#define NS_IPRESENTATIONTRANSPORTBUILDERCONSTRUCTOR_IID \
  {0x706482b2, 0x1b51, 0x4bed, \
    { 0xa2, 0x1d, 0x78, 0x5a, 0x9c, 0xfc, 0xfa, 0xc7 }}

class NS_NO_VTABLE nsIPresentationTransportBuilderConstructor : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONTRANSPORTBUILDERCONSTRUCTOR_IID)

  /* nsIPresentationSessionTransportBuilder createTransportBuilder (in uint8_t type); */
  NS_IMETHOD CreateTransportBuilder(uint8_t type, nsIPresentationSessionTransportBuilder * *_retval) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationTransportBuilderConstructor, NS_IPRESENTATIONTRANSPORTBUILDERCONSTRUCTOR_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONTRANSPORTBUILDERCONSTRUCTOR \
  NS_IMETHOD CreateTransportBuilder(uint8_t type, nsIPresentationSessionTransportBuilder * *_retval) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONTRANSPORTBUILDERCONSTRUCTOR \
  NS_METHOD CreateTransportBuilder(uint8_t type, nsIPresentationSessionTransportBuilder * *_retval); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONTRANSPORTBUILDERCONSTRUCTOR(_to) \
  NS_IMETHOD CreateTransportBuilder(uint8_t type, nsIPresentationSessionTransportBuilder * *_retval) override { return _to CreateTransportBuilder(type, _retval); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONTRANSPORTBUILDERCONSTRUCTOR(_to) \
  NS_IMETHOD CreateTransportBuilder(uint8_t type, nsIPresentationSessionTransportBuilder * *_retval) override { return !_to ? NS_ERROR_NULL_POINTER : _to->CreateTransportBuilder(type, _retval); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationTransportBuilderConstructor : public nsIPresentationTransportBuilderConstructor
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONTRANSPORTBUILDERCONSTRUCTOR

  nsPresentationTransportBuilderConstructor();

private:
  ~nsPresentationTransportBuilderConstructor();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationTransportBuilderConstructor, nsIPresentationTransportBuilderConstructor)

nsPresentationTransportBuilderConstructor::nsPresentationTransportBuilderConstructor()
{
  /* member initializers and constructor code */
}

nsPresentationTransportBuilderConstructor::~nsPresentationTransportBuilderConstructor()
{
  /* destructor code */
}

/* nsIPresentationSessionTransportBuilder createTransportBuilder (in uint8_t type); */
NS_IMETHODIMP nsPresentationTransportBuilderConstructor::CreateTransportBuilder(uint8_t type, nsIPresentationSessionTransportBuilder * *_retval)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIPresentationTCPSessionTransportBuilder */
#define NS_IPRESENTATIONTCPSESSIONTRANSPORTBUILDER_IID_STR "cde36d6e-f471-4262-a70d-f932a26b21d9"

#define NS_IPRESENTATIONTCPSESSIONTRANSPORTBUILDER_IID \
  {0xcde36d6e, 0xf471, 0x4262, \
    { 0xa7, 0x0d, 0xf9, 0x32, 0xa2, 0x6b, 0x21, 0xd9 }}

class NS_NO_VTABLE nsIPresentationTCPSessionTransportBuilder : public nsIPresentationSessionTransportBuilder {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONTCPSESSIONTRANSPORTBUILDER_IID)

  /* void buildTCPSenderTransport (in nsISocketTransport aTransport, in nsIPresentationSessionTransportBuilderListener aListener); */
  NS_IMETHOD BuildTCPSenderTransport(nsISocketTransport *aTransport, nsIPresentationSessionTransportBuilderListener *aListener) = 0;

  /* void buildTCPReceiverTransport (in nsIPresentationChannelDescription aDescription, in nsIPresentationSessionTransportBuilderListener aListener); */
  NS_IMETHOD BuildTCPReceiverTransport(nsIPresentationChannelDescription *aDescription, nsIPresentationSessionTransportBuilderListener *aListener) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationTCPSessionTransportBuilder, NS_IPRESENTATIONTCPSESSIONTRANSPORTBUILDER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONTCPSESSIONTRANSPORTBUILDER \
  NS_IMETHOD BuildTCPSenderTransport(nsISocketTransport *aTransport, nsIPresentationSessionTransportBuilderListener *aListener) override; \
  NS_IMETHOD BuildTCPReceiverTransport(nsIPresentationChannelDescription *aDescription, nsIPresentationSessionTransportBuilderListener *aListener) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONTCPSESSIONTRANSPORTBUILDER \
  NS_METHOD BuildTCPSenderTransport(nsISocketTransport *aTransport, nsIPresentationSessionTransportBuilderListener *aListener); \
  NS_METHOD BuildTCPReceiverTransport(nsIPresentationChannelDescription *aDescription, nsIPresentationSessionTransportBuilderListener *aListener); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONTCPSESSIONTRANSPORTBUILDER(_to) \
  NS_IMETHOD BuildTCPSenderTransport(nsISocketTransport *aTransport, nsIPresentationSessionTransportBuilderListener *aListener) override { return _to BuildTCPSenderTransport(aTransport, aListener); } \
  NS_IMETHOD BuildTCPReceiverTransport(nsIPresentationChannelDescription *aDescription, nsIPresentationSessionTransportBuilderListener *aListener) override { return _to BuildTCPReceiverTransport(aDescription, aListener); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONTCPSESSIONTRANSPORTBUILDER(_to) \
  NS_IMETHOD BuildTCPSenderTransport(nsISocketTransport *aTransport, nsIPresentationSessionTransportBuilderListener *aListener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->BuildTCPSenderTransport(aTransport, aListener); } \
  NS_IMETHOD BuildTCPReceiverTransport(nsIPresentationChannelDescription *aDescription, nsIPresentationSessionTransportBuilderListener *aListener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->BuildTCPReceiverTransport(aDescription, aListener); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationTCPSessionTransportBuilder : public nsIPresentationTCPSessionTransportBuilder
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONTCPSESSIONTRANSPORTBUILDER

  nsPresentationTCPSessionTransportBuilder();

private:
  ~nsPresentationTCPSessionTransportBuilder();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationTCPSessionTransportBuilder, nsIPresentationTCPSessionTransportBuilder)

nsPresentationTCPSessionTransportBuilder::nsPresentationTCPSessionTransportBuilder()
{
  /* member initializers and constructor code */
}

nsPresentationTCPSessionTransportBuilder::~nsPresentationTCPSessionTransportBuilder()
{
  /* destructor code */
}

/* void buildTCPSenderTransport (in nsISocketTransport aTransport, in nsIPresentationSessionTransportBuilderListener aListener); */
NS_IMETHODIMP nsPresentationTCPSessionTransportBuilder::BuildTCPSenderTransport(nsISocketTransport *aTransport, nsIPresentationSessionTransportBuilderListener *aListener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void buildTCPReceiverTransport (in nsIPresentationChannelDescription aDescription, in nsIPresentationSessionTransportBuilderListener aListener); */
NS_IMETHODIMP nsPresentationTCPSessionTransportBuilder::BuildTCPReceiverTransport(nsIPresentationChannelDescription *aDescription, nsIPresentationSessionTransportBuilderListener *aListener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


/* starting interface:    nsIPresentationDataChannelSessionTransportBuilder */
#define NS_IPRESENTATIONDATACHANNELSESSIONTRANSPORTBUILDER_IID_STR "8131c4e0-3a8c-4bc1-a92a-8431473d2fe8"

#define NS_IPRESENTATIONDATACHANNELSESSIONTRANSPORTBUILDER_IID \
  {0x8131c4e0, 0x3a8c, 0x4bc1, \
    { 0xa9, 0x2a, 0x84, 0x31, 0x47, 0x3d, 0x2f, 0xe8 }}

class NS_NO_VTABLE nsIPresentationDataChannelSessionTransportBuilder : public nsIPresentationSessionTransportBuilder {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPRESENTATIONDATACHANNELSESSIONTRANSPORTBUILDER_IID)

  /* void buildDataChannelTransport (in uint8_t aRole, in mozIDOMWindow aWindow, in nsIPresentationSessionTransportBuilderListener aListener); */
  NS_IMETHOD BuildDataChannelTransport(uint8_t aRole, mozIDOMWindow *aWindow, nsIPresentationSessionTransportBuilderListener *aListener) = 0;

  /* void onOffer (in nsIPresentationChannelDescription offer); */
  NS_IMETHOD OnOffer(nsIPresentationChannelDescription *offer) = 0;

  /* void onAnswer (in nsIPresentationChannelDescription answer); */
  NS_IMETHOD OnAnswer(nsIPresentationChannelDescription *answer) = 0;

  /* void onIceCandidate (in DOMString candidate); */
  NS_IMETHOD OnIceCandidate(const nsAString & candidate) = 0;

  /* void notifyDisconnected (in nsresult reason); */
  NS_IMETHOD NotifyDisconnected(nsresult reason) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPresentationDataChannelSessionTransportBuilder, NS_IPRESENTATIONDATACHANNELSESSIONTRANSPORTBUILDER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPRESENTATIONDATACHANNELSESSIONTRANSPORTBUILDER \
  NS_IMETHOD BuildDataChannelTransport(uint8_t aRole, mozIDOMWindow *aWindow, nsIPresentationSessionTransportBuilderListener *aListener) override; \
  NS_IMETHOD OnOffer(nsIPresentationChannelDescription *offer) override; \
  NS_IMETHOD OnAnswer(nsIPresentationChannelDescription *answer) override; \
  NS_IMETHOD OnIceCandidate(const nsAString & candidate) override; \
  NS_IMETHOD NotifyDisconnected(nsresult reason) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPRESENTATIONDATACHANNELSESSIONTRANSPORTBUILDER \
  NS_METHOD BuildDataChannelTransport(uint8_t aRole, mozIDOMWindow *aWindow, nsIPresentationSessionTransportBuilderListener *aListener); \
  NS_METHOD OnOffer(nsIPresentationChannelDescription *offer); \
  NS_METHOD OnAnswer(nsIPresentationChannelDescription *answer); \
  NS_METHOD OnIceCandidate(const nsAString & candidate); \
  NS_METHOD NotifyDisconnected(nsresult reason); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPRESENTATIONDATACHANNELSESSIONTRANSPORTBUILDER(_to) \
  NS_IMETHOD BuildDataChannelTransport(uint8_t aRole, mozIDOMWindow *aWindow, nsIPresentationSessionTransportBuilderListener *aListener) override { return _to BuildDataChannelTransport(aRole, aWindow, aListener); } \
  NS_IMETHOD OnOffer(nsIPresentationChannelDescription *offer) override { return _to OnOffer(offer); } \
  NS_IMETHOD OnAnswer(nsIPresentationChannelDescription *answer) override { return _to OnAnswer(answer); } \
  NS_IMETHOD OnIceCandidate(const nsAString & candidate) override { return _to OnIceCandidate(candidate); } \
  NS_IMETHOD NotifyDisconnected(nsresult reason) override { return _to NotifyDisconnected(reason); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPRESENTATIONDATACHANNELSESSIONTRANSPORTBUILDER(_to) \
  NS_IMETHOD BuildDataChannelTransport(uint8_t aRole, mozIDOMWindow *aWindow, nsIPresentationSessionTransportBuilderListener *aListener) override { return !_to ? NS_ERROR_NULL_POINTER : _to->BuildDataChannelTransport(aRole, aWindow, aListener); } \
  NS_IMETHOD OnOffer(nsIPresentationChannelDescription *offer) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnOffer(offer); } \
  NS_IMETHOD OnAnswer(nsIPresentationChannelDescription *answer) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnAnswer(answer); } \
  NS_IMETHOD OnIceCandidate(const nsAString & candidate) override { return !_to ? NS_ERROR_NULL_POINTER : _to->OnIceCandidate(candidate); } \
  NS_IMETHOD NotifyDisconnected(nsresult reason) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NotifyDisconnected(reason); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPresentationDataChannelSessionTransportBuilder : public nsIPresentationDataChannelSessionTransportBuilder
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPRESENTATIONDATACHANNELSESSIONTRANSPORTBUILDER

  nsPresentationDataChannelSessionTransportBuilder();

private:
  ~nsPresentationDataChannelSessionTransportBuilder();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPresentationDataChannelSessionTransportBuilder, nsIPresentationDataChannelSessionTransportBuilder)

nsPresentationDataChannelSessionTransportBuilder::nsPresentationDataChannelSessionTransportBuilder()
{
  /* member initializers and constructor code */
}

nsPresentationDataChannelSessionTransportBuilder::~nsPresentationDataChannelSessionTransportBuilder()
{
  /* destructor code */
}

/* void buildDataChannelTransport (in uint8_t aRole, in mozIDOMWindow aWindow, in nsIPresentationSessionTransportBuilderListener aListener); */
NS_IMETHODIMP nsPresentationDataChannelSessionTransportBuilder::BuildDataChannelTransport(uint8_t aRole, mozIDOMWindow *aWindow, nsIPresentationSessionTransportBuilderListener *aListener)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onOffer (in nsIPresentationChannelDescription offer); */
NS_IMETHODIMP nsPresentationDataChannelSessionTransportBuilder::OnOffer(nsIPresentationChannelDescription *offer)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onAnswer (in nsIPresentationChannelDescription answer); */
NS_IMETHODIMP nsPresentationDataChannelSessionTransportBuilder::OnAnswer(nsIPresentationChannelDescription *answer)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void onIceCandidate (in DOMString candidate); */
NS_IMETHODIMP nsPresentationDataChannelSessionTransportBuilder::OnIceCandidate(const nsAString & candidate)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void notifyDisconnected (in nsresult reason); */
NS_IMETHODIMP nsPresentationDataChannelSessionTransportBuilder::NotifyDisconnected(nsresult reason)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPresentationSessionTransportBuilder_h__ */
