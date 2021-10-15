import { App } from "vue";

import FullScreen from "./FullScreen.vue";
import BreadCrumb from "./BreadCrumb.vue"
import RouteTab from "./RouteTab.vue"

export default {
    install(app: App<Element>) {
        app.component(FullScreen.name, FullScreen)
        app.component(BreadCrumb.name, BreadCrumb)
        app.component(RouteTab.name, RouteTab)
    }
}