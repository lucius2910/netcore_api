import { defineStore } from "pinia";
import type { Toast } from "@/interfaces/toast.interface";
import _guid from "@/utils/guidHelper";

export const useToastStore = defineStore("useToastStore", {
  state: () => ({
    items: <Toast[]>[],
  }),
  getters: {
    getItems(state) {
      return state.items;
    },
  },
  actions: {
    push(item: Toast) {
      item.is_show = true;
      item._id = _guid.newGuid();
      this.items.push(item);
    },

    fromApiResponse(response: any) {
      const item = {
        is_show: true,
        _id: _guid.newGuid(),
        type: response.code == 0 ? "success" : "error",
        message:
          response.errors?.length > 0
            ? response.errors.map((x: any) => x.message).join(", ")
            : response.message,
      };
      this.items.push(item);
    },

    dissmisNotify(id: string) {
      this.items.map(
        (obj) => (obj.is_show = obj._id === id ? false : obj.is_show)
      );

      setTimeout(() => {
        this.pop();
      }, 4000);
    },

    pop() {
      if (this.items.filter((obj) => obj.is_show)?.length <= 0) this.items = [];
    },
  },
});
