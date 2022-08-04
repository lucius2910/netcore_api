<template>
  <el-container class="v-application">
    <TheSideBar :isCollapse="isCollapse" v-show="loggedIn" />
    <el-container class="is-vertical">
      <TheNavBar @toogleSidebar="onCollapse" v-show="loggedIn" />
      <el-main>
        <RouterView />
      </el-main>
    </el-container>
    <vc-toast />
  </el-container>
</template>

<script setup lang="ts">
import { useRouter } from "vue-router";
import TheNavBar from "./TheNavBar.vue";
import TheSideBar from "./TheSideBar.vue";

import { storeToRefs } from "pinia";
import { useAuthStore } from "@/stores/auth.store";
import { onMounted, ref } from "vue";

const router = useRouter();
const authStore = useAuthStore();
const { loggedIn } = storeToRefs(authStore);

const isCollapse = ref<boolean>(false);

onMounted(() => {
  if (!loggedIn.value) {
    authStore.refresh().then((isLoggedIn) => {
      if (!isLoggedIn) {
        router.push({
          name: "Login",
        });
      }
    });
  }
});

const onCollapse = (value: any) => {
  isCollapse.value = value;
};
</script>

<style lang="scss">
@import "@/assets/styles/main.scss";
</style>
