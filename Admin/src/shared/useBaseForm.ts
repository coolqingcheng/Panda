import { ElForm } from "element-plus";
import { reactive, ref } from "vue";

export function usBaseForm() {

    const loading = ref(false);

    const instance = ref<InstanceType<typeof ElForm>>();

    return {
        loading,
        instance
    }
}