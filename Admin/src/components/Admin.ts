import { App } from "vue";

import FullScreen from "./FullScreen.vue";
import BreadCrumb from "./BreadCrumb.vue"
import RouteTab from "./RouteTab.vue"
import Tab from "./Tab.vue"
import LeftMenu from "./LeftMenu.vue"

export default {
    install(app: App<Element>) {
        app.component(FullScreen.name, FullScreen)
        app.component(BreadCrumb.name, BreadCrumb)
        app.component(RouteTab.name, RouteTab)
        app.component(Tab.name, Tab)
        app.component(LeftMenu.name, LeftMenu)
    }
}