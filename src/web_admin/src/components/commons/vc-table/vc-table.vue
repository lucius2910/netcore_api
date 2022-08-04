<template>
  <!-- <vc-card class="pb-3"> -->
  <div>
    <el-table
      border
      class="vc-table"
      style="width: 100%"
      :height="height ?? '500px'"
      :data="datas"
      v-loading="loading"
    >
      <el-table-column
        type="selection"
        width="50"
        v-if="tableConfig.checkbox"
      />
      <el-table-column type="index" v-if="tableConfig.index" />
      <template v-for="(col, index) in colConfigs" :key="index">
        <el-table-column :prop="col.key" :label="col.title" />
      </template>
      <!-- :label="tl('Common', 'Action')" -->
      <el-table-column width="70" v-if="tableConfig.action">
        <template #default="scope">
          <slot name="action" :scope="scope"></slot>
        </template>
      </el-table-column>
    </el-table>
    <!-- PAGING -->
    <div class="table-footer mt-3" v-if="tableConfig?.showPaging">
      <vc-pagination
        :pageConfig="pageConfig"
        @changed="onPageChanged"
      ></vc-pagination>
    </div>
    <!-- PAGING -->

    <!-- LOADING -->
    <!-- <v-overlay
      contained
      :model-value="loading"
      class="align-center justify-center"
    >
      <v-progress-circular indeterminate size="30"></v-progress-circular>
    </v-overlay> -->
    <!-- LOADING -->
    <!-- </vc-card> -->
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
  "editClick",
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
const editClick = (item: any) => {
  emit("editClick", item);
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
// eslint-disable-next-line @typescript-eslint/no-unused-vars
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

/**
 * Return selected
 */
// eslint-disable-next-line @typescript-eslint/no-unused-vars
const isSelected = (item: any) => {
  return rowSelected.value.includes(item);
};

/**
 * Calculate colSpan by config
 */
// eslint-disable-next-line @typescript-eslint/no-unused-vars
const colSpan = () => {
  return (
    colSettings.value.length +
    (tableConfig.value.action ? 1 : 0) +
    (tableConfig.value.checkbox ? 1 : 0) +
    (tableConfig.value.index ? 1 : 0)
  );
};

/**
 * Calculate row index width paging
 */
// eslint-disable-next-line @typescript-eslint/no-unused-vars
const rowPagedIndex = (index: number) => {
  return (pageConfig.value.page - 1) * pageConfig.value.size + index + 1;
};
</script>
<style lang="scss">
@import "@/assets/styles/commons/vc-table";
</style>
