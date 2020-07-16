<template>
    <div>
        <v-row>
            <v-col cols="3" offset="4">
                <div class="login-title">Login to your account</div>
                <v-form ref="loginForm" @submit.prevent="onSubmit()">
                    <v-row>
                        <v-col>
                            <v-text-field
                                    :rules="[requiredValidator]"
                                    v-model="username"
                                    outlined=""
                                    label="Username"
                                    prepend-inner-icon="mdi-account"
                            >
                            </v-text-field>
                            <v-text-field
                                    :rules="[requiredValidator]"
                                    prepend-inner-icon="mdi-lock-question"
                                    :append-icon="
                                    showPassword ? 'mdi-eye' : 'mdi-eye-off'
                                "
                                    :type="showPassword ? 'text' : 'password'"
                                    v-model="password"
                                    outlined=""
                                    label="Password"
                                    @click:append="showPassword = !showPassword"
                            ></v-text-field>
                            <span class="error--text" v-if="userPassIncorrect">Username/password incorrect</span>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col>
                            <router-link
                                    class="register-link"
                                    :to="{ name: 'Register' }"
                            >Create an account
                            </router-link
                            >
                        </v-col>
                        <v-col class="text-right">
                            <v-btn color="primary" type="submit">Login</v-btn>
                        </v-col>
                    </v-row>
                </v-form>
            </v-col>
        </v-row>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import UserService from "../services/user-service";
    import UserLoginVM from "@/view-models/user-login-vm";
    import Statics from "@/shared/statics";
    import {Component, Ref} from "vue-property-decorator";
    import axios from "axios";

    @Component({
        name: "login"
    })
    export default class Login extends Vue {
        private username = "";
        private password = "";
        private showPassword = false;
        private userPassIncorrect = false;
        @Ref("loginForm") readonly loginForm!: Vue & any;

        onSubmit() {
            const formRef = this.$refs.loginForm as any;
            if (!formRef.validate()) {
                return false;
            }
            const userLoginVM = new UserLoginVM();
            userLoginVM.Username = this.username;
            userLoginVM.Password = this.password;
            UserService.login(userLoginVM).then(response => {
                Statics.userToken = response.data;
                this.$store.commit("setIsUserAuthenticated", true);
                UserService.addUserTokenToLocalStorage(response.data);
                axios.defaults.headers.common = {
                    "user_token": response.data
                };
                this.$router.push({name: "Home"})
            }, error => {
                this.userPassIncorrect = true;
            })
        }

        requiredValidator(value: string) {
            if (value.length === 0) {
                return "This field is required";
            }
            return true;
        }
    }
</script>

<style scoped>
    .remember-me {
        margin-top: -10px;
    }
</style>
<style>
    .register-link {
        text-decoration: none;
    }

    .login-title {
        font-size: 30px;
        margin-bottom: 5px;
    }
</style>