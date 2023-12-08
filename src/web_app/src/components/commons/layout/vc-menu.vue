<template>
  <el-menu>
    <template v-for="(item, itemIndex) in menu" :key="itemIndex">
      <el-menu-item v-if="!item.items && item.path" :index="`${itemIndex}`" @click="goTo(item)">
        <el-icon>
          <component :is="item.icon"></component>
        </el-icon>
        <template #title>{{ item.text }}</template>
      </el-menu-item>
      <template v-if="item.items">
        <el-sub-menu :index="`${itemIndex}`">
          <template #title>
            <el-icon><component :is="item.icon"></component></el-icon>
            <span> {{ item.text }}</span>
          </template>
          <el-menu-item-group>
            <template v-for="(childItem, childIndex) in item.items" :key="childIndex">
              <el-menu-item v-if="childItem.path" :index="`${itemIndex}-${childIndex}`" @click="goTo(childItem)">
                <template #title>{{ childItem.text }}</template>
              </el-menu-item>
            </template>
          </el-menu-item-group>
        </el-sub-menu>
      </template>
    </template>
  </el-menu>
</template>

<script setup lang="ts">
import { useRouter } from "vue-router";
import menu from "@/commons/defines/menu";

const router = useRouter();

const goTo = (item: any) => {
  router.push({
    path: item.path ?? "/404",
  });
};

</script>
