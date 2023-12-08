import { fileURLToPath, URL } from "url";

import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
      "@master": fileURLToPath(new URL("./src/modules/master", import.meta.url)),
      "@auth": fileURLToPath(new URL("./src/modules/auth", import.meta.url)),
    },
  },
});
