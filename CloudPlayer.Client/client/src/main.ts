import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Statics from './shared/statics'

if (window.location.hostname.endsWith("cloudplayer.liborb.com")) {
    Statics.baseApiUrl = "/";
}
else {
    Statics.baseApiUrl = "https://localhost:5001/api/";
}

Vue.config.productionTip = false

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')
