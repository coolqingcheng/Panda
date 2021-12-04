
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
import { useRouter } from 'vue-router'
import { onMounted, ref } from 'vue';
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
            font-size: 16px;
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
