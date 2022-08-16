<template>
  <div class="page-login flex-center">
    <vc-card class="card-login pa-10">
      <vc-card-content>
        <el-form
          ref="loginFormRef"
          label-position="top"
          :model="request"
          :rules="rules"
        >
          <p class="logo">三宝化成工業</p>
          <vc-input-group
            required
            prop="user_name"
            :label="tl('User', 'UserName')"
          >
            <vc-input v-model="request.user_name" />
          </vc-input-group>

          <vc-input-group
            required
            prop="password"
            :label="tl('Login', 'Password')"
          >
            <vc-input v-model="request.password" type="password" />
          </vc-input-group>

          <vc-input-group class="d-flex space-between">
            <vc-checkbox :label="tl('Login', 'RememberLogin')"> </vc-checkbox>

            <vc-button
              type="primary"
              class="mt-5"
              @click="onLogin(loginFormRef)"
              :loading="isLoading"
              :icon="ArrowRight"
            >
              {{ tl("Login", "BtnLogin") }}
            </vc-button>
          </vc-input-group>
        </el-form>
      </vc-card-content>
    </vc-card>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref } from "vue";
import { useAuthStore } from "@/stores/auth.store";
import { useRouter } from "vue-router";
import validate from "@/utils/validate_elp";
import tl from "@/utils/locallize";
import type { FormInstance } from "element-plus";
import { ArrowRight } from '@element-plus/icons-vue';

const isLoading = ref<boolean>(false);
const authStore = useAuthStore();
const router = useRouter();
const loginFormRef = ref<FormInstance>();

const request = reactive({
  user_name: null,
  password: null,
  remember: false,
});

const rules = reactive({
  user_name: [
    { required: true, validator: validate.required, trigger: "blur" },
  ],
  password: [{ required: true, validator: validate.required, trigger: "blur" }],
});

const onLogin = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;

  await formEl.validate(async (valid) => {
    if (!valid) return;

    isLoading.value = true;

    await authStore
      .login(request)
      .then((isLogged) => {
        if (isLogged)
          router.push({
            name: "Dashboard",
          });
      })
      .finally(() => {
        isLoading.value = false;
      });
  });
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/page/login";
</style>
