import type { WebStorageStateStore } from "oidc-client-ts";

export default {
  userStore: {} as WebStorageStateStore,
  authority: `${import.meta.env.VITE_API_URL}`,
  client_id: import.meta.env.VITE_AUTH_CLIENT_ID,
  client_secret: import.meta.env.VITE_AUTH_CLIENT_SECRET,
  redirect_uri: `${import.meta.env.VITE_APP_URL}/callback`,
  response_type: "code",
  response_mode: "query",
  automaticSilentRenew: true,
  monitorSession: true,
  silent_redirect_uri: `${import.meta.env.VITE_APP_URL}/silent-renew`,
  scope: "openid profile phone",
  post_logout_redirect_uri: `${import.meta.env.VITE_APP_URL}/logout-callback`,
  loadUserInfo: true,
  metadata: {
    issuer: `${import.meta.env.VITE_AUTH_SSO_URL}`,
    jwks_uri: `${import.meta.env.VITE_AUTH_SSO_URL}/.well-known/openid-configuration/jwks`,
    authorization_endpoint: `${import.meta.env.VITE_AUTH_SSO_URL}/connect/authorize`,
    end_session_endpoint: `${import.meta.env.VITE_AUTH_SSO_URL}/connect/endsession`,
    token_endpoint: `${import.meta.env.VITE_AUTH_SSO_URL}/connect/token`,
    userinfo_endpoint: `${import.meta.env.VITE_AUTH_SSO_URL}/connect/userinfo`
  }
}