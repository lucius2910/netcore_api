import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import resourceService from '@/services/resource.service'
import VcRegister from '@/components/commons/vc-register'

import { registerLayouts } from '@/layouts/register'

// ===================== ELEMENT PLUS =========================
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import '@/assets/styles/main.scss'

const app = createApp(App)

// ===================== BASE COMPONENT =========================
import * as ElementPlusIconsVue from '@element-plus/icons-vue'

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component)
}
VcRegister.register(app)

// ===================== ENABLE STORE PINIA =====================
const pinia = createPinia()
app.use(pinia)

// eslint-disable-next-line no-constant-condition
if (false) {
  resourceService
    .getList({ page: 1, size: 10000, sort: 'screen.asc' })
    .then((reponse: any) => {
      localStorage.setItem(
        'i18n.ja',
        reponse.data ? JSON.stringify(reponse.data) : '[]'
      )
    })
}

app.use(router)
app.use(ElementPlus)
registerLayouts(app)

app.mount('#app')
