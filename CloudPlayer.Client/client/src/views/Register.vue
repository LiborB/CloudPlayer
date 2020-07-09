<template>
    <div>
        <register></register>
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import Register from "../components/Register.vue";
export default Vue.extend({
    name: "register-page",
    components: {
        Register
    }
});
</script>