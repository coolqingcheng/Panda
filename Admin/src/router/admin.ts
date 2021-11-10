import { RouteRecordRaw } from "vue-router";
import Dash from '../views/dashboard/Dash.vue'
import Home from '../views/dashboard/home.vue'


import Post from "../views/blogs/posts/post.vue"
import { post } from './post'
import { setting } from './setting'

import TagList from "../views/blogs/tag/tag-list.vue"
import TagEdit from "../views/blogs/tag/tag-edit.vue"

import TabItemContainer from "../views/TabItemContainer.vue"

const dashRoute: RouteRecordRaw[] = [
    {
        path: '/admin',
        component: Dash,
        children: [
            {
                path: 'post',
                component: Post,
                children: [
                    ...post
                ]
            },
            {
                path: 'dash',
                component: Home
            },
            {
                path: 'tag',
                component: TabItemContainer,
                children: [
                    {
                        path: 'edit', component: TagEdit, name: '标签编辑'
                    },
                    {
                        path: '', component: TagList, name: '标签'
                    }
                ]
            },
            ...setting,

        ]
    }
]

export {
    dashRoute
}