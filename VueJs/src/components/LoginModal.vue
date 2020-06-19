<template>
  <div :class="{'show-modal1': showLogin}" class="wrap-modal1 js-modal1 p-t-60 p-b-20">
    <div @click="closeLogin()" class="overlay-modal1 js-hide-modal1">
      <div class="container">
        <!-- Outer Row -->
        <div class="row justify-content-center">
          <div class="col-xl-6 col-lg-6 col-md-6 how-pos6-parent mt-md-5">
            <button class="how-pos6 hov3 trans-04 js-hide-modal1" @click="closeLogin()">
              <img src="~@/assets/images/icons/icon-close.png" alt="CLOSE" />
            </button>
            <div
              class="card o-hidden shadow-lg my-5 border-success"
              style="background: none!important;background-color: rgba(192,192,192,0.3)!important;"
            >
              <div class="card-body p-0 m-4">
                <!-- Nested Row within Card Body -->
                <div class="row">
                  <!-- <div class="col-lg-6 d-none d-lg-block bg-login-image" style="background-image: url(admin/congcu/img/bg_login.jpg)"></div> -->
                  <div class="col-lg-12">
                    <div class="p-5">
                      <div class="text-center">
                        <h1
                          class="h4 text-gray-900 mb-4"
                          style="color:#fff!important"
                        >{{title}}</h1>
                      </div>
                      <hr />
                      <div class="text-left float-left">
                        <button @click="isLogin ? login() : logout()" style="
                          border: 1px solid #4cae4c; padding: 10px 25px; position: absolute;
                          color: rgb(92, 184, 92); border-radius: 2px;"
                        
                        >Confirm</button>
                      </div>
                      <div class="text-right clearfix">
                        <button style="border: 1px solid #4cae4c; padding: 10px 30px; position: relative;
                          color: rgb(92, 184, 92); border-radius: 2px;">Cancel</button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { bus } from "../main";
import mgr from "../services/security"
export default {
  data() {
    return {
      showLogin: false,
      title: "",
      isLogin: true,
    };
  },
  created() {
    bus.$on("logModal", (response) => {
      this.showLogin = response.status;
      if(response.title == 'logout') {
        this.title = "Are you sure you want to logout?";
        this.isLogin = false;
      } else if(response.title == 'login') {
        this.title = "Go to login page";
        this.isLogin = true;
      }
    });
  },
  methods: {
    closeLogin() {
      this.showLogin = false;
    },
    logout() {
      mgr.signoutRedirect();
    },
    login() {
      mgr.signinRedirect({state: window.location.pathname});
    }
  }

  // declare Select2Component
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
