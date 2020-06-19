import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import jQuery from 'jquery';
import 'popper.js';
import 'bootstrap';
import 'vue-slick';
import './assets/app.scss';
import { store } from './store';
import VueSweetalert2 from 'vue-sweetalert2';
//import mgr from './services/security';

window.$ = window.jQuery = jQuery;
Vue.use(VueSweetalert2);
Vue.config.productionTip = false;
export const bus = new Vue();
new Vue({
  store,
  router,
  render: h => h(App)
}).$mount("#app");
