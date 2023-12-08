import { defineStore } from "pinia";

export const useSystemInfoStore = defineStore("useSystemInfoStore", {
  state: () => ({
    data: <any>[],
  }),
  getters: {
    getData(state) {
      return state.data;
    },
  },
  actions: {
    update(data: any) {
      this.data = data;
    },
  },
});
