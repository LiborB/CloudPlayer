<script lang="ts">
    import Vue from "vue"
    import Component from "vue-class-component";
    import * as moment from "moment";

    @Component
    export default class TimeUtility extends Vue {
        public hello = 5;
        secondsToDurationFormat(seconds: number) {
            return moment.utc(seconds * 1000).format("mm:ss");
        }
    }
</script>