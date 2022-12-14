<template>
  <div class="vc-page">
    <vc-card class="mb-4 mt-2 pa-4">
      <vc-card-content>
        <el-form  
            ref="roleForm"
            :model="master"
            :rules="rules"
            label-width="120px">
          <vc-row>
            <vc-col :lg="12" :md="12" :sm="24" :xs="24">
              <vc-input-group required :label="tl('MasterCode', 'Type')">
                <vc-input
                  v-model="master.type"
                />
              </vc-input-group>

              <vc-input-group required :label="tl('MasterCode', 'Key')">
                <vc-input
                  v-model="master.key"
                />
              </vc-input-group>

              <vc-input-group required :label="tl('MasterCode', 'Value')">
                <vc-input
                  v-model="master.value"
                />
              </vc-input-group>
            </vc-col>
          </vc-row>
        </el-form >
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
          class="ml-2"
          color="error"
          @click="onDeleteConfirm"
          :loading="isLoading"
          :icon="Delete"
          v-if="master.id"
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
import masterService from "@/services/master.service";
import tl from "@/utils/locallize";
import {ArrowLeft, Delete} from '@element-plus/icons-vue';

const masterForm = ref<any>(null);
const router = useRouter();
const route = useRoute();
const isLoading = ref(false);
const rules = ref({});

const master = reactive({
  id: null,
  type: null,
  key: null,
  value: false
});

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
  Object.assign(master, response?.data);
};

const goBack = () => {
  router.push({ name: "MasterCodeList" });
};

const onSave = async () => {
  const { valid } = await masterForm.value.validate();
  if (!valid) return;
  isLoading.value = true;

  if (_id) {
    await masterService.update(master).finally(() => {
      isLoading.value = false;
    });
  } else {
    await masterService.create(master).finally(() => {
      isLoading.value = false;
      Object.assign(master, {
        type: null,
        key: null,
        value: null,
      });
    });
  }
};

const onDeleteConfirm = () => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [
      `${master.type}-${master.value}`,
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
