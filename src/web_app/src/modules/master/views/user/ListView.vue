<template>
  <div class="vc-page page-user">
    <vc-row>
      <vc-col :md="12" class="align-centter">
        <el-breadcrumb cla :separator-icon="ArrowRight">
          <el-breadcrumb-item :to="{ path: '/' }">Master</el-breadcrumb-item>
          <el-breadcrumb-item :to="{ path: '/user' }">User</el-breadcrumb-item>
        </el-breadcrumb>
      </vc-col>
      <vc-col :md="12" class="d-flex flex-end">
        <vc-button class="ml-2" @click="onAddNew" code="F00004" :icon="Download">
          {{ tl("Common", "BtnExport") }}
        </vc-button>
        <vc-button class="ml-2" @click="onAddNew" code="F00004" :icon="Upload">
          {{ tl("Common", "BtnImport") }}
        </vc-button>
        <vc-button class="ml-2" @click="onAddNew" type="primary" code="F00004" :icon="Plus">
          {{ tl("Common", "BtnAddNew") }}
        </vc-button>
      </vc-col>
    </vc-row>

    <vc-row class="box-search mt-4">
      <vc-col :span="10" class="d-flex">
        <vc-input v-model="search" hide-details="true" :prefix-icon="Search"></vc-input>
        <vc-button class="ml-2" @click="getList" type="primary" :icon="Search">
          {{ tl("Common", "BtnSearch") }}
        </vc-button>
      </vc-col>
    </vc-row>

    <vc-row>
      <vc-col :span="24">
        <vc-table :datas="gridData" :tableConfig="tableConfig" :colConfigs="colConfig" :page="pageConfig" :loading="loading"
          @dbClick="onEdit" @sorted="onSort" @rowSelected="onRowSelected" @pageChanged="onPageChanged">
        </vc-table>
      </vc-col>
    </vc-row>
    <vc-confirm ref="confirmDialog"></vc-confirm>
    <detail-modal ref="detailRef"></detail-modal>
  </div>
</template>

<script setup lang="ts">
import { onBeforeMount, ref } from "vue";
import { useRouter } from "vue-router";
import userService from "@master/services/user.service";
import userStore from "@master/store/user.store";
import { colConfig, tableConfig } from "@/commons/tables/user.table";
import tl from "@/utils/locallize";
import { Search, Plus, ArrowRight, Upload, Download } from '@element-plus/icons-vue';
import DetailModal from './DetailModal.vue'
import { storeToRefs } from "pinia";

// const users = ref<any[]>([]);
const { gridData } = storeToRefs(userStore);

const pageConfig = ref<any>({});
const router = useRouter();
const search = ref<any>("");
const goSort = ref<any>("");
const selectedItems = ref<any[]>([]);
const loading = ref<boolean>(false);
const confirmDialog = ref<any>(null);
const detailRef = ref<any>(null);

onBeforeMount(async () => {
  getList();
});

const getList = async () => {
  loading.value = true;
  await userService
    .getList({
      sort: goSort.value,
      is_actived: true,
      search: search.value,
      ...pageConfig.value,
    })
    .then((data) => {
      gridData.value = data.data ?? [];
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
  pageConfig.value = { ...page };
  getList();
};

const onAddNew = () => {
  //router.push({ name: "UserAddNew" });
  detailRef.value.open()

};

const onEdit = (item: any) => {
  router.push({ name: "UserEditByID", params: { id: item.id } });
};

// const onDeleteItem = (item: any) => {
//   confirmDialog.value.confirm(
//     tl("Common", "Delete"),
//     tl("Common", "ConfirmDelete", [item.code]),
//     async (res: any) => {
//       if (res) await onDeleteMulti([item]);
//     }
//   );
// };

// const onDeleteMulti = async (data: any) => {
//   await userService.delete(data[0]).then(() => {
//     getList();
//   });
// };
</script>

<style lang="scss">
.box-search {
  background-color: #fff;
  padding: 12px
}
</style>