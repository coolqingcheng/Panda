import { RouteRecordRaw } from "vue-router";
import Dash from '../views/dashboard/Dash.vue'


import Post from "../views/blogs/post.vue"
import { post } from './post'

const dashRoute: RouteRecordRaw[] = [
    {
        path: '/dash',
        component: Dash,
        children: [
            {
                path: 'post',
                component: Post,
                children: [
                    ...post
                ]
            },
        ]
    }
]

export {
    dashRoute
}