<template>
  <el-alert class="vc-alert mt-2" v-show="is_show" :type="type" show-icon>
    {{ message }}
  </el-alert>
</template>

<script setup lang="ts">
import { useToastStore } from "@/stores/toast.store";

const props = defineProps<{
  is_show: boolean;
  type: string;
  message: string;
  id: string;
}>();

const toastStore = useToastStore();

setTimeout(() => {
  toastStore.dissmisNotify(props.id);
}, 4000);
</script>

<style lang="scss">
.v-alert__avatar {
  display: none !important;
}
</style>
