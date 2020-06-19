<template>
<div id="container-all">
  <body>
    <!-- Header -->
    <Header :activeCart.sync="activeCart" @show-cart="activeCart = $event"></Header>

    <!-- Cart -->
    <Cart :activeCart.sync="activeCart" @hideCart="activeCart = $event"></Cart>

    <!-- Slider -->
    <!-- <Slider></Slider> -->

    <!-- Banner -->
    <!-- <Banner></Banner> -->

    <!-- Product -->
    <!-- <Product :showModal1.sync="showModal1" @show-modal="showModal1 = $event"></Product> -->
    <router-view></router-view>

    <!-- Footer -->
    <Footer></Footer>

    <!-- Back to top -->
    <div class="btn-back-to-top" id="myBtn">
      <span class="symbol-btn-back-to-top">
        <i class="zmdi zmdi-chevron-up"></i>
      </span>
    </div>

    <LoginModal :showLogin="showLogin" @hide-login="showLogin = $event"></LoginModal>
  </body>
</div>
</template>

<style lang="scss" scoped>
@import "~@/assets/css/main.scss";
@import "~@/assets/css/util.scss";
</style>
<script>
import Header from "../components/Header.vue";
import Footer from "../components/Footer.vue";
import Cart from "../components/Cart.vue";
import LoginModal from "../components/LoginModal.vue";
//import { store } from '../store';
export default {
  components: {
    Header,
    // Banner,
    // Slider,
    // Product,
    Footer,
    Cart,
    LoginModal
  },
  created() {
    //this.fetch();
  },
  data() {
    return {
      activeCart: false,
      showModal1: false,
      showLogin: false,
    };
  },
  methods: {
  }
};
</script>
