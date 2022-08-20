<template>
  <div class="vc-page page-master">
    <vc-row>
      <vc-col :lg="6" :md="6" :sm="12">
        <vc-input-group :label="tl('Resource', 'Module')">
          <vc-select
            v-model="searchModule"
            :items="modulelist"
            @selected="onSelectedModule"
            fieldValue="key"
            fieldText="value"
          ></vc-select>
        </vc-input-group>
      </vc-col>
      <vc-col :lg="6" :md="6" :sm="12">
        <vc-input-group :label="tl('Resource', 'Screen')" class="ml-2">
          <vc-select
            v-model="searchScreen"
            :items="screenList"
            @selected="onSelectedScreen"
          ></vc-select>
        </vc-input-group>
      </vc-col>
      <vc-col :lg="12" :md="12" :sm="12" class="d-flex flex-end">
        <vc-button type="primary" class="ml-2" @click="onAddNew" :icon="Plus">
          {{ tl("Common", "BtnAddNew") }} 
        </vc-button>
      </vc-col>
    </vc-row>
    <vc-row :gutter="20">
      <vc-col :span="12">
        <vc-table
          :datas="resourceList"
          :tableConfig="tableConfig"
          :colConfigs="colConfig"
          :page="pageConfig"
          :loading="isLoading"
          @sorted="onSort"
          @dbClick="onDbClick"
          @pageChanged="onPageChanged"
        >
        </vc-table>
      </vc-col>
      <vc-col :span="12">
        <vc-card class="pa-4">
          <vc-card-content>
            <el-form 
              label-width="70px"
              ref="detailForm"
              :model="detailItem"
              :rules="rules">
              <vc-row>
                <vc-col>
                  <vc-input-group
                    required
                    :label="tl('Resource', 'Key')"
                    class="mb-2"
                    prop="key"
                  >
                    <vc-input
                      v-model="detailItem.key"
                    >
                    </vc-input>
                  </vc-input-group>
                  <vc-input-group required :label="tl('Resource', 'Value')" prop="text">
                    <vc-textarea
                      rows="17"
                      v-model="detailItem.text"
                    />
                  </vc-input-group>
                </vc-col>
              </vc-row>
            </el-form>
          </vc-card-content>
          <vc-card-action class="d-flex pa-3">
            <vc-button @click="onSave(detailForm)" :loading="isLoading" class="ml-2" type="primary">
              {{ tl("Common", "BtnSave") }}
            </vc-button>
            <vc-button
              class="ml-2"
              type="danger"
              v-show="detailItem.id"
              :loading="isLoading"
              :icon="Delete"
              @click="onDeleteConfirm"
            >
              {{ tl("Common", "BtnDelete") }}
            </vc-button>
          </vc-card-action>
          <vc-confirm ref="confirmDialog"></vc-confirm>
        </vc-card>
      </vc-col>
    </vc-row>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref, reactive } from "vue";
import resourceService from "@/services/resource.service";
import masterService from "@/services/master.service";
import tl from "@/utils/locallize";
import validate from "@/utils/validate";
import { colConfig, tableConfig } from "@/commons/tables/resource.table";
import { Delete, Plus} from '@element-plus/icons-vue';
import type { FormInstance } from "element-plus";

const defaultItem = {
  id: null,
  lang: "ja",
  module: null,
  screen: null,
  key: null,
  text: null,
};

const modulelist = ref<any>([]);
const screenList = ref<any>([]);
const resourceList = ref<any[]>([]);

const goSort = ref<any>("");
const searchModule = ref<any>("");
const searchScreen = ref<any>("");
const pageConfig = ref<any>({size: 30});
const isLoading = ref<boolean>(false);
const detailForm = ref<FormInstance>();
const confirmDialog = ref<any>(null);

const detailItem = reactive({...defaultItem});

const rules = reactive({
  key: [
    { required: true, validator: validate.required, trigger: ["blur"] },
  ]
});

onMounted(async () => {
  await getListModule();
});

const onDbClick = (selected: any) => {
  Object.assign(detailItem, selected);
};

const onSort = async (sortBy: any) => {
  goSort.value = sortBy;
  getListResource();
};

const onSelectedModule = async () => {
  await getListScreen();
  await getListResource();
  detailItem.module = searchModule.value;
};

const onSelectedScreen = async () => {
  await getListResource();
  detailItem.screen = searchScreen.value;
};

const getListModule = async () => {
  await masterService
    .getList({
      search: "MODULE",
      sort: goSort.value
    })
    .then(async (data) => {
      modulelist.value = data.data ?? [];
      searchModule.value = modulelist.value[0].value;
      await getListScreen();
    })
    .finally(() => {
      isLoading.value = false;
    });
};

const getListScreen = async () => {
  await resourceService
    .getListScreen({
      module: searchModule.value,
    })
    .then(async (data) => {
      screenList.value =
        data.data.map((x: any) => {
          return { text: x, value: x };
        }) ?? [];
      searchScreen.value = data.data[0];
      await getListResource();
    })
    .finally(() => {
      isLoading.value = false;
    });
};

const getListResource = async () => {
  isLoading.value = true;
  await resourceService
    .getList({
      screen: searchScreen.value,
      module: searchModule.value,
      sort: goSort.value,
      ...pageConfig.value
    })
    .then((data) => {
      resourceList.value = data.data ?? [];
      pageConfig.value.total = data.total
    })
    .finally(() => {
      isLoading.value = false;
    });
};

const onSave = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;

  await formEl.validate(async (valid) => {
    if (!valid) return;

    isLoading.value = true;

    if (detailItem.id) {
      await resourceService.update(detailItem).finally(() => {
        isLoading.value = false;
      });
    } else {
      await resourceService.create(detailItem).finally(() => {
        isLoading.value = false;
      });
    }

    await getListResource();
    onAddNew();
  })
};

const onDeleteConfirm = () => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [detailItem.key]),
    async (res: any) => {
      if (res) await onDelete();
    }
  );
};

const onPageChanged = async (page: any) => {
  pageConfig.value = {...page};
  getListResource();
};

const onDelete = async () => {
  isLoading.value = false;
  await resourceService.delete(detailItem.id).finally(async () => {
    isLoading.value = false;
    await getListResource();
    onAddNew();
  });
};

const onAddNew = () => {
  Object.assign(detailItem, defaultItem)
};
</script>
