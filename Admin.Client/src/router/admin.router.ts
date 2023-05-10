import { RouteRecordRaw } from "vue-router";


import DashBoard from "@/views/admin/dashboard/DashBoard.vue"
import SysSettingVue from "@/views/admin/regular/Setting.vue";
import FileManager from '@/views/admin/regular/FileManager.vue'



const User: RouteRecordRaw[] = [
    {
        path: 'user',
        name: '用户管理',
        redirect: '',
        children: [
            {
                path: '',
                name: '用户管理',
                component: () => import("@/views/auth/accounts/AccountList.vue"),
                meta: {
                    title: '账号管理'
                }
            },
            {
                path: 'role',
                name: '角色管理',
                component: () => import("@/views/admin/users/RoleManager.vue"),
                meta: {
                    title: '角色管理'
                }
            },
            // {
            //     path: 'permission',
            //     name: '权限管理',
            //     component: () => import("@/views/admin/users/PermissionManager.vue"),
            //     meta: {
            //         title: '权限管理'
            //     }
            // }
        ]
    }

]




const DashBoardRouter: RouteRecordRaw[] = [
    {
        path: "", component: DashBoard,
        meta: {
            title: '控制台'
        }
    },
    {
        path: 'sys',
        children: [
            {
                path: 'setting', component: SysSettingVue,
                meta: {
                    title: '设置'
                }
            },
            {
                path: 'file-manager', component: FileManager
            }
        ]
    }

]




export {
    DashBoardRouter, User
}