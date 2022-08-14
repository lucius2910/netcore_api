<template>
  <vc-modal ref="modal" :title="tl('User', 'DetailTitle')" width="70%">
      <template #content>
        <el-form
          ref="userForm"
          :model="user"
          :rules="rules"
          label-width="150px"
          label-position="left"
        >
          <vc-row :gutter="20">
            <vc-col :lg="12" :md="12" :sm="24" :xs="24">
              <vc-input-group prop="code" :label="tl('User', 'Code')">
                <vc-input disabled v-model="user.code" />
              </vc-input-group>

              <vc-input-group
                required
                prop="user_name"
                :label="tl('User', 'UserName')"
              >
                <vc-input v-model="user.user_name" />
              </vc-input-group>

              <vc-input-group
                required
                prop="full_name"
                :label="tl('User', 'FullName')"
              >
                <vc-input v-model="user.full_name" />
              </vc-input-group>

              <vc-input-group
                required
                prop="email"
                :label="tl('User', 'Email')"
              >
                <vc-input v-model="user.email" />
              </vc-input-group>

              <vc-input-group
                required
                prop="phone"
                :label="tl('User', 'Phone')"
              >
                <vc-input v-model="user.phone"></vc-input>
              </vc-input-group>

              <vc-input-group
                required
                prop="birthday"
                :label="tl('User', 'BidthDay')"
              >
                <vc-input-date v-model="user.birthday" />
              </vc-input-group>

              <vc-input-group required :label="tl('User', 'Gender')">
                <vc-select
                  v-model="user.gender"
                  fieldValue="value"
                  fieldText="name"
                  :items="genders"
                ></vc-select>
              </vc-input-group>

              <vc-input-group>
                <vc-checkbox
                  :label="tl('User', 'Active')"
                  v-model="user.is_actived"
                ></vc-checkbox>
              </vc-input-group>
            </vc-col>

            <vc-col :lg="12" :md="12" :sm="24" :xs="24">
              <vc-input-group :label="tl('User', 'Role')">
                <vc-treeview
                  ref="treeRef"
                  :data="roles"
                  v-model="user.roles"
                  show-checkbox
                  style="width: 100%"
                ></vc-treeview>
              </vc-input-group>
            </vc-col>
          </vc-row>
        </el-form>
      </template>

      <template #acttion>
        <vc-button @click="goBack" :icon="Back">
          {{ tl("Common", "BtnBack") }}
        </vc-button>

        <vc-button
          type="primary"
          class="ml-2"
          code="F00004"
          @click="onSave(userForm)"
          :loading="isLoading"
          :icon="FolderChecked"
        >
          {{ tl("Common", "BtnSave") }}
        </vc-button>

        <vc-button
          v-if="user.id"
          type="danger"
          class="ml-2"
          code="F00006"
          :icon="Delete"
          :loading="isLoading"
          @click="onDeleteConfirm"
        >
          {{ tl("Common", "BtnDelete") }}
        </vc-button>
      </template>

      <vc-confirm ref="confirmDialog"></vc-confirm>
  </vc-modal>
</template>

<script setup lang="ts">
import { onBeforeMount, ref, reactive } from "vue";
import validate from "@/utils/validate_elp";
import tl from "@/utils/locallize";
import { useRouter, useRoute } from "vue-router";
import type { FormInstance } from "element-plus";
import userService from "@/services/user.service";
import roleService from "@/services/role.service";
import masterCodeService from "@/services/master.service";
import { FolderChecked, Back, Delette } from '@element-plus/icons-vue';

const rules = reactive({
  user_name: [
    { required: true, validator: validate.required, trigger: ["blur"] },
  ],
  full_name: [
    { required: true, validator: validate.required, trigger: ["blur"] },
  ],
  birthday: [{ validator: validate.dateRule, trigger: ["change"] }],
  email: [
    { required: true, validator: validate.required, trigger: ["blur"] },
    { validator: validate.emailRule, trigger: ["change"] },
  ],
  phone: [
    { required: true, validator: validate.required, trigger: ["blur"] },
    { validator: validate.phoneNumberRule, trigger: ["change"] },
  ],
});

const userForm = ref<FormInstance>();
const router = useRouter();
const route = useRoute();
const isLoading = ref(false);
const roles = ref<any>([]);
const genders = ref<any>([]);
const confirmDialog = ref<any>(null);
const modal = ref<any>(null);

const user = reactive({
  id: null,
  user_name: null,
  is_actived: false,
  full_name: null,
  email: null,
  phone: null,
  gender: null,
  birthday: null,
  roles: [],
  code: null,
});

const _id = route.params.id as string;

onBeforeMount(async () => {
  await init();

  if (_id) await getUserDetail();
});

const getUserDetail = async () => {
  const response = await userService.detail(_id);
  Object.assign(user, response?.data);
};

const init = async () => {
  genders.value = (await masterCodeService.getByType("GENDER"))?.data;
  roles.value = (await roleService.getList({ page: 1, size: 100 }))?.data;
};

const goBack = () => {
  //router.push({ name: "UserList" });
  modal.value.close()
};

const onSave = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;

  await formEl.validate(async (valid) => {
    if (!valid) return;

    isLoading.value = true;
    user.roles = user.roles ?? [];

    if (_id) {
      await userService.update(user).finally(() => {
        isLoading.value = false;
      });
    } else {
      await userService.create(user).finally(() => {
        isLoading.value = false;
      });
    }
  });
};

const onDeleteConfirm = () => {
  confirmDialog.value.confirm(
    tl("Common", "Delete"),
    tl("Common", "ConfirmDelete", [user.code]),
    async (res: any) => {
      if (res) await onDelete();
    }
  );
};

const onDelete = async () => {
  await userService.delete(_id).then(() => {
    goBack();
  });
};

const open = () => {
  modal.value.open();
};

const close = () => {
  modal.value.close()
};

defineExpose({
  open,
  close,
});

</script>
