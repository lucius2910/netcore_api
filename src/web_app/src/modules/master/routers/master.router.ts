export default  [
	{
    path: "/master",
    name: "MasterCodeList",
    component: () => import("@master/views/master-code/ListView.vue"),
  },
  {
    path: "/master/create",
    name: "MasterCodeAddNew",
    component: () => import("@master/views/master-code/DetailView.vue"),
  },
  {
    path: "/master/:id/edit",
    name: "MasterCodeEditByID",
    component: () => import("@master/views/master-code/DetailView.vue"),
  },
  {
    path: "/resource/",
    name: "ResourceList",
    component: () => import("@master/views/resource/ListView.vue"),
  },
]