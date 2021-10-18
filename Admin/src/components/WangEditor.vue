
<template>
    <div ref="container"></div>
</template>

<script lang="ts">
import { onMounted, ref } from 'vue';
import E from 'wangeditor'
import { ElMessage } from 'element-plus';
export default {
    setup() {

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
            editor.create();
        })

        return {
            container
        }
    }
}
</script>

<style>
</style>