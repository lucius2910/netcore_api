import { UserManager, type UserManagerSettings, User, WebStorageStateStore } from "oidc-client-ts";
import ssoSettings from "@/commons/defines/ssoSettings";

ssoSettings.userStore = new WebStorageStateStore({ store: window.localStorage });
const userManager = new UserManager(ssoSettings as UserManagerSettings);

userManager.events.addUserLoaded(newUser => {
  userManager.storeUser(newUser);
  userManager.getUser().then(usr => {
    // console.log('usr',usr)
  });
});

userManager.events.addAccessTokenExpired(() => {
  localStorage.clear();
  sessionStorage.clear();
  // userManager.signoutPopup();
})

userManager.events.addSilentRenewError(error => {
  console.log(error);
})

export default {

  async getUser() {
    return await userManager.getUser().then((res) => {
      return res;
    });
  },

  async login(): Promise<void> {
    return userManager.signinRedirect();
  },

  async signinRedirectCallback(): Promise<User | null> {
    // Load token
    const data = await userManager.signinRedirectCallback()
    return data;
  },

  signoutPopupCallback(): void {
    userManager.signoutPopupCallback();
  },

  logout(callback: any): void {
    try {
      userManager.signoutPopup({
        popupWindowFeatures : {
          width: 400,
          height: 150,
          resizable: false,
          scrollbars: false,
          location: false,
          toolbar: false,
          menubar: false,
        }
      }).then((res) => {
        callback();
      });

      userManager.clearStaleState()
    } catch(error){
      console.log(error);
    }
    
  }
}