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
            <vc-input-group prop="code" :label="tl('Role', 'Code')">
              <vc-input
                v-model="role.code"
              />
            </vc-input-group>

            <vc-input-group prop="name" :label="tl('Role', 'Name')">
              <vc-input
                v-model="role.name"
              />
            </vc-input-group>

            <vc-input-group :label="tl('Role', 'Description')">
              <vc-textarea rows="5" v-model="role.description" />
            </vc-input-group>
          </vc-col>

          <vc-col :lg="12" :md="12" :sm="24" :xs="24">
            <vc-input-group :label="tl('Role', 'Permission')">
              <vc-treeview
                show-checkbox
                class="select-permission"
                node_key="code"
                v-model="role.permissions"
                :data="functions"
              />
            </vc-input-group>
          </vc-col>
        </vc-row>
      </el-form>

      <vc-button @click="goBack" :icon="ArrowLeft">
        {{ tl("Common", "BtnBack") }}
      </vc-button>

      <vc-button type="primary" @click="onSave(roleForm)" :loading="isLoading" class="ml-2">
        {{ tl("Common", "BtnSave") }}
      </vc-button>

      <vc-button
        type="danger"
        class="ml-2"
        :loading="isLoading"
        :icon="Delete"
        @click="onDeleteConfirm"
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
import { useRouter, useRoute } from "vue-router";
import roleService from "@/services/role.service";
import functionService from "@/services/function.service";
import type { FormInstance } from "element-plus";
import { ArrowLeft, Delete } from '@element-plus/icons-vue';
import tl from "@/utils/locallize";
import validate from "@/utils/validate_elp";

const roleForm = ref<FormInstance>();
const router = useRouter();
const route = useRoute();
const isLoading = ref(false);
const functions = ref<any>([]);

const confirmDialog = ref<any>(null);
const rules = reactive({
  code: [
    { required: true, validator: validate.required, trigger: ["blur"] },
  ],
  name: [
    { required: true, validator: validate.required, trigger: ["blur"] },
  ],
});

const role = reactive({
  id: null,
  code: null,
  name: null,
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
  Object.assign(role, response?.data);
};

const getFunction = async () => {
  await functionService.getTreeView().then((response) => {
    functions.value = response.data;
  });
};

const goBack = () => {
  router.push({ name: "RoleList" });
};

const onSave = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;

  await formEl.validate(async (valid) => {
    if (!valid) return;

    isLoading.value = true;
    role.permissions = role.permissions ?? [];

    if (_id) {
      await roleService.update(role).finally(() => {
        isLoading.value = false;
      });
    } else {
      await roleService.create(role).finally(() => {
        isLoading.value = false;
        console.log(role);
      });
    }
  })
};

const onDeleteConfirm = () => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [role.code]),
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

