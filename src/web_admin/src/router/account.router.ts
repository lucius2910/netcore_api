export default [
  {
    path: '/change-password',
    name: 'ChangePassWord',
    component: () => import('@/views/account/ChangePassword.vue'),
    meta: {
      layout: "Admin"
    }
  },
  {
    path: '/account-profile',
    name: 'AccountProfile',
    component: () => import('@/views/account/AccountProfile.vue'),
    meta: {
      layout: "Admin"
    }
  },
  {
    path: '/change-email',
    name: 'ChangeEmail',
    component: () => import('@/views/account/ChangeEmail.vue'),
    meta: {
      layout: "Admin"
    }
  },
]
