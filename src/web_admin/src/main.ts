import { createApp } from "vue";
import { createPinia } from "pinia";

import App from "./App.vue";
import router from "./router";
import resourceService from '@/services/resource.service'
import VcRegister from "@/components/commons/vc-register";
import ElementPlus from "element-plus";
import "element-plus/dist/index.css";
import "@/assets/styles/main.scss";

const app = createApp(App);

/**
 * BEGIN register el icon
 */
import * as ElementPlusIconsVue from '@element-plus/icons-vue'

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component)
}
/**
 * END register el icon
 */



resourceService
  .getList({ page: 1, size: 10000, sort: 'screen.asc' })
  .then((reponse: any) => {
    localStorage.setItem(
      'i18n.ja',
      reponse.data ? JSON.stringify(reponse.data) : '[]'
    )
  })
  .finally(() => {
    const app = createApp(App)

    VcRegister.register(app)

    app.use(createPinia());
    app.use(router);
    app.use(ElementPlus);

    app.mount("#app");
  })