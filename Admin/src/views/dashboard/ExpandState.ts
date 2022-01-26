import {ref} from 'vue'

const expandState = ref(true)

const handlerExpand = () => {
    expandState.value = !expandState.value
}

export {
    expandState,
    handlerExpand
}