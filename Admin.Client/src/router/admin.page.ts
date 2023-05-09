import { RouteRecordRaw } from "vue-router";

const blog = [
    {
        path: 'post',
        component: () => import("@/views/admin/blogs/post/Index.vue"),
        name: '文章列表'
    },
    {
        path: 'post-edit',
        component: () => import("@/views/admin/blogs/post/Edit.vue"),
        name: '编辑文章'
    },
    {
        path: 'cate',
        component: () => import("@/views/admin/blogs/cate/Index.vue"),
        name: '文章分类',
    },
    {
        path: 'cate-edit',
        component: () => import("@/views/admin/blogs/cate/Edit.vue"),
        name: '编辑分类',
    },
    {
        path: 'tag',
        name: '标签',
        component: () => import("@/views/admin/blogs/tag/List.vue")
    }
]

const user = [
    {
        path: 'user',
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
    }
]

const site = [
    {
        path: 'friendlink',
        name: '友情链接',
        component: () => import('@/views/admin/blogs/friendlink/List.vue')
    },
    {
        path: 'edit',
        name: '友情链接-编辑',
        component: () => import('@/views/admin/blogs/friendlink/Edit.vue')
    },
    {
        path: 'setting',
        name: '系统设置',
        component: () => import('@/views/setting/Setting.vue')
    },
    {
        path: 'file-manager',
        name: '文件管理',
        component: () => import('@/views/admin/regular/FileManager.vue')
    },
    {
        path: 'dashboard',
        component: () => import('@/views/admin/dashboard/DashBoard.vue')
    }
]

const test: RouteRecordRaw[] = [
    {
        path: 'test-form',
        name: '测试表单',
        meta: {
            keepName: 'SimpleFormDemo',
            title: '测试表单'
        },
        component: () => import("./../views/example/SimpleFormDemo.vue")
    },
    {
        path: 'test-table',
        name: '测试表格1',
        meta: {
            title: '测试表格',
            keepName: 'SimpleTableDemo'
        },
        component: () => import("./../views/example/SimpleTableDemo.vue")
    }
]




const allPages = [
    ...blog,
    ...user,
    ...site,
    ...test
]


export {
    allPages
}