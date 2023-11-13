<template>
  <div class="vc-page page-home">
    <vc-tabs type="card">
      <vc-tab-panel label="サマリー">サマリー</vc-tab-panel>
      <vc-tab-panel label="現金出納帳">現金出納帳</vc-tab-panel>
      <vc-tab-panel label="預金出納帳">預金出納帳</vc-tab-panel>
      <vc-tab-panel label="収支計算書">収支計算書</vc-tab-panel>
    </vc-tabs>

    <vc-card class="mb-4 pa-4">
      <h2>Button loading</h2>
      <vc-button :loading="loading" @click="onLogout"> Logout </vc-button>
      <vc-button :loading="loading" @click="getApi">Get api</vc-button>
    </vc-card>

    <vc-card class="mb-4 pa-4">
      <h2>Toast</h2>
      <vc-button type="success" @click="pushToast('success')">Success</vc-button>
      <vc-button type="danger" @click="pushToast('error')">Error</vc-button>
      <vc-button type="warning" @click="pushToast('warning')">Warning</vc-button>
      <vc-button type="info" @click="pushToast('info')">Info</vc-button>
    </vc-card>

    <vc-card class="mb-4 pa-4">
      <h2>Confirm & Alert</h2>
      <vc-button @click="confirms">Confirm</vc-button>
      <vc-button @click="onAlert">Modal</vc-button>
    </vc-card>

    <vc-card class="mb-4 pa-4 el-card--visible">
      <h2>Select</h2>
      <!-- <vc-select-custom :items="tableData" :colConfig="colConfig" :tableConfig="tableConfig"></vc-select-custom>
      <vc-select-custom :items="tableData" :colConfig="colConfig" :tableConfig="tableConfig"></vc-select-custom> -->
    </vc-card>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import userService from "@/services/user.service";
import { useAuthStore } from "@/stores/auth.store";
import { useToastStore } from "@/stores/toast.store";
import { colConfig } from "../commons/tables/role.table";

const loading = ref<boolean>(false);
const confirmDialog = ref<any>(null);
const user = ref<any>(null);
const authStore = useAuthStore();
const router = useRouter();

const tableConfig = {
  checkbox: false,
  action: true,
  showPaging: false,
  dbClick: true,
};
const tableData = [
  {
    id: 5,
    code: '2016-05-03',
    name: 'tom',
    description: 'No. 189, Grove St, Los Angeles',
  },
  {
    id: 7,
    code: '2016-05-03',
    name: 'aa',
    description: 'No. 189, Grove St, Los Angeles',
  },
  {
    id: 0,
    code: '2016-05-03',
    name: 'bb',
    description: 'No. 189, Grove St, Los Angeles',
  },
  {
    id: 1,
    code: '2016-05-03',
    name: 'cc',
    description: 'No. 189, Grove St, Los Angeles',
  },
  {
    id: 9,
    code: '2016-05-03',
    name: 'Cat',
    description: 'No. 189, Grove St, Los Angeles',
  },
  {
    id: 14,
    code: '2016-05-03',
    name: '',
    description: 'No. 189, Grove St, Los Angeles',
  },
]

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
