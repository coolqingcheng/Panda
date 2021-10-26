<template>
    <div class="q-slider-box">
        <div style="width:220px">
            <el-menu
                :uniqueOpened="true"
                :default-active="currActivePath"
                class="el-menu-vertical-demo"
                @open="handleOpen"
                @close="handleClose"
                background-color="#fafafa"
                text-color="--el-text-color-primary"
                active-text-color="blue"
                router
            >
                <el-sub-menu index="1">
                    <template #title>博文</template>
                    <el-menu-item-group>
                        <el-menu-item index="/categories">分类</el-menu-item>
                        <el-menu-item index="/article-write">写一篇</el-menu-item>
                        <el-menu-item index="/article-list">我的随笔</el-menu-item>
                    </el-menu-item-group>
                </el-sub-menu>
                <el-sub-menu index="2">
                    <template #title>自定义页面</template>
                    <el-menu-item index="/pages-write">新建页面</el-menu-item>
                    <el-menu-item index="/pages">所有页面</el-menu-item>
                </el-sub-menu>
            </el-menu>
        </div>
    </div>
</template>

<script lang="ts">
import { useRouter } from "vue-router"
import { onMounted, ref } from 'vue'

export default {
    setup() {
        const currActivePath = ref<string>();
        const handleOpen = () => {

        }
        const handleClose = () => {
        }
        const router = useRouter();
        onMounted(() => {
            console.log(router.currentRoute.value.path)
            currActivePath.value = router.currentRoute.value.path
        })
        return {
            handleOpen,
            handleClose,
            currActivePath
        }
    }
}
</script>

<style lang="less" scoped>
.q-slider-box {
    width: 240px;
    max-height: 100%;
    overflow-y: auto;
    .el-menu {
        border-right: none;
    }
}
</style>