export type User = {
  id?: string;
  code: string;
  user_name?: string;
  password?: string;
  full_name?: string;
  date_of_birth?: any;
  phone?: string;
  role?: number;
  role_name: string | undefined;
  gender?: number;
  gender_name: string | undefined;
};
