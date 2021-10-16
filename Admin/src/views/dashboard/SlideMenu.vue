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
                    <template #title>
                        <i class="el-icon-location"></i>
                        <span>博文</span>
                    </template>
                    <el-menu-item-group>
                        <el-menu-item index="/categories">分类</el-menu-item>
                    </el-menu-item-group>
                    <el-menu-item-group title="分组2">
                        <el-menu-item index="/article-write">写一篇</el-menu-item>
                        <el-menu-item index="/article-list">我的随笔</el-menu-item>
                    </el-menu-item-group>
                    <el-sub-menu index="1-4">
                        <template #title>选项4</template>
                        <el-menu-item index="1-4-1">选项1</el-menu-item>
                    </el-sub-menu>
                </el-sub-menu>
                <!-- <el-menu-item index="2">
                    <i class="el-icon-menu"></i>
                    <template #title>导航二</template>
                </el-menu-item>
                <el-menu-item index="3" disabled>
                    <i class="el-icon-document"></i>
                    <template #title>导航三</template>
                </el-menu-item>
                <el-menu-item index="4">
                    <i class="el-icon-setting"></i>
                    <template #title>导航四</template>
                </el-menu-item>
                <el-sub-menu index="5">
                    <template #title>
                        <i class="el-icon-location"></i>
                        <span>导航一</span>
                    </template>
                    <el-menu-item-group>
                        <template #title>分组一</template>
                        <el-menu-item index="5-1">选项1</el-menu-item>
                        <el-menu-item index="5-2">选项2</el-menu-item>
                    </el-menu-item-group>
                    <el-menu-item-group title="分组2">
                        <el-menu-item index="5-3">选项3</el-menu-item>
                    </el-menu-item-group>
                </el-sub-menu> -->
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
        onMounted(()=>{
            console.log(router.currentRoute.value)
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