<template>
  <el-container class="v-application">
    <!-- <TheSideBar :isCollapse="isCollapse" v-if="loggedIn" /> -->
    <div class="page-container is-vertical">
      <TheNavBar @toogleSidebar="onCollapse" v-show="loggedIn" />
      <!-- <TheMenu /> -->
      <el-main>
        <RouterView />
      </el-main>
    </div>
    <vc-toast />
  </el-container>
</template>

<script setup lang="ts">
// import { useRouter } from "vue-router";
import TheNavBar from './TheNavBar.vue'

import { storeToRefs } from 'pinia'
import { useAuthStore } from '@auth/stores/auth.store'
import { onMounted, ref } from 'vue'

//const router = useRouter();
const authStore = useAuthStore()
const { loggedIn } = storeToRefs(authStore)

const isCollapse = ref<boolean>(false)

onMounted(() => {
  if (!loggedIn.value) {
    // authStore.refresh().then((isLoggedIn) => {
    //   if (!isLoggedIn) {
    //     router.push({
    //       name: "Login",
    //     });
    //   }
    // });
  }
})

const onCollapse = (value: any) => {
  isCollapse.value = value
}
</script>

<style lang="scss">
@import '@/assets/styles/main.scss';
</style>
