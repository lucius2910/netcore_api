<template>
  <div class="container-income margin-bottom-30" style="margin-top: 10px">
    <div class="headline">
      <h2>
        <el-icon class="headline-icon rounded-x">
          <Plus class="headline-icon-svg" />
        </el-icon>
        収入
      </h2>
    </div>
    <div class="margin-bottom-0">
      <p>
        <small>団体の財布・銀行口座にお金が入ったら入力します。</small>
      </p>
    </div>
    <el-form class="income-form">
      <el-card shadow="never" class="income-form__card">
        <vc-row :gutter="12" class="income-form__card-row">
          <vc-col
            :span="24"
            :md="{ span: 4 }"
            class="income-form__card-row-col d-flex"
          >
            <label class="label">現金/銀行口座</label>
          </vc-col>
          <vc-col :span="24" :md="{ span: 8 }">
            <el-radio-group v-model="radio">
              <el-radio :label="1">現金</el-radio>
              <el-radio :label="2">銀行口座</el-radio>
            </el-radio-group>
          </vc-col>
        </vc-row>
        <vc-row v-if="radio == 2" :gutter="12" class="income-form__card-row">
          <vc-col
            :span="24"
            :md="{ span: 4 }"
            class="income-form__card-row-col d-flex"
          >
            <label class="label col col-2"
              >銀行口座
              <span class="label-color">*</span>
            </label>
          </vc-col>
          <vc-col :span="24" :md="{ span: 8 }">
            <vc-select
              :items="optionsNumbers"
              size="large"
              :modelValue="object.optionNumber"
              @selected="onItemSelected"
            />
          </vc-col>
        </vc-row>
        <vc-row :gutter="12" class="income-form__card-row">
          <vc-col
            :span="24"
            :md="{ span: 4 }"
            class="income-form__card-row-col d-flex"
          >
            <label class="label col col-2"
              >日付
              <span class="label-color">*</span>
            </label>
          </vc-col>
          <vc-col :span="24" :md="{ span: 8 }">
            <vc-input-date size="large" v-model="object.date" />
          </vc-col>
        </vc-row>
        <vc-row :gutter="12" class="income-form__card-row">
          <vc-col
            :span="24"
            :md="{ span: 4 }"
            class="income-form__card-row-col d-flex"
          >
            <label class="label col col-2">
              金額
              <span class="label-color">*</span>
            </label>
          </vc-col>
          <vc-col :span="24" :md="{ span: 8 }">
            <el-input-number
              v-model="object.number"
              controls-position="right"
              size="large"
              @change="handleChange"
            />
          </vc-col>
        </vc-row>
        <vc-row :gutter="12" class="income-form__card-row">
          <vc-col
            :span="24"
            :md="{ span: 4 }"
            class="income-form__card-row-col d-flex"
          >
            <label class="label col col-2">
              摘要
              <span class="label-color">*</span>
            </label>
          </vc-col>
          <vc-col :span="24" :md="{ span: 8 }">
            <vc-input size="large" v-model="object.textInput" />
          </vc-col>
        </vc-row>
        <vc-row :gutter="12" class="income-form__card-row">
          <vc-col
            :span="24"
            :md="{ span: 4 }"
            class="income-form__card-row-col d-flex"
          >
            <label class="label col col-2">科目</label>
          </vc-col>
          <vc-col :span="24" :md="{ span: 8 }">
            <el-select v-model="object.optionValue" size="large">
              <el-option-group
                v-for="group in dataOptions"
                :key="group.label"
                :label="group.label"
              >
                <el-option
                  v-for="item in group.options"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-option-group>
            </el-select>
          </vc-col>
        </vc-row>
      </el-card>
      <div class="card-footer">
        <vc-row :gutter="12">
          <vc-col :span="24" :md="{ span: 4 }"></vc-col>
          <vc-col :span="24" :md="{ span: 8 }" class="card-footer-col">
            <vc-button
              type="primary"
              class="card-footer-col-btn"
              :icon="EditPen"
              @click="handleSubmit"
            >
              登 録
            </vc-button>
          </vc-col>
        </vc-row>
      </div>
    </el-form>
  </div>
</template>

<script setup lang="ts">
  import { Plus, EditPen } from '@element-plus/icons-vue'
  import { ref, reactive } from 'vue'
  import { optionsNumbers, dataOptions } from '@/commons/dataExample/income'

  const radio = ref(1)

  const object = reactive({
    date: Date(),
    number: 0,
    textInput: '',
    optionValue: '4',
    optionNumber: '999999',
  })

  const handleSubmit = () => {
    console.log(object)
  }

  const handleChange = (value: number) => {
    object.number = value
  }
  const onItemSelected = (item: any) => {
    object.optionNumber = item.value
  }
</script>

<style lang="scss" scoped>
  @import '@/assets/styles/page/inCome';
</style>

<style>
  .el-input--large .el-input__inner {
    text-align: left;
  }
</style>
