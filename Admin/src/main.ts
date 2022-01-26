import {createApp} from 'vue'
import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import 'remixicon/fonts/remixicon.css'
import Admin from "./components/Admin"

import {router} from './router/index'
import zhCn from 'element-plus/es/locale/lang/zh-cn'


const app = createApp(App)
app.use(Admin)
app.use(router)
app.use(ElementPlus, {zIndex: 3000, locale: zhCn})
app.mount('#app')
