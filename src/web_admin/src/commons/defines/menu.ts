export default [
  { routerName: "Dashboard", text: "Dashboard", icon: "mdi-view-dashboard" },
  {
    text: "Users",
    icon: "mdi-account-multiple",
    items: [{ routerName: "UserList", text: "List" }],
  },
  {
    routerName: "Settings",
    text: "System settings",
    icon: "mdi-cogs",
    items: [
      { routerName: "RoleList", text: "Role" },
      { routerName: "FunctionList", text: "Function" },
      { routerName: "MasterCodeList", text: "Master Code" },
    ],
  },
  {
    text: "Manufactures",
    icon: "mdi-warehouse",
    routerName: "InventoryManufacture",
  },
];
