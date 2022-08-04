<template>
  <tr class="tb-row" @dblclick="dbClick">
    <td class="pa-2 tb-col wd-50" v-if="tableConfig.checkbox">
      <vc-checkbox
        class="ml-2"
        @change="onRowSelected"
        v-model="isSelected"
      ></vc-checkbox>
    </td>
    <td class="pa-2 text-center tb-col wd-50" v-if="tableConfig.index">
      {{ index }}
    </td>
    <td
      v-for="(colItem, colIndex) in colConfigs"
      :key="colIndex"
      :class="['pa-2 tb-col', colItem.class]"
    >
      {{ formatData(colItem) }}
    </td>
    <td class="pa-2 tb-col" v-if="tableConfig.action">
      <slot name="action"></slot>
    </td>
  </tr>
</template>

<script setup lang="ts">
import { toRef, ref, watch } from "vue";
import dateTime from "@/utils/dateTime";
import numberUtils from "@/utils/number";
import type { ColConfig, TableConfig } from "@/interfaces/table.interface";
import { DATA_TYPE } from "@/commons/const";

const props = defineProps<{
  data: any;
  index: number;
  selected?: boolean;
  colConfigs: ColConfig[];
  tableConfig: TableConfig;
}>();

const data = toRef(props, "data");
const selected = toRef(props, "selected");
const colConfigs = toRef(props, "colConfigs");
const tableConfig = toRef(props, "tableConfig");
const isSelected = ref<boolean | undefined>(false);

watch(selected, (newVal) => {
  isSelected.value = newVal;
});

const emit = defineEmits(["dbClick", "rowSelected", "delete"]);

const formatData = (colItem: ColConfig) => {
  let cellData = null;
  switch (colItem.type) {
    case DATA_TYPE.DATE:
      cellData = dateTime.formatDate(
        data.value[colItem.text_field ?? colItem.key]
      );
      break;

    case DATA_TYPE.DATE_TIME:
      cellData = dateTime.formatDateTime(
        data.value[colItem.text_field ?? colItem.key]
      );
      break;

    case DATA_TYPE.NUMBER:
      cellData = numberUtils.format(
        data.value[colItem.text_field ?? colItem.key],
        colItem.number_fixed
      );
      break;

    default:
      cellData = data.value[colItem.text_field ?? colItem.key];
      break;
  }
  return cellData;
};

const dbClick = () => {
  emit("dbClick", data.value);
};

const onRowSelected = () => {
  emit("rowSelected", data.value);
};
</script>
