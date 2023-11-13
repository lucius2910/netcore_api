<template>
  <vc-side-bar-left>
    <div class="container-finance mb-8">
      <div class="headline">
        <h2>メールアドレスの変更</h2>
      </div>
      <div class="margin-bottom-0">
        <p>
          <small>
            アカウントに登録されているメールアドレスを最新の状態に保つことは、アカウントの</small>
          <small>セキュリティを守るための重要な作業です。</small>
        </p>
        <p>
          <small>
            メールアドレスを更新すると、変更の確認を求めるメールがHonkiの会計から送信され</small>
          <small>ます。受信したメールの [確認する]
            ボタンをクリックしてください。そのメールアドレ</small>
          <small>スが本人のものであるということがHonkiの会計に通知されます。</small>
          <vc-notify></vc-notify>
        </p>
      </div>
      <div class="changepass-content">
        <el-form ref="changeForm" :model="formValidate" :rules="rules" label-width="200px" :label-position="'left'">
          <vc-input-group required prop="curPass" label="パスワード" class="groupinput form-label">
            <vc-input class="text-input" v-model.trim="formValidate.curPass" placeholder="パスワード" type="password"
              autocomplete="off" show-password />
          </vc-input-group>

          <vc-input-group required prop="newEmail" label="新しいメールアドレ" class="groupinput form-label">
            <vc-input class="text-input" v-model.trim="formValidate.newEmail" placeholder="新しいメールアドレ"
              autocomplete="off" />
          </vc-input-group>

          <vc-button type="primary" class="card-footer-col-btn" @click="handleChangeEmail(changeForm)">
            確認用メールの送信
          </vc-button>
        </el-form>
      </div>
    </div>
  </vc-side-bar-left>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue'
import validate from '@/utils/validate_elp'
import type { FormInstance } from 'element-plus'
import authService from '@/services/auth.service'
import { useAuthStore } from '@/stores/auth.store'
import { storeToRefs } from 'pinia'

const changeForm = ref<FormInstance>()
const authStore = useAuthStore()
const { account } = storeToRefs(authStore)

const formValidate = reactive({
  curPass: '',
  newEmail: '',
})

const rules = reactive({
  curPass: [
    {
      required: true,
      validator: validate.required,
      trigger: 'blur',
      full_field: 'パスワード ',
    },
    { validator: validate.alphabetAndSymbolRule, trigger: 'blur' },
  ],
  newEmail: [
    {
      required: true,
      validator: validate.required,
      trigger: 'blur',
      full_field: 'メールアドレス ',
    },
    { validator: validate.emailRule, trigger: 'blur' },
  ],
})

const changeEmail = async () => {
  const currentURL = window.location.origin
  const data = {
    id: account.value.profile.sub,
    password: formValidate.curPass,
    email: formValidate.newEmail,
    UrlLogin: currentURL,
  }
  if (data) {
    await authService.changeEmail(data)
  }
}

const handleChangeEmail = async (formEl: FormInstance | undefined) => {
  if (!formEl) return

  await formEl.validate((valid, fields) => {
    if (valid) {
      changeEmail()
    }
  })
}
</script>

<style lang="scss" scoped>
@import '@/assets/styles/page/account/changePasswordAd.scss';
</style>
<style>
.form-label>.el-form-item__label::after {
  content: '*';
  color: var(--el-color-danger);
  margin-right: 4px;
}

.form-label>.el-form-item__label::before {
  content: '' !important;
}
</style>
