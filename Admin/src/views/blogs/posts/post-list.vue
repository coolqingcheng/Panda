<template>
  <left-menu>
    <template #menu>
      <button>test</button>
      <router-link to="post/write">写</router-link>
    </template>
    <template v-slot:content>
      <el-table :data="data.list" v-loading="loading">
        <el-table-column label="标题" prop="title"></el-table-column>
        <el-table-column label="分类" prop="categoryItems" width="200">
          <template #default="scope">
            <el-space>
              <el-tag v-for="(item,i) in scope.row.categoryItems" :key="i">{{ item.cateName }}</el-tag>
            </el-space>
          </template>
        </el-table-column>
        <el-table-column label="时间" prop="updateTime" width="150"></el-table-column>
        <el-table-column label="操作" fixed="right" width="150">
          <template #default="scope">
            <el-button type="default" size="mini" @click="edit(scope.row)">编辑</el-button>
            <el-button type="danger" size="mini" @click="edit(scope.row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        layout="total , prev, pager, next"
        :total="data.total"
        @current-change="currentChange"
      ></el-pagination>
    </template>
  </left-menu>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import { useRouter } from "vue-router";

import { get } from 'shared/http/HttpClient'

import { PageResponse } from 'shared/base'
import LeftMenu from "../../../components/LeftMenu.vue";

interface ArticleItem {
  id: number
  title: string
  updateTime: string
  categoryItems: Categories[]
}



interface Categories {
  id: number
  cateName: string
}

export default defineComponent({
  components: { LeftMenu },
  setup() {

    const router = useRouter();
    const edit = (item: ArticleItem) => {
      router.push({ path: "/article-write", query: { id: item.id } })
    }

    const data = ref<{ list: ArticleItem[], total: number }>({
      list: [],
      total: 0
    });

    var params = ref({
      index: 1, size: 10,
      keyword: ''
    })

    const loading = ref(false)

    const loadData = async () => {
      loading.value = true
      try {
        var res = await get<PageResponse<ArticleItem>>('/admin/article/getlist', params.value)
        data.value.list = res.data
        data.value.total = res.total
      } finally {
        loading.value = false
      }
    }
    onMounted(() => {
      loadData();
    })
    const currentChange = async (index: number) => {
      params.value.index = index;
      await loadData();
    }
    return {
      edit,
      data,
      loading,
      currentChange
    }
  }
})
</script>

<style>
</style>