import axios from 'axios'
import Statics from '@/shared/statics'
import UserLoginVM from "@/view-models/user-login-vm";
import UserRegisterVM from "@/view-models/user-register-vm";

export default class UserService {
    private static readonly apiPrefix = "users/";
    public static isUserAuthenticated(): boolean {
        return Statics.userToken.length > 0;
    }

    public static setUserToken(value: string) {
        Statics.userToken = value;
    }

    public static login(userLoginVM: UserLoginVM) {
        return axios.post<string>(Statics.baseApiUrl + this.apiPrefix + "login", userLoginVM);
    }

    public static isUsernameAvailable(username: string) {
        return axios.get<boolean>(Statics.baseApiUrl + this.apiPrefix + "isusernameavailable", {
            params: {
                username: username
            }
        });
    }

    public static register(userRegisterVM: UserRegisterVM) {
        return axios.post<string>(Statics.baseApiUrl + this.apiPrefix + "register", userRegisterVM);
    }

    public static addUserTokenToLocalStorage(token: string) {
        localStorage.setItem("user_token", token);
    }

    public static removeUserTokenFromLocalStorage() {
        localStorage.removeItem("user_token");
    }

    public static getUserTokenFromLocalStorage() {
        return localStorage.getItem("user_token");
    }

    public static isUserTokenValid(token: string) {
        return axios.get<boolean>(Statics.baseApiUrl + this.apiPrefix + "istokenvalid", {
            params: {
                token: token
            }
        });
    }
}