<template>
  <div class="tab">
    <ul>
      <li class="q-module-item"
          v-for="item in moduleList"
          :key="item.label"
          :class="item.active ? 'active' : ''" @click="handlerItem(item)">
        <!--        <router-link :to="item.path">{{ item.label }}</router-link>-->
        <i v-if="item.icon" :class="item.icon"></i>
        <a href="#">{{ item.label }}</a>
      </li>
    </ul>
  </div>
  <div class="q-menu-container" :class="[isOpen?'q-menu-open':'q-menu-close']">
    <SlideMenu :sub-menu="[]"></SlideMenu>
  </div>
</template>

<script lang="ts">
import {useRouter} from 'vue-router'
import {onMounted, ref, computed, defineComponent} from 'vue';
import SlideMenu, {SubMenu} from "./SlideMenu.vue";
import {reactive} from "@vue/reactivity";

export interface ModuleItem {
  label: string
  begin: string
  icon?: string
  subMenu?: SubMenu[]
  active?: boolean
}

export default defineComponent({
  name: 'tab',
  components: {SlideMenu},
  props: {
    open: Boolean
  },
  setup(props) {
    const router = useRouter();
    router.afterEach(guard => {
      activePath.value = guard.path
    })

    const isOpen = computed(() => {
      console.log('props变化:' + props.open)
      return props.open
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

    const moduleList = reactive<ModuleItem[]>([
      {
        label: '仪表盘',
        begin: '/dash',
        icon: 'ri-dashboard-2-line'
      },
      {
        label: '文章',
        begin: '/dash',
        active: true,
        icon: 'ri-article-line'
      },
      {
        label: '评论',
        begin: '/dash',
        icon: 'ri-message-3-line'
      }, {
        label: '标签',
        begin: '/dash',
        icon: 'ri-price-tag-3-line'
      },
      {
        label: '友链',
        begin: '/dash',
        icon: 'ri-links-line'
      },
      {
        label: '设置',
        begin: '/dash',
        icon: 'ri-settings-3-line'
      }
    ])

    onMounted(() => {
      // console.log('path', router.currentRoute.value)
      let path = router.currentRoute.value.path;
      activePath.value = path
    })

    const handlerItem = () => {

    }

    return {
      tabList,
      activePath,
      isOpen,
      handlerItem,
      moduleList
    }

  }
})
</script>

<style lang="scss">
.tab {
  // $radius: 10px;
  display: flex;
  flex-direction: column;
  position: fixed;
  left: 0;
  width: 70px;
  bottom: 0;
  top: 0;
  background: #282c34;
  box-shadow: 3px 0px 6px rgba(0, 0, 0, .3);
  z-index: 999;

  .q-module-item {
    display: flex;
    flex-direction: column;
    align-items: center;
    i {
      font-size: 24px;
      margin-bottom: 5px;
    }
  }

  ul {
    padding-left: 0;
    list-style: none;
    display: flex;
    flex-direction: column;
    color: gray;
    max-height: 100%;
    align-items: center;

    li {
      font-size: 12px;
      width: 56px;
      height: 56px;
      border-radius: 5px;
      box-sizing: border-box;
      display: flex;
      flex-direction: row;
      align-items: center;
      justify-content: center;
      margin-top: 8px;

      a {
        color: #cecece;
      }

      &:hover {
        background: #1890ff;
        cursor: pointer;
        color: white;
      }
    }

    .active {
      a {
        color: white !important;
      }

      color: white;

      background: #1890ff;
    }
  }


}

.q-menu-open {
  left: 70px;
}

.q-menu-close {
  left: -130px;
}

.q-menu-container {
  width: 200px;
  transition: left 0.3s linear;
  position: fixed;
  top: 0;
  bottom: 0;
  background: #ffffff;
  z-index: 900;

  .el-menu-item-group {
    padding: 0 10px 0 10px;
  }

  .el-menu-item.is-active {
    border-radius: 10px;
    background: var(--el-color-primary-light-8);
    color: #0d1b25;
  }

  .el-sub-menu .el-menu-item {
    min-width: 180px;

    &:hover {
      border-radius: 10px;
    }
  }
}
</style>
