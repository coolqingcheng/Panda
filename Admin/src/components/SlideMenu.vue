<template>
  <el-menu default-active="1-1" :default-openeds="defaultOpened">
    <el-sub-menu :index="item.index" v-for="(item,index) in menuData" :key="index">
      <template #title>
        {{ item.label }}
      </template>
      <el-menu-item-group v-for="(item,i) in item.children" :key="i">
        <el-menu-item :index="item.index">
          {{ item.label }}
        </el-menu-item>
      </el-menu-item-group>
    </el-sub-menu>
  </el-menu>
</template>

<script lang="ts">
import {useRouter} from "vue-router"
import {onMounted, ref, defineComponent, PropType, watch, reactive} from 'vue'

export interface MenuItem {
  index: string
  label: string
  icon?: string
}

export interface SubMenu {
  index: string
  label: string
  icon?: string
  children: MenuItem[]
}

export default defineComponent({
  props: {
    subMenu: Object as PropType<SubMenu[]>
  },
  setup(props) {
    const currActivePath = ref<string>();

    const defaultOpened = ref(['1'])

    const menuData = reactive<SubMenu[]>([])


    watch(() => props.subMenu, () => {
      if (props.subMenu && props.subMenu.length > 0) {
        menuData.length = 0;
        menuData.push(...props.subMenu)
      }
    })

    const handleOpen = () => {

    }
    const handleClose = () => {
    }
    const router = useRouter();
    onMounted(() => {
      console.log(router.currentRoute.value.path)
      currActivePath.value = router.currentRoute.value.path
    })
    return {
      handleOpen,
      handleClose,
      currActivePath,
      defaultOpened,
      menuData
    }
  }
})
</script>

<style lang="less" scoped>
.q-slider-box {
  width: 240px;
  max-height: 100%;
  overflow-y: auto;

  .el-menu {
    border-right: none;
  }
}
</style>