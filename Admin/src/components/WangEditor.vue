
<template>
    <div ref="container"></div>
</template>

<script lang="ts">
import { defineComponent, onBeforeUnmount, onMounted, ref, toRefs, watch } from 'vue';
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
        let editor: E;
        onMounted(() => {
            editor = new E(container.value);
            editor.config.customAlert = (s: string, t: any) => {
                ElMessage({
                    type: t,
                    message: s
                })
            }
            editor.config.uploadImgServer = "/upload";
            editor.config.uploadImgHooks = {
                customInsert: function (insertImgFn: any, result: any) {
                    if (result.code == 0) {
                        insertImgFn(result.url);
                    }
                }
            }
            editor.config.height = 600;
            editor.config.zIndex = 10;
            editor.create();
            
            editor.txt.html(props.modelValue)
            editor.config.onchangeTimeout = 1000;
            editor.config.pasteFilterStyle = false;

            editor.config.onchange = (newHtml: string) => {
                let txt = editor.txt.html();
                if (txt) {
                    context.emit('update:modelValue', txt)
                }
            }

        })
        watch(modelValue, () => {
            if (editor) {
                if (editor.txt.html() != props.modelValue) {
                    editor.txt.html(props.modelValue)
                }
            }
        })
        onBeforeUnmount(() => {
            editor?.destroy();
        })

        return {
            container
        }
    }
})
</script>

<style>
</style>