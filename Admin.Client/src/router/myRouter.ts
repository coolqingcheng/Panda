import { createRouter, createWebHashHistory } from 'vue-router'
import AdminIndex from '@/views/admin/Index.vue'
import Page404 from "@/views/common/V404.vue"

import { BlogRouter } from "./admin.blog"
import { User } from './admin.router'
import { SiteRouter } from './admin.site'
import { allPages } from './admin.page'
import { useRouteStore } from '@/store/RouteStore'


const myRouter = createRouter({
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
            component: AdminIndex,
            children: [
                // {
                //     path: '',
                //     component: () => import('@/views/admin/dashboard/DashBoard.vue')
                // }
                // ,
                // ...BlogRouter,
                // ...User,
                // ...SiteRouter,
                // {
                //     path: 'setting',
                //     name: '系统设置',
                //     component: () => import('@/views/setting/Setting.vue')
                // },
                // {
                //     path: '404',
                //     component: Page404
                // }
                // , {
                //     path: ':pathMatch(.*)',
                //     redirect: '/admin/404'
                // }
            ]
        }
    ]
})

var isDynamicLoad = false;


const addDynamicRoute = () => {
    let pages = allPages;
    let routes = myRouter.getRoutes();
    pages.forEach(item => {
        // console.log('加载路由:' + item.path)
        let path = `/admin/${item.path}`;
        if (routes.findIndex(a => a.path == path) == -1) {
            myRouter.addRoute('admin', {
                path: path,
                name: item.name,
                component: item.component,
                children: []
            })
        }
    })
}

myRouter.beforeEach((to, from, next) => {
    // console.log('路由守卫', to, from)
    // const routeStore = useRouteStore();
    if (to.path.startsWith('/admin')) {
        console.log('是否已经动态加载路由:', isDynamicLoad)
        if (isDynamicLoad == false) {
            addDynamicRoute();
            isDynamicLoad = true;
            // next({...to,replace:true})
            next({ ...to, replace: true })
            // next();
            console.log(myRouter.getRoutes())
        }else{
            next()
        }
    }else{
        next();
    }
})
export {
    myRouter
}