<template>
    <v-card height="70vh" style="overflow: auto">
<!--        <v-card-title class="justify-center">Currently Playing</v-card-title>-->
<!--        <v-list>-->
<!--            <v-list-item two-line>-->
<!--                <v-list-item-title>{{song.title}}</v-list-item-title>-->
<!--                <v-list-item-title>{{song.duration}}</v-list-item-title>-->
<!--            </v-list-item>-->
<!--        </v-list>-->
        <v-card-title class="justify-center">Up Next</v-card-title>
        <v-list>
            <v-list-item @contextmenu="openSongContextMenu($event, song)" :ripple="false" link v-for="(song, index) in queuedSongs" v-bind:key="index" >
                <v-list-item-content>
                    <v-list-item-title>{{song.title}}</v-list-item-title>
                </v-list-item-content>
                <v-list-item-action>
                    <v-list-item-action-text>{{secondsToDurationFormat(song.duration)}}</v-list-item-action-text>
                </v-list-item-action>
            </v-list-item>
        </v-list>
        <song-context-menu :is-queue-mode="true" ref="contextMenu"></song-context-menu>
    </v-card>
</template>

<script lang="ts">
    import Vue from "vue"
    import Component from "vue-class-component";
    import SongVM from "@/view-models/song-vm";
    import TimeUtility from "@/components/mixins/TimeUtility.vue";
    import {Ref} from "vue-property-decorator";
    import SongContextMenu from "@/components/home/SongContextMenu.vue";

    @Component({
        name: "queue",
        components: {SongContextMenu}
    })
    export default class Queue extends TimeUtility {
        private queuedSongs: SongVM[] = [];
        @Ref("contextMenu")
        contextMenuRef!: SongContextMenu;
        mounted() {
            this.queuedSongs = this.$store.state.queuedSongs;
        }

        openSongContextMenu(event: MouseEvent, songVM: SongVM) {
            (this.contextMenuRef as any).openMenu(event, songVM);
        }
    }
</script>

<style scoped>

</style>