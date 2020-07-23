import Vue from 'vue'
import Vuex from 'vuex'
import SongVM from "@/view-models/song-vm";

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        isUserAuthenticated: false,
        songAdded: {value: false},
        playSong: new SongVM(),
        queuedSongs: [] as SongVM[],
        defaultQueuedSongs: [] as SongVM[]
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
            const songVmClone = {...songVm} as SongVM;
            songVmClone.uniqueId = Date.now();
            state.queuedSongs.push(songVmClone);
        },

        removeFromQueue(state, songVm: SongVM) {
            const songIndex = state.queuedSongs.findIndex(x => x.uniqueId === songVm.uniqueId);
            state.queuedSongs.splice(songIndex, 1);
        },

        moveToTopOfQueue(state, songVM: SongVM) {
            const queuedSongIndex = state.queuedSongs.findIndex(x => x.uniqueId === songVM.uniqueId);
            if (queuedSongIndex > 0) {
                const queuedSong = state.queuedSongs.find(x => x.uniqueId === songVM.uniqueId);
                state.queuedSongs.splice(queuedSongIndex, 1);
                state.queuedSongs.unshift(queuedSong!);
            }
        },

        setDefaultQueuedSongs(state, songs: SongVM[]) {
            state.defaultQueuedSongs.length = 0;
            state.defaultQueuedSongs.push(...songs);
        }
    },
    actions: {
        playNextDefaultSong(context, currentSongWhichEnded: SongVM) {
            const currentSongIndex = context.state.defaultQueuedSongs.findIndex(x => x.id === currentSongWhichEnded.id);
            if (currentSongIndex >= 0 && currentSongIndex < context.state.defaultQueuedSongs.length - 1) {
                context.commit("playSong", context.state.defaultQueuedSongs[currentSongIndex + 1]);
            }
        }
    },
    modules: {}
})
