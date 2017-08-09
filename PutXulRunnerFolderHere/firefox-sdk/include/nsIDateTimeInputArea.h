/*
 * DO NOT EDIT.  THIS FILE IS GENERATED FROM ../../../dist/idl\nsIDateTimeInputArea.idl
 */

#ifndef __gen_nsIDateTimeInputArea_h__
#define __gen_nsIDateTimeInputArea_h__


#ifndef __gen_nsISupports_h__
#include "nsISupports.h"
#endif

#include "js/Value.h"

/* For IDL files that don't want to include root IDL files. */
#ifndef NS_NO_VTABLE
#define NS_NO_VTABLE
#endif

/* starting interface:    nsIDateTimeInputArea */
#define NS_IDATETIMEINPUTAREA_IID_STR "465c0cc3-24cb-48ce-af1a-b18402326b05"

#define NS_IDATETIMEINPUTAREA_IID \
  {0x465c0cc3, 0x24cb, 0x48ce, \
    { 0xaf, 0x1a, 0xb1, 0x84, 0x02, 0x32, 0x6b, 0x05 }}

class NS_NO_VTABLE nsIDateTimeInputArea : public nsISupports {
 public:

  NS_DECLARE_STATIC_IID_ACCESSOR(NS_IDATETIMEINPUTAREA_IID)

  /* void notifyInputElementValueChanged (); */
  NS_IMETHOD NotifyInputElementValueChanged(void) = 0;

  /* void setValueFromPicker (in jsval value); */
  NS_IMETHOD SetValueFromPicker(JS::HandleValue value) = 0;

  /* void focusInnerTextBox (); */
  NS_IMETHOD FocusInnerTextBox(void) = 0;

  /* void blurInnerTextBox (); */
  NS_IMETHOD BlurInnerTextBox(void) = 0;

  /* void setPickerState (in boolean isOpen); */
  NS_IMETHOD SetPickerState(bool isOpen) = 0;

};

  NS_DEFINE_STATIC_IID_ACCESSOR(nsIDateTimeInputArea, NS_IDATETIMEINPUTAREA_IID)

/* Use this macro when declaring classes that implement this interface. */
#define NS_DECL_NSIDATETIMEINPUTAREA \
  NS_IMETHOD NotifyInputElementValueChanged(void) override; \
  NS_IMETHOD SetValueFromPicker(JS::HandleValue value) override; \
  NS_IMETHOD FocusInnerTextBox(void) override; \
  NS_IMETHOD BlurInnerTextBox(void) override; \
  NS_IMETHOD SetPickerState(bool isOpen) override; 

/* Use this macro when declaring the members of this interface when the
   class doesn't implement the interface. This is useful for forwarding. */
#define NS_DECL_NON_VIRTUAL_NSIDATETIMEINPUTAREA \
  NS_METHOD NotifyInputElementValueChanged(void); \
  NS_METHOD SetValueFromPicker(JS::HandleValue value); \
  NS_METHOD FocusInnerTextBox(void); \
  NS_METHOD BlurInnerTextBox(void); \
  NS_METHOD SetPickerState(bool isOpen); 

/* Use this macro to declare functions that forward the behavior of this interface to another object. */
#define NS_FORWARD_NSIDATETIMEINPUTAREA(_to) \
  NS_IMETHOD NotifyInputElementValueChanged(void) override { return _to NotifyInputElementValueChanged(); } \
  NS_IMETHOD SetValueFromPicker(JS::HandleValue value) override { return _to SetValueFromPicker(value); } \
  NS_IMETHOD FocusInnerTextBox(void) override { return _to FocusInnerTextBox(); } \
  NS_IMETHOD BlurInnerTextBox(void) override { return _to BlurInnerTextBox(); } \
  NS_IMETHOD SetPickerState(bool isOpen) override { return _to SetPickerState(isOpen); } 

/* Use this macro to declare functions that forward the behavior of this interface to another object in a safe way. */
#define NS_FORWARD_SAFE_NSIDATETIMEINPUTAREA(_to) \
  NS_IMETHOD NotifyInputElementValueChanged(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->NotifyInputElementValueChanged(); } \
  NS_IMETHOD SetValueFromPicker(JS::HandleValue value) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetValueFromPicker(value); } \
  NS_IMETHOD FocusInnerTextBox(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->FocusInnerTextBox(); } \
  NS_IMETHOD BlurInnerTextBox(void) override { return !_to ? NS_ERROR_NULL_POINTER : _to->BlurInnerTextBox(); } \
  NS_IMETHOD SetPickerState(bool isOpen) override { return !_to ? NS_ERROR_NULL_POINTER : _to->SetPickerState(isOpen); } 

#if 0
/* Use the code below as a template for the implementation class for this interface. */

/* Header file */
class nsDateTimeInputArea : public nsIDateTimeInputArea
{
public:
  NS_DECL_ISUPPORTS
  NS_DECL_NSIDATETIMEINPUTAREA

  nsDateTimeInputArea();

private:
  ~nsDateTimeInputArea();

protected:
  /* additional members */
};

/* Implementation file */
NS_IMPL_ISUPPORTS(nsDateTimeInputArea, nsIDateTimeInputArea)

nsDateTimeInputArea::nsDateTimeInputArea()
{
  /* member initializers and constructor code */
}

nsDateTimeInputArea::~nsDateTimeInputArea()
{
  /* destructor code */
}

/* void notifyInputElementValueChanged (); */
NS_IMETHODIMP nsDateTimeInputArea::NotifyInputElementValueChanged()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void setValueFromPicker (in jsval value); */
NS_IMETHODIMP nsDateTimeInputArea::SetValueFromPicker(JS::HandleValue value)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void focusInnerTextBox (); */
NS_IMETHODIMP nsDateTimeInputArea::FocusInnerTextBox()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void blurInnerTextBox (); */
NS_IMETHODIMP nsDateTimeInputArea::BlurInnerTextBox()
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* void setPickerState (in boolean isOpen); */
NS_IMETHODIMP nsDateTimeInputArea::SetPickerState(bool isOpen)
{
    return NS_ERROR_NOT_IMPLEMENTED;
}

/* End of implementation class template. */
#endif


#endif /* __gen_nsIDateTimeInputArea_h__ */
