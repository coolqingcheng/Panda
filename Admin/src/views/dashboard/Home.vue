<template>
  <div class="dash-content">
    <el-row :gutter="20">
      <el-col v-bind="grid">
        <el-card shadow="never" class="dash-card" style="background: #1976D2;" v-loading="loading">
          <template #header>
            <h4>文章数</h4>
          </template>
          <p class="dash-card-value">{{ model.postCount }}</p>
        </el-card>
      </el-col>
      <el-col v-bind="grid">
        <el-card shadow="never" class="dash-card" style="background: #F57C00" v-loading="loading">
          <template #header>
            <h4>今日IP数</h4>
          </template>
          <p class="dash-card-value">{{ model.ipCount }}</p>
        </el-card>
      </el-col>
      <el-col v-bind="grid">
        <el-card shadow="never" class="dash-card" style="background: #5D4037" v-loading="loading">
          <template #header>
            <h4>图片数量</h4>
          </template>
          <p class="dash-card-value">4546<span>张</span></p>
        </el-card>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <el-card shadow="never">
          <template #header>
            <h2>最近访问</h2>
          </template>
          <el-table :data="list">
            <el-table-column label="访问地址" prop="url"></el-table-column>
            <el-table-column label="Ip" prop="ip"></el-table-column>
            <el-table-column label="时间" prop="addTime"></el-table-column>
            <el-table-column label="Id" prop="uId"></el-table-column>
            <el-table-column label="浏览器" prop="browser"></el-table-column>
            <el-table-column label="操作系统" prop="os"></el-table-column>
          </el-table>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, reactive } from 'vue'

import { get } from "shared/http/HttpClient"

import { ElTable, ElTableColumn } from "element-plus"
import { react } from '@babel/types';

const loading = ref(false)

const grid = ref({
  xs: 24, sm: 24, md: 12, lg: 8, xl: 6
})

const model = ref({
  postCount: 0,
  ipCount: 0
})

const list = reactive([

])

const page = ref(1)

const url = ref('')

const close = (e: { base64: string }) => {
  url.value = e.base64

}

onMounted(() => {
  get('/admin/statistic/get', {}).then((res: any) => {
    model.value = res
  })
  loadRecentAccess();
})

const loadRecentAccess = () => {
  get('/admin/statistic/GetRecentAccessRecord', { page: page.value }).then((res: any) => {
    console.log(res)
    list.length = 0;
    list.push(...res.data)
  })
}

</script>

<style lang="scss">
body {
  font-family: 'Helvetica Neue', Helvetica, 'PingFang SC', 'Hiragino Sans GB',
    'Microsoft YaHei', '微软雅黑', Arial, sans-serif;
}

.dash-content {
  padding: 10px;
}

.dash-card {
  min-height: 170px;
  margin-bottom: 20px;

  color: white;

  .el-card__header {
    border-bottom: none;
  }
}

.dash-content {
  h4 {
    margin: 0;
  }

  .dash-card-value {
    font-size: 2rem;
    font-weight: 400;

    span {
      font-size: 1rem;
      margin-left: 5px;
    }
  }
}
</style>