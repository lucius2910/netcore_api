import tl from "@/utils/locallize";

export const tableConfig = {
  showPaging: true,
  showIndex: true,
  dbClick: true,
};

export const colConfig = [
  {
    key: "key",
    title: tl("Resource", "Key"),
    is_sort: true,
  },
  {
    key: "text",
    title: tl("Resource", "Value"),
    is_sort: true,
  },
];

export default { tableConfig, colConfig };
