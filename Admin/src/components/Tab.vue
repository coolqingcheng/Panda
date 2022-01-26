<template>
  <div class="tab">
    <ul>
      <li
          class="q-module-item"
          v-for="(item, i) in moduleList"
          :key="i"
          :class="item.active ? 'active' : ''"
          @click="handlerItem(item,true)"
      >
        <i v-if="item.icon" :class="item.icon"></i>
        <span>{{ item.label }}</span>
      </li>
    </ul>
  </div>
  <div class="q-menu-container" :class="[isOpen ? 'q-menu-open' : 'q-menu-close']">
    <template v-for="(item, i) in moduleList">
      <SlideMenu :sub-menu="item.subMenu" v-if="item.active"></SlideMenu>
    </template>
  </div>
</template>

<script lang="ts">
import {useRouter} from 'vue-router'
import {onMounted, ref, computed, defineComponent, nextTick} from 'vue';
import SlideMenu from "./SlideMenu.vue";
import {reactive} from "@vue/reactivity";
import {SubMenu, MenuItem, PostMenu, settingMenu, tagMenu} from "./MenuConfig"
import {forEach} from "wangeditor/dist/utils/util";

export interface ModuleItem {
  label: string
  index?: string
  icon?: string
  subMenu?: SubMenu[]
  active?: boolean
}

export default defineComponent({
  name: 'tab',
  components: {SlideMenu},
  props: {
    expand: Boolean
  },
  emits: ['update:expand'],
  setup(props, context) {
    const router = useRouter();
    router.afterEach(guard => {
      activePath.value = guard.path

    })


    const isOpen = computed(() => {
      console.log('props变化:' + props.expand)
      return props.expand
    })

    const activePath = ref('')

    onMounted(() => {
      activePath.value = router.currentRoute.value.path
      nextTick(() => {
        updateSelectStatus();
      })
    })

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
        active: true,
        index: '/admin/dash',
        icon: 'ri-dashboard-2-line'
      },
      {
        label: '文章',
        icon: 'ri-article-line',
        subMenu: PostMenu
      },
      {
        label: '评论',
        icon: 'ri-message-3-line'
      },
      {
        label: '标签',
        index: '/admin/tag',
        icon: 'ri-price-tag-3-line',
        subMenu: tagMenu

      },
      {
        label: '友链',
        icon: 'ri-links-line',
        index: '/admin/friendlink'
      },
      {
        label: '设置',
        index: '/admin/setting',
        icon: 'ri-settings-3-line'
      }
    ])

    const handlerItem = (item: ModuleItem, updateRouter: boolean) => {
      moduleList.forEach(temp => {
        temp.active = temp == item;
        if (temp.active) {
          if (!temp.subMenu || temp.subMenu.length == 0) {
            context.emit('update:expand', false)
          } else {
            context.emit('update:expand', true)
          }
          if (updateRouter) {
            if (temp.subMenu && temp.subMenu.length > 0) {
              router.replace(temp.subMenu[0].children[0]?.index)
            } else {
              router.replace(temp.index!)
            }
          }
        }
      });


    }
    const updateSelectStatus = () => {
      console.log('当前的路由:', activePath.value)
      let path = activePath.value;
      moduleList.forEach(item => {
        if (item.index == path) {
          handlerItem(item, false)
        }
        item.subMenu?.forEach(subItem => {
          if (subItem.label == path) {
            handlerItem(item, false)
          }
          subItem.children?.forEach(childrenItem => {
            if (childrenItem.index == path) {
              handlerItem(item, false)
            }
          })
        })
      })
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
  box-shadow: 3px 0px 6px rgba(0, 0, 0, 0.3);
  z-index: 999;

  .q-module-item {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 8px 0 8px 0;
    box-sizing: border-box;

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
  padding-top: 10px;
  box-sizing: border-box;

  .el-menu-item-group {
    padding: 0 10px 0 10px;
  }

  .el-menu-item {
    margin: 0 10px 0 10px;
    box-sizing: border-box;
    border-radius: 10px;
  }

  .el-menu-item.is-active {

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
