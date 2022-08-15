<template>
  <div class="vc-page page-master">
    <vc-row>
      <vc-col :span="10" class="d-flex">
        <vc-input v-model="search"></vc-input>
        <vc-button type="primary" class="ml-2" @click="onSearch" :icon="Search">
          {{ tl("Common", "BtnSearch") }}
        </vc-button>
      </vc-col>
      <vc-col :span="14" class="d-flex flex-end">
        <vc-button type="danger" @click="deleteSelected" :icon="Delete">
          {{ tl("Common", "BtnDeleteMulti") }}
        </vc-button>
        <vc-button type="primary" class="ml-2" @click="onCreateNew" :icon="Plus">
          {{ tl("Common", "BtnAddNew") }}
        </vc-button>
      </vc-col>
    </vc-row>

    <vc-row class="mt-4">
      <vc-col :span="24">
        <vc-table
          :datas="masters"
          :tableConfig="tableConfig"
          :colConfigs="colConfig"
          :page="pageConfig"
          :loading="loading"
          @sorted="onSort"
          @rowSelected="onRowSelected"
          @pageChanged="onPageChanged"
        >
          <template #action="{ data }">
            <div class="flex-center">
              <vc-button
                type="primary"
                size="small"
                :icon="Edit"
                @click="onEdit(data)"
              >
              </vc-button>
              <vc-button
                type="danger"
                size="small"
                :icon="Delete"
                @click="onDeleteItem(data)"
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
import masterService from "@/services/master.service";
import { colConfig, tableConfig } from "@/commons/tables/master.table";
import { useToastStore } from "@/stores/toast.store";
import tl from "@/utils/locallize";
import { Delete, Edit, Search, Plus } from '@element-plus/icons-vue';

const masters = ref<any[]>([]);
const pageConfig = ref<any>({});
const router = useRouter();
const search = ref<any>("");
const goSort = ref<any>("");
const selectedItems = ref<any[]>([]);
const loading = ref<boolean>(false);
const confirmDialog = ref<any>(null);
const toastStore = useToastStore();

onMounted(async () => {
  onSearch();
});

const onSearch = async () => {
  loading.value = true;
  await masterService
    .getList({
      ...search.value,
      ...pageConfig.value,
      sort: goSort.value,
    })
    .then((data) => {
      masters.value = data.data ?? [];
      pageConfig.value.total = data.total
    })
    .finally(() => {
      loading.value = false;
    });
};

const deleteSelected = async () => {
  if (!selectedItems.value || selectedItems.value.length == 0) {
    toastStore.push({
      type: "warning",
      message: tl("Common", "NoRecordSelected"),
    });
  } else {
    confirmDialog.value.confirm(
      tl("Common", "Delete"),
      tl("Common", "ConfirmDeleteMulti", [selectedItems.value.length]),
      async (res: any) => {
        if (res) await onDeleteMulti(selectedItems.value);
      }
    );
  }
};

const onRowSelected = (selected: any) => {
  selectedItems.value = selected;
};

const onSort = async (sortBy: any) => {
  goSort.value = sortBy;
  onSearch();
};

const onPageChanged = async (page: any) => {
  pageConfig.value = {...page};
  onSearch();
};

const onEdit = (item: any) => {
  router.push({ name: "MasterCodeEditByID", params: { id: item.id } });
};

const onCreateNew = () => {
  router.push({ name: "MasterCodeAddNew" });
};

const onDeleteItem = (item: any) => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [`${item.type}-${item.value}`]),
    async (res: any) => {
      if (res) await onDeleteMulti([item]);
    }
  );
};

const onDeleteMulti = async (data: any) => {
  await masterService.deleteMulti(data.map((x: any) => x.id)).then(() => {
    onSearch();
  });
};
</script>
