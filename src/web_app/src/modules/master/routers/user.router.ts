export default [
  {
    path: '/user',
    name: 'UserList',
    component: () => import('@master/views/user/ListView.vue'),
  },
  {
    path: '/user/new',
    name: 'UserAddNew',
    component: () => import('@master/views/user/DetailView.vue'),
  },
  {
    path: '/user/:id/edit',
    name: 'UserEditByID',
    component: () => import('@master/views/user/DetailView.vue'),
  },
]
