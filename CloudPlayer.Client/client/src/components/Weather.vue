<template>
    <div class="hello">
        weather works!
        {{ weathers }}
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import axios from "axios";
import Statics from "../shared/statics";

export default Vue.extend({
    name: "Weather",
    data() {
        return {
            weathers: ""
        };
    },
    mounted() {
        axios.get(Statics.baseApiUrl + "weather/getweathers").then(
            response => {
                this.weathers = response.data;
            },
            error => {
                console.log(error);
            }
        );
    }
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
    margin: 40px 0 0;
}
ul {
    list-style-type: none;
    padding: 0;
}
li {
    display: inline-block;
    margin: 0 10px;
}
a {
    color: #42b983;
}
</style>
