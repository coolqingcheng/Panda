
<template>
    <div ref="container"></div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, toRefs, watch } from 'vue';
import E from 'wangeditor'
import { ElMessage } from 'element-plus';
export default defineComponent({
    props: {
        modelValue: {
            type: String,
            default: ''
        }
    },
    setup(props, context) {
        const { modelValue } = toRefs(props)
        const container = ref<HTMLElement>();
        var editor: E;
        onMounted(() => {
            editor = new E(container.value);
            editor.config.customAlert = (s: string, t: any) => {
                ElMessage({
                    type: t,
                    message: s
                })
            }
            editor.config.zIndex = 1000;
            editor.create();
            editor.txt.html(props.modelValue)
            editor.config.onchange = (newHtml: string) => {
                context.emit('update:modelValue', newHtml)
            }
            editor.config.onchangeTimeout = 500;
        })
        watch(modelValue, () => {
            if (editor) {
                editor.txt.html(props.modelValue)
            }
        })

        return {
            container
        }
    }
})
</script>

<style>
</style>