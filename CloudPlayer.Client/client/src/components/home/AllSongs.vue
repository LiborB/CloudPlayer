<template>
    <v-card height="70vh" outlined>
        <v-data-table class="song-table" style="height: 100%" item-class="h1" :items="songs" :headers="headers"
                      :search="search" disable-pagination @current-items="currentSongsChange"
                      hide-default-footer>
            <template slot="top">
                <v-text-field v-model="search" label="Search" class="mx-4"></v-text-field>
            </template>
            <template v-slot:body="{items}">
                <tbody>
                <tr @contextmenu="openSongContextMenu($event, item)" @dblclick="playSong(item)" class="song-row"
                    @click="selectedItem = item"
                    :class="{'selected-row': selectedItem.id === item.id, 'current-playing-row': currentPlayingItem.id === item.id}"
                    v-for="item in items" :key="item.id">
                    <td>
                        <v-icon color="primary" v-if="currentPlayingItem.id === item.id">mdi-music-note-outline</v-icon>&nbsp;{{item.title}}
                    </td>
                    <td>{{secondsToDurationFormat(item.duration)}}</td>
                </tr>
                </tbody>

            </template>
        </v-data-table>
        <song-context-menu ref="contextMenu"></song-context-menu>
    </v-card>
</template>

<script lang="ts">
    import {Component, Mixins, Ref, Watch,} from "vue-property-decorator";
    import SongService from "@/services/song-service";
    import SongVM from "@/view-models/song-vm";
    import TimeUtility from "@/components/mixins/TimeUtility.vue";
    import SongContextMenu from "@/components/home/SongContextMenu.vue";

    @Component({
        name: "all-songs",
        components: {SongContextMenu}
    })
    export default class AllSongs extends Mixins(TimeUtility) {
        private songs: SongVM[] = [];
        private search = "";
        private selectedItem: SongVM = new SongVM();
        private headers = [
            {
                text: "Title",
                value: "title"
            },
            {
                text: "Duration",
                value: "duration"
            }
        ]
        @Ref("contextMenu")
        contextMenuRef!: SongContextMenu;

        mounted() {
            this.getSongs();
        }

        getSongs() {
            SongService.getAllSongs().then(response => {
                this.songs = response.data;
            });
        }

        get songAdded() {
            return this.$store.state.songAdded;
        }

        @Watch('songAdded')
        songAddedHandler() {
            this.getSongs();
        }

        get currentPlayingItem() {
            return this.$store.state.playSong;
        }

        playSong(song: SongVM) {
            this.$store.commit("playSong", song);
        }

        openSongContextMenu(event: MouseEvent, songVM: SongVM) {
            (this.contextMenuRef as any).openMenu(event, songVM);
        }

        currentSongsChange(songs: SongVM[]) {
            this.$store.commit("setDefaultQueuedSongs", songs);
        }
    }
</script>

<style scoped lang="scss">
    @import "../../styles/variables";

    .selected-row {
        //
    }

    .song-row:hover {
        cursor: pointer;
    }

    .song-row {
        user-select: none;
    }

    .current-playing-row {
        color: $primary;
        font-weight: bold;
    }

    .song-table {
        overflow: auto;
        height: 100%;
    }
</style>