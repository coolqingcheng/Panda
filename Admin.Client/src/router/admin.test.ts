import { RouteRecordRaw } from "vue-router";

const TestRouter: RouteRecordRaw[] = [
    {
        path: 'form',
        component: () => import("./../views/example/SimpleFormDemo.vue")
    }
]

export {
    TestRouter
}