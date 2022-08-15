<template>
  <div class="vc-page page-function">
    <vc-row>
      <vc-col :lg="8" :md="12">
        <vc-input-group>
          <vc-input v-model="search.key" :placeholder="tl('Function', 'SearchPlaceholder')"></vc-input>
        </vc-input-group>
      </vc-col>
      <vc-col :lg="16" :md="12" class="d-flex flex-end">

        <vc-button type="danger" class="ml-2" @click="onClear" :icon="Delete">
          {{ tl("Common", "BtnClear") }}
        </vc-button>

        <vc-button type="primary" sclass="ml-2 mt-2" @click="onAddNew" :icon="Plus">
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
import tl from "@/utils/locallize";
import serviceApi from "@/services/function.service";
import { colConfig, tableConfig } from "@/commons/tables/function.table";
import { Edit, Delete, Plus} from '@element-plus/icons-vue';

const router = useRouter();
const functions = ref<any[]>([]);
const loading = ref<boolean>(false);
const goSort = ref<any>("");
const confirmDialog = ref<any>(null);
const search = ref<any>({});
const pageConfig = ref<any>({});

onMounted(async () => {
  onSearch();
});

const onSearch = async () => {
  loading.value = true;
  await serviceApi
    .getList({
      ...search.value,
      ...pageConfig.value,
      sort: goSort.value,
    })
    .then((data) => {
      functions.value = data.data ?? [];
      pageConfig.value.total = data.total
    })
    .finally(() => {
      loading.value = false;
    });
};

const onPageChanged = async (page: any) => {
  pageConfig.value = {...page};
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
