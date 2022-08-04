<template>
  <div class="vc-page page-function">
    <vc-row>
      <vc-col :lg="6" :md="12">
        <vc-input-group :label="tl('Function', 'Module')">
          <vc-input v-model="search.module"></vc-input>
        </vc-input-group>
      </vc-col>
      <vc-col :lg="6" :md="12">
        <vc-input-group :label="tl('Function', 'Parent')">
          <vc-input v-model="search.parent"></vc-input>
        </vc-input-group>
      </vc-col>
      <vc-col :lg="8" :md="12">
        <vc-input-group :label="tl('Function', 'Name')">
          <vc-input v-model="search.name"></vc-input>
        </vc-input-group>
      </vc-col>
      <vc-col :lg="12" :md="12">
        <vc-button type="primary" class="ml-2" @click="onSearch">
          {{ tl("Common", "BtnSearch") }}
        </vc-button>

        <vc-button type="danger" class="ml-2" @click="onClear">
          {{ tl("Common", "BtnClear") }}
        </vc-button>

        <vc-button type="primary" sclass="ml-2 mt-2" @click="onAddNew">
          {{ tl("Common", "BtnAddNew") }}
        </vc-button>
      </vc-col>
    </vc-row>

    <vc-row class="mt-4">
      <vc-col :span="24">
        <vc-table
          :datas="functions"
          :tableConfig="tableConfig"
          :colConfigs="colConfig"
          :page="pageConfig"
          :loading="loading"
          @dbClick="onEdit"
          @sorted="onSort"
          @pageChanged="onPageChanged"
        >
          <template #action="{ data }">
            <div class="d-flex flex-center">
              <vc-button
                class="btn-action"
                type="primary"
                size="small"
                :icon="Edit"
                @click="onEdit(data)"
              >
              </vc-button>

              <vc-button
                class="btn-action"
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
import serviceApi from "@/services/function.service";
import { colConfig, tableConfig } from "@/commons/tables/function.table";
import tl from "@/utils/locallize";
import { useToastStore } from "@/stores/toast.store";
import { Edit, Delete } from '@element-plus/icons-vue';

const router = useRouter();
const functions = ref<any[]>([]);
const pageConfig = ref<any>({});
const loading = ref<boolean>(false);
const pageNo = ref<any>(1);
const goSort = ref<any>("");
const confirmDialog = ref<any>(null);
const search = ref<any>({});
const selectedItems = ref<any>([]);
const searchKey = ref<any>("");
const toastStore = useToastStore();

onMounted(async () => {
  onSearch();
});

const onSearch = async () => {
  loading.value = true;
  await serviceApi
    .getList({
      ...search.value,
      sort: goSort.value,
      page: pageNo.value,
      size: 15,
    })
    .then((data) => {
      functions.value = data.data ?? [];
      pageConfig.value = {
        size: data.size,
        page: data.page,
        total: data.total,
      };
    })
    .finally(() => {
      loading.value = false;
    });
};

const onPageChanged = async (picked: any) => {
  pageNo.value = picked;
  onSearch();
};

const onSort = async (sortBy: any) => {
  goSort.value = sortBy;
  onSearch();
};

const onClear = () => {
  search.value = {
    module: null,
    parent: null,
    name: null,
  };
  onSearch();
};

const onChangeItem = async (data: any) => {
  loading.value = true;
  data.is_active = !data.is_active;
  await serviceApi.update(data).finally(() => {
    loading.value = false;
  });
};

const onAddNew = () => {
  router.push({ name: "FunctionAddNew" });
};

const onEdit = (item: any) => {
  router.push({
    name: "FunctionEditById",
    params: {
      id: item.id,
    },
  });
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
  await serviceApi.deleteMulti(data.map((x: any) => x.id)).then(() => {
    onSearch();
  });
};
</script>
