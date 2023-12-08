import tl from "@/utils/locallize";

export default {
  required: (value: any, name: string) => {
    return !!value || tl("Common", "ValidateFieldRequied", [name]);
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
  emailRule: (value: any, name: string) => {
    if (!value) return true;
    return (
      /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(value) ||
      tl("Common", "ValidateInvalid", [name])
    );
  },
  dateRule: (value: any, name: string) => {
    return (
      /^([12]\d{3})[-](((0[13456789]|1[0-2])[-](0[1-9]|[12][0-9]|3[01]))|02[-](0[1-9]|[12][0-9]))$/.test(
        value
      ) || tl("Common", "ValidateInvalid", [name])
    );
  },
  phoneNumberRule: (value: any, name: string) => {
    if (!value) return true;
    return (
      /^[+0-9][./0-9]{8,19}$/.test(value) ||
      tl("Common", "ValidateInvalid", [name])
    );
  },
  checkPassword: (value: any, newVal: any, name: string) => {
    return value === newVal || tl("User", "OtherPassWord", [name, length]);
  },
};
