<template>
    <v-card height="70vh" >
        <v-data-table class="song-table" style="height: 100%" item-class="h1" :items="songs" :headers="headers" :search="search" disable-pagination
                      hide-default-footer>
            <template slot="top">
                <v-text-field v-model="search" label="Search" class="mx-4"></v-text-field>
            </template>
            <template v-slot:body="{items}">
                <tbody>
                <tr @dblclick="playSong(item)" class="song-row" @click="selectedItem = item"
                    :class="{'selected-row': selectedItem === item, 'current-playing-row': currentPlayingItem === item}" v-for="item in items" :key="item.id">
                    <td><v-icon color="primary" v-if="currentPlayingItem === item">mdi-music-note-outline</v-icon>&nbsp;{{item.title}}</td>
                    <td>{{item.duration}}</td>
                </tr>
                </tbody>

            </template>
        </v-data-table>

    </v-card>
</template>

<script lang="ts">
    import Vue from "vue"
    import {Component, Watch,} from "vue-property-decorator";
    import SongService from "@/services/song-service";
    import SongVM from "@/view-models/song-vm";
    import {Howl} from "howler"

    @Component({
        name: "all-songs"
    })
    export default class AllSongs extends Vue {
        private songs: SongVM[] = [];
        private search = "";
        private selectedItem: SongVM = new SongVM();
        private currentPlayingItem: SongVM = new SongVM();
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

        playSong(song: SongVM) {
            this.$store.commit("playSong", song);
            this.currentPlayingItem = song;
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