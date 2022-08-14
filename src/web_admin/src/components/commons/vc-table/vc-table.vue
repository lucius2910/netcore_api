<template>
  <!-- <vc-card class="pb-3"> -->
  <div class="vc-table">
    <el-table
      style="width: 100%"
      :height="height ?? '500px'"
      :data="datas"
      v-loading="loading"
      stripe
    >
      <!-- CHECK BOX -->
      <el-table-column
        type="selection"
        width="50"
        v-if="tableConfig.checkbox"
      />

      <!-- INDEX -->
      <el-table-column type="index" v-if="tableConfig.index" />

      <!-- DATA -->
      <template v-for="(col, index) in colConfigs" :key="index" >
        <el-table-column :prop="col.key" :label="col.title"/>
      </template>

      <!-- ACTIONS -->
      <el-table-column width="70" v-if="tableConfig.action">
        <template #default="scope">
          <slot name="action" :data="scope.row" :scope="scope"></slot>
        </template>
      </el-table-column>
    </el-table>

    <!-- PAGING -->
    <div class="table-footer pa-2 pt-3" v-if="tableConfig?.showPaging">
      <vc-pagination
        :pageConfig="pageConfig"
        @changed="onPageChanged"
      ></vc-pagination>
    </div>
    <!-- PAGING -->
  </div>
</template>

<script setup lang="ts">
import { ref, toRefs, onMounted } from "vue";
import type { MetaResponse } from "@/interfaces/response.interface";
import type { ColConfig, TableConfig } from "@/interfaces/table.interface";
import VcPagination from "./vc-pagination.vue";
import tl from "@/utils/locallize";

const props = defineProps<{
  datas: any[];
  colConfigs: ColConfig[];
  tableConfig: TableConfig;
  page: MetaResponse;
  height?: string;
  loading: boolean;
}>();

const rowSelected = ref<any[]>([]);
const allSelected = ref(false);
const sortBy = ref<any>("");
const {
  datas,
  tableConfig,
  colConfigs,
  page: pageConfig,
  height,
  loading,
} = toRefs(props);

const emit = defineEmits([
  "dbClick",
  "onDelete",
  "sorted",
  "rowSelected",
  "pageChanged",
]);

const colSettings = ref<ColConfig[]>([]);

onMounted(() => {
  colSettings.value = colConfigs.value;
});

const onPageChanged = (page: any) => {
  emit("pageChanged", page);
};

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const dbClick = (item: any) => {
  if (tableConfig.value.dbClick) emit("dbClick", item);
};

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const onSelectAll = () => {
  rowSelected.value = [];
  if (allSelected.value) {
    Object.assign(rowSelected.value, datas.value);
  }
  emit("rowSelected", rowSelected.value);
};

/**
 * Event clicked row check box
 * Emit rowSelected
 */
// eslint-disable-next-line @typescript-eslint/no-unused-vars
const onRowSelected = (item: any) => {
  const indexCheck = rowSelected.value.indexOf(item);
  if (indexCheck == -1) {
    rowSelected.value.push(item);
  } else {
    rowSelected.value.splice(indexCheck, 1);
  }

  allSelected.value = rowSelected.value.length == datas.value.length;
  emit("rowSelected", rowSelected.value);
};

/**
 * Event click sort table header
 * Emit sorted
 */
const onSort = (index: number) => {
  colSettings.value = colSettings.value.map((el, i) => {
    if (i == index) {
      if (!el.sort) {
        el.sort = "asc";
      } else {
        el.sort = el.sort == "asc" ? "desc" : null;
      }
    } else {
      el.sort = null;
    }
    return el;
  });
  sortBy.value = colSettings.value
    .filter((item) => item.sort)
    .map((item) => `${item.key_field ?? item.key}.${item.sort}`)
    .join(",");
  emit("sorted", sortBy.value);
};

</script>
<style lang="scss">
@import "@/assets/styles/commons/vc-table";
</style>
