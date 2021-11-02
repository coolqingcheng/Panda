import { RouteRecordRaw } from 'vue-router'
import Categories from '../views/blogs/categories/Categories.vue'

import PostWrite from "../views/blogs/posts/post-write.vue"
import PostList from "../views/blogs/posts/post-list.vue"

var post: RouteRecordRaw[] = [
    {
        path: 'categories', component: Categories
    },
    {
        path: 'write', component: PostWrite
    },
    {
        path: '',
        name: '随笔',
        component: PostList
    }
]
export {
    post
}