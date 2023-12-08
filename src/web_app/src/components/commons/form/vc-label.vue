<template>
  <div :class="['vc-label py-1', { required: required }]">
    <label>
      <slot></slot>
    </label>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  required?: boolean;
}>();

</script>

<style lang="scss">
@import "@/assets/styles/commons/vc-label.scss";
</style>
