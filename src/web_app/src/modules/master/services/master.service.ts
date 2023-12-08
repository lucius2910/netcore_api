import apiClient from "@/services/api.service";
import CONST from "@/services/_constant";
import type { APIResponse } from "@/interfaces/response.interface";
import { useToastStore } from "@/stores/toast.store";

const serviceApi = {
  async getList(params?: unknown): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.MASTER.LIST, {
      params: params,
    });
  },

  async detail(id: string): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.MASTER.DETAIL(id));
  },

  async getByType(type: string): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.MASTER.GET_BY_TYPE, {
      params: { type: type, page: 1, size: 1000 },
    });
  },

  async create(data: any) {
    const toastStore = useToastStore();
    return await apiClient
      .post(CONST.API.MASTER.CREATE, data)
      .then((response) => {
        toastStore.fromApiResponse(response);
        return response.data;
      });
  },

  async update(data: any) {
    const toastStore = useToastStore();
    return await apiClient
      .put(CONST.API.MASTER.UPDATE(data.id || ""), data)
      .then((response) => {
        toastStore.fromApiResponse(response);
        return response.data;
      });
  },

  async delete(id: string | string[]) {
    const toastStore = useToastStore();
    return await apiClient
      .delete(CONST.API.MASTER.DELETE(id))
      .then((response) => {
        toastStore.fromApiResponse(response);
        return response.data;
      });
  },

  async deleteMulti(ids: string[]) {
    const toastStore = useToastStore();
    return await apiClient
      .delete(CONST.API.MASTER.DELETE_MULTI, { data: ids })
      .then((response: any) => {
        toastStore.fromApiResponse(response);
        return response;
      });
  },
};

export default serviceApi;
