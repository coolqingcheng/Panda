import { RouteRecordRaw } from 'vue-router'
import Categories from '../views/blogs/categories/Categories.vue'

import WriteArticle from "../views/blogs/articles/WriteArticle.vue"

import ArticleList from "../views/blogs/articles/ArticleList.vue"

var blog: RouteRecordRaw[] = [
    {
        path: '/categories', component: Categories
    },
    {
        path: '/article-write', component: WriteArticle
    },
    {
        path: '/article-list', component: ArticleList
    }
]
export {
    blog
}