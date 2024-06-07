import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { DatePicker } from 'ant-design-vue';
import store from "./store";

import "bootstrap/dist/css/bootstrap.css";
// import vuetify from "./plugins/vuetify";
import bootstrap from "bootstrap/dist/js/bootstrap.bundle.js";

// import 'font-awesome/css/font-awesome.css'

// Vuetify
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { aliases, md } from 'vuetify/iconsets/md'


const vuetify = createVuetify({
  components,
  directives,
  icons: {
    defaultSet: 'md',
    aliases,
    sets: {
      md,
    },
  },
})

import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { library } from "@fortawesome/fontawesome-svg-core";
import { fas } from "@fortawesome/free-solid-svg-icons";

library.add(fas);

import axios from "axios";
import VueAxios from "vue-axios";
import { createPinia } from "pinia";

import ToastPlugin from "vue-toast-notification";
import "vue-toast-notification/dist/theme-bootstrap.css";
const pinia = createPinia();

//editor blog
import mavonEditor from 'mavon-editor'
import 'mavon-editor/dist/css/index.css'

//phân trang
import VueAwesomePaginate from "vue-awesome-paginate";
import "vue-awesome-paginate/dist/style.css";

createApp(App)
  .use(bootstrap)
  .use(VueAxios, axios)
  .use(ToastPlugin)
  .use(store)
  .use(vuetify)
  .use(DatePicker)
  .use(pinia)
  .component("font-awesome-icon", FontAwesomeIcon)
  .use(router)
  .use(mavonEditor)
  .use(VueAwesomePaginate)
  .mount("#app");
