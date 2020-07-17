import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        isUserAuthenticated: false,
        songAdded: {value:false}

    },
    mutations: {
        setIsUserAuthenticated(state, payload) {
            state.isUserAuthenticated = payload
        },
        setSongAdded(state) {
            state.songAdded = {
                value: true
            }
        }
    },
    actions: {
    },
    modules: {
    }
})
