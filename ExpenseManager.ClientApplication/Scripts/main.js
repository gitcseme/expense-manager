import Vue from "vue";
import App from "./App";
import router from "./router/index";

import PaperDashboard from "./plugins/paperDashboard";
import "vue-notifyjs/themes/default.css";

Vue.use(PaperDashboard);

// Configurations
import VueConfirmDialog from "vue-confirm-dialog";
import "./configurations/Axios";
import "./configurations/VeeValidate";
import "@scripts/Global/global_registration";
import ApplicationContextService from "@scripts/Services/ApplicationContextService";

Vue.use(VueConfirmDialog);
Vue.component("vue-confirm-dialog", VueConfirmDialog.default);

// *** //

/* eslint-disable no-new */
ApplicationContextService.setup().then(() => {
    let appContext = ApplicationContextService.getContext();

    new Vue({
        router,
        render: h => h(App)
    }).$mount("#app");

    if (appContext.user == null) {
        router.push({ name: 'login' });
    }
    console.log('main.js -> then');
})
.catch(() => {
    new Vue({
        router,
        render: h => h(App)
    }).$mount("#app");
    router.push({ name: 'login' });
    console.log('main.js -> catch');
});
