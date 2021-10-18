<template>
    <el-dialog v-model="showDialog" width="450px" @close="close()">
        <div v-loading="loading">
            <el-form label-width="80px" :rules="formRule" :model="formModel" ref="form">
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
                </el-form-item> -->
                <el-form-item>
                    <el-button type="primary" @click="save()">保存</el-button>
                    <el-button @click="close()">取消</el-button>
                </el-form-item>
            </el-form>
        </div>
    </el-dialog>
</template>

<script lang="ts">
import { reactive } from 'vue'
import { defineComponent, onMounted, PropType, ref, toRefs, watch } from 'vue'
import { http } from 'shared/http/HttpClient'
import { ElForm, ElMessage } from 'element-plus'
import { CategoryItem } from './CategoryModel'
export default defineComponent(
    {
        props: {
            show: {
                type: Boolean,
                require: true
            },
            item: {
                type: Object as PropType<CategoryItem>,
                required: false
            }
        },
        setup(props, context) {

            const { item } = toRefs(props)

            const selectList = reactive(
                [
                    {
                        label: '无',
                        value: 0
                    },
                    {
                        label: '分类1',
                        value: 1
                    },

                ]
            )
            const loading = ref(false)
            const formModel = ref<CategoryItem>({
                cateName: '',
                pid: 0
            })
            const form = ref<InstanceType<typeof ElForm>>();

            const formRule = reactive({
                cateName: {
                    required: true, message: '分类名称必填'
                }
            })

            watch(item, () => {
                console.log(item.value)
                if (item.value) {
                    formModel.value = item.value;
                }
            })


            const close = () => {

                context.emit("update:show", false)
                context.emit('close')
            }

            const showDialog = ref(props.show);

            const { show } = toRefs(props)

            watch(show, () => {
                showDialog.value = show.value;
            })

            const save = async () => {
                try {
                    await form.value?.validate();
                    loading.value = true
                    await http.post('/admin/category/addorupdate', formModel.value);
                    ElMessage({
                        message: '保存成功', type: 'success'
                    })
                    form.value?.resetFields();
                    form.value?.clearValidate();
                    close();
                } finally {
                    loading.value = false
                }
            }

            onMounted(() => {
                console.log('onMounted')
            })

            return {
                selectList,
                formModel,
                showDialog,
                close,
                loading,
                save,
                formRule,
                form
            }
        }
    }
)
</script>

<style lang="less">
</style>