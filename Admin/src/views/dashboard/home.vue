<template>
  <div class="dash-content">
    <el-row :gutter="20">
      <el-col :xs="24" :sm="24" :md="12" :lg="8" :xl="6" v-for="item in 4" :key="item">
        <el-card shadow="never" class="dash-card">
          访问统计
        </el-card>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <el-card shadow="never">
          <template #header>
            <h2>今日评论</h2>
          </template>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts">
import {ref} from 'vue'

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

<style lang="scss">
.dash-content {
  padding: 10px;
}

.dash-card {
  min-height: 170px;
  margin-bottom: 20px;
}
</style>