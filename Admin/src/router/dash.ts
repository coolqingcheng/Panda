import { RouteRecordRaw } from "vue-router";
import Dash from '../views/dashboard/Dash.vue'

import { blog } from './blog'

const dashRoute: RouteRecordRaw[] = [
    {
        path: '/dash',
        component: Dash,
        children:[
            ...blog
        ]
    }
]

export {
    dashRoute
}