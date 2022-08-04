import apiClient from "./api.service";
import CONST from "@/services/_constant";
import type { APIResponse } from "@/interfaces/response.interface";
import { useToastStore } from "@/stores/toast.store";

const roleService = {
  async getList(params?: unknown): Promise<APIResponse<any[]>> {
    return await apiClient.get(CONST.API.ROLE.LIST, {
      params: params,
    });
  },

  async detail(id: string): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.ROLE.DETAIL(id));
  },

  async create(data: any) {
    const toastStore = useToastStore();
    return await apiClient
      .post(CONST.API.ROLE.CREATE, data)
      .then((response: any) => {
        toastStore.fromApiResponse(response);
        return response;
      });
  },

  async update(data: any) {
    const toastStore = useToastStore();
    return await apiClient
      .put(CONST.API.ROLE.UPDATE(data.id || ""), data)
      .then((response: any) => {
        toastStore.fromApiResponse(response);
        return response;
      });
  },

  async delete(id: string) {
    const toastStore = useToastStore();
    return await apiClient
      .delete(CONST.API.ROLE.DELETE(id))
      .then((response: any) => {
        toastStore.fromApiResponse(response);
        return response;
      });
  },

  async deleteMulti(ids: string[]) {
    const toastStore = useToastStore();
    return await apiClient
      .delete(CONST.API.ROLE.DELETE_MULTI, { data: ids })
      .then((response: any) => {
        toastStore.fromApiResponse(response);
        return response;
      });
  },
};

export default roleService;
