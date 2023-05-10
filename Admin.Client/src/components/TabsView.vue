<template>
    <div class="v-tabs">
        <ElScrollbar ref="scrollbarRef" @scroll="onScroll">
            <ul>
                <li v-for="item in 30" :key="item">
                    <div>
                        菜单:{{ item }}
                    </div>
                    <div class="v-tabs-close" @click="close">
                        <el-icon>
                            <Close />
                        </el-icon>
                    </div>
                </li>
            </ul>
        </ElScrollbar>
        <div class="v-tabs-tools">
            <!-- <div>{{ scrollVaue }}</div> -->
            <div class="tools-button" @click="scrollHandler(-50)">
                <el-icon>
                    <ArrowLeft />
                </el-icon>
            </div>
            <div class="tools-button" @click="scrollHandler(50)">
                <el-icon>
                    <ArrowRight />
                </el-icon>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { Close, ArrowLeft, ArrowRight } from '@element-plus/icons-vue'
import { ref } from 'vue';
import { ElScrollbar } from 'element-plus'


const scrollbarRef = ref<InstanceType<typeof ElScrollbar>>();
const scrollVaue = ref(0);

const close = () => {
    console.log('关闭')
}
const onScroll = (item: {
    scrollTop: number;
    scrollLeft: number;
}) => {
    scrollVaue.value = item.scrollLeft;

}

const scrollHandler = (v: number) => {
    scrollbarRef.value?.setScrollLeft(scrollVaue.value + v)
}

</script>

<style lang="scss" scoped>
.v-tabs {
    width: 100%;
    box-sizing: border-box;
    background-color: var(--el-bg-color);
    border: 1px solid var(--el-border-color-light);
    user-select: none;
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;

    .v-tabs-tools {
        display: flex;
        flex-direction: row;
        border-left: 1px solid var(--el-border-color-light);
        border-right: 1px solid var(--el-border-color-light);

        .tools-button {
            width: 40px;
            height: 40px;
            line-height: 40px;
            border-right: 1px solid var(--el-border-color-light);
            text-align: center;

            &:hover {
                cursor: pointer;
            }
        }
    }

    ul {
        padding: 0;
        margin: 0;
        height: 100%;
        display: flex;
        flex-direction: row;
        list-style: none;
        align-items: center;

        li {
            padding: 0 0px 0 20px;
            height: 40px;
            display: flex;
            flex-direction: row;
            justify-content: center;
            align-items: center;
            border-right: 1px solid var(--el-border-color-light);
            border-bottom: 2px solid transparent;
            color: var(--el-text-color-regular);
            // color: green;
            white-space: nowrap;

            &.active {
                border-bottom: 2px solid var(--el-color-primary);
                color: var(--el-color-primary);
            }

            &:hover {
                border-bottom: 2px solid var(--el-color-primary-light-3);
                cursor: pointer;
            }

            .v-tabs-close {
                margin-left: 4px;
                margin-right: 4px;
                display: flex;
                flex-direction: row;
                align-items: center;
                opacity: 0;
                color: red;

                .el-icon {
                    transform: scale(1);
                }
            }

            &:hover {
                .v-tabs-close {
                    opacity: 1;
                    transition: all 0.5s;
                }

                .el-icon {
                    transform: scale(1.4);
                    transition: all 0.5s;
                    cursor: pointer;
                }
            }


        }
    }
}

.el-scrollbar .el-scrollbar__wrap .el-scrollbar__view {
    white-space: nowrap;
}
</style>