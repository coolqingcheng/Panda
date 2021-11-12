<template>
    <left-menu-layout>
        <template #menu>
            <router-link to="/admin/tag/edit">新建标签</router-link>
        </template>
        <template #content>
            <el-row>
                <el-col :xs="24" :sm="24" :md="12" :xl="8" :lg="6">
                    <el-form label-position="top">
                        <el-form-item label="标签名称">
                            <el-input placeholder="标签名称"></el-input>
                        </el-form-item>
                        <el-form-item label>
                            <el-button type="primary">保存</el-button>
                            <el-button type="default" @click="back()">返回</el-button>
                        </el-form-item>
                    </el-form>
                </el-col>
            </el-row>
        </template>
    </left-menu-layout>
</template>

<script>
import { useRouter } from 'vue-router'
import LeftMenuLayout from '../../../components/LeftMenuLayout.vue'
export default {
    components: { LeftMenuLayout },
    setup() {
        const router = useRouter()
        const back = () => {
            router.back()
        }

        const formModel = ref({
            id:0,
            tag:''
        })
        return {
            back,
            formModel
        }
    }
}
</script>

<style>
</style>