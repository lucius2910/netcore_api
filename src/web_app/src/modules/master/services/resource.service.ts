import apiClient from "@/services/api.service";
import CONST from "@/services/_constant";
import type { APIResponse } from "@/interfaces/response.interface";

const serviceApi = {
  async getList(params?: unknown): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.RESOURCE.LIST, {
      params: params,
    });
  },

  async getListScreen(params: any): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.RESOURCE.LIST_SCREEN, {
      params: params,
    });
  },

  async detail(id: string): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.RESOURCE.DETAIL(id));
  },

  async create(data: any) {
    return await apiClient
      .post(CONST.API.RESOURCE.CREATE, data)
      .then((response) => {
        return response.data;
      });
  },

  async update(data: any) {
    return await apiClient
      .put(CONST.API.RESOURCE.UPDATE(data.id || ""), data)
      .then((response) => {
        return response.data;
      });
  },

  async delete(id: string | string[]) {
    return await apiClient
      .delete(CONST.API.RESOURCE.DELETE(id))
      .then((response) => {
        return response.data;
      });
  },
};

export default serviceApi;
