<template>
  <left-menu-layout>
    <template #menu>
      <router-link to="/admin/tag/edit">新建标签</router-link>
    </template>
    <template #content>
      <el-table border v-loading="loading" :data="data?.data">
        <el-table-column label="标签名称" prop="tagName"></el-table-column>
        <el-table-column label="文章数" prop="count"></el-table-column>
        <el-table-column label="操作">
          <template #default="scope">
            <el-button type="default" size="mini" @click="edit(scope.row)">编辑</el-button>
            <el-button type="danger" size="mini" @click="del(scope.row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        layout="total , prev, pager, next"
        :total="data?.total"
        @current-change="currentChange"
      ></el-pagination>
    </template>
  </left-menu-layout>
</template>

<script lang="ts">
import { onMounted, ref } from 'vue'
import LeftMenuLayout from '../../../components/LeftMenuLayout.vue'
import { get } from "shared/http/HttpClient"
import { PageResponse } from 'shared/base'

interface TagItem {
  tagName: ''
  id: 0
  count: 0
}

export default {
  components: { LeftMenuLayout },
  setup() {
    const data = ref<PageResponse<TagItem[]>>()
    const load = () => {
      get('/admin/tag/getlist', page)
        .then((res: any) => {
          console.log(res)
          data.value = res
        })
    }
    const page = {
      index: 1,
      size: 10
    }
    const loading = ref(false)
    onMounted(() => {
      load();
    })

    const edit = (item: TagItem) => {

    }
    const del = (item: TagItem) => {

    }

    const currentChange = (index: number) => {
      page.index = index;
      load();
    }
    return {
      loading,
      edit,
      del,
      currentChange,
      data
    }
  }
}
</script>

<style>
</style>