<template>
  <div class="vc-page page-role">
    <vc-row>
      <vc-col :lg="8" :md="12">
        <vc-input-group>
          <vc-input v-model="search" :placeholder="tl('Role', 'SearchPlaceholder')"></vc-input>
        </vc-input-group>
      </vc-col>
      <vc-col :lg="16" :md="12" class="d-flex flex-end">
        <vc-button type="danger" @click="onDeleteSelected" code="F00015" :icon="Delete">
          {{ tl("Common", "BtnDeleteMulti") }}
        </vc-button>
        <vc-button type="primary" class="ml-2" @click="onAddNew" code="F00012" :icon="Plus">
          {{ tl("Common", "BtnAddNew") }}
        </vc-button>
      </vc-col>
    </vc-row>

    <vc-row class="mt-4">
      <vc-col :col="24">
        <vc-table
          :datas="roles"
          :tableConfig="tableConfig"
          :colConfigs="colConfig"
          :page="pageConfig"
          :loading="loading"
          @dbClick="onEdit"
          @sorted="onSort"
          @rowSelected="onRowSelected"
          @pageChanged="onPageChanged"
        >
          <template #action="{ data }">
            <div class="d-flex flex-center">
              <vc-button
                type="primary"
                size="small" 
                class="btn-acttion"
                @click="onEdit(data)"
                :icon="Edit"
              >
              </vc-button>
              <vc-button
                type="danger"
                code="F00015"
                size="small" 
                class="btn-acttion"
                @click="onDeleteItem(data)"
                :icon="Delete"
              >
              </vc-button>
            </div>
            
          </template>
        </vc-table>
      </vc-col>
    </vc-row>
    <vc-confirm ref="confirmDialog"></vc-confirm>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import roleService from "@/services/role.service";
import { colConfig, tableConfig } from "@/commons/tables/role.table";
import tl from "@/utils/locallize";
import { useToastStore } from "@/stores/toast.store";
import { Delete, Edit, Search, Plus } from '@element-plus/icons-vue';

const roles = ref<any[]>([]);
const pageConfig = ref<any>({});
const router = useRouter();
const search = ref<any>("");
const goSort = ref<any>("");
const selectedItems = ref<any[]>([]);
const loading = ref<boolean>(false);
const confirmDialog = ref<any>(null);
const toastStore = useToastStore();

onMounted(async () => {
  getList();
});

const getList = async () => {
  loading.value = true;
  await roleService
    .getList({
      sort: goSort.value,
      ...search.value,
      ...pageConfig.value,
    })
    .then((data) => {
      roles.value = data.data ?? [];
      pageConfig.value.total = data.total
    })
    .finally(() => {
      loading.value = false;
    });
};

const onRowSelected = (selected: any) => {
  selectedItems.value = selected;
};

const onDeleteSelected = async () => {
  if (!selectedItems.value || selectedItems.value.length == 0) {
    toastStore.push({
      type: "warning",
      message: tl("Common", "NoRecordSelected"),
    });
    return;
  }

  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDeleteMulti", [selectedItems.value.length]),
    async (res: any) => {
      if (res) await onDeleteMulti(selectedItems.value);
    }
  );
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
  router.push({ name: "RoleAddNew" });
};

const onEdit = (item: any) => {
  router.push({ name: "RoleEditByID", params: { id: item.id } });
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
  await roleService.deleteMulti(data.map((x: any) => x.id)).then(() => {
    getList();
  });
};
</script>
