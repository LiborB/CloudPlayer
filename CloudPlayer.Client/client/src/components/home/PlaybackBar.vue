<template>
    <div class="playback-bar">
        <v-row no-gutters>
            <v-col></v-col>
            <v-col cols="6">
                <div class="d-flex justify-center">
                    <div class="playback-row">
                        <v-btn icon @click="previousSongClick">
                            <v-icon>mdi-rewind-outline</v-icon>
                        </v-btn>
                        <v-btn icon @click="togglePauseSong">
                            <v-icon v-show="!paused">mdi-pause</v-icon>
                            <v-icon large v-show="paused">mdi-play</v-icon>
                        </v-btn>
                        <v-btn icon @click="nextSongClick">
                            <v-icon>mdi-fast-forward-outline</v-icon>
                        </v-btn>
                    </div>
                </div>
                <div class="slider-row d-flex justify-center align-center">

                    <v-slider step="0.1" :thumb-label="true" @start="startGrabbing" @end="endGrabbing" class="progress-bar" @change="progressUserChange" v-model="songProgress" min="0" :max="song.duration">
                        <template v-slot:prepend>
                            <span>{{currentSongTimeFormatted}}</span>
                        </template>
                        <template v-slot:append>
                            <span>{{totalSongDurationFormatted}}</span>
                        </template>
                        <template v-slot:thumb-label="{value}">
                            {{secondsToDurationFormat(value)}}
                        </template>
                    </v-slider>

                </div>
            </v-col>
            <v-col align-self="end">
                <v-row>
                    <v-col offset="11">
                        hell
                    </v-col>
                </v-row>
            </v-col>
        </v-row>

    </div>
</template>

<script lang="ts">
    import {Component, Mixins, Watch} from "vue-property-decorator";
    import SongVM from "@/view-models/song-vm";
    import SongService from "@/services/song-service";
    import {Howl} from "howler";
    import * as moment from "moment";
    import TimeUtility from "@/components/mixins/TimeUtility.vue";

    @Component({
        name: "playback-bar"
    })
    export default class PlaybackBar extends Mixins(TimeUtility) {
        private grabbing = false;
        private volume = 0.2;
        private songProgress = 0;
        private howlSound: Howl | null = null;
        private song: SongVM = new SongVM();
        private paused = true;
        private totalSongDurationFormatted = "00:00";
        private currentSongTimeFormatted = "00:00";
        private songHistory = [] as SongVM[];

        @Watch("playSongState")
        playSongHandler(song: SongVM) {
            this.playSong(song);
        }

        playSong(song: SongVM) {
            if (this.howlSound && this.howlSound.playing()) {
                this.howlSound.stop();
                this.resetSongProgress();
            }
            SongService.getSingleSongBlob(song.id).then(response => {
                this.song = song;
                const url = window.URL.createObjectURL(new Blob([response.data]));
                this.howlSound = new Howl({
                    format: ["mp3", "mpeg", "opus", "ogg", "oga", "wav", "aac", "caf", "m4a", "mp4", "weba", "webm", "dolby", "flac"],
                    src: url,
                    volume: this.volume,
                    onplay: this.onSongPlay,
                    onend: this.onSongEnd
                });
                this.howlSound.play();
                this.totalSongDurationFormatted = moment.utc(this.song.duration * 1000).format("mm:ss");
                this.paused = false;
            });
        }

        onSongPlay() {
            this.progressInterval();
        }

        progressInterval() {
            if (this.howlSound && this.howlSound.playing()) {
                const currentDuration = this.howlSound.seek() as number;
                if (!this.grabbing) {
                    this.songProgress = currentDuration;
                }

                this.currentSongTimeFormatted = moment.utc(currentDuration * 1000).format("mm:ss");
                setTimeout(this.progressInterval, 100);
            }
        }

        resetSongProgress() {
            this.songProgress = 0;
            if (this.howlSound) {
                this.howlSound.stop();
                this.howlSound.play();
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

        progressUserChange(value: number) {
            if (!this.howlSound) {
                return;
            }
            this.currentSongTimeFormatted = moment.utc(value * 1000).format("mm:ss");
            this.howlSound?.seek(value);
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

        playNextSong() {
            if (this.$store.state.queuedSongs.length > 0) {
                const nextSong = this.$store.state.queuedSongs[0];
                this.$store.commit("removeFromQueue", nextSong);
                this.$store.commit("playSong", nextSong);
            }
            else {
                this.$store.dispatch("playNextDefaultSong", this.song);
            }
        }

        addToSongHistory(song: SongVM) {
            this.songHistory.push(song);
        }

        onSongEnd() {
            this.addToSongHistory(this.song);
            this.playNextSong();
        }

        nextSongClick() {
            this.addToSongHistory(this.song);
            this.playNextSong();
        }

        previousSongClick() {
            if (this.howlSound && this.howlSound.state() === "loaded" && this.howlSound.seek() > 5) {
                this.resetSongProgress();
            }
            else {
                const song = this.songHistory.pop();
                if (song) {
                    this.howlSound?.unload();
                    this.$store.commit("playSong", song);
                }
                else {
                    this.resetSongProgress();
                }
            }
        }
    }
</script>

<style scoped lang="scss">
    .playback-row {
        /*width: 100%;*/
        text-align: center;
    }

    .progress-bar {
        width: 30%;
    }

    .playback-bar {
        background-color: #272727;
        /*position: fixed;*/
        /*bottom: 0px;*/
        /*left: 0px;*/
        /*right: 0px;*/
        /*margin-bottom: 0px;*/
        /*height: 7%;*/
        width: 100%;
    }

    .slider-row {
        /*padding-left:30%;*/
        /*padding-right: 30%;*/
    }
</style>