<template>
  <div>SSO callback ...</div>
  <vc-confirm ref="logoutPopup"></vc-confirm>
</template>

<script setup lang="ts">
  import oidcAuth from '@/utils/oidcAuth'
  import { onMounted, ref } from 'vue'
  import { useRouter } from 'vue-router'
  import { useAuthStore } from '@/stores/auth.store'

  const router = useRouter()
  const authStore = useAuthStore()

  const logoutPopup = ref<any>(null)

  onMounted(async () => {
    await oidcAuth.signinRedirectCallback().then(async (res) => {
      authStore.setUserInfo(res)
      router.push({
        name: '/',
      })
    })
  })
</script>
