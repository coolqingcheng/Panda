<template>
    <ElRow>
        <ElCol>
            <ElTable v-loading="loading" :data="config.data" :border="true">
                <slot></slot>
            </ElTable>
        </ElCol>
    </ElRow>
    <ElRow>
        <ElCol>
            <el-pagination :page-sizes="[10, 20, 50, 100]" background layout="total,prev, pager, next,sizes"
                :total="config.total" v-model:current-page="params.index" v-model:page-size="params.pageSize" />
        </ElCol>
    </ElRow>
</template>
<script lang="ts" setup>
import { onMounted, reactive, ref, watch } from 'vue';

import { serviceOptions } from '@/shared/service'


const props = defineProps({
    url: String,
    condition: Object
})

const loading = ref(false)

const config = reactive({
    total: 0,
    data: []
})

const params = reactive({
    index: 1,
    pageSize: 10,

})

/**
 * 加载table数据
 */
const loadData = () => {
    loading.value = true;
    serviceOptions.axios?.get(props.url ?? '', {
        params: {
            ...params,
            ...props.condition
        }
    })
        .then(res => {
            console.log(res)
            if (res.data) {
                config.total = res.data.total;
                config.data = res.data.data
            }
        }).finally(() => {
            loading.value = false;
        })
}

defineExpose({
    loadData
})


watch(() => params, () => {
    loadData();
}, { deep: true })


watch(() => props.condition, () => {
    console.log('condition变化')
    loadData();
}, { deep: true })


onMounted(() => {
    loadData()
})


</script>