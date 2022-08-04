import tl from "@/utils/locallize";

export const tableConfig = {
  checkbox: true,
  action: true,
  showPaging: true,
  dbClick: false,
};

export const colConfig = [
  {
    key: "type",
    title: tl("MasterCode", "Type"),
    is_sort: true,
  },
  {
    key: "key",
    title: tl("MasterCode", "Key"),
    is_sort: true,
  },
  {
    key: "value",
    title: tl("MasterCode", "Value"),
    is_sort: true,
  },
];

export default { tableConfig, colConfig };
