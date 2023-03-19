<template>
    <!-- 后台仪表盘，根目录 -->

    <div class="layout">
        <AsideLayout></AsideLayout>
        <div class="main">
            <div class="header">
                <HeaderItem @click="expandHandler()">
                    <el-icon class="expand-icon">
                        <Expand />
                    </el-icon>

                </HeaderItem>
                <div class="nav-crumb">
                    <!-- <el-breadcrumb :separator-icon="ArrowRight">
                        <template v-for="item in crumbList">
                            <el-breadcrumb-item :to="{ path: item.path }">{{ item.name }}</el-breadcrumb-item>
                        </template>
                    </el-breadcrumb> -->
                </div>
                <ElDropdown>
                    <HeaderItem>
                        <ElIcon>
                            <User></User>
                        </ElIcon>
                    </HeaderItem>
                    <template #dropdown>
                        <ElDropdownItem @click="showChangePwd()">修改密码</ElDropdownItem>
                        <ElDropdownItem @click="LoginOut()">退出登录</ElDropdownItem>
                    </template>
                </ElDropdown>
            </div>
            <div :class="{ 'route-view': setting.showViewBg }">
                <RouterView></RouterView>
            </div>
        </div>
    </div>
    <ChangePwd v-model="ChangePwdStatus"></ChangePwd>
</template>
<script lang="ts" setup>
import HeaderItem from './layout/header/HeaderItem.vue';
import { expandHandler, closeSlideMenu } from './layout/MenuStatus'
import { Expand, User } from '@element-plus/icons-vue'
import AsideLayout from './layout/AsideLayout.vue'
import { useRoute, useRouter } from 'vue-router'
import { onMounted, ref } from 'vue';
import { ArrowRight } from '@element-plus/icons-vue'

import { useVSetting } from '@/store/VSetting'

import { AuthService } from '@/shared/service'
import { ElLoading, ElMessage } from 'element-plus';
import ChangePwd from './users/ChangePwd.vue';
import { allPages } from '@/router/admin.page';
import { useDynamicRouter } from '@/shared/useDynamic';

const ChangePwdStatus = ref(false)

const showChangePwd = ()=>{
    ChangePwdStatus.value = true
}


const router = useRouter();

const route = useRoute();

const setting = useVSetting();

// const crumbList = ref<{ name: string, path: string }[]>([])

const showBg = ref(true)

router.afterEach((to, from, fail) => {
    closeSlideMenu();
    if (!fail) {
        // crumbList.value = [];
        // to.matched.filter(a => a.name).map(a => {
        //     crumbList.value.push({
        //         name: a.name!.toString(),
        //         path: a.path
        //     })
        // })
    }
    showBg.value = route.meta.showbg as any;
    if (route.meta.title) {
        document.title = route.meta.title as string
    }
})

onMounted(() => {
    // crumbList.value = []
    // route.matched.filter(a => a.name).map(a => {
    //     crumbList.value.push({
    //         name: a.name!.toString(),
    //         path: a.path
    //     })
    // })
})

const LoginOut = () => {
    let loading = ElLoading.service({
        lock: true
    })
    AuthService.loginOut().then(() => {
        ElMessage.success('退出成功')
        router.replace({
            name: '登录'
        })
    }).finally(() => {
        loading.close();
    })
}

console.log('动态路由添加')

router.beforeEach(guard=>{
    console.log('路由跳转之前')
})

useDynamicRouter();

console.log(router.getRoutes())



</script>

<style lang="scss">
@import "../../../src/styles/config.scss";

@import "@/styles/aside.scss";

.app-content {
    max-height: 100%;
    overflow-y: auto;
}

.menu-box {
    max-height: calc(100% - var(--el-header-height));
    overflow-y: auto;
}

.nav-crumb {
    display: flex;
    flex-direction: row;
    align-items: center;
    flex: 1;
}



.layout {
    display: flex;
    flex-direction: row;
    height: 100%;
    max-height: 100%;
    overflow-y: auto;
    background-color: var(--el-bg-color-page);

    .logo {
        width: 100%;
        height: $header-h;
        line-height: $header-h;
        text-align: center;

        font-size: 14px;
        user-select: none;

        h1 {
            color: var(--logo-text-color);
        }
    }




    .main {
        flex: 1;
        display: flex;
        flex-direction: column;
        padding: 10px;
        box-sizing: border-box;
        padding-top: $header-h + 20px;
        position: relative;

        .route-view {
            width: 100%;
            box-sizing: border-box;
            min-height: 200px;
            overflow-y: auto;
            overflow-x: auto;
        }

        .header {
            position: absolute;
            top: 0;
            right: 0;
            left: 0;
            height: $header-h;
            width: 100%;
            box-shadow: 3px 3px 5px rgba(0, 0, 0, 0.1);
            z-index: 100;
            background-color: var(--el-bg-color);
            display: flex;
            flex-direction: row;

            .expand-icon {
                font-size: 25px;
                color: var(--el-text-color-primary);
            }
        }
    }
}
</style>