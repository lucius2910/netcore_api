<template>
  <el-aside :class="['side-bar', { isCollapse: isCollapse }]">
    <el-scrollbar height="100vh">
      <el-menu :collapse="isCollapse">
        <template v-for="(item, itemIndex) in menu" :key="itemIndex">
          <el-menu-item
            v-if="!item.items && item.url"
            :index="`${itemIndex}`"
            @click="goTo(item)"
          >
            <el-icon><Setting /></el-icon>
            <template #title>{{ item.name }}</template>
          </el-menu-item>
          <template v-if="item.items">
            <el-sub-menu :index="`${itemIndex}`">
              <template #title>
                <el-icon><icon-menu /></el-icon>
                <span> {{ item.name }}</span>
              </template>
              <el-menu-item-group>
                <template 
                  v-for="(childItem, childIndex) in item.items"
                  :key="childIndex">
                   <el-menu-item
                      v-if="childItem.url"
                      :index="`${itemIndex}-${childIndex}`"
                      @click="goTo(childItem)">
                      <template #title>{{ childItem.name }}</template>
                    </el-menu-item>
                  </template>
              </el-menu-item-group>
            </el-sub-menu>
          </template>
        </template>
      </el-menu>
    </el-scrollbar>
  </el-aside>
</template>

<script setup lang="ts">
import { toRef, ref, onMounted} from "vue";
import router from "@/router";
import serviceApi from "@/services/function.service";
import { Setting, Menu as IconMenu } from "@element-plus/icons-vue";

const props = defineProps<{
  show?: boolean;
  isCollapse?: boolean;
}>();

const isCollapse = toRef<any, string>(props, "isCollapse");
const menu = ref();

onMounted(async () => {
  menu.value = await serviceApi.getTreeView().then(res => {
    return res.data;
  })
})

const goTo = (item: any) => {
  router.push({
    path: item.url ?? "/404",
  });
};
</script>

<style lang="scss">
@import "@/assets/styles/commons/sidebar";
</style>
