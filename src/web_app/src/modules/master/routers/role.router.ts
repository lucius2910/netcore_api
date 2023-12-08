export default  [
	{
    path: "/role",
    name: "RoleList",
    component: () => import("@master/views/role/ListView.vue"),
  },
  {
    path: "/role/new",
    name: "RoleAddNew",
    component: () => import("@master/views/role/DetailView.vue"),
  },
  {
    path: "/role/:id/edit",
    name: "RoleEditByID",
    component: () => import("@master/views/role/DetailView.vue"),
  },
]