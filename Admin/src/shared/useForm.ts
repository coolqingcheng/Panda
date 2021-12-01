import { ElForm } from "element-plus";
import { nextTick, ref } from "vue";

export function useForm() {

    const loading = ref(false);

    const instance = ref<InstanceType<typeof ElForm>>();

    const clearForm = () => {
        instance.value?.resetFields()
        nextTick(() => {
            instance.value?.clearValidate();
        })
    }

    return {
        loading,
        instance,
        clearForm
    }
}