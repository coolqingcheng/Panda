import { RouteRecordRaw } from 'vue-router'

import PostWrite from "../views/blogs/posts/post-write.vue"
import PostList from "../views/blogs/posts/post-list.vue"

var post: RouteRecordRaw[] = [
 
    {
        path: 'write', component: PostWrite, name: '编辑'
    },
    {
        path: '',
        name: '随笔',
        component: PostList
    },

]
export {
    post
}