<template>
    <div>
        <v-row>
            <v-col cols="3" offset="4">
                <div class="login-title">Create a new account</div>
                <v-form ref="registerForm" @submit.prevent="onSubmit()">
                    <v-row>
                        <v-col>
                            <v-text-field
                                    @blur="usernameAvailableValidator"
                                    :rules="[requiredValidator]"
                                    validate-on-blur
                                    v-model="username"
                                    outlined=""
                                    label="Username"
                                    prepend-inner-icon="mdi-account"
                                    :error-messages="usernameErrors"
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
                            <v-text-field
                                    :rules="[requiredValidator, matchingPasswordValidator]"
                                    type="password"
                                    v-model="confirmPassword"
                                    outlined=""
                                    label="Confirm Password"
                                    validate-on-blur
                            ></v-text-field>
                            <span class="red" v-if="unknownErrorMessage">{{unknownErrorMessage}}</span>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col>
                            <router-link
                                    class="register-link"
                                    :to="{ name: 'Login' }"
                            >Already have an account? sign in
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
    import UserService from "@/services/user-service";
    import {Component, Ref} from "vue-property-decorator";
    import UserRegisterVM from "@/view-models/user-register-vm";
    import Statics from "@/shared/statics";

    @Component({
        name: "register"
    })
    export default class Register extends Vue {
        private username = "";
        private password = "";
        private confirmPassword = "";
        private showPassword = false;
        private usernameErrors: string[] = [];
        private unknownErrorMessage = "";
        @Ref("registerForm") readonly registerForm!: Vue & any;

        requiredValidator(value: string) {
            if (value.length === 0) {
                return "This field is required";
            }
            return true;
        }

        matchingPasswordValidator(value: string) {
            if (value !== this.password) {
                return "The passwords do not match";
            }
            return true;
        }

        usernameAvailableValidator() {
            UserService.isUsernameAvailable(this.username).then(response => {
                if (!response.data) {
                    this.usernameErrors = ["This username is taken"];
                } else {
                    this.usernameErrors = [];
                }
            }, error => {
                this.usernameErrors = ["Oops, something went wrong"];
            })
        }

        onSubmit() {
            if (!this.registerForm.validate() || this.usernameErrors.length > 0) {
                return false;
            }
            const userRegisterVM = new UserRegisterVM();
            userRegisterVM.Username = this.username;
            userRegisterVM.Password = this.password;
            UserService.register(userRegisterVM).then(response => {
                Statics.userToken = response.data;
                this.$store.commit("setIsUserAuthenticated", true);
                UserService.addUserTokenToLocalStorage(response.data);
                this.$router.push({name: "Home"});
            }, error => {
                this.unknownErrorMessage = error
            })
        }
    }
</script>