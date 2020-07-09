import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        isUserAuthenticated: false
    },
    mutations: {
        setIsUserAuthenticated(state, payload) {
            state.isUserAuthenticated = payload
        }
    },
    actions: {
    },
    modules: {
    }
})
