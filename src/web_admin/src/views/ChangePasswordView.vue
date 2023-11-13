<template>
  <div class="page-login flex-center">
    <vc-card class="card-login pa-10">
      <vc-card-content>
        <vc-form ref="loginFormRef" label-position="top" :model="request" :rules="rules">
          <p class="logo-container">
            <image class="logo" src="/images/logo_honki.png" />
            <span class="item-logo text-logo">企業の未来を考える会社です。</span>
          </p>
          <p class="logo">パスワードを再設定しますか？</p>
          <p class="content-change-pwd">
            ちまたの会計
            アカウントの新規登録時に使用したユーザー名またはメールアドレスを入力して下さい。パスワードをリセットできるメールが送信されます。
          </p>

          <vc-input-group required prop="user_name">
            <vc-input-icon
              v-model="request.user_name"
              :icon="Avatar"
              placeholder="ユーザー名 or メールアドレス"
            />
          </vc-input-group>

          <p class="container-btn-login">
            <vc-button
              type="primary"
              class="mt-5"
              @click="onLogin(loginFormRef)"
              :loading="isLoading"
            >
              {{ "確認メールを送信" }}
            </vc-button>
          </p>
        </vc-form>
      </vc-card-content>
    </vc-card>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref } from "vue";
// import { useAuthStore } from "@/stores/auth.store";
// import { useRouter } from "vue-router";
import validate from "@/utils/validate_elp";
import type { FormInstance } from "element-plus";
import { Avatar } from "@element-plus/icons-vue";
import imageLogo from "../assets/images/Login/logo_honki.png";

const isLoading = ref<boolean>(false);
// const authStore = useAuthStore();
// const router = useRouter();
const loginFormRef = ref<FormInstance>();

const request = reactive({
  user_name: null,
  password: null,
  remember: false,
});

const rules = reactive({
  user_name: [{ required: true, validator: validate.required, trigger: "blur" }],
  password: [{ required: true, validator: validate.required, trigger: "blur" }],
});

const onLogin = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;

  await formEl.validate(async (valid) => {
    if (!valid) return;

    // isLoading.value = true;

    // await authStore
    //   .login(request)
    //   .then((isLogged) => {
    //     if (isLogged)
    //       router.push({
    //         name: "Dashboard",
    //       });
    //   })
    //   .finally(() => {
    //     isLoading.value = false;
    //   });
  });
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/page/changePassword.scss";
</style>
