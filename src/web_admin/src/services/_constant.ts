const CONST = {
  API: {
    // Auth
    AUTH: {
      SIGN_IN: "auth/login",
      GET_INFO: "auth/info",
      REFRESH_TOKEN: "auth/refresh-token",
      CHANGE_PASSWORD: "auth/change-password",
    },

    // User
    USER: {
      LIST: "user",
      CREATE: "user/create",
      DETAIL: (id: string) => `user/${id}`,
      UPDATE: (id: string) => `user/update/${id}`,
      DELETE: (id: string | string[]) => `user/delete/${id}`,
      RESET_PASSWORD: (id: string) => `user/${id}/reset_password`,
    },

    // User
    ROLE: {
      LIST: "role/get_list",
      CREATE: "role",
      DETAIL: (id: string) => `role/${id}`,
      UPDATE: (id: string) => `role/${id}`,
      DELETE: (id: string | string[]) => `role/${id}`,
      DELETE_MULTI: "role/delete-multi",
    },

    // Function
    FUNCTION: {
      LIST: "/function/get_list",
      CREATE: "/function",
      MENU: "/function/menu",
      DETAIL: (id: string) => `function/${id}`,
      CHANGE_STATUS: (id: string) => `function/${id}/avtived`,
      UPDATE: (id: string) => `function/${id}`,
      DELETE: (id: string | string[]) => `function/${id}`,
      DELETE_MULTI: "function/delete-multi",
    },

    // Unit
    UNIT: {
      LIST: "/unit",
      CREATE: "/unit",
      MENU: "/unit/menu",
      DETAIL: (id: string) => `unit/${id}`,
      CHANGE_STATUS: (id: string) => `unit/${id}/avtived`,
      UPDATE: (id: string) => `unit/${id}`,
      DELETE: (id: string | string[]) => `unit/${id}`,
      DELETE_MULTI: "unit/delete-multi",
    },

    // Resource
    RESOURCE: {
      LIST: "/resource",
      CREATE: "/resource",
      LIST_SCREEN: `resource/screens`,
      SEARCH_RESOURCE: `resource/search_resource`,
      DETAIL: (id: string) => `resource/${id}`,
      UPDATE: (id: string) => `resource/${id}`,
      DELETE: (id: string | string[]) => `resource/${id}`,
    },

    // Master
    MASTER: {
      LIST: "/master_code",
      GET_BY_TYPE: "/master_code/get_by_type",
      CREATE: "/master_code",
      DETAIL: (id: string) => `master_code/${id}`,
      UPDATE: (id: string) => `master_code/${id}`,
      DELETE: (id: string | string[]) => `master_code/${id}`,
      DELETE_MULTI: "master_code/delete-multi",
    },

    // Inventory
    INVENTORY: {
      LIST: "/inventory",
      CREATE: "/inventory",
      DETAIL: (id: string) => `inventory/${id}`,
      UPDATE: (id: string) => `inventory/${id}`,
      DELETE: (id: string | string[]) => `inventory/${id}`,
      DELETE_MULTI: "inventory/delete-multi",
    },
    // Tax
    TAX: {
      LIST: "/tax",
      CREATE: "/tax",
      DETAIL: (id: string) => `tax/${id}`,
      UPDATE: (id: string) => `tax/${id}`,
      DELETE: (id: string | string[]) => `tax/${id}`,
      DELETE_MULTI: "tax/delete-multi",
    },
  },
};

export default CONST;
