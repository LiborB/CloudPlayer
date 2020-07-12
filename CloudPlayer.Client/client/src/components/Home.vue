<template>
    <div>
        <v-tabs vertical>
            <v-tab>Songs</v-tab>
            <v-tab-item>
                <div class="tab-container">
                    <all-songs></all-songs>
                </div>
            </v-tab-item>
        </v-tabs>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import {Component} from "vue-property-decorator";
    import AllSongs from "@/components/AllSongs.vue";

    @Component({
        name: "home",
        components: {AllSongs}
    })
    export default class Home extends Vue {
    }
</script>

<style scoped>
    .tab-container {
        margin: 20px;
    }
</style>