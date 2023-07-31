import { createRouter, createWebHashHistory } from 'vue-router'
import AdminLayout from '@/views/admin/AdminLayout.vue'

import { routeList } from './admin.page'

// import Empty from '../components/Empty.vue'

// import Page404 from "@/views/common/V404.vue"


const AdminRouter = createRouter({
    history: createWebHashHistory(),
    routes: [
        {
            path: '/',
            redirect: '/auth'
        },
        {
            path: '/auth',
            name: '登录',
            component: () => import("@/views/auth/Login.vue")
        },
        {
            path: '/admin',
            name: 'admin',
            component: AdminLayout,
            children: [
                {
                    path: '',
                    redirect: {
                        name: '仪表盘'
                    }
                },
                {
                    path: "dashboard",
                    name: '仪表盘',
                    meta: {
                        leftMenu: true
                    },
                    component: () => import("@/views/admin/dashboard/DashBoard.vue"),
                },
                ...routeList
            ]
        }
    ]
})


export {
    AdminRouter
}