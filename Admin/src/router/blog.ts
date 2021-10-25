import { RouteRecordRaw } from 'vue-router'
import Categories from '../views/blogs/categories/Categories.vue'

import WriteArticle from "../views/blogs/articles/WriteArticle.vue"

import ArticleList from "../views/blogs/articles/ArticleList.vue"

import Pages from '../views/blogs/pages/Pages.vue'
import PageWrite from '../views/blogs/pages/PageWrite.vue'


var blog: RouteRecordRaw[] = [
    {
        path: '/categories', component: Categories
    },
    {
        path: '/article-write', component: WriteArticle
    },
    {
        path: '/article-list', component: ArticleList
    },
    {
        path: '/pages-write', component: PageWrite
    }, 
    {
        path: '/pages',component: Pages
    }
]
export {
    blog
}