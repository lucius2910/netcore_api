<template>
  <el-select v-model="modelValue" class="m-2" @change="onSelected">
    <el-option
      v-for="item in datas"
      :key="item[fieldValue]"
      :label="item[fieldTittle]"
      :value="item[fieldValue]"
    />
  </el-select>
</template>

<script setup lang="ts">
import { ref, toRef, watch, useAttrs } from "vue";
import tl from "@/utils/locallize";

const props = defineProps<{
  items: any[];
  rules?: [];
  modelValue: any;
}>();

const selectedItem = ref<any>({});
const datas = toRef(props, "items");
const modelValue = toRef(props, "modelValue");

type ObjectKeys = keyof typeof datas.value[0];

const attrs = useAttrs();
const dataText = ref<any>(null);
const fieldValue = (attrs["fieldValue"] ?? "value") as ObjectKeys;
const fieldTittle = (attrs["fieldTittle"] ?? "name") as ObjectKeys;

const emit = defineEmits(["update:modelValue", "selected"]);

watch(modelValue, (newVal) => {
  selectedItem.value = getItemFromValue(newVal);
  emit("selected", selectedItem.value);
});

watch(selectedItem, (newVal) => {
  dataText.value = newVal ? getValue(newVal, fieldTittle) : null;
});

const onSelected = (selected: any) => {
  selectedItem.value = getItemFromValue(selected);
  emit("update:modelValue", selected);
  emit("selected", selectedItem.value);
};

const getItemFromValue = (value: any) => {
  return datas.value.find((x: any) => x[fieldValue] == value);
};

const getValue = (obj: any, key: any) => {
  return obj ? obj[key] : null;
};
</script>

<style lang="scss">
@import "@/assets/styles/commons/vc-select.scss";
</style>
