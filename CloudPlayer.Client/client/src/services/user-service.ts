import axios from 'axios'
import Statics from '@/shared/statics'

export default class UserService {
    public static isUserAuthenticated(): boolean {
        return Statics.userToken.length > 0
    }

    public static setUserToken(value: string) {
        Statics.userToken = value
    }
}