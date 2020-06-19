<template>
  <header :class="{'header-v4': isShopPage}">
    <!-- Header desktop -->
    <div
      class="container-menu-desktop"
      :class="{'fix-menu-desktop': fixMenuActive}"
      ref="containerMenuDesktop"
    >
      <!-- Topbar -->
      <div class="top-bar" ref="topbar">
        <div class="content-topbar flex-sb-m h-full container">
          <div class="left-top-bar">Free shipping for standard order over $100</div>

          <div class="right-top-bar flex-w h-full">
            <a href="#" class="flex-c-m trans-04 p-lr-25">Help & FAQs</a>

            <a href="#" class="flex-c-m trans-04 p-lr-25">My Account</a>

            <a href="#" class="flex-c-m trans-04 p-lr-25">EN</a>

            <a href="#" class="flex-c-m trans-04 p-lr-25">USD</a>
          </div>
        </div>
      </div>

      <div
        class="wrap-menu-desktop"
        :class="{'how-shadow1': isShopPage}"
        :style="cssTopActive ? {'top': topMenu + 'px'} : {}"
        ref="wrapMenuDesktop"
      >
        <nav class="limiter-menu-desktop container">
          <!-- Logo desktop -->
          <router-link to="/" class="logo">
            <img src="~@/assets/images/icons/logo-01.png" alt="IMG-LOGO" />
          </router-link>

          <!-- Menu desktop -->
          <div class="menu-desktop">
            <ul class="main-menu">
              <router-link tag="li" active-class="active-menu" to="/" exact>
                <a>Home</a>
              </router-link>

              <router-link tag="li" active-class="active-menu" to="/shop">
                <a>Shop</a>
              </router-link>

              <!-- <li class="label1" data-label1="hot">
                <a href="shoping-cart.html">Features</a>
              </li>

              <li>
                <a href="blog.html">Blog</a>
              </li>

              <li>
                <a href="about.html">About</a>
              </li>

              <li>
                <a href="contact.html">Contact</a>
              </li>-->
            </ul>
          </div>

          <!-- Icon header -->
          <div class="wrap-icon-header flex-w flex-r-m">
            <ul class="main-menu">
              <li>
                <div
                  @click="showCart"
                  class="icon-header-item cl2 hov-cl1 trans-04 p-l-22 p-r-11 icon-header-noti js-show-cart"
                  :data-notify="amountInCart"
                >
                  <i class="fas fa-shopping-cart"></i>
                </div>
              </li>

              <li
                v-if="isLogin"
                class="icon-header-item cl2 hov-cl1 trans-04 p-l-22 p-r-11"
              >
                <i class="fas fa-user" too></i>
                <ul class="sub-menu">
                  <li>
                    <a>Hello, {{userName}}</a>
                  </li>
                  <li v-if="isAdmin">
                    <router-link to="/admin">Go to Admin page</router-link>
                  </li>
                </ul>
              </li>

              <li
                v-else
                @click="showLogModal('login')"
                class="icon-header-item cl2 hov-cl1 trans-04 p-l-22 p-r-11"
              >
                <i class="far fa-user"></i>
              </li>

              <li v-if="isLogin"
                  @click="showLogModal('logout')"
                  class="icon-header-item cl2 hov-cl1 trans-04 p-l-22 p-r-11"
                >
                  <i class="fas fa-sign-out-alt"></i>
              </li>
            </ul>
          </div>
        </nav>
      </div>
    </div>


    <!-- Modal Search -->
    <div class="modal-search-header flex-c-m trans-04 js-hide-modal-search">
      <div class="container-search-header">
        <button class="flex-c-m btn-hide-modal-search trans-04 js-hide-modal-search">
          <img src="~@/assets/images/icons/icon-close2.png" alt="CLOSE" />
        </button>

        <form class="wrap-search-header flex-w p-l-15">
          <button class="flex-c-m trans-04">
            <i class="zmdi zmdi-search"></i>
          </button>
          <input class="plh3" type="text" name="search" placeholder="Search..." />
        </form>
      </div>
    </div>
  </header>
</template>

<script>
import { bus } from "../main";
//import mgr from '../services/security';
import { store } from '../store';
export default {
  name: "Header",
  data() {
    return {
      isCart: this.activeCart,
      fixMenuActive: false,
      cssTopActive: false,
      posWrapHeader: "",
      topMenu: 0,
      isShopPage: false,
      showLogin: false,
      userName: "",
      isLogin: false,
      isAdmin: false,
    };
  },
  props: ["activeCart"],
  created() { 
    this.fecthUserData();
    window.addEventListener("scroll", this.handleScroll);
  },
  destroyed() {
    window.removeEventListener("scroll", this.handleScroll);
  },
  computed: {
    amountInCart() {
      return store.state.amountInCart;
    }
  },
  watch: {
    $route() {
      this.isShopPage = this.$route.path.startsWith("/shop");
    },
  },
  mounted() {
    this.isShopPage = this.$route.path.startsWith("/shop");
  },
  methods: {
    async fecthUserData() {
      const isLoginError = (await store.state.isAuthenticated).error;
      if (!isLoginError) {
        this.userName = (await store.state.user).profile.name;
        if((await store.state.user).profile.role.includes("Admin")) {
          this.isAdmin = true;
        } else {
          this.isAdmin = false;
        }
        this.isLogin = true;
      }
      else {
        console.log("chưa đăng nhập")
      }
    },
    handleScroll() {
      this.cssTopActive = true;
      if (this.$refs.topbar) {
        this.posWrapHeader = this.$refs.topbar.clientHeight;
      } else {
        this.posWrapHeader = 0;
      }
      if (window.scrollY > this.posWrapHeader) {
        this.fixMenuActive = true;
        this.topMenu = 0;
      } else {
        this.fixMenuActive = false;
        this.topMenu = this.posWrapHeader - window.scrollY;
      }
    },
    showCart() {
      if (this.activeCart) {
        this.isCart = false;
      } else {
        this.isCart = true;
      }
      this.$emit("show-cart", this.isCart);
    },
    showLogModal(title) {
      this.showLogin = true;
      bus.$emit("logModal", { status: this.showLogin, title: title });
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
</style>
