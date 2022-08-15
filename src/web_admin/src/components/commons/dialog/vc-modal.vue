<template>
  <el-dialog v-model="is_show" :show-close="false" :lock-scroll="true" :before-close="handleClose">
    <template #header="{titleId, titleClass }">
      <div class="d-flex space-between align-centter">
        <h4 :id="titleId" :class="titleClass">{{title}}</h4>
        <div>
          <slot name="acttion"></slot>
        </div>
      </div>
    </template>
    <slot name="content"></slot>
  </el-dialog>
</template>

<script setup lang="ts">

import { ref, toRef } from "vue";
// import { ElMessageBox } from 'element-plus'

const props = defineProps<{
  title?: string
}>()

const is_show = ref(false);
const title = toRef(props, "title");

const handleClose = (done: () => void) => {
  done();
  // ElMessageBox.confirm('Are you sure to close this dialog?')
  //   .then(() => {
  //     done()
  //   })
  //   .catch(() => {
  //     // catch error
  //   })
}

const open = () => {
  is_show.value = true;
};

const close = () => {
  is_show.value = false;
};

defineExpose({
  open,
  close,
});
</script>

<style lang="scss">
@import "@/assets/styles/commons/vc-modal.scss";
</style>
