<template>
        <v-row>
            <v-col>
                <v-row>
                    <v-col>
                        <upload-song></upload-song>
                    </v-col>
                </v-row>
                <v-row no-gutters class="content-row">
                    <v-col cols="3">
                        <v-list>
                            <v-list-item-group v-model="listItemSelected" mandatory>
                                <v-list-item>
                                    <v-list-item-content>All Songs</v-list-item-content>
                                </v-list-item>
                            </v-list-item-group>
                        </v-list>
                    </v-col>
                    <v-col cols="9">
                        <all-songs v-if="listItemSelected === 0"></all-songs>
                    </v-col>
                </v-row>
            </v-col>
        </v-row>
</template>

<script lang="ts">
    import Vue from "vue";
    import {Component} from "vue-property-decorator";
    import UploadSong from "@/components/home/UploadSong.vue"
    import AllSongs from "@/components/home/AllSongs.vue";
    import PlaybackBar from "@/components/home/PlaybackBar.vue";

    @Component({
        name: "home",
        components: {AllSongs, UploadSong}
    })
    export default class Home extends Vue {
        private listItemSelected = 0;
    }
</script>

<style scoped>
    .content-row {
        overflow: hidden;
        /*max-height: 73vh;*/
        /*margin-bottom: 100px;*/
        height: 100%;
    }
</style>