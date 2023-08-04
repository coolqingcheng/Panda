<template>
    <el-row :gutter="8">
        <el-col v-bind="cardGrid">
            <el-card shadow="hover">
                标题
            </el-card>
        </el-col>
        <el-col :="cardGrid">
            <el-card shadow="hover">
                标题
            </el-card>
        </el-col>
        <el-col :="cardGrid">
            <el-card shadow="hover">
                标题
            </el-card>
        </el-col>
        <el-col :="cardGrid">
            <el-card shadow="hover">
                标题
            </el-card>
        </el-col>
    </el-row>
    <el-row :gutter="8">
        <el-col v-bind="cardGrid">
            <el-card shadow="hover">
                标题
            </el-card>
        </el-col>
        <el-col :="cardGrid">
            <el-card shadow="hover">
                标题
            </el-card>
        </el-col>
        <el-col :="cardGrid">
            <el-card shadow="hover">
                标题
            </el-card>
        </el-col>
        <el-col :="cardGrid">
            <el-card shadow="hover">
                标题
            </el-card>
        </el-col>
    </el-row>
    <el-row :gutter="8">
        <el-col :="{ lg: 12, xl: 12 }">
            <el-card>
                <template #header>
                    最近登录
                </template>
                <simple-table url="/admin/account/getlist">
                    <!-- <el-table-column prop="id" label="Id" width="180"></el-table-column> -->
                    <el-table-column prop="userName" label="用户名" width="180"></el-table-column>
                    <el-table-column prop="" label="访问时间" width="180"></el-table-column>
                    <el-table-column prop="" label="访问Ip"></el-table-column>
                    <el-table-column prop="" label="访问位置" width="180"></el-table-column>
                </simple-table>
            </el-card>
        </el-col>
    </el-row>
</template>

<script setup lang="ts">

import { onMounted, ref } from 'vue'

import { useVSetting } from "@/store/VSetting"
import { useRoute, useRouter } from 'vue-router';
import SimpleTable from '@/components/SimpleTable.vue';

const cardGrid = { xs: 24, sm: 12, md: 8, lg: 6, xl: 4 }

const show = ref(false)

const test = () => {
}

const setting = useVSetting();

onMounted(() => {
    setting.showViewBg = false;
})

const router = useRouter();
const route = useRoute();
router.afterEach((to, from, fail) => {
    if (!fail) {
        if (to.name != '控制台') {
            setting.showViewBg = true;
        }
    }
})


</script>

<style lang="scss" scoped>
.route-view {
    background-color: #000 !important;
}

.el-row {
    margin-bottom: 8px;
}
.el-card__body{
    padding: 4px !important;
}
</style>