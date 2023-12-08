import tl from "@/utils/locallize";

export const tableConfig = {
  checkbox: false,
  action: true,
  showPaging: true,
  dbClick: false,
};

export const colConfig = [
  {
    key: "module",
    title: tl("Function", "Module"),
    is_sort: true,
  },
  {
    key: "code",
    title: tl("Function", "Code"),
    is_sort: true,
  },
  {
    key: "name",
    title: tl("Function", "Name"),
    is_sort: true,
  },
  {
    key: "parent",
    title: tl("Function", "Parent"),
    is_sort: true,
  },
  {
    key: "url",
    title: tl("Function", "Url"),
    is_sort: true,
  },
  {
    key: "path",
    title: tl("Function", "Api"),
    is_sort: true,
  },
  {
    key: "method",
    title: tl("Function", "Method"),
  },
];

export default { tableConfig, colConfig };
