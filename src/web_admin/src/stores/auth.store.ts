import type { AuthRequestLogin } from "@/interfaces/auth.interface";
import authService from "@/services/auth.service";
import { defineStore } from "pinia";

export const useAuthStore = defineStore("useAuthStore", {
  state: () => ({
    loggedIn: false,
    account: <any>{},
  }),

  getters: {
    getAuth: (state) => state.account,
  },

  actions: {
    async login(data: AuthRequestLogin): Promise<boolean> {
      const response = await authService.signIn(data);
      // Login success
      if (response?.data?.access_token) {
        // saved token
        authService.updateLocalStorage(response?.data);

        // Update state
        this.loggedIn = true;

        // Get account info
        this.account = (await authService.getInfo())?.data;
      }
      return this.loggedIn;
    },
    async refresh() {
      // Check localstorage
      const token = localStorage.getItem("auth.access_token");

      if (!token) return this.loggedIn;

      // Call api get me
      this.account = (await authService.getInfo())?.data;
      if (this.account) this.loggedIn = true;
      return this.loggedIn;
    },
    logout() {
      authService.clearLocalStorage();
      this.account = {};
      this.loggedIn = false;
    },
    async setUserInfo(account: any) {
      this.account = account;
      this.loggedIn = account ? true : false;
    },
  },
});
