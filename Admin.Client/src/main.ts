import { createApp } from 'vue'
import './style.scss'
import App from './App.vue'
import 'element-plus/dist/index.css'
import 'default-passive-events'

import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'

import ElementPlus from 'element-plus'

import { myRouter } from '@/router/myRouter'

import { axiosConfig } from "@/shared/Axios.Config"

axiosConfig()

const app = createApp(App)
const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)
app.use(pinia)
app.use(myRouter)
app.use(ElementPlus)
app.mount('#app')

