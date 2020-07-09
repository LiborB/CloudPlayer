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
                            <v-checkbox
                                class="remember-me"
                                label="Remember Me"
                            ></v-checkbox>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col>
                            <router-link
                                class="register-link"
                                :to="{ name: 'Register' }"
                                >Create an account</router-link
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
import { VForm } from "vuetify/lib";
import UserService from "../services/user-service";
export default Vue.extend({
    name: "login",
    data() {
        return {
            username: "",
            password: "",
            showPassword: false
        };
    },
    methods: {
        onSubmit() {
            const formRef = this.$refs.loginForm as any;
            if (!formRef.validate()) {
                return false;
            }
            this.$store.commit("setIsUserAuthenticated", true);
        },
        requiredValidator(value: string) {
            if (value.length === 0) {
                return "This field is required";
            }
            return true;
        }
    }
});
</script>

<style>
.login-title {
    font-size: 30px;
    margin-bottom: 5px;
}

.register-link {
    text-decoration: none;
}

.remember-me {
    margin-top: -10px;
}
</style>