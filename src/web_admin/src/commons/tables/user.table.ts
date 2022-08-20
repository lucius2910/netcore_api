import tl from "@/utils/locallize";

export const tableConfig = {
  checkbox: true,
  action: false,
  showPaging: true,
  dbClick: false,
  index: true,
};

export const colConfig = [
  {
    key: "code",
    title: tl("User", "Code"),
    is_sort: true,
  },
  {
    key: "full_name",
    title: tl("User", "FullName"),
    is_sort: true,
  },
  {
    key: "email",
    title: tl("User", "Email"),
  },
  {
    key: "phone",
    title: tl("User", "Phone"),
  },
  {
    key_field: "gender",
    text_field: "gender_name",
    title: tl("User", "Gender"),
  },
  {
    key_field: "role",
    text_field: "role_name",
    title: tl("User", "Role"),
  },
  {
    key: "is_active",
    title: tl("User", "Active"),
  },
];

export default { tableConfig, colConfig };
