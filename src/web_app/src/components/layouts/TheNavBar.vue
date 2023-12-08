<template>
  <div class="navbar">
    <el-header class="navbar-header">
      <el-row :gutter="20">
        <el-col :span="21" class="col-left">
          <el-header style="width: 960px;">
            <vc-menu v-if="isMenuVisible" mode="horizontal" style="width: initial;"></vc-menu>
          </el-header>
        </el-col>

        <el-col :span="3" class="col-right">
          <el-link type="primary" :underline="false" @click="goPage('AccountProfile')">
            {{ account?.profile?.name }}
            <el-icon class="el-icon--right">
              <CaretBottom />
            </el-icon>
          </el-link>
        </el-col>
      </el-row>
    </el-header>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { storeToRefs } from 'pinia'
import { useAuthStore } from '@auth/stores/auth.store'
import { ref } from 'vue';

const authStore = useAuthStore()
const { account } = storeToRefs(authStore)
const router = useRouter()

const isMenuVisible = ref(false);

const goPage = async (name: string) => {
  router.push({
    name: name,
  })
}
</script>

<style lang="scss" scoped>
@import '@/assets/styles/commons/navbar.scss';
</style>
