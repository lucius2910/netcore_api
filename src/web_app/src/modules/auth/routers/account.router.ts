export default [
  {
    path: '/change-password',
    name: 'ChangePassWord',
    component: () => import('@auth/views/account/ChangePassword.vue'),
    meta: {
      layout: "Admin"
    }
  },
  {
    path: '/account-profile',
    name: 'AccountProfile',
    component: () => import('@auth/views/account/AccountProfile.vue'),
    meta: {
      layout: "Admin"
    }
  }
]
