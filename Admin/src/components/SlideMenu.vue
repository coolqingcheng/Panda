<template>
  <el-menu :default-active="currActivePath" :default-openeds="defaultOpened" router>
    <template v-for="(item, index) in menuData" :key="index">
      <el-sub-menu :index="item.index" v-if="!item.hideGroup">
        <template #title>{{ item.label }} - {{ item.hideGroup }}</template>
        <el-menu-item-group v-for="(item, i) in item.children" :key="i">
          <el-menu-item :index="item.index">{{ item.label }}</el-menu-item>
        </el-menu-item-group>
      </el-sub-menu>
      <template v-for="(item, i) in item.children" :key="i" v-if="item.hideGroup">
        <el-menu-item :index="item.index">{{ item.label }}</el-menu-item>
      </template>
    </template>

  </el-menu>
</template>

<script lang="ts">
import {useRouter} from "vue-router"
import {onMounted, ref, defineComponent, PropType, watch, reactive, nextTick} from 'vue'
import {SubMenu} from "./MenuConfig"


export default defineComponent({
  props: {
    subMenu: Object as PropType<SubMenu[]>
  },
  setup(props) {
    const currActivePath = ref<string>();

    const defaultOpened = ref([])


    const menuData = reactive<SubMenu[]>([])
    if (props.subMenu) {
      menuData.push(...props.subMenu)
    }

    watch(() => props.subMenu, () => {
      if (props.subMenu && props.subMenu.length > 0) {
        menuData.length = 0;
        menuData.push(...props.subMenu)
        console.log(props.subMenu)
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
      nextTick(() => {

      })
    })
    router.afterEach(() => {
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