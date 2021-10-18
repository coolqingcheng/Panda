<template>
    <el-card class="anim1">
        <template #header>
            <h1>写</h1>
        </template>
        <el-form
            label-width="80px"
            v-loading="loading"
            :rules="rules"
            ref="formRef"
            :model="formModel"
        >
            <el-form-item label="标题" prop="title">
                <el-input placeholder="输入标题" v-model="formModel.title"></el-input>
            </el-form-item>
            <el-form-item label="正文" prop="content">
                <WangEditor v-model="formModel.content"></WangEditor>
            </el-form-item>
            <el-form-item label="分类" prop="categories">
                <el-checkbox-group v-model="formModel.categories">
                    <el-checkbox :label="item.id" v-for="(item,i) in categoryItems" :key="i" name="categories">{{item.cateName}}</el-checkbox>
                </el-checkbox-group>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="submitForm()">保存</el-button>
                <el-button type="default" @click="back()">返回</el-button>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script  lang="ts">
import { defineComponent, onMounted, toRefs } from "@vue/runtime-core";
import { ElForm, ElMessage } from "element-plus";
import { get, http, post } from "shared/http/HttpClient";
import { ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import WangEditor from "../../../components/WangEditor.vue";
import { CategoryItem } from "../categories/CategoryModel";

export default defineComponent({
    components: {
        WangEditor
    },
    props:{
        id:{
            type:Number,
            default:0
        }
    },
    setup(props) {
        const {id} = toRefs(props)
        const formModel = ref({
            id:0,
            categories: [],
            title: '',
            content: ''
        })
        const loading = ref(false)
        const categoryItems = ref<CategoryItem[]>()
        const loadCategories = async () => {
            var res = await get<CategoryItem[]>('/admin/category/getlist', {})
            categoryItems.value = res;
        }

        onMounted(async () => {
            await loadCategories();
            if(props.id>0){
               await loadArticleData();
            }
        })

        var formRef = ref<InstanceType<typeof ElForm>>()

        const submitForm = async () => {

            formRef.value?.validate(async (valid) => {
                if (valid) {
                    try {
                        loading.value = true;
                        await post('/admin/article/addorupdate', formModel.value)
                        ElMessage({
                            type: 'success',
                            message: '保存成功'
                        })
                    } finally {
                        loading.value = false
                    }
                }
            });
        }

        const rules = {
            title: [{
                required: true, message: '标题不能为空'
            }],
            content: [{
                required: true, message: '内容不能为空'
            }],
            categories: [{
                required: true, message: '分类至少选择一个'
            }]
        }

        var router = useRouter();

        const back = () => {
            router.back();
        }


        

        const loadArticleData = async ()=>{
           var res =  await get('/admin/article/get',{id:id.value})
           console.log('res:',res)
        }

        return {
            formModel,
            categoryItems,
            loading,
            submitForm,
            back,
            rules,
            formRef
        }
    }
})
</script>

<style>
</style>
