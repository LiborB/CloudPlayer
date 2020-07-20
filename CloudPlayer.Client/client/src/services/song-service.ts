import AddSongVM from "@/view-models/add-song-vm";
import axios from "axios";
import Statics from "@/shared/statics";
import SongVM from "@/view-models/song-vm";

export default class SongService {
    private static readonly apiPrefix = "songs/";

    public static addSong(song: AddSongVM) {
        const formData = new FormData();
        formData.set("Title", song.title)
        formData.set("Duration", Math.round(song.duration).toString());
        formData.append("File", song.file!);
        return axios.post(Statics.baseApiUrl + this.apiPrefix + "addsong", formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        })
    }

    public static getAllSongs() {
        return axios.get<SongVM[]>(Statics.baseApiUrl + this.apiPrefix + "getallsongs");
    }

    public static getSingleSongBlob(songId: number) {
        return axios.get(Statics.baseApiUrl + this.apiPrefix + "getsinglesongblob", {
            params: {
                songId: songId
            },
            responseType: "blob"
        });
    }
}