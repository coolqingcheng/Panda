
<template>
    <el-table :data="tableData" style="width: 100%" border v-loading="loading">
        <el-table-column prop="cateName" label="名称" width="180" />
        <el-table-column prop="num" label="数量" width="60" />
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
    <el-row style="margin-top:2rem">
        <el-col :xs="24" :sm="24" :md="12" :lg="6" :xl="6">
            <el-form
                label-width="80px"
                :rules="formRule"
                :model="formModel"
                ref="form"
                label-position="top"
            >
                <el-form-item label="名称" prop="cateName">
                    <el-input placeholder="输入分类名称" v-model="formModel.cateName"></el-input>
                </el-form-item>
                <!-- <el-form-item label="上级" prop="pid">
                    <el-select placeholder="选择上级分类" v-model="formModel.pid">
                        <el-option
                            v-for="item in selectList"
                            :key="item.value"
                            :label="item.label"
                            :value="item.value"
                        ></el-option>
                    </el-select>
                </el-form-item>-->
                <el-form-item>
                    <el-button type="primary" @click="save()">{{ formModel.id ? '保存修改' : '添加' }}</el-button>
                    <el-button @click="cancel()">重置</el-button>
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
</template>

<script lang="ts">
import { defineComponent, onMounted, reactive, ref, toRefs } from 'vue';
import EditCategory from "./EditCategory.vue"
import { get, http } from "shared/http/HttpClient"
import { deepCopy } from 'shared/utils/ObjectUtils';
import { CategoryItem } from './CategoryModel';
import LeftMenuLayout from '../../../components/LeftMenuLayout.vue';
import LeftMenu from '../posts/LeftMenu.vue';
import { ElForm, ElMessage } from 'element-plus';
export default defineComponent({
    components: {
        EditCategory,
        LeftMenuLayout,
        LeftMenu
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
            show.value = true;
            formModel.value = deepCopy(item);
            console.log(formModel.value)

        }

        const form = ref<InstanceType<typeof ElForm>>();

        const formRule = reactive({
            cateName: {
                required: true, message: '分类名称必填', trigger: 'blur'
            }
        })

        const fromLoading = ref(false)
        const formModel = ref<CategoryItem>({
            id: 0,
            cateName: '',
            pid: 0
        })

        const save = async () => {
            try {
                await form.value?.validate();
                loading.value = true
                await http.post('/admin/category/addorupdate', formModel.value);
                ElMessage({
                    message: '保存成功', type: 'success'
                })
                cancel();
                getData()
            } finally {
                loading.value = false
            }
        }

        const cancel = () => {
            form.value?.clearValidate()
            form.value?.resetFields();
            console.log(formModel.value)
            formModel.value.id = 0
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
            cateItem,
            formRule,
            form,
            fromLoading,
            formModel,
            save,
            cancel
        }
    }
})
</script>

<style lang="less">
</style>