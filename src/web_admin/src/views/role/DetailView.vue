<template>
  <vc-card class="mb-4 mt-2 pa-4">
    <vc-card-content>
      <v-form ref="roleForm" lazy-validation>
        <vc-row>
          <vc-col :lg="4" :md="6" :sm="12" :xs="12">
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
              <vc-textarea v-model="role.description" />
            </vc-input-group>
          </vc-col>

          <vc-col :lg="8" :md="6" :sm="12" :xs="12">
            <vc-input-group :label="tl('Role', 'Permission')">
              <vc-treeview
                height="500"
                v-model="role.permissions"
                :data="functions"
                fieldTitle="description"
                fieldItems="items"
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
        color="error"
        @click="onDeleteConfirm"
        :loading="isLoading"
        class="ml-2"
        v-if="role.id"
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
import roleService from "@/services/role.service";
import functionService from "@/services/function.service";
import tl from "@/utils/locallize";

const roleForm = ref<any>(null);
const router = useRouter();
const route = useRoute();
const isLoading = ref(false);
const functions = ref<any>([]);
const role = ref<any>({});

const confirmDialog = ref<any>(null);

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
