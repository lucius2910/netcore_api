<template>
  <el-date-picker
    :format="dateFormat"
    :value-format="dateFormat"
  ></el-date-picker>
</template>
<script setup lang="ts">
const dateFormat = "YYYY-MM-DD";
</script>
