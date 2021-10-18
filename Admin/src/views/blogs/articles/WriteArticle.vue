<template>
    <el-card class="anim1">
        <template #header>
            <h1>写</h1>
        </template>
        <el-form label-width="80px">
            <el-form-item label="标题">
                <el-input placeholder="输入标题"></el-input>
            </el-form-item>
            <el-form-item label="正文">
                <WangEditor></WangEditor>
            </el-form-item>
            <el-form-item label="分类">
                <el-checkbox-group v-model="formModel.categories">
                    <el-checkbox label="分类1"></el-checkbox>
                </el-checkbox-group>
            </el-form-item>
            <el-form-item>
                <el-button type="primary">保存</el-button>
                <el-button type="default">返回</el-button>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script  lang="ts">
import { defineComponent } from "@vue/runtime-core";
import { ref } from "vue";
import WangEditor from "../../../components/WangEditor.vue";

export default defineComponent({
    components: {
        WangEditor
    },
    setup() {

        const formModel = ref({
            categories: []
        })

        return {
            formModel
        }
    }
})
</script>

<style>
</style>
