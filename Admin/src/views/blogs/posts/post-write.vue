<template>
    <left-menu-layout>
        <template #menu>
            <LeftMenu></LeftMenu>
        </template>
        <template #content>
            <el-form
                label-width="80px"
                v-loading="loading"
                :rules="rules"
                ref="formRef"
                :model="formModel"
                label-position="top"
            >
                <el-form-item label="标题" prop="title">
                    <el-input placeholder="输入标题" v-model="formModel.title"></el-input>
                </el-form-item>
                <el-form-item label="正文" prop="content">
                    <WangEditor v-model="formModel.content"></WangEditor>
                </el-form-item>
                <el-form-item label="分类" prop="categories">
                    <el-checkbox-group v-model="formModel.categories">
                        <el-checkbox
                            :label="item.id"
                            v-for="(item,i) in categoryItems"
                            :key="i"
                            name="categories"
                        >{{ item.cateName }}</el-checkbox>
                    </el-checkbox-group>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="submitForm()">保存</el-button>
                    <el-button type="default" @click="back()">返回</el-button>
                </el-form-item>
            </el-form>
        </template>
    </left-menu-layout>
</template>

<script  lang="ts">
import { defineComponent, onMounted, toRefs } from "@vue/runtime-core";
import { ElForm, ElMessage } from "element-plus";
import { get, http, post } from "shared/http/HttpClient";
import { ref, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import LeftMenuLayout from "../../../components/LeftMenuLayout.vue";
import WangEditor from "../../../components/WangEditor.vue";
import { CategoryItem } from "../categories/CategoryModel";
import LeftMenu from "./LeftMenu.vue";


interface articleItem {
    id: number
    title: string
    content: string
    categories: number[]
}

export default defineComponent({
    components: {
        WangEditor,
        LeftMenu,
        LeftMenuLayout
    },
    setup() {
        const formModel = ref<articleItem>({
            id: 0,
            categories: [],
            title: '',
            content: ''
        })
        const title = ref("新建")
        const loading = ref(false)
        const categoryItems = ref<CategoryItem[]>()
        const loadCategories = async () => {
            var res = await get<CategoryItem[]>('/admin/category/getlist', {})
            categoryItems.value = res;
        }

        onMounted(async () => {
            await loadCategories();
            await loadArticleData();
        })

        var formRef = ref<InstanceType<typeof ElForm>>()

        const submitForm = async () => {

            formRef.value?.validate(async (valid) => {
                if (valid) {
                    try {
                        loading.value = true;
                        await post('/admin/post/addorupdate', formModel.value)
                        ElMessage({
                            type: 'success',
                            message: '保存成功'
                        })
                        formRef.value?.resetFields();
                        router.push('/admin/post')
                    } finally {
                        loading.value = false
                    }
                }
            });
        }

        const rules = {
            title: [{
                required: true, message: '标题不能为空',
                trigger: 'blur'
            }],
            content: [{
                required: true, message: '内容不能为空'
            }],
            categories: [{
                required: true, message: '分类至少选择一个', trigger: 'blur'
            }]
        }

        var router = useRouter();
        var route = useRoute();

        const back = () => {
            router.back();
        }

        console.log('params:', route.query.id)

        router.afterEach(guard => {
            console.log('id:' + route.query.id)
            if (!route.query.id) {
                reSet();
            }
        })

        const reSet = () => {
            formRef.value?.resetFields();
            setTimeout(() => {
                formRef.value?.clearValidate();
            }, 200);
        }




        const loadArticleData = async () => {
            try {
                loading.value = true
                if (route.query.id) {
                    title.value = "编辑"
                    var res = await get<{ title: string, id: number, content: string, categories: { id: number, cateName: string }[] }>
                        ('/admin/post/get', { id: route.query.id })
                    console.log('res:', res)
                    formModel.value = {
                        title: res.title,
                        content: res.content,
                        categories: res.categories.map(a => a.id),
                        id: res.id
                    }
                    console.log('formModel:', JSON.stringify(formModel.value.categories))


                }
            } finally {
                loading.value = false
            }

        }

        return {
            formModel,
            categoryItems,
            loading,
            submitForm,
            back,
            rules,
            formRef,
            title
        }
    }
})
</script>

<style>
</style>
