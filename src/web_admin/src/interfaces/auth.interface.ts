export type AuthResponse = {
  access_token: string;
  refresh_token: string;
};

export type AuthRequestLogin = {
  user_name: string | null;
  password: string | null;
  remember: boolean;
};

export type AuthChangePassword = {
  current_password: string;
  new_password: string;
};
