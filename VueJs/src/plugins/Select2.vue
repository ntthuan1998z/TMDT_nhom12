<template>
  <!-- <div class="size-204 respon6-next">
      <div class="rs1-select2 bor8 bg0">
        <select2
          class="js-select2"
          v-model="myValue"
          :options="myOptions"
          @change="myChangeEvent($event)"
          @select="mySelectEvent($event)"
        />
      </div>
    </div>
  <h4>Value: {{ myValue }}</h4>-->
  <!-- <div class="size-204 respon6-next">
    <div class="rs1-select2 bor8 bg0">
      <select class="js-select2" name="time">
        <option v-for="option in options" :key="option.key">{{option.value}}</option>
      </select>
      <div class="dropDownSelect2"></div>
    </div>
  </div> -->
  <div>
      <select ref="sele" class="js-select2" name="time">
        <option v-for="option in options" :key="option.key">{{option.value}}</option>
      </select>
      <div class="dropDownSelect2"></div>
    </div>
</template>

<script>
//import Select2 from "v-select2-component";
import "select2";
export default {
  components: {
    //Select2,
  },
  mounted() {
    var vm = this;
    var dropDown = $(vm.$el).find(".dropDownSelect2");
    var select = $(vm.$el).find(".js-select2");
    $(select).select2({
      minimumResultsForSearch: 20,
      dropdownParent: dropDown,
    });
  },
  props: ["options", "selected"],
  beforeDestroy: function() {
    $(this.$refs.sele).select2("destroy");
  },
  // declare Select2Component
  data() {
    return {};
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>

