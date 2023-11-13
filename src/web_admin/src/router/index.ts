import { createRouter, createWebHistory } from 'vue-router'
import DashboardView from '@/views/DashboardView.vue'
import NotFoundView from '@/views/NotFoundView.vue'

import user from '@/router/user.router'
import functions from '@/router/functions.router'
import role from '@/router/role.router'
import master from '@/router/master.router'
import auth from '@/router/auth.router'
import account from '@/router/account.router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    // ======================= STATIC PAGE =========================
    {
      path: '/404',
      name: 'NotFound',
      component: NotFoundView,
    },

    // ======================= NOT FOUND =========================
    {
      path: '/',
      name: 'Dashboard',
      component: DashboardView,
      meta: {
        layout: "Admin"
      }
    },

    // ======================= SETTINGS =========================
    {
      path: '/settings',
      name: 'Settings',
      component: () => import('@/views/SettingView.vue'),
    },

    // ======================= LOGIN =========================
    {
      path: '/login',
      name: 'Login',
      component: () => import('@/views/LoginView.vue'),
    },

    // ======================= FORGOT PASSWORD =========================
    {
      path: '/forgot_password',
      name: 'forgot_password',
      component: () => import('@/views/ChangePasswordView.vue'),
    },
    // ======================= Chart =========================
    {
      path: '/chart',
      name: 'chart',
      component: () => import('@/views/ChartView.vue'),
    },

    ...user,
    ...role,
    ...functions,
    ...master,
    ...auth,
    ...account
  ],
})

export default router
