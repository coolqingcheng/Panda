import { RouteRecordRaw } from "vue-router";
import Dash from '../views/dashboard/Dash.vue'

const dashRoute: RouteRecordRaw[] = [
    {
        path: '/dash',
        component: Dash
    }
]

export {
    dashRoute
}