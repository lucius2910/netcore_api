import tl from "@/utils/locallize";

export default {
  required: (rule: any, value: any, callback: any) => {
    if (value === '' || value === null) {
      callback(new Error(tl("Common", "ValidateFieldRequied", [rule.field])))
    } else {
      callback()
    }
  },
  requiredRadioRule: (value: any, name: string) => {
    return value !== null || tl("Common", "ValidateFieldRequied", [name]);
  },
  minLengthRule: (value: any, name: string, min: number) => {
    return (
      value?.length >= min || tl("Common", "ValidateMinLength", [name, min])
    );
  },
  maxLengthRule: (value: any, name: string, max: number) => {
    if (!value) return true;
    return (
      value.length <= max || tl("Common", "ValidateMaxLength", [name, max])
    );
  },
  lengthRule: (value: any, name: string, length: number) => {
    return (
      value?.length === length || tl("Common", "ValidateLength", [name, length])
    );
  },
  emailRule: (rule: any, value: any, callback: any) => {
    if (!value) callback();

    if (/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(value)) {
      callback()
    } else {
      callback(new Error(tl("Common", "ValidateInvalid", [rule.field])))      
    }
  },
  dateRule: (rule: any, value: any, callback: any) => {
    if (!value) callback();
    // 2021-01-30
    if (/^([12]\d{3})[-](((0[13456789]|1[0-2])[-](0[1-9]|[12][0-9]|3[01]))|02[-](0[1-9]|[12][0-9]))$/.test(value)) {
      callback()
    } else {
      callback(new Error(tl("Common", "ValidateInvalid", [rule.field])))      
    }
  },
  phoneNumberRule: (rule: any, value: any, callback: any) => {
    if (!value) callback();

    if (/^[+0-9][./0-9]{8,19}$/.test(value)) {
      callback()
    } else {
      callback(new Error(tl("Common", "ValidateInvalid", [rule.field])))      
    }
  },
  checkPassword: (value: any, newVal: any, name: string) => {
    return value === newVal || tl("User", "OtherPassWord", [name, length]);
  },
};
