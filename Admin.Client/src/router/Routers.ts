import { createRouter, createWebHashHistory } from "vue-router"


import Login from "@/views/auth/Login.vue"


const AppRouter = createRouter({
    history: createWebHashHistory(),
    routes: [
        {
            path: '/', redirect: '/auth'
        },
        {
            path: '/auth', component: Login
        }
    ]
})

export {
    AppRouter
}