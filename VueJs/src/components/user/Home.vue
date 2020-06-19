<template>
  <div>
    <!-- Slider -->
    <Slider></Slider>

    <!-- Banner -->
    <Banner></Banner>

    <!-- Product -->
    <Product></Product>
  </div>
</template>

<script>
import Banner from "../Banner.vue";
import Slider from "../Slider.vue";
import Product from "../Product.vue";
export default {
  components: { Banner, Slider, Product }
  // declare Select2Component
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
