export default  [
  {
    path: "/function",
    name: "FunctionList",
    component: () => import("@/views/function/ListView.vue"),
  },
  {
    path: "/function/new",
    name: "FunctionAddNew",
    component: () => import("@/views/function/DetailView.vue"),
  },
  {
    path: "/function/:id/edit",
    name: "FunctionEditById",
    component: () => import("@/views/function/DetailView.vue"),
  },
]