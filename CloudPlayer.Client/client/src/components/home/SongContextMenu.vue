<template>
    <v-menu @close="closeSongContextMenu" v-model="songContextMenu.open" absolute :position-x="songContextMenu.x" :position-y="songContextMenu.y">
        <v-list>
            <v-list-item v-if="!removeFromQueueButton" @click="addToQueue(songContextMenu.song)">
                <v-list-item-title>
                    Add to queue
                </v-list-item-title>
            </v-list-item>
            <v-list-item v-else @click="removeFromQueue(songContextMenu.song)">
                <v-list-item-title>
                    Remove from queue
                </v-list-item-title>
            </v-list-item>
        </v-list>
    </v-menu>
</template>

<script lang="ts">
    import Vue from "vue"
    import {Component, Prop, Watch} from "vue-property-decorator";
    import SongVM from "../../view-models/song-vm";

    @Component({
        name: "song-context-menu"
    })
    export default class SongContextMenu extends Vue {
        @Prop(Boolean)
        removeFromQueueButton = false;
        private songContextMenu = {
            open: false,
            x: 0,
            y: 0,
            song: new SongVM()
        };
        private rightClickedSong = new SongVM();
        closeSongContextMenu() {
            this.rightClickedSong = new SongVM();
            this.songContextMenu.open = false;
            this.songContextMenu.song = new SongVM();
        }

        addToQueue(song: SongVM) {
            this.$store.commit("addToQueue", song);
        }

        removeFromQueue(song: SongVM) {
            this.$store.commit("removeFromQueue", song);
        }

        openMenu(event: MouseEvent, songVM: SongVM) {
            event.preventDefault();
            this.songContextMenu.x = event.clientX;
            this.songContextMenu.y = event.clientY;
            this.songContextMenu.song = songVM;
            this.songContextMenu.open = true;
        }

    }
</script>

<style scoped>

</style>