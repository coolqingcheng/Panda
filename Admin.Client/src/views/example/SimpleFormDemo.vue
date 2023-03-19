<template>
    <SimpleForm :="formOption" @ok="okHandler">
    </SimpleForm>
</template>

<script lang="ts" setup>
import { SimpleFormModel } from '@/components/SimpleFormModel';
import SimpleForm from '@/components/SimpleForm.vue';
import { reactive } from 'vue';

const formList = reactive<SimpleFormModel[]>([
    {
        label: '测试', value: '', type: 'input', name: 'userName', placeholder: '输入账号'
    }
    ,
    {
        label: '用户Id', value: 200, type: 'input', name: 'id'
    },
    {
        label: '数量', value: 0, type: 'number', name: 'num'
    },
    {
        label: '选择时间', value: '', type: 'date', name: 'datetime',
        dateTimeOption: {
            type: 'datetime',
            format: 'YYYY-MM-DD HH:mm:ss'
        }
    },
    {
        label: '选择时间', value: '', type: 'date', name: 'date'
    },
    {
        label: '时间段选择', value: '', type: 'date', name: 'date-range',
        dateTimeOption: {
            type: 'daterange'
        }
    }
])

const formOption = reactive({
    items: formList,
    rules: {
        userName: {
            required: true,
            message: '用户名不能为空'
        }
    }
})



const okHandler = (params: any) => {
    console.log('pramas', params)
}

</script>