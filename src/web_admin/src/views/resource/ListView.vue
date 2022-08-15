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
              ref="roleForm"
              :model="detailItem"
              :rules="rules"
              label-width="70px">
              <vc-row>
                <vc-col>
                  <vc-input-group
                    required
                    :label="tl('Resource', 'Key')"
                    class="mb-2"
                  >
                    <vc-input
                      v-model="detailItem.key"
                    >
                    </vc-input>
                  </vc-input-group>
                  <vc-input-group required :label="tl('Resource', 'Value')">
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
            <vc-button @click="onSave" :loading="isLoading" class="ml-2" type="primary">
              {{ tl("Common", "BtnSave") }}
            </vc-button>
            <vc-button
              class="ml-2"
              color="error"
              @click="onDeleteConfirm"
              :loading="isLoading"
              :icon="Delete"
              v-if="detailItem.id"
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

const defaultItem = {
  lang: "ja",
  module: null,
  screen: null,
  key: null,
  text: null,
};

const goSort = ref<any>("");
const detailItem = ref<any>(defaultItem);
const searchModule = ref<any>("");
const modulelist = ref<any>([]);
const searchScreen = ref<any>("");
const screenList = ref<any>([]);
const pageConfig = ref<any>({size: 30});
const resourceList = ref<any[]>([]);
const isLoading = ref<boolean>(false);
const resourceForm = ref<any>(null);
const confirmDialog = ref<any>(null);


const rules = reactive({
  key: [
    { required: true, validator: validate.required, trigger: ["blur"] },
  ]
});

onMounted(async () => {
  await getListModule();
});

const onDbClick = (selected: any) => {
  detailItem.value = { ...selected };
};

const onSort = async (sortBy: any) => {
  goSort.value = sortBy;
  getListResource();
};

const onSelectedModule = async () => {
  await getListScreen();
  await getListResource();
  detailItem.value.module = searchModule.value;
};

const onSelectedScreen = async () => {
  await getListResource();
  detailItem.value.screen = searchScreen.value;
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

const onSave = async () => {
  const { valid } = await resourceForm.value.validate();
  if (!valid) return;

  isLoading.value = true;

  if (detailItem.value.id) {
    await resourceService.update(detailItem.value).finally(() => {
      isLoading.value = false;
    });
  } else {
    await resourceService.create(detailItem.value).finally(() => {
      isLoading.value = false;
    });
  }

  await getListResource();
  onAddNew();
};

const onDeleteConfirm = () => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [detailItem.value.key]),
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
  await resourceService.delete(detailItem.value.id).finally(async () => {
    isLoading.value = false;
    await getListResource();
    onAddNew();
  });
};

const onAddNew = () => {
  detailItem.value = defaultItem;
};
</script>
