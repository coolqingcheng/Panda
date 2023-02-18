import { RouteRecordRaw } from "vue-router";


const SiteRouter: RouteRecordRaw[] = [
    {
        path: 'friendlink',
        children: [
            {
                path: 'friendlink',
                name: '友情链接',
                component: () => import('@/views/admin/blogs/friendlink/List.vue')
            },
            {
                path: 'edit',
                name: '友情链接-编辑',
                component: () => import('@/views/admin/blogs/friendlink/Edit.vue')
            }
        ]
    }
]

export {
    SiteRouter
}