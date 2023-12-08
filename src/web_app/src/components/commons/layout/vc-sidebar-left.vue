<template>
  <vc-row :gutter="12" class="sidebar-left">
    <vc-col :span="24" :md="{ span: 6 }" class="sidebar-left-menu">
      <template v-for="(item, index) in menuSideBars" :key="index">
        <div v-if="item.permission.indexOf(userInfo?.profile?.user_type) != -1">
          <div class="sidebar-left-menu-head">
            <h2 class=" heading-title">
              <vc-icon class="heading-title-icon" :type="item.icon"></vc-icon>
              {{ item.title }}
            </h2>
          </div>
          <div class="sidebar-left-menu-link d-flex pl-4">
            <template v-for="(childItem, childIndex) in item.items" :key="childIndex">
              <router-link :to="{ name: childItem.pathName }" class="btn-link mt-2 hover-link"
                v-if="item.permission.indexOf(userInfo?.profile?.user_type) != -1">
                {{ childItem.name }}
              </router-link>
            </template>
          </div>
          <div class="border-footer mt-4"></div>
        </div>
      </template>
      <vc-button class="btn-logout ma-4" @click="onLogout()" link>ログアウト</vc-button>
    </vc-col>
    <vc-col :span="24" :md="{ span: 18 }" class="sidebar-left-content">
      <slot></slot>
    </vc-col>
  </vc-row>
  <vc-confirm ref="logoutPopup"></vc-confirm>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import menuSideBars from '@/commons/defines/menuSideBars'
import oidcAuth from "@/utils/oidcAuth";
import { MESSAGE } from '@/commons/const';

const logoutPopup = ref<any>(null)
const userInfo = ref<any>({});

onMounted(async () => {
  userInfo.value = await oidcAuth.getUser();
})

const onLogout = async () => {
  logoutPopup.value.confirm(
    MESSAGE.LOGOUT_CONFIRM_TITLE,
    MESSAGE.LOGOUT_CONFIRM_MESSAGE,
    async (res: any) => {
      if (res) {
        localStorage.clear()
        const logoutUrl = `${import.meta.env.VITE_AUTH_SSO_URL}/Account/LogoutSSO?ReturnUrl=${import.meta.env.VITE_APP_URL}`
        window.location.replace(logoutUrl)
      }
    }
  )
}
</script>

<style lang="scss" scoped>
@import '@/assets/styles/commons/vc-sidebar-left.scss';
</style>
