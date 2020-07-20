<template>

    <div class="playback-bar">
        <div class="d-flex justify-center">
            <div class="playback-row">
                <v-btn icon>
                    <v-icon>mdi-rewind-outline</v-icon>
                </v-btn>
                <v-btn icon @click="togglePauseSong">
                    <v-icon v-show="!paused">mdi-pause</v-icon>
                    <v-icon large v-show="paused">mdi-play</v-icon>
                </v-btn>
                <v-btn icon>
                    <v-icon>mdi-fast-forward-outline</v-icon>
                </v-btn>
            </div>
        </div>
        <div class="d-flex justify-center align-center">

            <v-slider @start="startGrabbing" @end="endGrabbing" class="progress-bar" @change="progressUserChange" v-model="songProgress" step="0.1">
                <template v-slot:prepend>
                    <span>{{currentSongTimeFormatted}}</span>
                </template>
                <template v-slot:append>
                    <span>{{totalSongDurationFormatted}}</span>
                </template>
            </v-slider>

        </div>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import {Component, Watch} from "vue-property-decorator";
    import SongVM from "@/view-models/song-vm";
    import SongService from "@/services/song-service";
    import {Howl} from "howler";
    import * as moment from "moment";

    @Component({
        name: "playback-bar"
    })
    export default class PlaybackBar extends Vue {
        private grabbing = false;
        private volume = 0.2;
        private songProgress = 0;
        private howlSound: Howl | null = null;
        private song: SongVM = new SongVM();
        private paused = true;
        private totalSongDurationFormatted = "00:00";
        private currentSongTimeFormatted = "00:00";

        @Watch("playSongState")
        playSongHandler(song: SongVM) {
            this.playSong(song);
        }

        playSong(song: SongVM) {
            if (this.howlSound && this.howlSound.playing()) {
                this.howlSound.stop();
                this.songProgress = 0;
            }
            SongService.getSingleSongBlob(song.id).then(response => {
                const url = window.URL.createObjectURL(new Blob([response.data]));
                this.howlSound = new Howl({
                    format: ["mp3", "mpeg", "opus", "ogg", "oga", "wav", "aac", "caf", "m4a", "mp4", "weba", "webm", "dolby", "flac"],
                    src: url,
                    volume: this.volume,
                    onplay: this.progressInterval
                });
                this.howlSound.play();
                this.song = song;
                this.totalSongDurationFormatted = moment.utc(this.song.duration * 1000).format("mm:ss");
                this.paused = false;
            });
        }

        progressInterval() {
            if (this.howlSound && this.howlSound.playing()) {
                const currentDuration = this.howlSound.seek() as number;
                if (!this.grabbing) {
                    this.songProgress = currentDuration / this.song.duration * 100;
                }

                this.currentSongTimeFormatted = moment.utc(currentDuration * 1000).format("mm:ss");
                setTimeout(this.progressInterval, 100);
            }
        }

        togglePauseSong() {
            if (this.howlSound) {
                if (this.paused) {
                    this.howlSound.play();
                    this.paused = false;
                } else {
                    this.howlSound.pause();
                    this.paused = true;
                }
            }
        }

        progressUserChange(value: string) {
            if (!this.howlSound) {
                return;
            }
            const progressPercent = +value / 100;
            const seekPosition = this.song.duration * progressPercent;
            this.currentSongTimeFormatted = moment.utc(seekPosition * 1000).format("mm:ss");
            this.howlSound?.seek(seekPosition);
        }

        get playSongState() {
            return this.$store.state.playSong;
        }

        startGrabbing() {
            this.grabbing = true;
        }

        endGrabbing() {
            this.grabbing = false;
        }
    }
</script>

<style scoped lang="scss">
    .playback-row {
        width: 100%;
        text-align: center;
    }

    .progress-bar {
        width: 30%;
    }

    .playback-bar {
        background-color: #272727;
        position: fixed;
        bottom: 0px;
        left: 0px;
        right: 0px;
        margin-bottom: 0px;
        height: 7%;
    }
</style>