<template>
  <div class="container-fluid">
    <!-- Page Heading -->
    <h1 class="h3 mb-4 text-gray-800">{{values}}</h1>
    <h1 class="h3 mb-4 text-gray-800">{{products}}</h1>
    <button>Call API</button>
  </div>
</template>

<script>
import RepositoryFactory from '@/repository/RepositoryFactory';
const ProductRepository = RepositoryFactory.get('product');
const CartRepository = RepositoryFactory.get('cart');
//import ProductRepository from '@/repository/ProductRepository';
export default {
  data() {
    return {
      products: [],
      errors: [],
      values: [],
    };
  },
  created() {
    this.fetchData()
    // axios.get('http://localhost:5000/api/product/1')
    // .then(response => {
    //   this.products = response.data
    // })
    // .catch(e => {
    //   this.errors.push(e)
    // })
  },
  methods: {
    async fetchData() {
      //console.log(ProductRepository.get());
      //const {data} = await ProductRepository.get();
      let { data } = await ProductRepository.get();
      this.products = data;
      const d = (await CartRepository.get('1')).data;
      this.values = d;
    }
  }
  // declare Select2Component
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
