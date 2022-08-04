<template>
  <vc-card class="mb-4 mt-2 pa-4">
    <vc-card-content>
      <v-form ref="userForm" lazy-validation>
        <vc-row>
          <vc-col :lg="4" :md="6" :sm="12" :xs="12">
            <vc-input-group required :label="tl('User', 'FullName')">
              <vc-input
                v-model="user.full_name"
                :rules="[ (v: any) => validate.required(v, tl('User', 'FullName'))]"
              />
            </vc-input-group>

            <vc-input-group required :label="tl('User', 'Email')">
              <vc-input
                v-model="user.email"
                :rules="[ (v: any) => validate.required(v, tl('User', 'Email')), (v: any) => validate.emailRule(v, tl('User', 'Email'))]"
              />
            </vc-input-group>

            <vc-input-group required :label="tl('User', 'Phone')">
              <vc-input
                v-model="user.phone"
                :rules="[ (v: any) => validate.required(v, tl('User', 'Phone')), (v: any) => validate.phoneNumberRule(v, tl('User', 'Phone'))]"
              ></vc-input>
            </vc-input-group>

            <vc-input-group required :label="tl('User', 'Gender')">
              <vc-select
                v-model="user.gender"
                :items="genders"
                fieldValue="key"
                fieldText="value"
                :rules="[ (v: any) => validate.required(v, tl('User', 'Gender'))]"
              ></vc-select>
            </vc-input-group>
          </vc-col>
        </vc-row>
      </v-form>
    </vc-card-content>
    <vc-card-action class="d-flex pa-3">
      <v-spacer></v-spacer>
      <vc-button @click="onSave" :loading="isLoading" class="ml-2">
        <v-icon light>mdi-content-save-outline</v-icon>
        {{ tl("Common", "BtnSave") }}
      </vc-button>
    </vc-card-action>
    <vc-confirm ref="confirmDialog"></vc-confirm>
  </vc-card>
</template>

<script setup lang="ts">
import { onBeforeMount, ref } from "vue";
import validate from "@/utils/validate";
import userService from "@/services/user.service";
import tl from "@/utils/locallize";
import masterCodeService from "@/services/master.service";
import { useAuthStore } from "@/stores/auth.store";
import { storeToRefs } from "pinia";
const userForm = ref<any>(null);
const isLoading = ref(false);
const genders = ref<any>([]);
const user = ref<any>({});
const authStore = useAuthStore();
const confirmDialog = ref<any>(null);
const { account } = storeToRefs(authStore);
const _id = account.value.id as string;

onBeforeMount(async () => {
  await init();

  if (_id) await getUserDetail();
  user.value.roles = user.value.roles ?? [];
});

const getUserDetail = async () => {
  const response = await userService.detail(_id);
  user.value = response?.data;
};

const init = async () => {
  genders.value = (await masterCodeService.getByType("GENDER"))?.data;
};
const onSave = async () => {
  const { valid } = await userForm.value.validate();
  if (!valid) return;
  isLoading.value = true;
  await userService.update(user.value).finally(() => {
    isLoading.value = false;
  });
};
</script>
