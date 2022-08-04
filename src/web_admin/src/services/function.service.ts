import apiClient from "./api.service";
import CONST from "@/services/_constant";
import type { APIResponse } from "@/interfaces/response.interface";
import { useToastStore } from "@/stores/toast.store";

const serviceApi = {
  async getList(params?: unknown): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.FUNCTION.LIST, {
      params: params,
    });
  },

  async detail(id: string): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.FUNCTION.DETAIL(id));
  },

  async getTreeView(): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.FUNCTION.MENU);
  },

  async create(data: any) {
    const toastStore = useToastStore();
    return await apiClient
      .post(CONST.API.FUNCTION.CREATE, data)
      .then((response: any) => {
        toastStore.fromApiResponse(response);
        return response;
      });
  },

  async update(data: any) {
    const toastStore = useToastStore();
    return await apiClient
      .put(CONST.API.FUNCTION.UPDATE(data.id || ""), data)
      .then((response) => {
        toastStore.fromApiResponse(response);
        return response.data;
      });
  },

  async delete(id: string | string[]) {
    const toastStore = useToastStore();
    return await apiClient
      .delete(CONST.API.FUNCTION.DELETE(id))
      .then((response) => {
        toastStore.fromApiResponse(response);
        return response.data;
      });
  },

  async deleteMulti(ids: string[]) {
    const toastStore = useToastStore();
    return await apiClient
      .delete(CONST.API.FUNCTION.DELETE_MULTI, { data: ids })
      .then((response: any) => {
        toastStore.fromApiResponse(response);
        return response;
      });
  },
};

export default serviceApi;
