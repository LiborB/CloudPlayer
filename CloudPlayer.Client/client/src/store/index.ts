import Vue from 'vue'
import Vuex from 'vuex'
import SongVM from "@/view-models/song-vm";

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        isUserAuthenticated: false,
        songAdded: {value: false},
        playSong: new SongVM(),
        queuedSongs: [] as SongVM[]
    },
    mutations: {
        setIsUserAuthenticated(state, payload) {
            state.isUserAuthenticated = payload
        },
        setSongAdded(state) {
            state.songAdded = {
                value: true
            }
        },

        playSong(state, songVm: SongVM) {
            const newSongVM = new SongVM();
            newSongVM.id = songVm.id;
            newSongVM.title = songVm.title;
            newSongVM.duration = songVm.duration;
            state.playSong = newSongVM;
        },

        addToQueue(state, songVm: SongVM) {
            state.queuedSongs.push(songVm);
        },

        removeFromQueue(state, songVm: SongVM) {
            const songIndex = state.queuedSongs.findIndex(x => x.id === songVm.id);
            state.queuedSongs.splice(songIndex, 1);
        }
    },
    actions: {},
    modules: {}
})
