<template>
    <div>
        <Weather />
    </div>
</template>

<script>
// @ is an alias to /src
import Weather from "@/components/Weather.vue";
import Vue from "vue";
export default Vue.extend({
    name: "WeatherPage",
    components: {
        Weather
    }
});
</script>
