
<template>
  <el-card>
    <h1 @click="test()">Home {{ visible }}</h1>
    <cropper-box
      v-model="visible"
      @cropper="close"
      url="https://img2.baidu.com/it/u=1232855683,1763177858&fm=253&fmt=auto&app=138&f=PNG?w=499&h=270"
    ></cropper-box>
    <img :src="url" />
  </el-card>
</template>

<script lang="ts">
import { ref } from 'vue'
export default {
  setup() {
    const visible = ref(false)

    const url = ref('')

    const test = () => {
      visible.value = !visible.value
    }

    const close = (e: { base64: string }) => {
      url.value = e.base64

    }
    return {
      visible,
      test,
      close,
      url
    }
  }
}
</script>

<style>
</style>