<template>
  <el-aside :class="['side-bar', { isCollapse: isCollapse }]">
    <el-scrollbar height="100vh">
      <vc-menu :collapse="isCollapse"></vc-menu>
    </el-scrollbar>
  </el-aside>
</template>

<script setup lang="ts">
import { toRef} from "vue";

const props = defineProps<{
  show?: boolean;
  isCollapse?: boolean;
}>();

const isCollapse = toRef<any, string>(props, "isCollapse");


</script>

<style lang="scss">
@import "@/assets/styles/commons/sidebar";
</style>
