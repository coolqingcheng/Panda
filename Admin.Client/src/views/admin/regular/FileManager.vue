<template>
    <ElCard>
        <template #header>
            附件管理
        </template>
        <BasicTable v-bind="model">
            <ElTableColumn label="文件名称" prop="url"></ElTableColumn>
            <ElTableColumn label="后缀名" prop="suffix"></ElTableColumn>
            <ElTableColumn label="大小" prop="sizeName"></ElTableColumn>
            <ElTableColumn label="上传人" prop="account"></ElTableColumn>
            <ElTableColumn label="上传时间" prop="createTime"></ElTableColumn>
            <ElTableColumn label="存储方式" prop="strageType">
                <template #default="scope">
                    <ElTag>{{ scope.row.storageName }}</ElTag>
                </template>
            </ElTableColumn>
        </BasicTable>
    </ElCard>
</template>

<script lang="ts" setup>
import BasicTable from '@/components/BasicTable.vue';
import { BasePageModel } from '@/shared/ElConfig';
import { ResourceService } from "@/shared/service"
import { onMounted, reactive } from 'vue';

const model = reactive({
    data: [] as any[],
    index: 1,
    total: 0,
    loading:false
})

onMounted(() => {
    model.loading = true
    ResourceService.get({
        pageSize: 10,
        index: model.index
    }).then(res => {
        console.log('res:', res)
        model.data = res.data!;
        model.total = res.total!;
    }).finally(()=>model.loading = false)
})
</script>