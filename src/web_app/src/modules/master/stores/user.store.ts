import { defineStore } from 'pinia'

export const userStore = defineStore('userStore', {
  state: () => ({
    gridData: <any>[],
  }),
  getters: {
    getData(state) {
      return state.data
    },
  },
  actions: {
    async getList() {
      console.log('getList')
      this.data = data
    },
    async delete() {
      console.log('delete')
      this.data = data
    },
    async add() {
      console.log('add')
      this.data = data
    },
    async getByKey(key: any) {
      console.log('key', key)
      this.data = data
    },
    async update(data: any) {
      console.log('update')
      this.data = data
    },
  },
})
