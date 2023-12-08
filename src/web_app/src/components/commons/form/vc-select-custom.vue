<template>
  <div class="vc-select-custom" ref="dropdownContainer">
    <el-input ref="input" :value="user" class="w-50 m-2 title mb-3 select" @click="toggleDropdown">
      <template #append>
        <el-button :icon="CloseBold" @click="ClearData" />
      </template>
    </el-input>
    <div v-show="isOpen" class="search">
      <vc-input v-model="search" class="w-50 m-2 input-search mb-2" placeholder="Search" />
      <vc-table :height="200" class="table" :datas="datas" :colConfigs="colConfig" :tableConfig="tableConfig" :page="1"
        :loading="false" @dbClick="handleDbRowClick"></vc-table>
    </div>
  </div>
</template>

<script setup lang="ts">
import { CloseBold } from '@element-plus/icons-vue'
import { ref, toRef, watch, watchEffect } from "vue";
const emit = defineEmits([
  "handleSearch"
]);

const props = defineProps<{
  items?: any[];
  colConfig?: [];
  tableConfig?: any[];
  width?: number
}>();
const dropdownContainer = ref<any>(null);
const input = ref<any>(null);
const search = ref<any>();
const datas = toRef(props, "items");
const colConfig = toRef(props, "colConfig");
const tableConfig = toRef(props, "tableConfig");
const isOpen = ref<boolean>(false);
const findMinIdElement = (datas: any) => {
  if (datas.length === 0) {
    return null;
  }

  let minIdElement = datas[0];

  for (let i = 1; i < datas.length; i++) {
    if (datas[i].id < minIdElement.id) {
      minIdElement = datas[i];
    }
  }

  return minIdElement;
}
const user = ref<any>(findMinIdElement(datas.value).name);

watch(search, async (currentValue) => {
  emit("handleSearch", currentValue);
});

const handleDbRowClick = (item: any) => {
  try {
    if (item.name == '') {
      user.value = "undefile"
    } else {
      user.value = item.name
    }
  } catch (err) {
    console.log(err);
  }

};
const toggleDropdown = () => {
  isOpen.value = !isOpen.value;
};
const closeDropdown = () => {
  isOpen.value = false;
};
watchEffect(() => {
  const handleClickOutside = (event: any) => {
    if (!dropdownContainer.value.contains(event.target)) {
      closeDropdown();
    }
  };

  document.addEventListener('click', handleClickOutside);

  return () => {

    document.removeEventListener('click', handleClickOutside);
  };
});

const ClearData = () => {
  user.value = null
}
</script>

<style lang="scss">
@import "@/assets/styles/commons/vc-select-custom.scss";
</style>
