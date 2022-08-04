<template>
  <el-row class="vc-pagination">
    <el-col :span="24">
      <el-pagination
        small
        background
        v-model:currentPage="meta.page"
        v-model:page-size="meta.size"
        :page-sizes="[10, 15, 30, 50]"
        layout="total, sizes, prev, pager, next, jumper"
        :total="totalPage"
        @size-change="onSizeChange"
        @current-change="onPageChange"
      >
      </el-pagination>
    </el-col>
  </el-row>
</template>

<script setup lang="ts">
import type { MetaResponse } from "@/interfaces/response.interface";
import { toRef, ref, watch } from "vue";

const props = defineProps<{
  pageConfig: MetaResponse;
}>();

const meta = toRef(props, "pageConfig");
const startPage = ref(0);
const endPage = ref(0);
const totalItem = ref(0);
const totalPage = ref(0);

watch(meta, (newVal) => {
  const page = newVal.page ?? 1;
  const size = newVal.size ?? 10;
  const total = newVal.total ?? 0;
  startPage.value = size * (page - 1) + 1;
  endPage.value = total < size * page ? total : size * page;
  totalPage.value = Math.ceil(total / (size ?? 1));
  totalItem.value = total;
});

const emit = defineEmits(["pageChanged", "sizeChanged"]);

const onPageChange = () => {
  emit("pageChanged", meta.value.page);
};

const onSizeChange = () => {
  emit("sizeChanged", meta.value.page);
};
</script>

<style lang="scss">
@import "@/assets/styles/commons/vc-pagination.scss";
</style>
