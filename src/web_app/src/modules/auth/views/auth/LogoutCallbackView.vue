<template>
  <div> SSO logout callback ... </div>
</template>

<script setup lang="ts">
import oidcAuth from '@/utils/oidcAuth'
import { onMounted } from 'vue';

onMounted(() => {
  oidcAuth.signoutPopupCallback();
})

</script>