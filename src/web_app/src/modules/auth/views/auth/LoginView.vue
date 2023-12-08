<template>
  <div class="page-login flex-center">
    <vc-card class="card-login pa-10">
      <vc-card-content>
        <el-form ref="loginFormRef" label-position="top" :model="request" :rules="rules">
          <p class="logo-container">
            <vc-image :src="'/images/logo_honki.png'" />
            <span class="text-logo">企業の未来を考える会社です。</span>
          </p>

          <p class="logo">ログイン</p>

          <vc-input-group required prop="user_name">
            <vc-input-icon v-model="request.user_name" :icon="Avatar" placeholder="ユーザー名 or メールアドレス" autocomplete="off" />
          </vc-input-group>

          <vc-input-group class="content-pwd" required prop="password">
            <vc-input-icon v-model="request.password" :icon="Lock" placeholder="パスワード" type="password" show-password />
          </vc-input-group>

          <vc-input-group class="d-flex space-between" style="padding-bottom: 20px">
            <vc-checkbox label="ログインしたままにする"></vc-checkbox>
          </vc-input-group>
          <p class="container-btn-login">
            <vc-button type="primary" class="mt-5" @click="onLogin(loginFormRef)" :loading="isLoading">ログインする
            </vc-button>
          </p>
          <p class="forgot-pass">
            <router-link to="/forgot_password">パスワードを忘れた場合はこちら</router-link>
          </p>
        </el-form>
      </vc-card-content>
    </vc-card>
  </div>
</template>

<script setup lang="ts">
import { reactive, onMounted, ref } from "vue";
import { useAuthStore } from "@auth/stores/auth.store";
import { useRouter } from "vue-router";
import validate from "@/utils/validate_elp";
import type { FormInstance } from "element-plus";
import { Avatar, Lock } from "@element-plus/icons-vue";
import { storeToRefs } from 'pinia'

const isLoading = ref<boolean>(false);
const authStore = useAuthStore();
const { loggedIn } = storeToRefs(authStore)
const router = useRouter();
const loginFormRef = ref<FormInstance>();

const request = reactive({
  user_name: null,
  password: null,
  remember: false,
});

const rules = reactive({
  user_name: [{ required: true, validator: validate.required, trigger: "blur", full_field: "ユーザー名 or メールアドレス" }],
  password: [
    { required: true, validator: validate.required, trigger: "blur", full_field: "パスワード" },
    { validator: validate.alphabetAndSymbolRule, trigger: "blur" }],
});

onMounted(() => {
  if (loggedIn.value) {
    authStore.refresh().then((isLoggedIn) => {
      if (isLoggedIn) {
        router.push({
          name: "Dashboard",
        });
      }
    });
  }
})

const onLogin = async (formEl: FormInstance | undefined) => {
  if (!formEl) return

  await formEl.validate(async (valid) => {
    if (!valid) return

    isLoading.value = true

    await authStore
      .login(request)
      .then((isLogged) => {
        if (isLogged)
          router.push({
            name: 'Dashboard',
          })
      })
      .finally(() => {
        isLoading.value = false
      })
  })
}

</script>

<style lang="scss" scoped>
@import "@/assets/styles/page/login";
</style>
