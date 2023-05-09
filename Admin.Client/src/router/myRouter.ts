import { createRouter, createWebHashHistory } from 'vue-router'
import AdminIndex from '@/views/admin/Index.vue'

import { allPages } from './admin.page'

import Empty from '../components/Empty.vue'

import Page404 from "@/views/common/V404.vue"


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
            children: []
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
                children: [],
                meta:item.meta
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
        } else {
            next()
        }
    } else {
        next();
    }
})
export {
    myRouter
}