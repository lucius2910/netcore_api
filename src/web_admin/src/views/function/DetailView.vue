<template>
  <div class="vc-page">
    <vc-card class="mb-4 mt-2 pa-4">
      <vc-card-content>
        <el-form  
          ref="roleForm"
          :model="func"
          :rules="rules"
          label-width="120px">
          <vc-row :gutter="20">
            <vc-col :lg="122" :md="12" :sm="24" :xs="24">
              <vc-input-group required :label="tl('Function', 'Module')">
                <vc-select
                  v-model="func.module"
                  :items="modules"
                  fieldValue="key"
                  fieldText="value"
                  :rules="[ (v: any) => validate.required(v, tl('Function', 'Module'))]"
                ></vc-select>
              </vc-input-group>

              <vc-input-group required :label="tl('Function', 'Code')">
                <vc-input
                  v-model="func.code"
                  :rules="[ (v: any) => validate.required(v, tl('Function', 'Code'))]"
                />
              </vc-input-group>

              <vc-input-group required :label="tl('Function', 'Name')">
                <vc-input
                  v-model="func.text"
                  :rules="[ (v: any) => validate.required(v, tl('Function', 'Name'))]"
                />
              </vc-input-group>

              <vc-input-group :label="tl('Function', 'Description')">
                <vc-textarea v-model="func.description" rows="4"/>
              </vc-input-group>
            </vc-col>

            <vc-col :lg="12" :md="12" :sm="24" :xs="24">
              <vc-input-group :label="tl('Function', 'Parent')">
                <vc-select
                  v-model="func.parent"
                  :items="parents"
                  fieldValue="id"
                  fieldText="fullName"
                ></vc-select>
              </vc-input-group>

              <vc-input-group :label="tl('Function', 'Url')">
                <vc-input v-model="func.url" />
              </vc-input-group>

              <vc-input-group :label="tl('Function', 'Api')">
                <vc-input v-model="func.path" />
              </vc-input-group>

              <vc-input-group :label="tl('Function', 'Method')">
                <vc-select
                  v-model="func.method"
                  :items="methodItem"
                  fieldValue="key"
                  fieldText="value"
                ></vc-select>
              </vc-input-group>

              <vc-input-group :label="tl('Function', 'Order')">
                <vc-input v-model="func.order" />
              </vc-input-group>

              <vc-input-group>
                <vc-checkbox
                  :label="tl('Function', 'Active')"
                  v-model="func.is_active"
                ></vc-checkbox>
              </vc-input-group>
            </vc-col>
          </vc-row>
        </el-form>
      </vc-card-content>

      <vc-card-action class="d-flex pa-3">
        <v-spacer></v-spacer>
        <vc-button @click="goBack" :icon="ArrowLeft">
          {{ tl("Common", "BtnBack") }}
        </vc-button>

        <vc-button type="primary" @click="onSave" :loading="isLoading" class="ml-2">
          {{ tl("Common", "BtnSave") }}
        </vc-button>

        <vc-button
          color="error"
          class="ml-2"
          type="danger"
          :loading="isLoading"
          :icon="Delete"
          @click="onDeleteConfirm"
          v-if="func.id"
        >
          {{ tl("Common", "BtnDelete") }}
        </vc-button>
      </vc-card-action>
      <vc-confirm ref="confirmDialog"></vc-confirm>
    </vc-card>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref, reactive } from "vue";
import validate from "@/utils/validate";
import { useRouter, useRoute } from "vue-router";
import functionService from "@/services/function.service";
import tl from "@/utils/locallize";
import masterService from "@/services/master.service";
import methodItem from "@/commons/defines/methodItem";
import {ArrowLeft, Delete} from '@element-plus/icons-vue';

const functionForm = ref<any>(null);
const router = useRouter();
const route = useRoute();
const isLoading = ref(false);
const modules = ref<any>([]);
const parents = ref<any>([]);

const confirmDialog = ref<any>(null);
const rules = ref<any>({});

const _id = route.params.id as string;


const func = reactive({
  id: null,
  module: null,
  code: null,
  text: false,
  description: null,
  parent: null,
  url: null,
  path: null,
  method: null,
  is_active: false,
  order: null,
});

onMounted(async () => {
  await getListModule();
  await getListParent();

  if (_id) await getFunctionDetail();
});

const getListModule = async () => {
  await masterService
    .getList({
      search: "MODULE",
      page: 1,
      size: 100,
    })
    .then(async (data) => {
      modules.value = data.data ?? [];
    })
    .finally(() => {
      isLoading.value = false;
    });
};

const getListParent = async () => {
  await functionService
    .getList({
      page: 1,
      size: 10000,
      sort: "code.asc",
    })

    .then(async (data) => {
      parents.value = data.data ?? [];
      parents.value = parents.value.map((item: any) => {
        return { ...item, fullName: `${item.code} - ${item.description}` };
      });
    })
    .finally(() => {
      isLoading.value = false;
    });
};

const getFunctionDetail = async () => {
  const response = await functionService.detail(_id);
  Object.assign(func, response?.data);
};

const goBack = () => {
  router.push({ name: "FunctionList" });
};

const onSave = async () => {
  const { valid } = await functionForm.value.validate();
  if (!valid) return;
  isLoading.value = true;

  console.log(func);
  if (_id) {
    await functionService.update(func).finally(() => {
      isLoading.value = false;
    });
  } else {
    await functionService.create(func).finally(() => {
      isLoading.value = false;
      console.log(func);
    });
  }
};

const onDeleteConfirm = () => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [func.code]),

    async (res: any) => {
      if (res) await onDelete();
    }
  );
};

const onDelete = async () => {
  await functionService.delete(_id).then(() => {
    goBack();
  });
};
</script>
