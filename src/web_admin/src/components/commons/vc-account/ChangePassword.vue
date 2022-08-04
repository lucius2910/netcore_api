<template>
  <v-form ref="changePassForm" lazy-validation>
    <vc-modal ref="modal">
      <template #title>{{ tl("Common", "EditPass") }}</template>
      <template #content>
        <vc-input-group required :label="tl('User', 'OldPassWord')">
          <vc-input
            type="password"
            v-model="changePass.oldPass"
            :rules="[ (v: any) => validate.required(v, tl('User', 'OldPassWord'))]"
          />
        </vc-input-group>
        <vc-input-group required :label="tl('User', 'NewPassWord')">
          <vc-input
            type="password"
            v-model="changePass.newPass"
            :rules="[ (v: any) => validate.required(v, tl('User', 'NewPassWord'))]"
          />
        </vc-input-group>
        <vc-input-group required :label="tl('User', 'ConfirmNewPassWord')">
          <vc-input
            type="password"
            v-model="changePass.confirmPass"
            :rules="[ (v: any) => validate.required(v, tl('User', 'ConfirmNewPassWord')), (v: any) => validate.checkPassword(v, changePass.newPass, tl('User', 'ConfirmNewPassWord'))]"
          />
        </vc-input-group>
      </template>
      <template #action>
        <vc-button class="ml-4 bg-secondary" @click="goBack">
          <v-icon light>mdi-arrow-left</v-icon>
          {{ tl("Common", "BtnBack") }}
        </vc-button>
        <vc-button class="ml-2 bg-primary" @click="onSave" :loading="loading">
          <v-icon light>mdi-content-save-outline</v-icon>
          {{ tl("Common", "BtnSave") }}
        </vc-button>
      </template>
    </vc-modal>
  </v-form>
</template>

<script setup lang="ts">
import { ref } from "vue";
import authService from "@/services/auth.service";
import tl from "@/utils/locallize";
import validate from "@/utils/validate";
import { useRouter } from "vue-router";
import { useAuthStore } from "@/stores/auth.store";
const authStore = useAuthStore();
const router = useRouter();
const changePassForm = ref<any>(null);
const modal = ref<any>(null);
const changePass = ref<any>({ oldPass: "", newPass: "", confirmPass: "" });
const loading = ref<boolean>(false);
const onSave = async () => {
  const user = {
    current_password: changePass.value.oldPass,
    new_password: changePass.value.newPass,
  };
  const { valid } = await changePassForm.value.validate();
  if (!valid) return;
  await authService.changePassword(user).finally(() => {
    modal.value.hide();
    authStore.logout();
    router.push({
      name: "Login",
    });
  });
};
const goBack = async () => {
  modal.value.hide();
};

const show = () => {
  modal.value.show();
};

defineExpose({
  show,
});
</script>
