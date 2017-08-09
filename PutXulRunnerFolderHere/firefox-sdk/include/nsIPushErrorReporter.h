/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIPushErrorReporter.idl
 */

#ifndef __gen_nsIPushErrorReporter_h__
#define __gen_nsIPushErrorReporter_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIPushErrorReporter */
#define NS_IPUSHERRORREPORTER_IID_STR "b58249f9-1a04-48cc-bc20-2c992d64c73e"

#define NS_IPUSHERRORREPORTER_IID \
  {0xb58249f9, 0x1a04, 0x48cc, \
    { 0xbc, 0x20, 0x2c, 0x99, 0x2d, 0x64, 0xc7, 0x3e }}

class NS_NO_VTABLE nsIPushErrorReporter : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IPUSHERRORREPORTER_IID)

  enum {
    ACK_DELIVERED = 0U,
    ACK_DECRYPTION_ERROR = 1U,
    ACK_NOT_DELIVERED = 2U,
    UNSUBSCRIBE_MANUAL = 3U,
    UNSUBSCRIBE_QUOTA_EXCEEDED = 4U,
    UNSUBSCRIBE_PERMISSION_REVOKED = 5U,
    DELIVERY_UNCAUGHT_EXCEPTION = 6U,
    DELIVERY_UNHANDLED_REJECTION = 7U,
    DELIVERY_INTERNAL_ERROR = 8U
  };

  /* void reportDeliveryError (in DOMString messageId, [optional] in uint16_t reason); */
  NS_IMETHOD ReportDeliveryError(const nsAString & messageId, uint16_t reason) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIPushErrorReporter, NS_IPUSHERRORREPORTER_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIPUSHERRORREPORTER \
  NS_IMETHOD ReportDeliveryError(const nsAString & messageId, uint16_t reason) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIPUSHERRORREPORTER \
  NS_METHOD ReportDeliveryError(const nsAString & messageId, uint16_t reason); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIPUSHERRORREPORTER(_to) \
  NS_IMETHOD ReportDeliveryError(const nsAString & messageId, uint16_t reason) override { return _to ReportDeliveryError(messageId, reason); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIPUSHERRORREPORTER(_to) \
  NS_IMETHOD ReportDeliveryError(const nsAString & messageId, uint16_t reason) override { return !_to ? NS_ERROR_NULL_POINTER : _to->ReportDeliveryError(messageId, reason); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsPushErrorReporter : public nsIPushErrorReporter
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIPUSHERRORREPORTER

  nsPushErrorReporter();

private:
  ~nsPushErrorReporter();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsPushErrorReporter, nsIPushErrorReporter)

nsPushErrorReporter::nsPushErrorReporter()
{
  /* member initializers and constructor code */
}

nsPushErrorReporter::~nsPushErrorReporter()
{
  /* destructor code */
}

/* void reportDeliveryError (in DOMString messageId, [optional] in uint16_t reason); */
NS_IMETHODIMP nsPushErrorReporter::ReportDeliveryError(const nsAString & messageId, uint16_t reason)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIPushErrorReporter_h__ */
