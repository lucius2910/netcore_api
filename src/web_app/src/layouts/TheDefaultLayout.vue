<template>
  <el-container class="v-application">
    <!-- <TheSideBar :isCollapse="isCollapse" v-if="loggedIn" /> -->
    <div v-bind:class="loggedIn ? 'page-container is-vertical' : 'no-containner'">
      <TheNavBar @toogleSidebar="onCollapse" />
      <el-main>
        <!-- <RouterView /> -->
        <slot></slot>
      </el-main>
    </div>
    <vc-confirm ref="logoutPopup"></vc-confirm>
    <!-- <vc-toast /> -->
  </el-container>
</template>

<script setup lang="ts">
import TheNavBar from '@/components/layouts/TheNavBar.vue'

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
