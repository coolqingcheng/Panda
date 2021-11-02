
<template>
    <div class="tab">
        <ul>
            <li
                v-for="item in tabList"
                :key="item.label"
                :class="item.beginWith.indexOf(activePath) > -1 ? 'active' : ''"
            >
                <router-link :to="item.path">{{ item.label }}</router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts">
import { useRouter } from 'vue-router'
import { onMounted, ref } from 'vue';
export default {
    name: 'tab',
    setup() {
        const router = useRouter();
        router.afterEach(guard => {
            console.log('router:', guard)
            activePath.value = guard.path
        })

        const activePath = ref('')

        const tabList = ref([
            {
                label: '文章',
                beginWith: ['/dash', '/dash/post', '/dash/categories'],
                path: '/dash/post'
            },
            {
                label: '评论',
                beginWith: '/post',
                path: '/'
            },
            {
                label: '标签',
                beginWith: '/post',
                path: '/'
            },
            {
                label: '媒体',
                beginWith: '/post',
                path: '/'
            },
            {
                label: '页面',
                beginWith: ['/dash/pages', '/dash/pages-write'],
                path: '/dash/pages'
            },
            {
                label: '设置',
                beginWith: '/post',
                path: '/'
            }
        ])

        onMounted(() => {
            console.log('path', router.currentRoute.value)
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
    $radius: 10px;
    ul {
        background: #fff;
        padding-left: 0;
        margin: 0;
        list-style: none;
        display: inline-flex;
        flex-direction: row;
        border-top-left-radius: $radius;
        border-top-right-radius: $radius;
        color: white;
        &:first-child p {
            border-top-left-radius: $radius;
        }
        &:last-child p {
            border-top-right-radius: $radius;
        }
        li {
            padding: 10px 8px;
            border: 1px solid var(--el-border-color-light);
            font-size: 18px;
            a {
                color: black;
            }
        }
        .active {
            a {
                color: var(--el-color-primary) !important;
            }
        }
    }
}
</style>