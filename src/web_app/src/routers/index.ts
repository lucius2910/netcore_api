import { createRouter, createWebHistory } from 'vue-router'
import DashboardView from '@/views/DashboardView.vue'
import NotFoundView from '@/views/NotFoundView.vue'

import master from '@master/routers/index.master.router'
import auth from '@auth/routers/index.auth.router'

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

    ...master,
    ...auth,
  ],
})

export default router
