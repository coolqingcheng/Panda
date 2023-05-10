import { RouteRecordRaw } from "vue-router";

const blog: RouteRecordRaw[] = [
    {
        path: 'post',
        component: () => import("@/views/admin/blogs/post/Index.vue"),
        meta: {
            title: '文章管理'
        }
    },
    {
        path: 'post-edit',
        component: () => import("@/views/admin/blogs/post/Edit.vue"),
        meta: {
            title: '编辑文章'
        }
    },
    {
        path: 'cate',
        component: () => import("@/views/admin/blogs/cate/Index.vue"),
        meta: {
            title: '分类'
        }
    },
    {
        path: 'cate-edit',
        component: () => import("@/views/admin/blogs/cate/Edit.vue"),
        meta: {
            title: '编辑分类'
        }
    },
    {
        path: 'tag',
        meta: {
            title: '标签'
        },
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

const site: RouteRecordRaw[] = [
    {
        path: 'friendlink',
        meta: {
            title: '友情链接',
        },
        component: () => import('@/views/admin/blogs/friendlink/List.vue')
    },
    {
        path: 'edit',
        name: '友情链接-编辑',
        component: () => import('@/views/admin/blogs/friendlink/Edit.vue')
    },
    {
        path: 'setting',
        meta: {
            title: '系统设置',
        },
        component: () => import('@/views/setting/Setting.vue')
    },
    {
        path: 'file-manager',
        meta: {
            title: '文件管理'
        },
        component: () => import('@/views/admin/regular/FileManager.vue')
    },
    {
        path: 'dashboard',
        meta: {
            title: '控制台'
        },
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




const allPages: RouteRecordRaw[] = [
    ...blog,
    ...user,
    ...site,
    ...test
]


export {
    allPages
}