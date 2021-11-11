<template>
    <el-form
        label-width="100px"
        label-position="top"
        ref="instance"
        :model="formModel"
        :rules="rules"
        v-loading="loading"
    >
        <el-form-item label="网站名称" prop="site_name">
            <el-input placeholder="网站名称" v-model="formModel.site_name"></el-input>
        </el-form-item>
        <el-form-item label="ICP备案号" prop="icp">
            <el-input type="textarea" rows="5" v-model="formModel.icp"></el-input>
        </el-form-item>
        <el-form-item label="统计代码" prop="statistics">
            <el-input type="textarea" rows="8" v-model="formModel.statistics"></el-input>
        </el-form-item>
        <el-form-item>
            <el-button type="primary" @click="submit()">保存</el-button>
        </el-form-item>
    </el-form>
</template>

<script lang="ts">
import { ElForm } from 'element-plus'
import { onMounted, ref } from 'vue'
import { usBaseForm } from 'shared/useBaseForm'
import { post, get } from 'shared/http/HttpClient'
export default {
    setup() {

        const formModel = ref({
            site_name: '',
            icp: '',
            statistics: ''
        })

        const { instance, loading } = usBaseForm();


        const rules = {
            site_name: [
                {
                    required: true, message: '站点名称不能为空'
                }
            ]
        }

        const load = async () => {
            var res = await get<any>('/admin/dicdata/get', { groupName: 'site' })
            formModel.value = {
                site_name: res.site_name, icp: res.icp, statistics: res.statistics
            }
        }

        onMounted(()=>{
            load();
        })

        const submit = () => {
            loading.value = true
            console.log(instance.value)
            instance.value?.validate(async (valid) => {
                console.log(valid)
                if (valid) {
                    await post('/admin/dicdata/update', {
                        groupkey: 'site',
                        list: formModel.value
                    })
                    loading.value = false
                }
                else {
                    loading.value = false
                }
            })
        }

        onMounted(() => {

            console.log(loading.value)
        })

        return {
            formModel,
            rules,
            instance, loading,
            submit
        }
    }
}
</script>

<style>
</style>