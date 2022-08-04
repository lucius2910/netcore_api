<template>
  <el-aside :class="['side-bar', { isCollapse: isCollapse }]">
    <el-scrollbar height="100vh">
      <el-menu :collapse="isCollapse">
        <template v-for="(item, itemIndex) in menus" :key="itemIndex">
          <el-menu-item
            v-if="!item.items"
            :index="`${itemIndex}`"
            @click="goTo(item)"
          >
            <el-icon><Setting /></el-icon>
            <template #title>{{ item.text }}</template>
          </el-menu-item>
          <template v-else>
            <el-sub-menu :index="`${itemIndex}`">
              <template #title>
                <el-icon><icon-menu /></el-icon>
                <span> {{ item.text }}</span>
              </template>
              <el-menu-item-group>
                <el-menu-item
                  v-for="(childItem, childIndex) in item.items"
                  :key="childIndex"
                  :index="`${itemIndex}-${childIndex}`"
                  @click="goTo(childItem)"
                >
                  <template #title>{{ childItem.text }}</template>
                </el-menu-item>
              </el-menu-item-group>
            </el-sub-menu>
          </template>
        </template>
      </el-menu>
    </el-scrollbar>
  </el-aside>
</template>

<script setup lang="ts">
import { toRef } from "vue";
import router from "@/router";
import type { Menu } from "@/interfaces/menu.interface";
import menus from "@/commons/defines/menu";
import { Setting, Menu as IconMenu } from "@element-plus/icons-vue";

const props = defineProps<{
  show?: boolean;
  isCollapse?: boolean;
}>();

const isCollapse = toRef<any, string>(props, "isCollapse");

const goTo = (item: Menu) => {
  router.push({
    name: item.routerName ?? "NotFound",
  });
};
</script>

<style lang="scss">
@import "@/assets/styles/commons/sidebar";
</style>
