<template>
    <v-app>
        <v-app-bar app>
            <v-toolbar-title
                ><a href="/" class="app-title">CloudPlayer</a></v-toolbar-title
            >
            <v-spacer></v-spacer>
            <v-menu v-if="isUserAuthenticated" offset-y
                ><template v-slot:activator="{ on, attrs }">
                    <v-btn icon v-bind="attrs" v-on="on">
                        <v-icon>mdi-account-circle</v-icon>
                    </v-btn>
                </template>

                <v-list>
                    <v-list-item @click="logout()">
                        <v-list-item-title>Logout</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
        </v-app-bar>

        <v-main>
            <v-container fluid>
                <router-view></router-view>
            </v-container>
        </v-main>
    </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import UserService from "../src/services/user-service";
import { mapState } from "vuex";

export default Vue.extend({
    name: "App",
    data() {
        return {
            //
        };
    },
    methods: {
        logout() {
            this.$store.commit("setIsUserAuthenticated", false);
            this.$router.push({ name: "Login" });
        }
    },
    computed: {
        ...mapState({
            isUserAuthenticated: (state: any) => state.isUserAuthenticated
        })
    },
    mounted() {
        //
    }
});
</script>
<style>
html {
    overflow: auto !important;
}

body {
    font-family: "Open Sans", sans-serif !important;
}

#app .app-title {
    text-decoration-line: none;
    color: inherit;
    font-weight: bold;
}
</style>
