<template>
    <h1>角色管理</h1>
    <BasicTable v-bind="{
        data: data,
        loading: false,
        total: 1000,
        index: 1
    }" @page-change="load">
        <ElTableColumn prop="date" label="Date" width="180" />
        <ElTableColumn prop="name" label="Name" width="180" />
        <ElTableColumn prop="address" label="Address" />
    </BasicTable>
</template>

<script lang="ts" setup>
import { onMounted, ref } from 'vue';


import BasicTable from '@/components/BasicTable.vue'

const v = ref('')

onMounted(() => {
    console.log('role-onMounted')
})

const data = [
    {
        date: '2016-05-03',
        name: 'Tom',
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-02',
        name: 'Tom',
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-04',
        name: 'Tom',
        address: 'No. 189, Grove St, Los Angeles',
    },
    {
        date: '2016-05-01',
        name: 'Tom',
        address: 'No. 189, Grove St, Los Angeles',
    },
]

const load = (index: number, size: number) => {
    console.log('load', index, size)
}


</script> 