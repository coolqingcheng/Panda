import { RouteRecordRaw,useRoute,useRouter } from "vue-router";
import Login from '../views/auth/Login.vue'

const router = useRouter();

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