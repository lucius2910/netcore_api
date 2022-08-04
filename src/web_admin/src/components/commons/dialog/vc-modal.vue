<template>
  <v-dialog v-model="is_show">
    <v-card class="pa-4">
      <v-card-title>
        <slot name="title"></slot>
      </v-card-title>
      <vc-card-content>
        <slot name="content"></slot>
      </vc-card-content>
      <v-card-actions>
        <slot name="action"></slot>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { ref } from "vue";

const is_show = ref(false);

const show = () => {
  is_show.value = true;
};

const hide = () => {
  is_show.value = false;
};

defineExpose({
  show,
  hide,
});
</script>

<style lang="scss">
@import "@/assets/styles/commons/vc-modal.scss";
</style>
