export default  [
	{
    path: "/master",
    name: "MasterCodeList",
    component: () => import("@/views/master-code/ListView.vue"),
  },
  {
    path: "/master/create",
    name: "MasterCodeAddNew",
    component: () => import("@/views/master-code/DetailView.vue"),
  },
  {
    path: "/master/:id/edit",
    name: "MasterCodeEditByID",
    component: () => import("@/views/master-code/DetailView.vue"),
  },
  {
    path: "/resource/",
    name: "ResourceList",
    component: () => import("@/views/resource/ListView.vue"),
  },
]