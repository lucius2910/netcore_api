import apiClient from "./api.service";
import CONST from "@/services/_constant";
import type {
  AuthRequestLogin,
  AuthResponse,
  AuthChangePassword,
} from "@/interfaces/auth.interface";
import type { APIResponse } from "@/interfaces/response.interface";
import type { User } from "@/interfaces/user.interface";
import { useToastStore } from "@/stores/toast.store";
const authService = {
  async signIn(data: AuthRequestLogin): Promise<APIResponse<AuthResponse>> {
    return await apiClient.post(CONST.API.AUTH.SIGN_IN, data);
  },

  async getInfo(): Promise<APIResponse<User>> {
    return await apiClient.get(CONST.API.AUTH.GET_INFO);
  },
  async clearLocalStorage() {
    localStorage.removeItem("auth.access_token");
    localStorage.removeItem("auth.refresh_token");
  },
  async updateLocalStorage(data: AuthResponse) {
    localStorage.setItem("auth.access_token", data.access_token);
    localStorage.setItem("auth.refresh_token", data.refresh_token);
  },
  async changePassword(data: AuthChangePassword) {
    const toastStore = useToastStore();
    return await apiClient
      .put(CONST.API.AUTH.CHANGE_PASSWORD, data)
      .then((response: any) => {
        toastStore.fromApiResponse(response);
        return response;
      });
  },
};

export default authService;
