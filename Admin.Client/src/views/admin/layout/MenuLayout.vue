<template>
    <div class="menu-box" style="background-color:#393d49">
        <!-- {{ defaultActive }} -->
        <el-scrollbar>
            <el-menu class="el-menu-vertical-demo" background-color="#393d49" active-text-color="white" text-color="#ccc"
                unique-opened :collapse="isCollapse" :collapse-transition="false" :router="true"
                :default-active="defaultActive">
                <template v-for="item in memuList" :key="item.path">
                    <el-menu-item :index="item.path" v-if="item.children.length == 0">
                        <el-icon>
                            <Odometer />
                        </el-icon>
                        <template #title>
                            <span>{{ item.memuName }}</span>
                        </template>
                    </el-menu-item>

                    <el-sub-menu v-if="item.children.length > 0" :index="item.path">
                        <template #title>
                            <el-icon>
                                <component :is="item.icon"></component>
                            </el-icon>
                            <span>{{ item.memuName }} </span>
                        </template>
                        <el-menu-item :index="child.path" v-for="child in item.children" :key="child.path">{{
                            child.memuName }} {{ child.path }}</el-menu-item>
                    </el-sub-menu>
                </template>


            </el-menu>
        </el-scrollbar>
    </div>
</template>
<script setup>
import { isCollapse } from "./MenuStatus"

import { User, Setting, Odometer } from '@element-plus/icons-vue'
import { onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";

const defaultActive = ref('')

const route = useRoute();

const memuList = ref([])

onMounted(() => {
    getActivePath(route.path)
    genMenu()
})

const genMenu = () => {
    //路由生成左边菜单

    let routerList = router.getRoutes();
    let topChild = routerList.filter(a => a.path == '/admin' && a.redirect == undefined).map(a => a.children)[0];

    var list = topChild.filter(a => a.meta?.leftMenu).map(a => {
        var childList = a.children?.filter(a => a.name && a.meta?.leftMenu).map(item => {
            // console.log(item.name)
            return {
                path: routerList.filter(x => x.name == item.name)[0]?.path,
                memuName: item.name,
                icon: item.meta?.iconName
            }
        })
        let path = routerList.filter(x => x.name == a.name)[0]?.path;
        // console.log(path,a.name)
        return {
            memuName: a.name,
            path: path,
            children: childList ?? [],
            icon: a.meta?.iconName
        }

    })
    memuList.value.push(...list)
    console.log(memuList.value)
    console.log(routerList);

}

const router = useRouter();

router.afterEach((to, from, fail) => {
    if (!fail) {
        getActivePath(to.path)
    }
})

const getActivePath = (path) => {
    let rg = path.match('^/([^/]+)/([^/]+)');
    console.log('匹配出:', rg[0])
    defaultActive.value = rg[0]
}

//动态添加路由






</script>

<style lang="scss">
.menu-box {
    // max-height: 100%;
    // overflow-y: auto;
    user-select: none;

    .el-menu-item .is-active {
        background-color: var(--menu-item-active-bg);
    }

    .el-menu-vertical-demo:not(.el-menu--collapse) {
        width: 230px;
        min-height: 400px;
    }
}

.el-menu.el-menu--inline {
    padding: 8px;
    box-sizing: border-box;

    .el-menu-item.is-active {
        background-color: var(--el-color-primary);
        color: white;

    }
}

.el-sub-menu.is-active.is-opened {
    .el-menu-item {
        border-radius: 4px;
    }
}
</style>