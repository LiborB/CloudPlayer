import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Statics from './shared/statics'
import vuetify from './plugins/vuetify';
import UserService from "@/services/user-service";

if (window.location.hostname.endsWith("cloudplayer.liborb.com")) {
    Statics.baseApiUrl = "api/";
} else {
    Statics.baseApiUrl = "https://localhost:5001/api/";
}

Vue.config.productionTip = false
const userToken = UserService.getUserTokenFromLocalStorage();
if (userToken) {
    UserService.isUserTokenValid(userToken).then(response => {
        if (response.data === true) {
            Statics.userToken = userToken;
            store.commit("setIsUserAuthenticated", true);
        }
        new Vue({
            router,
            store,
            vuetify,
            render: h => h(App)
        }).$mount('#app')
    });
}
else {
    new Vue({
        router,
        store,
        vuetify,
        render: h => h(App)
    }).$mount('#app')
}


