<template>
    <h1 @click="test()">dashboard</h1>
    <el-dialog v-model="show">
        <h1></h1>
    </el-dialog>
</template>

<script setup lang="ts">

import { onMounted, ref } from 'vue'

import { useVSetting } from "@/store/VSetting"
import { useRoute, useRouter } from 'vue-router';

const show = ref(false)

const test = () => {
}

const setting = useVSetting();

onMounted(() => {
    setting.showViewBg = false;
})

const router = useRouter();
const route = useRoute();
router.afterEach((to, from, fail) => {
    if (!fail) {
        if (to.name != '控制台') {
            setting.showViewBg = true;
        }
    }
})


</script>

<style lang="scss" scoped>
.route-view {
    background-color: #000 !important;
}
</style>