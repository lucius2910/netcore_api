export default  [
	{
    path: "/user",
    name: "UserList",
    component: () => import("@/views/user/ListView.vue"),
  },
  {
    path: "/profile",
    name: "Profile",
    component: () => import("@/views/account/EditView.vue"),
  },
  {
    path: "/user/new",
    name: "UserAddNew",
    component: () => import("@/views/user/DetailView.vue"),
  },
  {
    path: "/user/:id/edit",
    name: "UserEditByID",
    component: () => import("@/views/user/DetailView.vue"),
  }
]