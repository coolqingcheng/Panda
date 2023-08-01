import { onMounted, reactive } from "vue";
import { useRoute, useRouter } from "vue-router"

const memuList = [
    {
        title: "仪表盘",
        icon: "",
        routeName: "仪表盘"
    },
    {
        title: "博客",
        children: [
            {
                title: "文章"
            },
            {
                title: "分类"
            },
            {
                title: "标签"
            },
            {
                title: "友情连接"
            }
        ]
    }
]


/**
 * 侧边菜单生成
 * @returns 
 */
export function useSlideMenu() {
    const router = useRouter();
    const route = useRoute();
    const memuTree = reactive<MenuItem[]>([])

    onMounted(() => {
        initMenuList();
    })

    const getUrl = (item) => {
        let url = ''
        if (item.routeName) {
            if (item.title) {
                item.routeName = item.title
            }
            url = router.getRoutes().filter(a => a.name == item.routeName).map(a => a.path)[0]
        }
        return url;
    }

    const initMenuList = () => {
        memuList.forEach(item => {
            let url = getUrl(item)
            var children: MenuItem[] = []
            item.children?.forEach(childItem => {
                children.push({
                    title: childItem.title,
                    url: getUrl(childItem)
                })
            });
            memuTree.push({
                title: item.title,
                url: url,
                children: children
            })
        })
    };

    return {
        memuTree
    }
}

interface MenuItem {
    title: string
    icon?: string
    url: string
    children?: MenuItem[]
}