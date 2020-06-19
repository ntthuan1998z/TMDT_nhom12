<template>
  <div :class="{'show-header-cart': activeCart}" class="wrap-header-cart js-panel-cart">
    <div @click="hideCart()" class="s-full js-hide-cart"></div>

    <div class="header-cart flex-col-l p-l-65 p-r-25">
      <div class="header-cart-title flex-w flex-sb-m p-b-8">
        <span class="mtext-103 cl2">Your Cart</span>

        <div
          class="fs-35 lh-10 cl2 p-lr-5 pointer hov-cl1 trans-04 js-hide-cart"
          @click="hideCart()"
        >
          <i class="zmdi zmdi-close"></i>
        </div>
      </div>

      <div class="header-cart-content flex-w js-pscroll">
        <ul class="header-cart-wrapitem w-full">
          <li
            v-for="item in listCart"
            :key="item.productId"
            class="header-cart-item flex-w flex-t m-b-12"
          >
            <div class="header-cart-item-img">
              <img :src="item.img" alt="IMG" />
            </div>

            <div class="header-cart-item-txt p-t-8">
              <a href="#" class="header-cart-item-name m-b-18 hov-cl1 trans-04">{{item.productName}}</a>

              <span class="header-cart-item-info">{{item.quantity}} x ${{item.price}}</span>
            </div>
          </li>

          <!-- <li class="header-cart-item flex-w flex-t m-b-12">
            <div class="header-cart-item-img">
              <img src="~@/assets/images/item-cart-02.jpg" alt="IMG" />
            </div>

            <div class="header-cart-item-txt p-t-8">
              <a href="#" class="header-cart-item-name m-b-18 hov-cl1 trans-04">Converse All Star</a>

              <span class="header-cart-item-info">1 x $39.00</span>
            </div>
          </li>

          <li class="header-cart-item flex-w flex-t m-b-12">
            <div class="header-cart-item-img">
              <img src="~@/assets/images/item-cart-03.jpg" alt="IMG" />
            </div>

            <div class="header-cart-item-txt p-t-8">
              <a href="#" class="header-cart-item-name m-b-18 hov-cl1 trans-04">Nixon Porter Leather</a>

              <span class="header-cart-item-info">1 x $17.00</span>
            </div>
          </li>-->
        </ul>

        <div class="w-full">
          <div class="header-cart-total w-full p-tb-40">Total: ${{total}}</div>

          <div class="header-cart-buttons flex-c-m w-full">
            <router-link
              @click.native="hideCart()"
              to="shoping-cart"
              class="flex-c-m w-full stext-101 cl0 size-107 bg3 bor2 hov-btn3 p-lr-15 trans-04 m-b-10"
            >Check Out</router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { store } from "@/store";
import RepositoryFactory from "@/repository/RepositoryFactory";
const ProductRepo = RepositoryFactory.get("product");
export default {
  data() {
    return {
      active: this.activeCart,
      total: 0,
      listCart: []
    };
  },
  props: ["activeCart"],
  created() {
    this.fetchCart();
  },
  computed: {
    cartStore() {
      return store.state.cart;
    }
  },
  watch: {
    cartStore() {
      //console.log("dc goi");
      this.fetchData();
    }
  },
  methods: {
    hideCart() {
      this.active = false;
      this.$emit("hideCart", this.active);
    },
    async fetchData() {
      const cartStore = (await this.cartStore);
      let cart = cartStore.data;
      let items = [];
      if(cart == undefined) {
        items = cartStore.items;
      } else {
        items = cart.items;
      }
    
      //console.log(items);
      this.listCart = [];
      if (items != [] && items != undefined) {
        // const items = cart.items;
        //console.log(items);
        items.forEach(async element => {
          const img = (await ProductRepo.getImageCart(element.productId)).data;
          const name = (await ProductRepo.getName(element.productId)).data;
          element["productName"] = name;
          element["img"] = img;
          this.total += element.quantity * element.price;
        });
        //store.dispatch('updateCart', cart);
        this.listCart = items;
      }
      if(this.listCart.length == 0) {
        this.total = 0;
      }
    },
    async fetchCart() {
      await store.dispatch("getCart");
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
