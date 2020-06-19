<template>
  <div class="container-fluid">
    <!-- Page Heading -->
    <h1 class="h3 mb-4 text-gray-800">Add Product</h1>
    <div>
      <div class="form-group">
        <input
          type="text"
          v-model="productData.name"
          class="form-control"
          placeholder="Tên sản phẩm"
        />
      </div>
      <div class="form-group row">
        <div class="col-sm-6 mb-3 mb-sm-0">
          <input
            type="text"
            v-model="productData.price"
            class="form-control"
            placeholder="Giá sản phẩm"
          />
        </div>
        <div class="col-sm-6">
            <select class="form-control" v-model="productData.typeid">
              <option v-for="type in types" :value="type.id" :key="type.id">{{type.name}}</option>
            </select>
        </div>
      </div>
      <div class="uploader-box" style="margin:20px 0; display: flex; justify-content: left;">
        <VueUpload
          @upload-success="uploadImageSuccess"
          @before-remove="beforeRemove"
          @edit-image="editImage"
          :data-images="uploaderProps.images"
          :dragText="uploaderProps.dragText"
          :browseText="uploaderProps.browseText"
          :showPrimary="uploaderProps.showPrimary"
          :maxImage="3"
          idUpload="myIdUpload"
          editUpload="myIdEdit"
        ></VueUpload>
      </div>
      <!-- <div class="custom-file" style="margin-bottom:1rem">
        <VueUpload
          @upload-success="uploadImageSuccess"
          @before-remove="beforeRemove"
          @edit-image="editImage"
          @data-change="dataChange"
          :data-images="images"
        ></VueUpload>
      </div>-->
    </div>
    <button class="btn btn-primary" @click="addProduct()">Add</button>
  </div>
</template>

<script>
import RepositoryFactory from "@/repository/RepositoryFactory";
import VueUpload from "vue-upload-multiple-image";
const ProductRepository = RepositoryFactory.get("product");
//const CartRepo = RepositoryFactory.get('cart');
export default {
  components: {
    VueUpload
  },
  data() {
    return {
      types: [],
      productData: {
        name: "",
        typeid: '1',
        price: "",
        listImages: []
      },
      uploaderProps: {
        images: [],
        dragText: "Drag your image",
        browseText: "(Or) Browse",
        showPrimary: false
      }
    };
  },
  created() {
    this.fetchData()
  },
  methods: {
    async fetchData() {
      const {data} = await ProductRepository.getTypes();
      this.types = data;
      //console.log(await CartRepo.get("3a38ec11-3839-42e9-bceb-25ea2107087d"));

    },
    addProduct() {
      ProductRepository.create(this.productData);
      this.$swal.fire({
        toast: true,
        showConfirmButton: false,
        timer: 3000,
        icon: "success",
        title: "Add product success"
      })
      this.productData = {
        name: "",
        typeid: '1',
        price: "",
        listImages: []
      };
      this.uploaderProps = {
        images: [],
        dragText: "Drag your image",
        browseText: "(Or) Browse",
        showPrimary: false
      }
    },
    uploadImageSuccess(formData, index, fileList) {
      this.productData.listImages = fileList;
    },
    beforeRemove(index, done, fileList) {
      console.log("index", index, fileList);
      var r = confirm("remove image");
      if (r == true) {
        done();
      } else {
        console.log("delete error");
      }
    },
    editImage(formData, index, fileList) {
      console.log("edit data", formData, index, fileList);
    }
  }
  // declare Select2Component
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
</style>
