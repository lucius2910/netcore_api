<template>
  <div class="vc-page page-home">
    <vc-card class="mb-4 pa-4">
      <h2>Button loading</h2>
      <vc-button :loading="loading" @click="onLogout"> Logout </vc-button>
      <vc-button :loading="loading" @click="getApi">Get api</vc-button>
    </vc-card>

    <vc-card class="mb-4 pa-4">
      <h2>Toast</h2>
      <vc-button type="success" @click="pushToast('success')"
        >Success</vc-button
      >
      <vc-button type="danger" @click="pushToast('error')">Error</vc-button>
      <vc-button type="warning" @click="pushToast('warning')"
        >Warning</vc-button
      >
      <vc-button type="info" @click="pushToast('info')">Info</vc-button>
    </vc-card>

    <vc-card class="mb-4 pa-4">
      <h2>Confirm & Alert</h2>
      <vc-button @click="confirms">Confirm</vc-button>
      <vc-button @click="onAlert">Modal</vc-button>
    </vc-card>

    <vc-confirm ref="confirmDialog"></vc-confirm>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import userService from "@/services/user.service";
import { useAuthStore } from "@/stores/auth.store";
import { useToastStore } from "@/stores/toast.store";

const loading = ref<boolean>(false);
const confirmDialog = ref<any>(null);
const authStore = useAuthStore();
const router = useRouter();

const onLogout = () => {
  authStore.logout();
  router.push({
    name: "Login",
  });
};

const getApi = async () => {
  loading.value = true;
  await userService
    .getList()
    .then((data) => {
      console.log(data);
    })
    .finally(() => {
      loading.value = false;
    });
};

const toastStore = useToastStore();

const pushToast = (type: string) => {
  toastStore.push({
    type: type,
    message: "123123123123123",
  });
};

const confirms = () => {
  confirmDialog.value.confirm("Logout", "Are your sure ?", (res: any) => {
    console.log(res);
  });
};

const onAlert = () => {
  confirmDialog.value.alert("Logout", "Are your sure ?", (res: any) => {
    console.log(res);
  });
};
</script>
