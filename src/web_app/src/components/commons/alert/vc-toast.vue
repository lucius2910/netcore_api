<template>
  <div class="vc-toast">
    <vc-alert
      v-for="(item, index) in items"
      :key="index"
      :is_show="item.is_show"
      :type="item.type"
      :message="item.message"
      :id="item._id"
    >
    </vc-alert>
  </div>
</template>

<script setup lang="ts">
import { storeToRefs } from "pinia";
import { useToastStore } from "@/stores/toast.store";

const toastStore = useToastStore();
const { items } = storeToRefs(toastStore);
</script>

<style lang="scss" scoped>
@import "@/assets/styles/commons/vc-toast";
</style>
