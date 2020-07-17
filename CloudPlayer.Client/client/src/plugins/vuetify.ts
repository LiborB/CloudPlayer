import Vue from 'vue';
import Vuetify from 'vuetify/lib';

Vue.use(Vuetify);

export default new Vuetify({
    theme: {
        dark: true,
        themes: {
            dark: {
                primary: "#BB86FC",
                secondary: "#00a895",
                error: "#CF6679"
            },
            light: {
                primary: "#efb7ff",
                secondary: "#66fff8"
            }
        }
    }
});
