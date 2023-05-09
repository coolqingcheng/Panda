import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from "path";

// https://vitejs.dev/config/
export default defineConfig({
  
  plugins: [vue()],
  resolve: {
    alias: {
      "@/shared": path.resolve(__dirname, "src/shared"),
      "@/views": path.resolve(__dirname, "src/views"),
      "@/router": path.resolve(__dirname, "src/router"),
      "@/styles": path.resolve(__dirname, "src/styles"),
      "@/store": path.resolve(__dirname, "src/store"),
      "@/components": path.resolve(__dirname, "src/components")
    },

  },
  server: {
    proxy: {
      '/admin': {
        target: 'https://iwscl.com', //'http://localhost:51775',
        changeOrigin: true
      },
      '/file': {
        target: 'http://localhost:51775',
        changeOrigin: true
      }
    }
  }
})
