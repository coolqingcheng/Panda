<template>
  <el-container>
    <el-aside width="220px" v-if="false">
      <div class="q-logo">
        <p>Admin</p>
      </div>
      <SlideMenu></SlideMenu>
    </el-aside>
    <el-container>
      <el-header v-if="true" :class="[isOpen?'':'el-header-close']">
        <div class="q-header-container">
          <div class="q-header-left">
            <div class="q-header-item" @click="expandMenu()">
              <div class="q-toolbar-item pointer">
                <i class="ri-menu-fold-line"></i>
              </div>
            </div>
            <div class="q-header-item">
              <el-breadcrumb :separator-icon="ArrowRight">
                <el-breadcrumb-item :to="{ path: '/' }">主页 {{ isOpen }}</el-breadcrumb-item>
                <el-breadcrumb-item>当前页面</el-breadcrumb-item>
              </el-breadcrumb>
            </div>
          </div>
          <div class="q-toolbar">
            <el-popover trigger="hover" :width="300">
              <template #reference>
                <div class="q-toolbar-item">
                  <i class="ri-notification-line"></i>
                </div>
              </template>
              <div class="notify-box">
                <el-tabs v-model="notifySelect">
                  <el-tab-pane label="通知" name="notify">通知</el-tab-pane>
                  <el-tab-pane label="消息" name="message">消息</el-tab-pane>
                  <el-tab-pane label="代办" name="todo">待办</el-tab-pane>
                </el-tabs>
              </div>
            </el-popover>
            <div class="q-toolbar-item">
              <q-fullscreen></q-fullscreen>
            </div>
            <el-dropdown trigger="hover">
              <div class="q-toolbar-item">
                <el-avatar :size="30" src="../../assets/logo.jpeg"></el-avatar>
              </div>
              <template #dropdown>
                <el-dropdown-menu>
                  <el-dropdown-item>主页</el-dropdown-item>
                  <el-dropdown-item>个人设置</el-dropdown-item>
                  <el-dropdown-item divided @click="quit()">退出</el-dropdown-item>
                </el-dropdown-menu>
              </template>
            </el-dropdown>
          </div>
        </div>
      </el-header>
      <el-main>
        <tab :open="isOpen"></tab>
        <div :class="[isOpen?'q-content-open':'q-content-close']" class="q-content">
          <el-card>
            <router-view></router-view>
          </el-card>
        </div>
      </el-main>
    </el-container>
  </el-container>
</template>
<script lang="ts">
import SlideMenu from "../../components/SlideMenu.vue";
import {Expand, FullScreen} from '@element-plus/icons'
import {useRouter} from "vue-router";
import {ref} from "@vue/reactivity";
import {http} from "shared/http/HttpClient"

export default {
  components: {
    SlideMenu,
    Expand,
    FullScreen
  },
  setup() {
    const router = useRouter()
    const quit = async () => {
      try {
        await http.get('/admin/account/LoginOut')
        router?.replace({
          path: '/login'
        })
      } catch (error) {

      }
    }
    const fullScreen = () => {
    }

    const isOpen = ref(true)

    const expandMenu = () => {
      isOpen.value = !isOpen.value
    }

    const notifySelect = ref('notify')

    return {
      quit,
      fullScreen,
      notifySelect,
      isOpen,
      expandMenu
    }
  }
}
</script>
<style lang="less" scoped>

.q-content {
  margin-top: 60px;
  background: #f6f8f9;
  transition: margin-left 0.3s linear;
}

.q-content-open {
  margin-left: 270px;
}

.q-content-close {
  margin-left: 70px;
}

.el-container {
  height: 100%;

  .el-aside {
    overflow-x: hidden;
    overflow-y: hidden;
    box-shadow: 10px 0 10px -10px #c7c7c7;
    z-index: 99;

    .q-logo {
      width: 100%;
      height: 60px;
      text-align: center;
      line-height: 60px;
      font-size: 30px;
      font-weight: 400;
    }
  }

  .el-header-close {
    left: 70px !important;
    transition: left 0.3s linear;
  }

  .el-header {
    background: #fff;
    box-shadow: 0 0 1px #ccc;
    display: flex;
    flex-direction: row;
    align-items: center;
    box-sizing: border-box;
    padding: 5px;
    border-bottom: 0.5px solid #dcdfe6;
    position: fixed;
    left: 270px;
    right: 0;
    height: 60px;
    z-index: 999;
    transition: left 0.3s linear;

    .q-header-container {
      display: flex;
      width: 100%;
      flex-direction: row;
      justify-content: space-between;

      .q-header-left {
        display: flex;
        flex-direction: row;
        align-items: center;
      }

      .q-header-item {
        margin-left: 15px;
        min-height: 45px;
        display: flex;
        flex-direction: row;
        align-items: center;
        min-width: 45px;
        box-sizing: border-box;
      }
    }

    .q-expand {
      height: 60px;
      width: 60px;
      cursor: pointer;
      display: flex;
      flex-direction: row;
      justify-content: center;
      align-items: center;
    }
  }

  .q-toolbar {
    display: flex;
    flex-direction: row;

    .q-toolbar-item {
      height: 100%;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      padding: 0 0.8rem;
      cursor: pointer;
    }

    .el-tabs__item {
      width: 1/3;
    }
  }
}

.notify-box {
  width: 100%;

  .el-tabs__nav {
    width: 100%;

    .el-tabs__item {
      width: 33.33%;
      text-align: center;
    }
  }
}

.el-main {
  box-sizing: border-box;
  padding: 0;

  .el-main-container {
    background: white;
    width: 100%;
    // height: 100%;
  }

  h1 {
    margin: 0;
    padding: 0;
  }
}
</style>