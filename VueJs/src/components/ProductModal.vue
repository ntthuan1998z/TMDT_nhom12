<template>
  <div :class="{'show-modal1': this.showModal1}" class="wrap-modal1 js-modal1 p-t-60 p-b-20">
    <div @click="closeModal1()" class="overlay-modal1 js-hide-modal1"></div>

    <div class="container">
      <div class="bg0 p-t-60 p-b-30 p-lr-15-lg how-pos3-parent">
        <button class="how-pos3 hov3 trans-04 js-hide-modal1" @click="closeModal1()">
          <img src="~@/assets/images/icons/icon-close.png" alt="CLOSE" />
        </button>

        <div class="row">
          <div class="col-md-6 col-lg-7 p-b-30">
            <div class="p-l-25 p-r-30 p-lr-0-lg">
              <SlickProduct v-if="showModal1" :listImages="productData.listImages"></SlickProduct>
            </div>
          </div>

          <div class="col-md-6 col-lg-5 p-b-30">
            <div class="p-r-50 p-t-5 p-lr-0-lg">
              <h4 class="mtext-105 cl2 js-name-detail p-b-14">{{productData.name}}</h4>

              <span class="mtext-106 cl2">${{productData.price}}</span>

              <p class="stext-102 cl3 p-t-23">{{productData.description}}</p>

              <!--  -->
              <div class="p-t-33">
                <div class="flex-w flex-r-m p-b-10">
                  <div class="size-203 flex-c-m respon6">Size</div>
                  <div class="size-204 respon6-next">
                    <div class="rs1-select2 bor8 bg0">
                      <select2 :options="select1.myOptions" v-model="select1.selected"></select2>
                    </div>
                  </div>
                </div>
                <div class="flex-w flex-r-m p-b-10">
                  <div class="size-203 flex-c-m respon6">Color</div>
                  <div class="size-204 respon6-next">
                    <div class="rs1-select2 bor8 bg0">
                      <select2 :options="select2.myOptions" v-model="select2.selected"></select2>
                    </div>
                  </div>
                </div>

                <div class="flex-w flex-r-m p-b-10">
                  <div class="size-204 flex-w flex-m respon6-next">
                    <div class="wrap-num-product flex-w m-r-20 m-tb-10">
                      <div
                        @click="quantity > 1 ? quantity-- : null"
                        class="btn-num-product-down cl8 hov-btn3 trans-04 flex-c-m"
                      >
                        <i class="fas fa-minus"></i>
                      </div>

                      <input
                        class="mtext-104 cl3 txt-center num-product"
                        type="number"
                        name="num-product"
                        v-model="quantity"
                      />

                      <div
                        @click="quantity < 10 ? quantity++ : null"
                        class="btn-num-product-up cl8 hov-btn3 trans-04 flex-c-m"
                      >
                        <i class="fas fa-plus"></i>
                      </div>
                    </div>

                    <button
                      @click="addToCart()"
                      class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04 js-addcart-detail"
                    >Add to cart</button>
                  </div>
                </div>
              </div>

              <!--  -->
              <div class="flex-w flex-m p-l-100 p-t-40 respon7">
                <div class="flex-m bor9 p-r-10 m-r-11">
                  <a
                    href="#"
                    class="fs-14 cl3 hov-cl1 trans-04 lh-10 p-lr-5 p-tb-2 js-addwish-detail tooltip100"
                    data-tooltip="Add to Wishlist"
                  >
                    <i class="zmdi zmdi-favorite"></i>
                  </a>
                </div>

                <a
                  href="#"
                  class="fs-14 cl3 hov-cl1 trans-04 lh-10 p-lr-5 p-tb-2 m-r-8 tooltip100"
                  data-tooltip="Facebook"
                >
                  <i class="fab fa-facebook-f"></i>
                </a>

                <a
                  href="#"
                  class="fs-14 cl3 hov-cl1 trans-04 lh-10 p-lr-5 p-tb-2 m-r-8 tooltip100"
                  data-tooltip="Twitter"
                >
                  <i class="fab fa-twitter"></i>
                </a>

                <a
                  href="#"
                  class="fs-14 cl3 hov-cl1 trans-04 lh-10 p-lr-5 p-tb-2 m-r-8 tooltip100"
                  data-tooltip="Google Plus"
                >
                  <i class="fab fa-google-plus-g"></i>
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
//import Slick from "vue-slick";
import SlickProduct from "@/plugins/SlickProduct";
import Select2 from "@/plugins/Select2";
import RepositoryFactory from "@/repository/RepositoryFactory";
import { store } from "@/store";
const ProductRepo = RepositoryFactory.get("product");
//const CartRepo = RepositoryFactory.get("cart");
import { bus } from "../main";
export default {
  //components: { Slick },
  components: {
    SlickProduct,
    Select2
  },
  data() {
    return {
      quantity: 1,
      showModal1: false,
      productId: null,
      select2Settings: null,
      productData: [],
      result1: "",
      select1: {
        selected: "s",
        myOptions: [
          { key: "s", value: "Size S" },
          { key: "m", value: "Size M" },
          { key: "l", value: "Size L" },
          { key: "xl", value: "Size XL" }
        ]
      },
      select2: {
        selected: "red",
        myOptions: [
          { key: "red", value: "Red" },
          { key: "yellow", value: "Yellow" },
          { key: "blue", value: "Blue" },
          { key: "Green", value: "Green" }
        ]
      }
    };
  },
  created() {
    bus.$on("modalChanged", response => {
      this.showModal1 = response.show;
      this.productId = response.productId;
    });
  },
  computed: {},
  watch: {
    showModal1: {
      handler: async function() {
        if (this.showModal1 == true) {
          const { data } = await ProductRepo.getProduct(this.productId);
          this.productData = data;
          //console.log(this.productData);
        }
      },
      deep: true
    }
  },
  methods: {
    closeModal1() {
      this.showModal1 = false;
      //this.$emit("hide-modal", this.activeModal);
    },
    async addToCart() {
      //store.commit("updateCart");
      await store.dispatch("getCart");
      const cart = (await store.state.cart).data;
      if (cart == undefined) {
        this.$swal.fire({
          toast: true,
          showConfirmButton: false,
          timer: 3000,
          icon: "error",
          title: "Required login"
        });
      } else {
        const item = {
          productId: this.productId,
          price: this.productData.price,
          quantity: this.quantity
        };
        console.log(cart.items);
        const indexItem = cart.items.findIndex(
          x => x.productId == item.productId
        );
        if (indexItem >= 0) {
          cart.items[indexItem].quantity += item.quantity;
        } else {
          cart.items.push(item);
        }
        console.log(cart);
        store.dispatch("updateCart", cart);
        this.$swal.fire({
          toast: true,
          showConfirmButton: false,
          timer: 3000,
          icon: "success",
          title: "Add product to cart success"
        });
      }

      this.closeModal1();
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
