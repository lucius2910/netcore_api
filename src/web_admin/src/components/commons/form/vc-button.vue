<template>
  <el-button :class="['vc-button text-none', { 'v-btn--loading': loading }]" :loading="loading" :disabled="loading"
    @click="onClicked" v-if="checkPermission()">
    <slot></slot>
  </el-button>
</template>

<script setup lang="ts">
import { toRef } from "vue";
import { storeToRefs } from "pinia";
import { useAuthStore } from "@/stores/auth.store";

const props = defineProps<{
  loading?: boolean;
  code?: string;
}>();

const code = toRef(props, "code");
const emit = defineEmits(["click"]);
const authStore = useAuthStore();
const { account } = storeToRefs(authStore);

const onClicked = () => {
  emit("click");
};

const checkPermission = () => {
  if (!code.value) return true;

  // return account.value.permissions?.find((x: any) => x == code.value)
  //   ? true
  //   : false;

  return true;
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/commons/vc-button";
</style>
