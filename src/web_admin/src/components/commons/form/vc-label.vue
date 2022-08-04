<template>
  <div :class="['vc-label py-1', type, { required: required }]">
    <label>
      <slot></slot>
      <!-- <span class="lbl-required" v-if="required"> * </span> -->
    </label>
  </div>
</template>

<script setup lang="ts">
import { useAttrs } from "vue";
defineProps<{
  required?: boolean;
}>();

const attrs = useAttrs();
const type = "trans"; //attrs['type'] ?? ''
</script>

<style lang="scss">
@import "@/assets/styles/commons/vc-label.scss";
</style>
