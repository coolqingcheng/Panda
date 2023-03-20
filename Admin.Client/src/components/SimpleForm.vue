<template>
    <ElRow>
        <ElCol>
            <ElForm ref="formRef" :label-width="labelWidth" :rules="rules" :model="fromModel">
                <ElRow>
                    <ElCol :="formGrid" v-for="item in items" :key="item.name">
                        <ElFormItem :label="item.label" :prop="item.name">
                            <template v-if="item.type == 'input'">
                                <ElInput v-model="fromModel[item.name]" :placeholder="item.placeholder"></ElInput>
                            </template>
                            <template v-if="item.type == 'number'">
                                <ElInputNumber v-model="fromModel[item.name]" :placeholder="item.placeholder" />
                            </template>
                            <template
                                v-if="['year', 'month', 'date', 'datetime', 'week', 'datetimerange', 'daterange'].indexOf(item.type) > 0">
                                <ElDatePicker v-model="fromModel[item.name]" :type="item.dateTimeOption?.type"
                                    :format="item.dateTimeOption?.format" :value-format="item.dateTimeOption?.format">
                                </ElDatePicker>
                            </template>
                            <template v-if="item.type == 'select'">
                                <ElSelect v-model="fromModel[item.name]" :placeholder="item.placeholder">
                                    <ElOption v-for="itemOption in item.selectOption?.items" :label="itemOption.label"
                                        :value="itemOption.value" :key="itemOption.value">
                                    </ElOption>
                                </ElSelect>
                            </template>
                            <template v-if="item.type == 'switch'">
                                <ElSwitch v-model="fromModel[item.name]" :placeholder="item.placeholder"></ElSwitch>
                            </template>
                        </ElFormItem>
                    </ElCol>
                </ElRow>
                <ElFormItem>
                    <ElSpace>
                        <ElButton type="primary" @click="submitHandler()">{{ submitText }}</ElButton>
                        <ElButton>{{ clearnText }}</ElButton>
                    </ElSpace>
                </ElFormItem>
            </ElForm>
        </ElCol>
    </ElRow>
</template>
<script lang="ts" setup>
import { ElForm, FormInstance, FormRules } from 'element-plus';
import { keysOf } from 'element-plus/es/utils';
import { ref, reactive, onMounted, watch } from 'vue';
import { SimpleFormModel } from './SimpleFormModel';

const formGrid = reactive({
    xs: 24,
    sm: 24,
    md: 12,
    lg: 8,
    xl: 6
})

const h = {
    xs: 24,
    sm: 24,
    md: 12,
    lg: 8,
    xl: 6
}

const v = {
    xs: 24,
    sm: 24,
    md: 24,
    lg: 24,
    xl: 24
}

const fromModel = reactive<Record<string, any>>({});


const props = withDefaults(defineProps<{
    items: SimpleFormModel[],
    submitText?: string
    clearnText?: string,
    direction?: 'v' | 'h'  // v竖 h横
    rules: FormRules,
    labelWidth?: number
}>(), {
    submitText: '确定', clearnText: '取消', direction: 'h', labelWidth: 100
})

// const formModel = reactive()

onMounted(() => {
    props.items.forEach(a => {
        fromModel[a.name] = a.value;
    })
    updateDir()
})

const updateDir = () => {
    console.log('update direction:', props.direction)
    let item = props.direction=='v'?v:h
        Object.assign(formGrid, item)
    console.log('update direction:', formGrid)
}

watch(() => props.direction, updateDir)



const submitHandler = () => {
    formRef.value?.validate((valid) => {
        console.log(valid)
        if (valid) {
            console.log('验证通过', fromModel)
            let params: Record<string, any> = {
                ...fromModel
            }
            emit('ok', params)
        }
    })
}

const emit = defineEmits<{
    (e: 'ok', value: Record<string, any>): void
}>()



const formRef = ref<FormInstance>();

</script>