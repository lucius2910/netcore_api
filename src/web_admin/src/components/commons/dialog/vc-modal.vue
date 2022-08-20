<template>
  <el-dialog v-model="is_show" :show-close="false" :lock-scroll="true" :before-close="handleClose">
    <template #header="{titleId, titleClass }">
      <div class="d-flex space-between align-centter">
        <h4 :id="titleId" :class="titleClass">{{title}}</h4>
        <div>
          <slot name="acttion"></slot>
          <vc-button type="info" class="btn-close" @click="onClose">
            <el-icon><Close /></el-icon>
          </vc-button>
        </div>
      </div>
    </template>
    <slot name="content"></slot>
  </el-dialog>
</template>

<script setup lang="ts">
import { ref, toRef } from "vue";
import { Close } from '@element-plus/icons-vue';

const props = defineProps<{
  title?: string
}>()

const is_show = ref(false);
const is_close = ref(false);
const title = toRef(props, "title");

const handleClose = () => {
  if(is_close.value) close();
}

const onClose = () => {
  is_close.value = true;
  handleClose();
}

const open = () => {
  is_show.value = true;
  is_close.value = false;
};

const close = () => {
  is_show.value = false;
  is_close.value = false;
};

defineExpose({
  open,
  close,
});
</script>

<style lang="scss">
@import "@/assets/styles/commons/vc-modal.scss";
</style>
