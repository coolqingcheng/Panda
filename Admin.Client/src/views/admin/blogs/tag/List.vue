<template>
    <ElCard>

        <template #header>
            标签
        </template>
        <ElTable :data="model.data" v-bind="table">
            <ElTableColumn label="Id" prop="id"></ElTableColumn>
            <ElTableColumn label="标签" prop="tagName"></ElTableColumn>
            <ElTableColumn label="关联文章" prop="count"></ElTableColumn>
            <ElTableColumn label="操作">
                <template #default="scope">
                    <ElSpace>
                        <ElButton :icon="Edit"></ElButton>
                        <ElButton :icon="Delete" type="danger"></ElButton>
                    </ElSpace>
                </template>
            </ElTableColumn>
        </ElTable>
        <ElPagination v-bind="pagination" :total="model.total" v-model:current-page="page.index"
            v-model:page-size="page.pageSize">
        </ElPagination>
    </ElCard>
</template>

<script  lang="ts" setup>
import { onMounted, reactive, ref } from 'vue';
import { Edit, Delete } from '@element-plus/icons-vue';

import { PostTagService, TagDtoModel } from '@/shared/service'
import {table,pagination} from '@/shared/ElConfig'

const loading = ref(true)

const model = reactive<{
    total: number,
    data: TagDtoModel[]
}>({
    total: 0,
    data: []
})

const page = reactive({
    index: 1,
    pageSize: 10
})

const save = () => {
    loading.value = true;
    PostTagService.getList({
        ...page
    }).then((res) => {
        if (res.total) {
            model.total = res.total
        }
        model.data = []
        if (res.data) {
            model.data.push(...res.data)
        }
    }).finally(() => {
        loading.value = false
    })
}

onMounted(() => {
    save();
})

</script>