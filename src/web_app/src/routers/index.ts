import { createRouter, createWebHistory } from 'vue-router'
import DashboardView from '@/views/DashboardView.vue'
import NotFoundView from '@/views/NotFoundView.vue'

import user from '@master/routers/user.router'
import functions from '@master/routers/functions.router'
import role from '@master/routers/role.router'
import master from '@master/routers/master.router'

import auth from '@auth/routers/auth.router'
import account from '@auth/routers/account.router'

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
      meta: {
        layout: "Client"
      }
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
