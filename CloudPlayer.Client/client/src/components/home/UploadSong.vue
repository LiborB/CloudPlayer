<template>
    <div>
        <input accept="audio/*" type="file" ref="file" class="d-none" @change="fileUploadChange">
        <v-btn class="primary" @click.stop="uploadClick">
            <v-icon left>mdi-cloud-upload</v-icon>
            Upload Song
        </v-btn>

        <v-dialog persistent v-model="dialog" max-width="500">
            <v-card>
                <v-card-title>Add Song</v-card-title>
                <v-card-text>
                    <v-container>
                        <v-form ref="formRef">
                            <v-text-field :rules="[titleValidator]" required v-model="title" label="Title"></v-text-field>
                        </v-form>
                    </v-container>
                </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn @click="dialog = false">Cancel</v-btn>
                    <v-btn color="primary" @click="formSubmit">Add Song</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <v-snackbar
                v-model="songAdded"
        >
            Song successfully added to collection

            <template v-slot:action="{ attrs }">
                <v-btn
                        color="secondary"
                        text
                        v-bind="attrs"
                        @click="songAdded = false"
                >
                    Close
                </v-btn>
            </template>
        </v-snackbar>
    </div>
</template>

<script lang="ts">
    import Vue from "vue"
    import {Component} from "vue-property-decorator"
    import SongService from "@/services/song-service";
    import AddSongVM from "@/view-models/add-song-vm";

    @Component({
        name: "upload-song"
    })
    export default class UploadSong extends Vue {
        private dialog = false;
        private title = "";
        private songAdded = false;

        fileUploadChange(event: Event) {
            const fileInput = event.target as HTMLInputElement;

            if (fileInput?.files?.length) {
                this.title = fileInput.files[0].name.replace(/\.[^/.]+$/, "");
            }
            this.dialog = true;
        }

        titleValidator(value: string) {
            if (value.length === 0) {
                return "Please enter a title.";
            }
            return true;
        }

        uploadClick() {
            const file = this.$refs.file as HTMLInputElement;
            file.value = "";
            file.click();
        }

        formSubmit() {
            const form = this.$refs.formRef as any;
            console.log(form)
            if (form.validate()) {
                const addSongVM = new AddSongVM();
                const file = this.$refs.file as HTMLInputElement;
                addSongVM.title = this.title;
                addSongVM.file = file.files![0];
                SongService.addSong(addSongVM).then(response => {
                    this.dialog = false;
                    this.songAdded = true;
                    this.$store.commit("setSongAdded");
                }, error => {
                    console.log(error)
                })

            }
        }
    }
</script>