

<script lang="ts" setup>
import { get } from 'shared/http/HttpClient';
import { onMounted, ref } from 'vue';
import LeftMenuLayout from '../../../components/LeftMenuLayout.vue'

const tableData = ref<{ id: number, tagName: string, count: number }[]>();
const load = async () => {
  get('/admin/tag/getlist', {}).then((res: any) => {
    console.log(res)
    tableData.value = res.data
  })
}
onMounted(() => {
  load();
})

</script>
<template>
  <left-menu-layout>
    <template #menu>
      <router-link to="/admin/tag/edit">新建标签</router-link>
    </template>
    <template #content>
      <el-table border :data="tableData">
        <el-table-column label="标签名称" prop="tagName"></el-table-column>
        <el-table-column label="文章数" prop="count"></el-table-column>
        <el-table-column label="操作">
          <template #default="scope">
            <el-button type="default" size="mini">编辑</el-button>
            <el-button type="danger" size="mini">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </template>
  </left-menu-layout>
</template>