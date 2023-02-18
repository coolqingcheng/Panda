import { RouteRecordRaw } from "vue-router";

import PostList from '@/views/admin/blogs/post/Index.vue'


const BlogRouter: RouteRecordRaw[] = [
    {
        path: 'blogs',
        name: '博客',
        redirect: '',
        children: [
            {
                path: 'post',
                component: PostList,
                name: '文章'
            },
            {
                path: 'post-edit',
                component: () => import("@/views/admin/blogs/post/Edit.vue"),
                name: '编辑文章'
            },
            {
                path: 'cate',
                component: () => import("@/views/admin/blogs/cate/Index.vue"),
                name:'文章分类',
            },
            {
                path: 'cate-edit',
                component: () => import("@/views/admin/blogs/cate/Edit.vue"),
                name:'编辑分类',
            },
            {
                path: 'tag',
                name:'标签',
                component:()=> import ("@/views/admin/blogs/tag/List.vue")
            }
        ]
    }
]

export {
    BlogRouter
}