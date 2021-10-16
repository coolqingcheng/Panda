import { createRouter, createWebHashHistory, RouteRecordRaw } from "vue-router";

import { dashRoute } from './dash'
import { authRoute } from './auth'


const route: RouteRecordRaw[] = [
    ...authRoute,
    ...dashRoute
]


export const router = createRouter({
    history: createWebHashHistory(),
    routes: route
})