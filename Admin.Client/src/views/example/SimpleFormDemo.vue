<template>
    <ElCard>
        <template #header>
            测试表单
        </template>
        <SimpleForm :="formOption" @ok="okHandler" :direction="formDirection">
        </SimpleForm>
        <ElButton @click="testHandler()">test</ElButton>
    </ElCard>
</template>

<script lang="ts" setup>
import { SimpleFormModel } from '@/components/SimpleFormModel';
import SimpleForm from '@/components/SimpleForm.vue';
import { reactive, ref } from 'vue';

const formDirection = ref<'v' | 'h'>('h')

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
    },
    {
        label: '选择项', value: '', type: 'select', name: 'select-demo',
        selectOption: {
            items: [
                { label: '选择1', value: 1 },
                { label: '选择2', value: 2 },
                { label: '选择3', value: 3 },
            ]
        }
    },
    {
        label: '开关', value: true, type: 'switch', name: 'switch-demo'
    }
])

const testHandler = () => {
    // formList.filter(a => a.label == '选择项')[0].selectOption?.items.push({
    //     label: '选择4', value: 4
    // })
    formDirection.value = formDirection.value == 'v' ? 'h' : 'v'

    console.log('更新了')
}

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