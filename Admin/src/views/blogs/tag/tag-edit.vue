
<template>
    <left-menu-layout>
        <template #menu>
            <router-link to="/admin/tag/edit">新建标签</router-link>
        </template>
        <template #content>
            <el-row>
                <el-col :xs="24" :sm="24" :md="12" :xl="8" :lg="6">
                    <el-form
                        label-position="top"
                        ref="form.instance"
                        :model="formModel"
                        v-loading="loading"
                        :rules="rules"
                    >
                        <el-form-item label="标签名称" prop="tag">
                            <el-input placeholder="标签名称" v-model="formModel.tag"></el-input>
                        </el-form-item>
                        <el-form-item label>
                            <el-button type="primary" @click="save()">保存{{ loading }}</el-button>
                            <el-button type="default" @click="back()">返回</el-button>
                        </el-form-item>
                    </el-form>
                </el-col>
            </el-row>
        </template>
    </left-menu-layout>
</template>

<script lang="ts">
import { useRouter } from 'vue-router'
import LeftMenuLayout from '../../../components/LeftMenuLayout.vue'
import { onMounted, ref } from 'vue'
import { useForm } from "shared/useForm"
export default {
    components: { LeftMenuLayout },
    setup() {
        const router = useRouter()
        const back = () => {
            router.back()
        }
        const form = useForm();

        const rules = {
            tag: [
                { required: true, message: '标签数量不能为空' }
            ]
        }

        const formModel = ref({
            id: 0,
            tag: ''
        })

        onMounted(() => {
        })

        const save = () => {

        }

        return {
            back,
            formModel,
            ...form,
            rules,
            save
        }
    }
}
</script>

<style>
</style>