export default  [
  {
    path: "/function",
    name: "FunctionList",
    component: () => import("@master/views/function/ListView.vue"),
  },
  {
    path: "/function/new",
    name: "FunctionAddNew",
    component: () => import("@master/views/function/DetailView.vue"),
  },
  {
    path: "/function/:id/edit",
    name: "FunctionEditById",
    component: () => import("@master/views/function/DetailView.vue"),
  },
]