<template>
  <section class="bg0 p-t-23 p-b-140">
    <div class="container">
      <div v-if="!isShopPage" class="p-b-10">
        <h3 class="ltext-103 cl5">Product Overview</h3>
      </div>

      <div class="flex-w flex-sb-m p-b-52">
        <div class="flex-w flex-l-m filter-tope-group m-tb-10">
          <button
            :class="{'how-active1': isFiltingType == 100}"
            class="stext-106 cl6 hov1 bor3 trans-04 m-r-32 m-tb-5"
            @click="filterType(100)"
          >All Products</button>

          <button
            v-for="type in listTypes"
            :key="type.id"
            @click="filterType(type.id)"
            :class="{'how-active1': isFiltingType == type.id}"
            class="stext-106 cl6 hov1 bor3 trans-04 m-r-32 m-tb-5"
          >{{type.name}}</button>
        </div>

        <div class="flex-w flex-c-m m-tb-10">
          <div
            @click="activeFilter()"
            class="flex-c-m stext-106 cl6 size-104 bor4 pointer hov-btn3 trans-04 m-r-8 m-tb-4 js-show-filter"
            :class="{'show-filter': showFilter}"
          >
            <i class="icon-filter cl2 m-r-6 fs-15 trans-04 zmdi zmdi-filter-list"></i>
            <i class="icon-close-filter cl2 m-r-6 fs-15 trans-04 zmdi zmdi-close dis-none"></i>
            Filter
          </div>

          <div
            @click="activeSearch()"
            class="flex-c-m stext-106 cl6 size-105 bor4 pointer hov-btn3 trans-04 m-tb-4 js-show-search"
            :class="{'show-search' : showSearch}"
          >
            <i class="icon-search cl2 m-r-6 fs-15 trans-04 zmdi zmdi-search"></i>
            <i class="icon-close-search cl2 m-r-6 fs-15 trans-04 zmdi zmdi-close dis-none"></i>
            Search
          </div>
        </div>

        <!-- Search product -->
        <transition name="show-search-panel">
          <div v-if="showSearch" class="panel-search w-full p-t-10 p-b-15">
            <div class="bor8 dis-flex p-l-15">
              <button class="size-113 flex-c-m fs-16 cl2 hov-cl1 trans-04">
                <i class="zmdi zmdi-search"></i>
              </button>

              <input
                class="mtext-107 cl2 size-114 plh2 p-r-15"
                type="text"
                name="search-product"
                v-model="searchText"
                @keyup="searchProduct"
                placeholder="Search"
              />
            </div>
          </div>
        </transition>
        <transition name="show-filter-panel">
          <!-- Filter -->
          <div v-if="showFilter" class="panel-filter w-full p-t-10">
            <div class="wrap-filter flex-w bg6 w-full p-lr-40 p-t-27 p-lr-15-sm">
              <div class="filter-col1 p-r-15 p-b-27">
                <div class="mtext-102 cl2 p-b-15">Sort By</div>

                <ul>
                  <li class="p-b-6">
                    <a
                      :class="{'filter-link-active': filterprop == 0}"
                      class="filter-link stext-106 trans-04"
                    >Default</a>
                  </li>

                  <li class="p-b-6">
                    <a
                      :class="{'filter-link-active': filterprop == 1}"
                      @click.prevent="resultProducts.sort((a,b) => {filterprop = 1 ; return a.price - b.price})"
                      class="filter-link stext-106 trans-04"
                    >Price: Low to High</a>
                  </li>

                  <li class="p-b-6">
                    <a
                      :class="{'filter-link-active': filterprop == 2}"
                      @click.prevent="resultProducts.sort((a,b) => {filterprop = 2 ; return b.price - a.price})"
                      class="filter-link stext-106 trans-04"
                    >Price: High to Low</a>
                  </li>
                </ul>
              </div>

              <div class="filter-col2 p-r-15 p-b-27">
                <div class="mtext-102 cl2 p-b-15">Price</div>

                <ul>
                  <li class="p-b-6">
                    <a
                      :class="{'filter-link-active': priceFilter == 0}"
                      @click.prevent="filterPrice(0,1000); priceFilter=0"
                      class="filter-link stext-106 trans-04"
                    >All</a>
                  </li>

                  <li class="p-b-6">
                    <a
                      :class="{'filter-link-active': priceFilter == 1}"
                      @click.prevent="filterPrice(0,50); priceFilter=1"
                      class="filter-link stext-106 trans-04"
                    >$0.00 - $50.00</a>
                  </li>

                  <li class="p-b-6">
                    <a
                      :class="{'filter-link-active': priceFilter == 2}"
                      @click.prevent="filterPrice(50,100); priceFilter=2"
                      class="filter-link stext-106 trans-04"
                    >$50.00 - $100.00</a>
                  </li>

                  <li class="p-b-6">
                    <a
                      :class="{'filter-link-active': priceFilter == 2}"
                      @click.prevent="filterPrice(100,150); priceFilter=2"
                      class="filter-link stext-106 trans-04"
                    >$100.00 - $150.00</a>
                  </li>

                  <li class="p-b-6">
                    <a
                      :class="{'filter-link-active': priceFilter == 3}"
                      @click.prevent="filterPrice(150,200); priceFilter=3"
                      class="filter-link stext-106 trans-04"
                    >$150.00 - $200.00</a>
                  </li>

                  <li class="p-b-6">
                    <a
                      :class="{'filter-link-active': priceFilter == 4}"
                      @click.prevent="filterPrice(200,1000); priceFilter=4"
                      class="filter-link stext-106 trans-04"
                    >$200.00+</a>
                  </li>
                </ul>
              </div>

              <div class="filter-col3 p-r-15 p-b-27">
                <div class="mtext-102 cl2 p-b-15">Color</div>

                <ul>
                  <li class="p-b-6">
                    <span class="fs-15 lh-12 m-r-6" style="color: #222;">
                      <i class="zmdi zmdi-circle"></i>
                    </span>

                    <a class="filter-link stext-106 trans-04">Black</a>
                  </li>

                  <li class="p-b-6">
                    <span class="fs-15 lh-12 m-r-6" style="color: #4272d7;">
                      <i class="zmdi zmdi-circle"></i>
                    </span>

                    <a class="filter-link stext-106 trans-04 filter-link-active">Blue</a>
                  </li>

                  <li class="p-b-6">
                    <span class="fs-15 lh-12 m-r-6" style="color: #b3b3b3;">
                      <i class="zmdi zmdi-circle"></i>
                    </span>

                    <a class="filter-link stext-106 trans-04">Grey</a>
                  </li>

                  <li class="p-b-6">
                    <span class="fs-15 lh-12 m-r-6" style="color: #00ad5f;">
                      <i class="zmdi zmdi-circle"></i>
                    </span>

                    <a class="filter-link stext-106 trans-04">Green</a>
                  </li>

                  <li class="p-b-6">
                    <span class="fs-15 lh-12 m-r-6" style="color: #fa4251;">
                      <i class="zmdi zmdi-circle"></i>
                    </span>

                    <a class="filter-link stext-106 trans-04">Red</a>
                  </li>

                  <li class="p-b-6">
                    <span class="fs-15 lh-12 m-r-6" style="color: #aaa;">
                      <i class="zmdi zmdi-circle-o"></i>
                    </span>

                    <a class="filter-link stext-106 trans-04">White</a>
                  </li>
                </ul>
              </div>

              <div class="filter-col4 p-b-27">
                <div class="mtext-102 cl2 p-b-15">Tags</div>

                <div class="flex-w p-t-4 m-r--5">
                  <a
                    class="flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5"
                  >Fashion</a>

                  <a
                    class="flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5"
                  >Lifestyle</a>

                  <a
                    class="flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5"
                  >Denim</a>

                  <a
                    class="flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5"
                  >Streetstyle</a>

                  <a
                    class="flex-c-m stext-107 cl6 size-301 bor7 p-lr-15 hov-tag1 trans-04 m-r-5 m-b-5"
                  >Crafts</a>
                </div>
              </div>
            </div>
          </div>
        </transition>
      </div>

      <div class="row isotope-grid">
        <div
          v-for="item in limitProducts"
          :key="item.id"
          class="col-sm-6 col-md-4 col-lg-3 p-b-35 isotope-item"
        >
          <!-- Block2 -->
          <div class="block2">
            <div class="block2-pic hov-img0">
              <img :src="item.image" alt="IMG-PRODUCT" />

              <a
                class="block2-btn flex-c-m stext-103 cl2 size-102 bg0 bor2 hov-btn1 p-lr-15 trans-04 js-show-modal1"
                @click.prevent="showModal(item.id)"
              >Quick View</a>
            </div>

            <div class="block2-txt flex-w flex-t p-t-14">
              <div class="block2-txt-child1 flex-col-l">
                <a
                  href="product-detail.html"
                  class="stext-104 cl4 hov-cl1 trans-04 js-name-b2 p-b-6"
                >{{item.name}}</a>

                <span class="stext-105 cl3">${{item.price}}</span>
              </div>

              <div class="block2-txt-child2 flex-r p-t-3">
                <a class="btn-addwish-b2 dis-block pos-relative js-addwish-b2">
                  <img
                    class="icon-heart1 dis-block trans-04"
                    src="~@/assets/images/icons/icon-heart-01.png"
                    alt="ICON"
                  />
                  <img
                    class="icon-heart2 dis-block trans-04 ab-t-l"
                    src="~@/assets/images/icons/icon-heart-02.png"
                    alt="ICON"
                  />
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Load more -->
      <div class="flex-c-m flex-w w-full p-t-45">
        <a
          @click.prevent="loadMore()"
          class="flex-c-m stext-101 cl5 size-103 bg2 bor1 hov-btn1 p-lr-15 trans-04"
        >Load More</a>
      </div>
      <!-- Modal1 -->
      <ProductModal></ProductModal>
    </div>
  </section>
</template>

<script>
import { bus } from "../main";
import ProductModal from "@/components/ProductModal";
import RepositoryFactory from "@/repository/RepositoryFactory";
const ProductRepo = RepositoryFactory.get("product");
export default {
  components: {
    ProductModal
  },
  name: "Product",
  data() {
    return {
      showSearch: false,
      showFilter: false,
      showModal1: false,
      isShopPage: false,
      listProducts: [],
      limit: 16,
      searchText: null,
      resultProducts: [],
      listTypes: [],
      isFiltingType: 100,
      isFilting: false,
      filterprop: 0,
      priceFilter: 0
    };
  },
  computed: {
    limitProducts() {
      return this.resultProducts.slice(0, this.limit);
    }
  },
  created() {
    this.fetchProduct();
  },
  mounted() {
    if (this.$route.path == "/shop") this.isShopPage = true;
  },
  //props: ["showModal1"],
  methods: {
    filterPrice(to, from) {
      this.resultProducts = this.listProducts.filter(item => {
        if (this.isFiltingType != 100) {
          return (
            item.typeId == this.isFiltingType &&
            item.price >= to &&
            item.price < from
          );
        } else return item.price >= to && item.price < from;
      });
    },
    filterType(id) {
      if (id == 100) {
        this.resultProducts = this.listProducts;
      } else {
        this.resultProducts = this.listProducts.filter(
          item => item.typeId == id
        );
        this.isFiltingType = id;
      }
    },
    searchProduct(event) {
      const key = this.searchText.toLowerCase();
      if (event.key == "Backspace") {
        this.filterType(this.isFiltingType);
      } else {
        if (this.isFiltingType != 100) {
          this.resultProducts = this.resultProducts.filter(item =>
            item.name.toLowerCase().includes(key)
          );
        } else {
          this.resultProducts = this.listProducts.filter(item =>
            item.name.toLowerCase().includes(key)
          );
        }
      }
    },
    loadMore() {
      this.limit += 16;
    },
    activeSearch() {
      if (this.showSearch) {
        this.showSearch = false;
      } else {
        this.showSearch = true;
      }
      if (this.showFilter) this.showFilter = false;
    },
    activeFilter() {
      if (this.showFilter) {
        this.showFilter = false;
      } else {
        this.showFilter = true;
      }
      if (this.showSearch) this.showSearch = false;
    },
    showModal(id) {
      this.showModal1 = true;
      bus.$emit("modalChanged", {
        show: this.showModal1,
        productId: id
      });
    },
    async fetchProduct() {
      const list = (await ProductRepo.getIndexProducts()).data;
      const types = (await ProductRepo.getTypes()).data;
      this.listProducts = list;
      this.resultProducts = this.listProducts;
      this.listTypes = types;
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
.show-filter-panel-enter-active,
.show-filter-panel-leave-active,
.show-search-panel-enter-active,
.show-search-panel-leave-active {
  transition: all 0.5s linear;
}
.show-filter-panel-enter, .show-filter-panel-leave-to
/* Trước 2.1.8 thì dùng .show-filter-panel-leave-active */ {
  transform: translateY(0px);
  margin-bottom: -300px;
  opacity: 0;
}
.show-search-panel-enter, .show-search-panel-leave-to
/* Trước 2.1.8 thì dùng .show-search-panel-leave-active */ {
  transform: translateY(0px);
  margin-bottom: -80px;
  opacity: 0;
}
</style>
