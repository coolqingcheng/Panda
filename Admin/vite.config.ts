import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

import * as path from 'path'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  build: {
    outDir: '../Panda.Site/wwwroot/admin'
  },
  server: {
    proxy: {
      "/admin": "http://localhost:5052",
      "/upload": "http://localhost:5052",
      "/img": "http://localhost:5052"
    }
  },
  resolve: {
    alias: {
      "shared": path.resolve(__dirname, "./src/shared")
    }
  }
})
