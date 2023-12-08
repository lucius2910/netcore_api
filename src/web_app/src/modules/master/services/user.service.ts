import apiClient from '@/services/api.service'
import CONST from '@/services/_constant'
import type { APIResponse } from '@/interfaces/response.interface'
import { useToastStore } from '@/stores/toast.store'

const userService = {
  async getList(params?: unknown): Promise<APIResponse<any[]>> {
    return await apiClient.get(CONST.API.USER.LIST, {
      params: params,
    })
  },

  async detail(id: string): Promise<APIResponse<any>> {
    return await apiClient.get(CONST.API.USER.DETAIL(id))
  },

  async create(data: any) {
    const toastStore = useToastStore()
    return await apiClient
      .post(CONST.API.USER.CREATE, data)
      .then((response: any) => {
        toastStore.fromApiResponse(response)
        return response
      })
  },

  async update(data: any) {
    const toastStore = useToastStore()
    return await apiClient
      .put(CONST.API.USER.UPDATE(data.id || ''), data)
      .then((response: any) => {
        toastStore.fromApiResponse(response)
        return response
      })
  },

  async delete(id: string) {
    const toastStore = useToastStore()
    return await apiClient
      .delete(CONST.API.USER.DELETE(id))
      .then((response: any) => {
        toastStore.fromApiResponse(response)
        return response
      })
  },
}

export default userService
