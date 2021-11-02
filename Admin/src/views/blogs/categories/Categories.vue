
<template>
    <el-card class="anim1">
        <template #header>
            <h1>分类</h1>
        </template>
        <el-row class="tool-bar">
            <el-button type="primary" @click="showDialog()">添加</el-button>
        </el-row>
        <el-table :data="tableData" style="width: 100%" border v-loading="loading">
            <el-table-column prop="id" label="Id" width="180" />
            <el-table-column prop="cateName" label="名称" width="180" />
            <el-table-column prop="address" label="说明" />
            <el-table-column label="操作" fixed="right" width="150">
                <template #default="scope">
                    <el-button type="primary" size="mini" @click="editCategory(scope.row)">编辑</el-button>
                    <el-popconfirm title="确定删除吗？" @confirm="delCategory(scope.row.id)">
                        <template #reference>
                            <el-button type="danger" size="mini">删除</el-button>
                        </template>
                    </el-popconfirm>
                </template>
            </el-table-column>
        </el-table>
    </el-card>
    <EditCategory v-model:show="show" @close="getData()" :item="cateItem"></EditCategory>
</template>

<script lang="ts">
import { onMounted, reactive, Ref, ref } from 'vue';
import EditCategory from "./EditCategory.vue"
import { get, http } from "shared/http/HttpClient"
import { deepCopy } from 'shared/utils/ObjectUtils';
import { CategoryItem } from './CategoryModel';
export default {
    components: {
        EditCategory
    },
    setup() {

        const tableData = ref<CategoryItem[]>([])
        const show = ref(false)
        const showDialog = () => {
            cateItem.value = {}
            show.value = !show.value
        }

        const loading = ref(false)

        const cateItem = ref<CategoryItem>();

        const close = () => {

        }

        const getData = async () => {
            try {
                loading.value = true
                var res = await get<CategoryItem[]>('/admin/category/getlist', {});
                tableData.value = res
            } finally {
                loading.value = false
            }
        }
        onMounted(() => {
            getData()
        })

        const delCategory = async (id: number) => {
            try {
                loading.value = true
                await http.delete('/admin/category/delete', { params: { categoryId: id } })
            } finally {
                loading.value = false
            }
        }

        const editCategory = (item: CategoryItem) => {
            console.log(item)
            console.log(deepCopy(item))
            show.value = true;
            cateItem.value = deepCopy(item)

        }

        return {
            tableData,
            show,
            showDialog,
            close,
            getData,
            loading,
            delCategory,
            editCategory,
            cateItem
        }
    }
}
</script>

<style lang="less">
</style>