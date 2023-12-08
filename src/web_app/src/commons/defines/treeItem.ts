export default [
  {
    id: 1,
    name: "User",
    locked: true,
    children: [
      { id: 2, name: "View List" },
      { id: 3, name: "Create new" },
      { id: 4, name: "Update" },
      { id: 4, name: "Delete" },
    ],
  },
  {
    id: 5,
    name: "System settings",
    children: [
      {
        id: 6,
        name: "Role",
        children: [
          { id: 2, name: "View List" },
          { id: 3, name: "Create new" },
          { id: 4, name: "Update" },
          { id: 4, name: "Delete" },
        ],
      },
      {
        id: 10,
        name: "Function",
        children: [
          { id: 2, name: "View List" },
          { id: 3, name: "Create new" },
          { id: 4, name: "Update" },
          { id: 4, name: "Delete" },
        ],
      },
    ],
  },
  {
    id: 15,
    name: "Manufacture",
    children: [
      { id: 2, name: "View List" },
      { id: 3, name: "Create new" },
      { id: 4, name: "Update" },
      { id: 4, name: "Delete" },
    ],
  },
];
