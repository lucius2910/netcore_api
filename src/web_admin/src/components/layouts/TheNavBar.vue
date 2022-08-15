<template>
  <el-header class="navbar">
    <vc-button
      :icon="ArrowLeftBold"
      :class="['mt-1 ml-2 pa-1 pl-2 btn-toggle-sidebar', { actived: showNav }]"
      @click="toogleSidebar"
    />
  </el-header>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { ArrowLeftBold } from "@element-plus/icons-vue";

defineProps<{
  show?: boolean;
}>();

const emit = defineEmits(["toogleSidebar"]);

const showNav = ref(false);

const toogleSidebar = () => {
  showNav.value = !showNav.value;
  emit("toogleSidebar", showNav.value);
};
</script>

<style lang="scss">
@import "@/assets/styles/commons/navbar.scss";
</style>
