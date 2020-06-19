<template>
  <section class="section-slide">
    <div class="wrap-slick1" ref="wrapSlick1">
      <slick
        ref="slick"
        @afterChange="handleAfterChange"
        @init="handleInit"
        @beforeChange="handleBeforeChange"
        :options="slickOptions"
      >
        <div
          class="item-slick1"
          :style="{'background-image': itemSlicks[index].img}"
          v-for="(item, index) in itemSlicks" :key="index"
        >
          <div class="container h-full">
            <div class="flex-col-l-m h-full p-t-100 p-b-30 respon5">
              <div
                class="layer-slick1 animated visible-false"
                :class="activeLayer[0] ? [item.appearLayer[0], visibleT] : {}"
              >
                <span class="ltext-101 cl2 respon2">{{item.text1}}</span>
              </div>

              <div
                class="layer-slick1 animated visible-false"
                :class="activeLayer[1] ? [item.appearLayer[1], visibleT] : {}"
              >
                <h2 class="ltext-201 cl2 p-t-19 p-b-43 respon1">{{item.text2}}</h2>
              </div>

              <div
                class="layer-slick1 animated visible-false"
                :class="activeLayer[2] ? [item.appearLayer[2], visibleT] : {}"
              >
                <a
                  href="product.html"
                  class="flex-c-m stext-101 cl0 size-101 bg1 bor1 hov-btn1 p-lr-15 trans-04"
                >Shop Now</a>
              </div>
            </div>
          </div>
        </div>
      </slick>
    </div>
  </section>
</template>

<script>
import Slick from "vue-slick";
export default {
  components: { Slick },
  data() {
    return {
      itemSlicks : [
        {
          text1 : 'Women Collection 2018',
          text2 : 'NEW SEANSON',
          img: 'url('+require('@/assets/images/slide-01.jpg')+')',
          appearLayer: ["fadeInDown", "fadeInUp", "zoomIn"]
        },
        {
          text1 : 'Men New-Season',
          text2 : 'Jackets & Coats',
          img: 'url('+require('@/assets/images/slide-02.jpg')+')',
          appearLayer: ["rollIn", "lightSpeedIn", "slideInUp"],
        },
        {
          text1 : 'Men Collection 2018',
          text2 : 'New arrivals',
          img: 'url('+require('@/assets/images/slide-03.jpg')+')',
          appearLayer: ["rotateInDownLeft", "rotateInUpRight", "rotateIn"],
        }
      ],
      visibleT: "visible-true",
      actionSlick: [],
      activeLayer: [],
      actionDelay: [0, 800, 1600],
      numberLayer: 3,
      slickOptions: {
        pauseOnFocus: false,
        pauseOnHover: false,
        slidesToShow: 1,
        slidesToScroll: 1,
        fade: true,
        speed: 1000,
        infinite: true,
        autoplay: true,
        autoplaySpeed: 6000,
        arrows: true,
        appendArrows: this.$refs.wrapSlick1,
        prevArrow:
          '<button class="arrow-slick1 prev-slick1"><i class="zmdi zmdi-caret-left"></i></button>',
        nextArrow:
          '<button class="arrow-slick1 next-slick1"><i class="zmdi zmdi-caret-right"></i></button>',
        customPaging: function(slick, index) {
          var linkThumb = $(slick.$slides[index]).data("thumb");
          var caption = $(slick.$slides[index]).data("caption");
          return (
            '<img src="' +
            linkThumb +
            '">' +
            '<span class="caption-dots-slick1">' +
            caption +
            "</span>"
          );
        }
        // Any other options that can be got from plugin documentation
      }
    };
  },
  methods: {
    next() {
      this.$refs.slick.next();
    },

    prev() {
      this.$refs.slick.prev();
    },

    reInit() {
      // Helpful if you have to deal with v-for to update dynamic lists
      this.$nextTick(() => {
        this.$refs.slick.reSlick();
      });
    },
    handleInit() {
      var i;
      for (i = 0; i < this.actionSlick.length; i++) {
        clearTimeout(this.actionSlick[i]);
      }
      for (i = 0; i < this.numberLayer; i++) {
        this.actionSlick[i] = setTimeout(
          function() {
            this.activeLayer.push(true);
          }.bind(this),
          this.actionDelay[i]
        );
      }
    },
    handleBeforeChange() {
      //console.log("handleBeforeChange", event, slick, currentSlide, nextSlide);
      this.activeLayer = [];
    },
    handleAfterChange() {
      var i;
      for (i = 0; i < this.actionSlick.length; i++) {
        clearTimeout(this.actionSlick[i]);
      }
      for (i = 0; i < this.numberLayer; i++) {
        this.actionSlick[i] = setTimeout(
          function() {
            this.activeLayer.push(true);
          }.bind(this),
          this.actionDelay[i]
        );
      }
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
