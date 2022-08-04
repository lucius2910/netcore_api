<template>
  <div class="vc-page">
    <vc-card class="mb-4 mt-2 pa-4">
      <vc-card-content>
        <v-form ref="functionForm" lazy-validation>
          <vc-row>
            <vc-col :lg="5" :md="6" :sm="12" :xs="12">
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
                <vc-textarea v-model="func.description" />
              </vc-input-group>
            </vc-col>

            <vc-col :lg="5" :md="6" :sm="12" :xs="12">
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
                  v-model="func.methodItem"
                  :items="methodItem"
                  fieldValue="key"
                  fieldText="value"
                ></vc-select>
              </vc-input-group>

              <vc-input-group :label="tl('Function', 'Order')">
                <vc-input v-model="func.order" />
              </vc-input-group>

              <vc-checkbox
                :label="tl('Function', 'Active')"
                v-model="func.is_active"
              ></vc-checkbox>
            </vc-col>
          </vc-row>
        </v-form>
      </vc-card-content>

      <vc-card-action class="d-flex pa-3">
        <v-spacer></v-spacer>
        <vc-button color="secondary" @click="goBack">
          <v-icon light>mdi-arrow-left</v-icon>
          {{ tl("Common", "BtnBack") }}
        </vc-button>

        <vc-button @click="onSave" :loading="isLoading" class="ml-2">
          <v-icon light>mdi-content-save-outline</v-icon>
          {{ tl("Common", "BtnSave") }}
        </vc-button>

        <vc-button
          color="error"
          @click="onDeleteConfirm"
          :loading="isLoading"
          class="ml-2"
          v-if="func.id"
        >
          <v-icon light>mdi-trash-can-outline</v-icon>
          {{ tl("Common", "BtnDelete") }}
        </vc-button>
      </vc-card-action>
      <vc-confirm ref="confirmDialog"></vc-confirm>
    </vc-card>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from "vue";
import validate from "@/utils/validate";
import { useRouter, useRoute } from "vue-router";
import functionService from "@/services/function.service";
import tl from "@/utils/locallize";
import masterService from "@/services/master.service";
import methodItem from "@/commons/defines/methodItem";

const functionForm = ref<any>(null);
const router = useRouter();
const route = useRoute();
const isLoading = ref(false);
const modules = ref<any>([]);
const parents = ref<any>([]);
const func = ref<any>({});

const confirmDialog = ref<any>(null);

const _id = route.params.id as string;

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
  func.value = response?.data;
};

const goBack = () => {
  router.push({ name: "FunctionList" });
};

const onSave = async () => {
  const { valid } = await functionForm.value.validate();
  if (!valid) return;
  isLoading.value = true;

  console.log(func.value);
  if (_id) {
    await functionService.update(func.value).finally(() => {
      isLoading.value = false;
    });
  } else {
    await functionService.create(func.value).finally(() => {
      isLoading.value = false;
      console.log(func.value);
    });
  }
};

const onDeleteConfirm = () => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [func.value.code]),

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
