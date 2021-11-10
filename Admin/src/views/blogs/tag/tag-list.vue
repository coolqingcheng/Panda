<template>
  <left-menu-layout>
    <template #menu>
      <router-link to="/admin/tag/edit">新建标签</router-link>
    </template>
    <template #content>
      <el-table border>
        <el-table-column label="标签名称"></el-table-column>
        <el-table-column label="文章数"></el-table-column>
        <el-table-column label="创建时间"></el-table-column>
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

<script>
import LeftMenuLayout from '../../../components/LeftMenuLayout.vue'
export default {
  components: { LeftMenuLayout },

}
</script>

<style>
</style>