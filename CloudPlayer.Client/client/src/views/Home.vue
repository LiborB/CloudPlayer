<template>
    <div>
        <home age="">

        </home>
    </div>
</template>

<script lang="ts">
    // @ is an alias to /src
    import Vue from "vue";
    import {Component} from "vue-property-decorator"
    import Home from "@/components/Home.vue";

    @Component({
        name: "home-page",
        components: {
            Home
        }
    })
    export default class HomePage extends Vue {
    }
</script>
