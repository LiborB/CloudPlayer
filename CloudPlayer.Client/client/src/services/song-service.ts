import AddSongVM from "@/view-models/add-song-vm";
import axios from "axios";
import Statics from "@/shared/statics";
import SongVM from "@/view-models/song-vm";

export default class SongService {
    private static readonly apiPrefix = "songs/";

    public static addSong(song: AddSongVM) {
        const formData = new FormData();
        formData.set("Title", song.Title)
        formData.append("File", song.File!);
        return axios.post(Statics.baseApiUrl + this.apiPrefix + "addsong", formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        })
    }

    public static getAllSongs() {
        return axios.get<SongVM[]>(Statics.baseApiUrl + this.apiPrefix + "getallsongs");
    }
}