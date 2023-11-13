<template>
  <vc-side-bar-left>
    <div class="container-finance mb-8">
      <div class="headline">
        <h2>パスワード変更</h2>
      </div>
      <div class="margin-bottom-0">
        <p>
          <small>
            パスワードの変更またはリセットができます。パスワードを定期的に変更し、以前のパ</small>
          <small>スワードを繰り返しお使いにならないことをおすすめします。</small>
          <vc-notify></vc-notify>
        </p>
      </div>
      <div class="changepass-content" lazy-validation>
        <el-form ref="changeForm" :model="passwordValidate" :rules="rules" :label-position="'top'">
          <vc-input-group required prop="olPass" label="現在のパスワード" class="form-label">
            <vc-input class="text-input" v-model.trim="passwordValidate.olPass" placeholder="現在のパスワード" type="password"
              autocomplete="off" show-password />
          </vc-input-group>

          <vc-input-group required prop="newPass" label="新しいパスワード" class="form-label">
            <vc-input class="text-input" v-model.trim="passwordValidate.newPass" placeholder="新しいパスワード" type="password"
              autocomplete="off" show-password />
          </vc-input-group>

          <vc-input-group required prop="cfPass" label="新しいパスワードを再入力" class="form-label">
            <vc-input class="text-input" v-model.trim="passwordValidate.cfPass" placeholder="新しいパスワードを再入力" type="password"
              autocomplete="off" show-password />
          </vc-input-group>

          <vc-button type="primary" class="card-footer-col-btn" @click="handleChangePass(changeForm)">
            変更を保存
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

const passwordValidate = reactive({
  olPass: '',
  newPass: '',
  cfPass: '',
})

const checkSamePassword = (rule: any, value: any, callback: any) => {
  if (value !== passwordValidate.newPass) {
    callback(
      new Error('確認用パスワードが新しいパスワードと同じではありません。')
    )
  } else {
    callback()
  }
}

const checkPasswrodInfection = (rule: any, value: any, callback: any) => {
  if (value === passwordValidate.olPass) {
    callback(new Error('新しいパスワードは現在のパスワードと同じです。'))
  } else {
    callback()
  }
}

const rules = reactive({
  cfPass: [
    {
      required: true,
      validator: validate.required,
      trigger: 'blur',
      full_field: '確認用パスワード',
    },
    { validator: validate.alphabetAndSymbolRule, trigger: 'blur' },
    { validator: checkSamePassword, trigger: 'blur' },
  ],
  newPass: [
    {
      required: true,
      validator: validate.required,
      trigger: 'blur',
      full_field: '新しいパスワード',
    },
    { validator: validate.alphabetAndSymbolRule, trigger: 'blur' },
    { validator: checkPasswrodInfection, trigger: 'blur' },
  ],
  olPass: [
    {
      required: true,
      validator: validate.required,
      trigger: 'blur',
      full_field: '旧パスワード',
    },
    { validator: validate.alphabetAndSymbolRule, trigger: 'blur' },
  ],
})

const changePassword = async () => {
  const data = {
    id: account.value.profile.sub,
    password: passwordValidate.olPass,
    passwordNew: passwordValidate.newPass,
  }
  if (data) {
    await authService.changePassword(data)
  }
}
const handleChangePass = async (formEl: FormInstance | undefined) => {
  if (!formEl) return
  await formEl.validate((valid, fields) => {
    if (valid) {
      changePassword()
    }
  })
}
</script>

<style lang="scss" scoped>
@import '@/assets/styles/page/account/changePasswordAd.scss';
</style>
<style>
.form-label>.el-form-item__label::after {
  content: '*' !important;
  color: red !important;
  margin-right: 4px !important;
}

.form-label>.el-form-item__label::before {
  content: '' !important;
}
</style>
