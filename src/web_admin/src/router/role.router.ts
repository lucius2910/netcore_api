export default  [
	{
    path: "/role",
    name: "RoleList",
    component: () => import("@/views/role/ListView.vue"),
  },
  {
    path: "/role/new",
    name: "RoleAddNew",
    component: () => import("@/views/role/DetailView.vue"),
  },
  {
    path: "/role/:id/edit",
    name: "RoleEditByID",
    component: () => import("@/views/role/DetailView.vue"),
  },
]