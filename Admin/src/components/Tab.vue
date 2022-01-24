<template>
  <div class="tab">
    <ul>
      <li
          v-for="item in tabList"
          :key="item.label"
          :class="item.active.indexOf(activePath) > -1 ? 'active' : ''"
      >
        <router-link :to="item.path">{{ item.label }}</router-link>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import {useRouter} from 'vue-router'
import {onMounted, ref} from 'vue';

export default {
  name: 'tab',
  setup() {
    const router = useRouter();
    router.afterEach(guard => {
      activePath.value = guard.path
    })

    const activePath = ref('')

    const tabList = ref([
      {
        label: '仪表盘',
        active: ['/admin/dash'],
        path: '/admin/dash'
      },
      {
        label: '文章',
        active: ['/admin/post', '/admin/post/write', '/admin/categories'],
        path: '/admin/post'
      },
      {
        label: '评论',
        active: '/admin/',
        path: '/'
      },
      {
        label: '标签',
        active: ['/admin/tag', '/admin/tag/edit'],
        path: '/admin/tag'
      },
      {
        label: '媒体',
        active: '/post',
        path: '/'
      },
      {
        label: '页面',
        active: ['/admin/pages', '/admin/pages-write'],
        path: '/admin/pages'
      },
      {
        label: '友情链接',
        active: ['/admin/friendlink'],
        path: '/admin/friendlink'
      },
      {
        label: '设置',
        active: ['/admin/setting'],
        path: '/admin/setting'
      }
    ])

    onMounted(() => {
      // console.log('path', router.currentRoute.value)
      let path = router.currentRoute.value.path;
      activePath.value = path
    })

    return {
      tabList,
      activePath
    }

  }
}
</script>

<style scoped lang="scss">
.tab {
  // $radius: 10px;
  display: flex;
  flex-direction: column;
  position: fixed;
  left: 0;
  width: 70px;
  bottom: 0;
  top: 0;
  background: #222b45;
  box-shadow: 3px 0px 6px rgba(0,0,0,.3);

  ul {
    padding-left: 0;
    list-style: none;
    display: flex;
    flex-direction: column;
    color: gray;
    max-height: 100%;

    li {
      font-size: 14px;
      height: 70px;
      box-sizing: border-box;
      display: flex;
      flex-direction: row;
      align-items: center;
      justify-content: center;

      a {
        color: #cecece;
      }

      &:hover {
        background: #334067;
        cursor: pointer;
      }
    }

    .active {
      a {
        color: white !important;
      }

      background: #334067;
    }
  }
}
</style>
