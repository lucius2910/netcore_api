<template>
  <th @click="onSort" :class="['pa-2', item.sort ?? '']">
    <span>
      {{ item.title }}
      <v-icon size="x-small" dark right v-if="item.sort" class="icon-sort">
        mdi-arrow-up-thick
      </v-icon>
    </span>
  </th>
</template>

<script setup lang="ts">
import { toRef } from "vue";
import type { ColConfig } from "@/interfaces/table.interface";

const props = defineProps<{
  item: ColConfig;
}>();

const item = toRef(props, "item");
const emit = defineEmits(["sorted"]);

const onSort = () => {
  if (item.value.is_sort) emit("sorted");
};
</script>
