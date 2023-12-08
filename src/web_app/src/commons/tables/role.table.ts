import tl from "@/utils/locallize";

export const tableConfig = {
  checkbox: true,
  action: true,
  showPaging: true,
  dbClick: false,
};

export const colConfig = [
  {
    key: "code",
    title: tl("Role", "Code"),
    is_sort: true,
  },
  {
    key: "name",
    title: tl("Role", "Name"),
    is_sort: true,
  },
  {
    key: "description",
    title: tl("Role", "Description"),
  },
];

export default { tableConfig, colConfig };
