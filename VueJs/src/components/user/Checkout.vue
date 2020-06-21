<template>
  <div>
    <!-- breadcrumb -->
    <div class="container">
      <div class="bread-crumb flex-w p-l-25 p-r-15 p-t-30 p-lr-0-lg">
        <a href="index.html" class="stext-109 cl8 hov-cl1 trans-04">
          Home
          <i class="fa fa-angle-right m-l-9 m-r-10" aria-hidden="true"></i>
        </a>

        <span class="stext-109 cl4">Shoping Cart</span>
      </div>
    </div>

    <!-- Shoping Cart -->
    <form class="bg0 p-t-75 p-b-85">
      <div class="container">
        <div class="row">
          <div class="col-lg-10 col-xl-7 m-lr-auto m-b-50">
            <div class="m-l-25 m-r--38 m-lr-0-xl">
              <div class="wrap-table-shopping-cart">
                <table class="table-shopping-cart">
                  <tr class="table_head">
                    <th class="column-1">Product</th>
                    <th class="column-2"></th>
                    <th class="column-3">Price</th>
                    <th class="column-4">Quantity</th>
                    <th class="column-5">Total</th>
                  </tr>

                  <tr v-for="(item, index) in listCart" :key="index" class="table_row">
                    <td class="column-1">
                      <div class="how-itemcart1">
                        <img :src="item.img" />
                      </div>
                    </td>
                    <td class="column-2">{{item.name}}</td>
                    <td class="column-3">$ {{item.price}}</td>
                    <td class="column-4">
                      <div class="wrap-num-product flex-w m-l-auto m-r-0">
                        <div
                          @click="item.quantity > 0 ? changeQuantity(-1, index) : null"
                          class="btn-num-product-down cl8 hov-btn3 trans-04 flex-c-m"
                        >
                          <i class="fs-12 fas fa-minus"></i>
                        </div>

                        <input
                          class="mtext-104 cl3 txt-center num-product"
                          type="number"
                          name="num-product1"
                          @keydown.prevent
                          v-model="item.quantity"
                        />

                        <div
                          @click="item.quantity < 10 ? changeQuantity(1, index) : null"
                          class="btn-num-product-up cl8 hov-btn3 trans-04 flex-c-m"
                        >
                          <i class="fs-12 fas fa-plus"></i>
                        </div>
                      </div>
                    </td>
                    <td class="column-5">$ {{item.unitTotal}}</td>
                  </tr>
                </table>
              </div>

              <div class="flex-w flex-sb-m bor15 p-t-18 p-b-15 p-lr-40 p-lr-15-sm">
                <div class="flex-w flex-m m-r-20 m-tb-5">
                  <input
                    class="stext-104 cl2 plh4 size-117 bor13 p-lr-20 m-r-10 m-tb-5"
                    type="text"
                    name="coupon"
                    placeholder="Coupon Code"
                  />

                  <div
                    class="flex-c-m stext-101 cl2 size-118 bg8 bor13 hov-btn3 p-lr-15 trans-04 pointer m-tb-5"
                  >Apply coupon</div>
                </div>

                <div
                  @click="updateCart()"
                  class="flex-c-m stext-101 cl2 size-119 bg8 bor13 hov-btn3 p-lr-15 trans-04 pointer m-tb-10"
                >Update Cart</div>
              </div>
            </div>
          </div>

          <div class="col-sm-10 col-lg-7 col-xl-5 m-lr-auto m-b-50">
            <div class="bor10 p-lr-40 p-t-30 p-b-40 m-l-63 m-r-40 m-lr-0-xl p-lr-15-sm">
              <h4 class="mtext-109 cl2 p-b-30">Cart Totals</h4>

              <div class="flex-w flex-t bor12 p-b-13">
                <div class="size-208">
                  <span class="stext-110 cl2">Subtotal:</span>
                </div>

                <div class="size-209">
                  <span class="mtext-110 cl2">${{order.total}}</span>
                </div>
              </div>

              <div class="flex-w flex-t bor12 p-t-15 p-b-30">
                <div class="size-208 w-full-ssm">
                  <span class="stext-110 cl2">Shipping:</span>
                </div>

                <div class="size-209 p-r-18 p-r-0-sm w-full-ssm">
                  <p
                    class="stext-111 cl6 p-t-2"
                  >There are no shipping methods available. Please double check your address, or contact us if you need any help.</p>
                </div>
              </div>
              <div class="p-t-15">
                <span class="stext-112 cl8">Enter your info</span>

                <div class="bor8 bg0 m-b-12">
                  <input
                    class="stext-111 cl8 plh3 size-111 p-lr-15"
                    type="text"
                    v-model="order.customerName"
                    name="state"
                    placeholder="Enter your name"
                  />
                </div>

                <div class="bor8 bg0 m-b-12">
                  <input
                    class="stext-111 cl8 plh3 size-111 p-lr-15"
                    type="text"
                    name="state"
                    v-model="location"
                    placeholder="Provine  /  City"
                  />
                </div>
                <div class="bor8 bg0 m-b-12">
                  <input
                    class="stext-111 cl8 plh3 size-111 p-lr-15"
                    type="text"
                    name="state"
                    v-model="aaddress"
                    placeholder="Adress"
                  />
                </div>

                <div class="bor8 bg0 m-b-22">
                  <input
                    class="stext-111 cl8 plh3 size-111 p-lr-15"
                    type="text"
                    name="postcode"
                    v-model="order['phoneNumber']"
                    placeholder="Phone number"
                  />
                </div>
              </div>

              <div class="flex-w flex-t p-t-27 p-b-33">
                <div class="size-208">
                  <span class="mtext-101 cl2">Total:</span>
                </div>

                <div class="size-209 p-t-1">
                  <span class="mtext-110 cl2">${{order.total}}</span>
                </div>
              </div>

              <button
                @click.prevent="checkOut()"
                class="flex-c-m stext-101 cl0 size-116 bg3 bor14 hov-btn3 p-lr-15 trans-04 pointer"
              >Proceed to Checkout</button>
            </div>
          </div>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { store } from "@/store";
import RepositoryFactory from "@/repository/RepositoryFactory";
const ProductRepo = RepositoryFactory.get("product");
const OrderRepo = RepositoryFactory.get("order");
export default {
  data() {
    return {
      location: "",
      aaddress: "",
      listCart: [],
      order: {
        customerId: "",
        customerName: "",
        address: "",
        total: 0,
        phoneNumber: "",
        items: []
      }
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    async fetchData() {
      await store.dispatch("getCart");
      const cart = (await store.state.cart).data;
      if (cart != "" && cart != undefined) {
        this.order["customerId"] = cart.id;
        const items = cart.items;
        items.forEach(async element => {
          const img = (await ProductRepo.getImageCart(element.productId)).data;
          const name = (await ProductRepo.getName(element.productId)).data;
          element["img"] = img;
          element["name"] = name;
          element["unitTotal"] = element.quantity * element.price;

          this.order.total += element.quantity * element.price;
        });
        this.listCart = items;
      }
    },
    changeQuantity(unit, index) {
      var item = this.listCart[index];
      item.quantity += unit;
      item.unitTotal = item.quantity * item.price;
      this.order.total += unit * item.price;
    },
    updateCart() {
      console.log(this.listCart);
    },
    prevent() {
      return false;
    },
    checkOut() {
      this.order.address = this.aaddress + "/" + this.location;
      this.listCart.forEach(element => {
        const itemOrder = {
          productId: element.productId,
          units: element.quantity,
          unitPrice: element.price
        };
        this.order.items.push(itemOrder);
      });
      OrderRepo.post(this.order);
      store.dispatch("finishCart", this.order.customerId);
      this.$swal.fire({
        toast: true,
        showConfirmButton: false,
        timer: 3000,
        icon: "success",
        title: "Checkout success"
      });
      this.$router.push("/");
    }
  }
  // declare Select2Component
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
