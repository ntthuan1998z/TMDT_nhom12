<template>
  <div class="wrap-slick3 flex-sb flex-w">
    <div class="wrap-slick3-dots"></div>
    <div class="wrap-slick3-arrows flex-sb-m flex-w"></div>
    <div class="slick3 gallery-lb">
      <div v-for="img in listImages" :key="img.id" class="item-slick3">
        <div class="wrap-pic-w pos-relative">
          <img :src="img.path" alt="IMG-PRODUCT" />

          <a
            class="flex-c-m size-108 how-pos1 bor0 fs-16 cl10 bg0 hov-btn3 trans-04"
            href="images/product-detail-01.jpg"
          >
            <i class="fa fa-expand"></i>
          </a>
        </div>
      </div>

      <!-- <div class="item-slick3">
                    <div class="wrap-pic-w pos-relative">
                      <img src="~@/assets/images/product-detail-02.jpg" alt="IMG-PRODUCT" />

                      <a
                        class="flex-c-m size-108 how-pos1 bor0 fs-16 cl10 bg0 hov-btn3 trans-04"
                        href="images/product-detail-02.jpg"
                      >
                        <i class="fa fa-expand"></i>
                      </a>
                    </div>
                  </div>
                  

                  <div class="item-slick3">
                    <div class="wrap-pic-w pos-relative">
                      <img src="~@/assets/images/product-detail-03.jpg" alt="IMG-PRODUCT" />

                      <a
                        class="flex-c-m size-108 how-pos1 bor0 fs-16 cl10 bg0 hov-btn3 trans-04"
                        href="images/product-detail-03.jpg"
                      >
                        <i class="fa fa-expand"></i>
                      </a>
                    </div>
      </div>-->
    </div>
  </div>
</template>
<script>
export default {
  mounted() {},
  props: ["listImages"],
  watch: {
    listImages: function() {
      this.$nextTick(() => {
        $(".wrap-slick3").each(function() {
          $(this)
            .find(".slick3")
            .slick({
              slidesToShow: 1,
              slidesToScroll: 1,
              fade: true,
              infinite: true,
              autoplay: false,
              autoplaySpeed: 6000,

              arrows: true,
              appendArrows: $(this).find(".wrap-slick3-arrows"),
              prevArrow:
                '<button class="arrow-slick3 prev-slick3"><i class="fa fa-angle-left" aria-hidden="true"></i></button>',
              nextArrow:
                '<button class="arrow-slick3 next-slick3"><i class="fa fa-angle-right" aria-hidden="true"></i></button>',

              dots: true,
              appendDots: $(this).find(".wrap-slick3-dots"),
              dotsClass: "slick3-dots",
              customPaging: function(slick, index) {
                //console.log($(slick.$slides[index]).find('.item-slick3').find('img').attr('src'));
                var portrait = $(slick.$slides[index])
                  .find(".item-slick3")
                  .find("img")
                  .attr("src");
                return (
                  '<img src=" ' +
                  portrait +
                  ' "/><div class="slick3-dot-overlay"></div>'
                );
              }
            });
        });
      });
    }
  }
};
</script>