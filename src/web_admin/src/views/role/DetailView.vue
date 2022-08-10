<template>
  <div class="vc-page page-role-detail">
    <vc-card>
      <el-form 
          ref="roleForm"
          :model="role"
          :rules="rules"
          label-width="120px">
        <vc-row :gutter="20">
          <vc-col :lg="12" :md="12" :sm="24" :xs="24">
            <vc-input-group required :label="tl('Role', 'Code')">
              <vc-input
                v-model="role.code"
                :rules="[ (v: any) => validate.required(v, tl('Role', 'Code'))]"
              />
            </vc-input-group>

            <vc-input-group required :label="tl('Role', 'Text')">
              <vc-input
                v-model="role.text"
                :rules="[ (v: any) => validate.required(v, tl('Role', 'Text'))]"
              />
            </vc-input-group>

            <vc-input-group :label="tl('Role', 'Description')">
              <vc-textarea rows="5" v-model="role.description" />
            </vc-input-group>
          </vc-col>

          <vc-col :lg="12" :md="12" :sm="24" :xs="24">
            <vc-input-group :label="tl('Role', 'Permission')">
              <vc-treeview
                class="select-permission"
                v-model="role.permissions"
                :data="functions"
                fieldTitle="description"
                fieldItems="items"
                show-checkbox
              />
            </vc-input-group>
          </vc-col>
        </vc-row>
      </el-form>

      <vc-button @click="goBack" :icon="ArrowLeft">
        {{ tl("Common", "BtnBack") }}
      </vc-button>

      <vc-button type="primary" @click="onSave" :loading="isLoading" class="ml-2">
        {{ tl("Common", "BtnSave") }}
      </vc-button>

      <vc-button
        color="error"
        @click="onDeleteConfirm"
        :loading="isLoading"
        class="ml-2"
        type="danger" 
        v-if="role.id"
      >
        {{ tl("Common", "BtnDelete") }}
      </vc-button>
      <vc-confirm ref="confirmDialog"></vc-confirm>
    </vc-card>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref, reactive } from "vue";
import validate from "@/utils/validate";
import { useRouter, useRoute } from "vue-router";
import roleService from "@/services/role.service";
import functionService from "@/services/function.service";
import tl from "@/utils/locallize";
import { ArrowLeft } from '@element-plus/icons-vue';

const roleForm = ref<any>(null);
const router = useRouter();
const route = useRoute();
const isLoading = ref(false);
const functions = ref<any>([]);

const confirmDialog = ref<any>(null);
const rules = reactive();
const role = reactive({
  code: null,
  text: null,
  description: null,
  permissions: [],
});

const _id = route.params.id as string;

onMounted(async () => {
  await getFunction();

  if (_id) await getRoleDetail();
});

const getRoleDetail = async () => {
  const response = await roleService.detail(_id);
  role.value = response?.data;
};

const getFunction = async () => {
  await functionService.getTreeView().then((response) => {
    functions.value = response.data;
  });
};

const goBack = () => {
  router.push({ name: "RoleList" });
};

const onSave = async () => {
  role.value.permissions = role.value.permissions ?? [];
  const { valid } = await roleForm.value.validate();
  if (!valid) return;
  isLoading.value = true;

  if (_id) {
    await roleService.update(role.value).finally(() => {
      isLoading.value = false;
    });
  } else {
    await roleService.create(role.value).finally(() => {
      isLoading.value = false;
      console.log(role.value);
    });
  }
};

const onDeleteConfirm = () => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [role.value.code]),
    async (res: any) => {
      if (res) await onDelete();
    }
  );
};

const onDelete = async () => {
  await roleService.delete(_id).then(() => {
    goBack();
  });
};

</script>


<style lang="scss">
@import "@/assets/styles/page/role";
</style>

