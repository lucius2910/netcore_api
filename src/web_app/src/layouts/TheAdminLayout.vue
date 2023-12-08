<template>
  <el-container class="v-application">
    <el-container>
      <TheSideBar :isCollapse="isCollapse" />
      <el-main>
        <TheNavBar @toogleSidebar="onCollapse" />
        <slot></slot>
      </el-main>
    </el-container>
    <vc-confirm ref="logoutPopup"></vc-confirm>
    <!-- <vc-toast /> -->
  </el-container>
</template>

<script setup lang="ts">
import TheNavBar from '@/components/layouts/TheNavBar.vue'
import TheSideBar from '@/components/layouts/TheSideBar.vue'

import { storeToRefs } from 'pinia'
import { useAuthStore } from '@auth/stores/auth.store'
import { onMounted, ref } from 'vue'
import oidcAuth from '@/utils/oidcAuth'

const authStore = useAuthStore()
const { loggedIn } = storeToRefs(authStore)

const isCollapse = ref<boolean>(false)
const logoutPopup = ref<any>(null)

const getCurrentURL = () => {
  return window.location.pathname
}

onMounted(async () => {
  const url = getCurrentURL()

  if (url.toString() === '/callback' || url.toString() === '/logout-callback')
    return;

  const userInfo = await oidcAuth.getUser();
  authStore.setUserInfo(userInfo);
  if (!loggedIn.value)
    gotoLogin()

})

const onCollapse = (value: any) => {
  isCollapse.value = value
}

const gotoLogin = () => {
  oidcAuth.login()
}

</script>

<style lang="scss">
@import '@/assets/styles/main.scss';
@import '@/assets/styles/page/client/_footerClient.scss';
</style>
