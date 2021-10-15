import { RouteRecordRaw } from "vue-router";
import Login from '../views/auth/Login.vue'

const authRoute: RouteRecordRaw[] = [
    {
        path: '/', component: Login
    },
    {
        path: '/login',
        component: Login
    }
]

export {
    authRoute
}