import { defineStore } from "pinia";

export const useLocalizeStore = defineStore("useLocalizeStore", {
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
