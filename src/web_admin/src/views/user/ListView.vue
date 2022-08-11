<template>
  <div class="vc-page page-user">
    <vc-row>
      <vc-col :span="10" class="d-flex">
        <vc-input v-model="search" hide-details="true"></vc-input>
        <vc-button class="ml-2" @click="getList" type="primary" :icon="Search">
          {{ tl("Common", "BtnSearch") }}
        </vc-button>
      </vc-col>
      <vc-col :span="14" class="flex-end">
        <vc-button class="ml-2" @click="onAddNew" type="primary" code="F00004" :icon="Plus">
          {{ tl("Common", "BtnAddNew") }}
        </vc-button>
      </vc-col>
    </vc-row>

    <vc-row class="mt-4">
      <vc-col :span="24">
        <vc-table
          :datas="users"
          :tableConfig="tableConfig"
          :colConfigs="colConfig"
          :page="pageConfig"
          :loading="loading"
          @dbClick="onEdit"
          @sorted="onSort"
          @rowSelected="onRowSelected"
          @pageChanged="onPageChanged"
        >
          <template #action>
            <vc-button
              size="small"
              type="danger"
              :icon="Delete"
              @click="onDeleteItem()"
            ></vc-button>
          </template>
        </vc-table>
      </vc-col>
    </vc-row>
    <vc-confirm ref="confirmDialog"></vc-confirm>
  </div>
</template>

<script setup lang="ts">
import { onBeforeMount, ref } from "vue";
import { useRouter } from "vue-router";
import userService from "@/services/user.service";
import { colConfig, tableConfig } from "@/commons/tables/user.table";
import { useToastStore } from "@/stores/toast.store";
import tl from "@/utils/locallize";
import { Search, Delete, Plus } from '@element-plus/icons-vue';

const users = ref<any[]>([]);
const pageConfig = ref<any>({});
const router = useRouter();
const search = ref<any>("");
const goSort = ref<any>("");
const selectedItems = ref<any[]>([]);
const loading = ref<boolean>(false);
const confirmDialog = ref<any>(null);
const toastStore = useToastStore();

onBeforeMount(async () => {
  getList();
});

const getList = async () => {
  loading.value = true;
  await userService
    .getList({
      sort: goSort.value,
      ...search.value,
      ...pageConfig.value,
    })
    .then((data) => {
      users.value = data.data ?? [];
      pageConfig.value.total = data.total
    })
    .finally(() => {
      loading.value = false;
    });
};

const onRowSelected = (selected: any) => {
  selectedItems.value = selected;
};

const onSort = async (sortBy: any) => {
  goSort.value = sortBy;
  getList();
};

const onPageChanged = async (page: any) => {
  pageConfig.value = {...page};
  getList();
};

const onAddNew = () => {
  router.push({ name: "UserAddNew" });
};

const onEdit = (item: any) => {
  router.push({ name: "UserEditByID", params: { id: item.id } });
};

const onDeleteItem = (item: any) => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [item.code]),
    async (res: any) => {
      if (res) await onDeleteMulti([item]);
    }
  );
};

const onDeleteMulti = async (data: any) => {
  await userService.deleteMulti(data.map((x: any) => x.id)).then(() => {
    getList();
  });
};
</script>
