<template>
  <vc-card class="mb-4 mt-2 pa-4">
    <vc-card-content>
      <v-form ref="masterForm" lazy-validation>
        <vc-row>
          <vc-col :lg="4" :md="6" :sm="12" :xs="12">
            <vc-input-group required :label="tl('MasterCode', 'Type')">
              <vc-input
                v-model="master.type"
                :rules="[ (v: any) => validate.required(v,  tl('MasterCode', 'Type'))]"
              />
            </vc-input-group>

            <vc-input-group required :label="tl('MasterCode', 'Key')">
              <vc-input
                v-model="master.key"
                :rules="[ (v: any) => validate.required(v,  tl('MasterCode', 'Key'))]"
              />
            </vc-input-group>

            <vc-input-group required :label="tl('MasterCode', 'Value')">
              <vc-input
                v-model="master.value"
                :rules="[ (v: any) => validate.required(v,  tl('MasterCode', 'Value'))]"
              />
            </vc-input-group>
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
        class="ml-2"
        color="error"
        @click="onDeleteConfirm"
        :loading="isLoading"
        v-if="master.id"
      >
        <v-icon light>mdi-trash-can-outline</v-icon>
        {{ tl("Common", "BtnDelete") }}
      </vc-button>
    </vc-card-action>
    <vc-confirm ref="confirmDialog"></vc-confirm>
  </vc-card>
</template>

<script setup lang="ts">
import { onMounted, ref } from "vue";
import validate from "@/utils/validate";
import { useRouter, useRoute } from "vue-router";
import masterService from "@/services/master.service";
import tl from "@/utils/locallize";

const masterForm = ref<any>(null);
const router = useRouter();
const route = useRoute();
const isLoading = ref(false);
const master = ref<any>({});

const confirmDialog = ref<any>(null);
const _id = route.params.id as string;

onMounted(() => {
  // get data form api
  if (_id) {
    getMasterCodeDetail();
  }
});

const getMasterCodeDetail = async () => {
  const response = await masterService.detail(_id);
  master.value = response?.data;
};

const goBack = () => {
  router.push({ name: "MasterCodeList" });
};

const onSave = async () => {
  const { valid } = await masterForm.value.validate();
  if (!valid) return;
  isLoading.value = true;

  if (_id) {
    await masterService.update(master.value).finally(() => {
      isLoading.value = false;
    });
  } else {
    await masterService.create(master.value).finally(() => {
      isLoading.value = false;
      master.value = {
        type: null,
        key: null,
        value: null,
      };
    });
  }
};

const onDeleteConfirm = () => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [
      `${master.value.type}-${master.value.value}`,
    ]),
    async (res: any) => {
      if (res) {
        await masterService.delete(_id).then(() => {
          goBack();
        });
      }
    }
  );
};
</script>
