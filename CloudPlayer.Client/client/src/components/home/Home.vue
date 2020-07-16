<template>
    <div>
    <v-row>
        <v-col>
            <upload-song></upload-song>
        </v-col>
    </v-row>
        <v-row>
            <v-col cols="3">
                <v-list>
                    <v-list-item-group>
                        <v-list-item>
                            <v-list-item-content>All Songs</v-list-item-content>
                        </v-list-item>
                    </v-list-item-group>
                </v-list>
            </v-col>
            <v-col cols="9"></v-col>
        </v-row>

    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import {Component} from "vue-property-decorator";
    import UploadSong from "@/components/home/UploadSong.vue"

    @Component({
        name: "home",
        components: {UploadSong}
    })
    export default class Home extends Vue {

    }
</script>

<style scoped>
</style>