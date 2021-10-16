<template>
    <el-container>
        <el-aside width="220px">
            <div class="q-logo">
                <p>Admin</p>
            </div>
            <SlideMenu></SlideMenu>
        </el-aside>
        <el-container>
            <el-header>
                <div class="q-header-container">
                    <div class="q-header-left">
                        <div class="q-expand">
                            <el-icon :size="25">
                                <expand />
                            </el-icon>
                        </div>
                        <q-breadcrumb></q-breadcrumb>
                    </div>
                    <div class="q-toolbar">
                        <el-popover trigger="hover" :width="300">
                            <template #reference>
                                <div class="q-toolbar-item">
                                    <i class="ri-notification-line"></i>
                                </div>
                            </template>
                            <div class="notify-box">
                                <el-tabs v-model="notifySelect">
                                    <el-tab-pane label="通知" name="notify">通知</el-tab-pane>
                                    <el-tab-pane label="消息" name="message">消息</el-tab-pane>
                                    <el-tab-pane label="代办" name="todo">待办</el-tab-pane>
                                </el-tabs>
                            </div>
                        </el-popover>
                        <div class="q-toolbar-item">
                            <q-fullscreen></q-fullscreen>
                        </div>
                        <el-dropdown trigger="hover">
                            <div class="q-toolbar-item">
                                <el-avatar :size="30" src="../../assets/logo.jpeg"></el-avatar>
                            </div>
                            <template #dropdown>
                                <el-dropdown-menu>
                                    <el-dropdown-item>主页</el-dropdown-item>
                                    <el-dropdown-item>个人设置</el-dropdown-item>
                                    <el-dropdown-item divided @click="quit()">退出</el-dropdown-item>
                                </el-dropdown-menu>
                            </template>
                        </el-dropdown>
                    </div>
                </div>
            </el-header>
            <!-- <q-route-tab></q-route-tab> -->
            <el-main>
                <router-view></router-view>
            </el-main>
        </el-container>
    </el-container>
</template>
<script lang="ts">
import SlideMenu from "./SlideMenu.vue";
import { Expand, FullScreen } from '@element-plus/icons'
import { useRouter } from "vue-router";
import { ref } from "@vue/reactivity";
import { http } from "shared/http/HttpClient"
export default {
    components: {
        SlideMenu,
        Expand,
        FullScreen
    },
    setup() {
        const router = useRouter()
        const quit = async () => {
            try {
                await http.get('/admin/account/LoginOut')
                router.replace({
                    path: '/login'
                })
            } catch (error) {

            }
        }
        const fullScreen = () => {
        }

        const notifySelect = ref('notify')

        return {
            quit,
            fullScreen,
            notifySelect
        }
    }
}
</script>
<style lang="less">
.el-container {
    height: 100%;
    .el-aside {
        overflow-x: hidden;
        overflow-y: hidden;
        box-shadow: 10px 0 10px -10px #c7c7c7;
        z-index: 99;
        .q-logo {
            width: 100%;
            height: 60px;
            text-align: center;
            line-height: 60px;
            font-size: 30px;
            font-weight: 400;
        }
    }
    .el-header {
        background: #fff;
        box-shadow: 0 0 1px #ccc;
        display: flex;
        flex-direction: row;
        align-items: center;
        box-sizing: border-box;
        padding: 5px;
        .q-header-container {
            display: flex;
            width: 100%;
            flex-direction: row;
            justify-content: space-between;
            border-bottom: 0.5px solid #dcdfe6;
            .q-header-left {
                display: flex;
                flex-direction: row;
            }
        }
        .q-expand {
            height: 60px;
            width: 60px;
            cursor: pointer;
            display: flex;
            flex-direction: row;
            justify-content: center;
            align-items: center;
        }
    }
    .q-toolbar {
        display: flex;
        flex-direction: row;
        .q-toolbar-item {
            height: 100%;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            padding: 0 0.8rem;
            cursor: pointer;
        }
        .el-tabs__item {
            width: 1/3;
        }
    }
}
.notify-box {
    width: 100%;
    .el-tabs__nav {
        width: 100%;
        .el-tabs__item {
            width: 33.33%;
            text-align: center;
        }
    }
}
.el-main {
    box-sizing: border-box;
    .el-main-container {
        background: white;
        width: 100%;
        // height: 100%;
    }
    h1 {
        margin: 0;
        padding: 0;
    }
}
</style>