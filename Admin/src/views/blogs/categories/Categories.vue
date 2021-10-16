
<template>
    <el-card class="anim1">
        <el-header>分类</el-header>
        <el-row class="tool-bar">
            <el-button type="primary" @click="showDialog()">添加</el-button>
        </el-row>
        <el-table :data="tableData" style="width: 100%" border>
            <el-table-column prop="date" label="Id" width="180" />
            <el-table-column prop="name" label="名称" width="180" />
            <el-table-column prop="address" label="说明" />
            <el-table-column label="操作" fixed="right" width="120">
                <template #default>
                    <el-button type="text" size="small">编辑</el-button>
                    <el-button type="text" size="small" style="color:red">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
    </el-card>
    <EditCategory v-model:show="show"></EditCategory>
</template>

<script lang="ts">
import { ref } from 'vue';
import EditCategory from "./EditCategory.vue"
export default {
    components: {
        EditCategory
    },
    setup() {
        
        const tableData = [
            {
                date: '2016-05-03',
                name: 'Tom',
                address: 'No. 189, Grove St, Los Angeles',
            },
            {
                date: '2016-05-02',
                name: 'Tom',
                address: 'No. 189, Grove St, Los Angeles',
            },
            {
                date: '2016-05-04',
                name: 'Tom',
                address: 'No. 189, Grove St, Los Angeles',
            },
            {
                date: '2016-05-01',
                name: 'Tom',
                address: 'No. 189, Grove St, Los Angeles',
            },
        ]
        const show = ref(false)
        const showDialog = ()=>{
            show.value = !show.value
        }
        return {
            tableData,
            show,
            showDialog
        }
    }
}
</script>

<style lang="less">
@keyframes test1 {
    0% {
        opacity: 0.1;
        transform: translateY(100px);
    }
    100% {
        transform: translateY(0);
        opacity: 1;
    }
}
.anim1 {
    animation: test1 0.5s ease-in-out;
}
</style>