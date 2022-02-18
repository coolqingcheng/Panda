<template>
    <el-card>
        <el-tabs lazy>
             <el-tab-pane label="站点配置">
                <Site></Site>
            </el-tab-pane>
            <el-tab-pane label="腾讯云COS配置">
                <Cos></Cos>
            </el-tab-pane>
            <el-tab-pane label="微信公众号">
                <WechatSetting></WechatSetting>
            </el-tab-pane>
            <el-tab-pane label="系统配置">
                <system></system>
            </el-tab-pane>
            <el-tab-pane label="定时任务"></el-tab-pane>
        </el-tabs>
    </el-card>
</template>

<script>
import TabItemContainer from '../TabItemContainer.vue'
import Cos from './cos.vue'
import Site from './site.vue'
import System from './system.vue'
import WechatSetting from './WechatSetting.vue'
export default {
    components: { TabItemContainer, Cos, Site, System, WechatSetting },

}
</script>

<style>
</style>