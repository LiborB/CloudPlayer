<template>
    <div>
        <login></login>
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Login from "../components/Login.vue";
export default Vue.extend({
    name: "login-page",
    components: {
        Login
    }
});
</script>