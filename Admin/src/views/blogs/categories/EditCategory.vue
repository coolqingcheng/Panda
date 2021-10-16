<template>
    <el-dialog v-model="showDialog" width="450px" @close="close()">
        <el-form label-width="80px">
            <el-form-item label="名称">
                <el-input placeholder="输入分类名称"></el-input>
            </el-form-item>
            <el-form-item label="上级">
                <el-select placeholder="选择上级分类" v-model="formModel.pid">
                    <el-option
                        v-for="item in selectList"
                        :key="item.value"
                        :label="item.label"
                        :value="item.value"
                    ></el-option>
                </el-select>
            </el-form-item>
            <el-form-item>
                <el-button type="primary">保存</el-button>
                <el-button>取消</el-button>
            </el-form-item>
        </el-form>
    </el-dialog>
</template>

<script lang="ts">
import { reactive } from 'vue-demi'
import { computed, defineComponent, ref, toRef, toRefs, watch } from 'vue'
export default defineComponent(
    {
        props: {
            show: {
                type: Boolean,
                require: true
            }
        },
        setup(props, context) {

            const selectList = reactive(
                [
                    {
                        label: '分类1',
                        value: 1
                    }
                ]
            )
            const formModel = reactive({
                cateName: '',
                pid: ''
            })

            const close = () => {

                context.emit("update:show", false)
            }

            const showDialog = ref(props.show);

            const { show } = toRefs(props)

            watch(show, () => {
                showDialog.value = show.value;
            })

            return {
                selectList,
                formModel,
                showDialog,
                close
            }
        }
    }
)
</script>

<style lang="less">
</style>