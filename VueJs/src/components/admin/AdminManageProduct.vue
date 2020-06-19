<template>
  <div class="container-fluid">
    <!-- Page Heading -->
    <h1 class="h3 mb-4 text-gray-800">Product Manage</h1>
    <!-- <h1 class="h3 mb-4 text-gray-800">{{products}}</h1> -->
    <transition>
      <div v-if="showPanel" class="container">
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
            <select class="form-control" v-model="productData.typeId">
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
        <button class="btn btn-primary" @click="editProduct()">Edit</button>
      </div>
    </transition>
    <div class="ui container">
      <vuetable
        ref="vuetable"
        :api-mode="false"
        :per-page="5"
        :fields="fields"
        :data-manager="dataManager"
        pagination-path="pagination"
        @vuetable:pagination-data="onPaginationData"
      >
        <div slot="actions" slot-scope="props">
          <button class="btn btn-outline-info btn-sm" @click="onActionClicked(props.rowData)">
            <i class="fa fa-edit"></i>
          </button>
          <button class="btn btn-outline-danger btn-sm" @click="deleteItem(props.rowData)">
            <i class="fa fa-trash"></i>
          </button>
        </div>
      </vuetable>
      <div style="padding-top:10px">
        <VuetablePagination
          :css="cssPagination"
          ref="pagination"
          @vuetable-pagination:change-page="onChangePage"
        ></VuetablePagination>
      </div>
    </div>
  </div>
</template>

<script>
import RepositoryFactory from "@/repository/RepositoryFactory";
const ProductRepository = RepositoryFactory.get("product");
import Vuetable from "vuetable-2";
import VueUpload from "vue-upload-multiple-image";
import VuetablePagination from "vuetable-2/src/components/VuetablePagination";
import _ from "lodash";
import productFields from '@/components/admin/productFields';
export default {
  components: {
    Vuetable,
    VuetablePagination,
    VueUpload
  },
  data() {
    return {
      fields: productFields,
      showPanel: false,
      data: [],
      errors: [],
      values: [],
      cssPagination: {
        wrapperClass: "pagination float-right",
        activeClass: "active",
        disabledClass: "disabled",
        pageClass: "page-link",
        linkClass: "page-link",
        paginationClass: "pagination",
        paginationInfoClass: "float-left",
        dropdownClass: "form-control",
        icons: {
          first: "fa fa-angle-double-left",
          prev: "fa fa-chevron-left",
          next: "fa fa-chevron-right",
          last: "fa fa-angle-double-right"
        }
      },
      types: [],
      productData: {
        id: null,
        name: "",
        typeId: null,
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
    this.fetchTypes();
  },
  watch: {
    data() {
      this.$refs.vuetable.refresh();
    }
  },
  mounted() {
    ProductRepository.get().then(response => {
      this.data = response.data;
    });
  },
  methods: {
    async fetchTypes() {
      const { data } = await ProductRepository.getTypes();
      this.types = data;
    },
    editProduct() {
      ProductRepository.put(this.productData.id, this.productData);
      this.$swal.fire({
        toast: true,
        showConfirmButton: false,
        timer: 3000,
        icon: "success",
        title: "Update product success"
      });
      this.showPanel = false;
    },
    onPaginationData(paginationData) {
      //console.log(paginationData);
      this.$refs.pagination.setPaginationData(paginationData);
    },
    onChangePage(page) {
      this.$refs.vuetable.changePage(page);
    },
    dataManager(sortOrder, pagination) {
      if (this.data.length < 1) return;

      let local = this.data;
      //console.log(local);

      // sortOrder can be empty, so we have to check for that as well
      if (sortOrder.length > 0) {
        //console.log("orderBy:", sortOrder[0].sortField, sortOrder[0].direction);
        local = _.orderBy(
          local,
          sortOrder[0].sortField,
          sortOrder[0].direction
        );
      }

      pagination = this.$refs.vuetable.makePagination(
        local.length,
        this.perPage
      );
      //console.log("pagination:", pagination);
      let from = pagination.from - 1;
      let to = from + pagination.per_page;

      //console.log(_.slice(local, from, to));
      return {
        pagination: pagination,
        data: _.slice(local, from, to)
      };
    },
    onActionClicked(data) {
      this.showPanel = true;
      data.listImages.forEach(async element => {
        element.path = (await ProductRepository.getImage(element.id)).data;
        //console.log((await ProductRepository.getImage(element.id)).data);
      });
      this.uploaderProps.images = data.listImages;
      this.productData = data;
      //console.log(data.listImages);
    },
    deleteItem(data) {
      this.$swal
        .fire({
          toast: true,
          icon: "warning",
          title: "Are you sure?",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Yes, delete it!"
        })
        .then((result) => {
          if (result.isConfirmed) {
            ProductRepository.delete(data.id);
            const index = this.data.findIndex(x => x.id == data.id);
            this.$delete(this.data, index);
          }
        });
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
button.ui.button {
  padding: 8px 3px 8px 10px;
  margin-top: 1px;
  margin-bottom: 1px;
}
/deep/ {
  .active {
    background: rgba(0, 0, 0, 0.05) !important;
    font-weight: 400 !important;
  }
  .disabled {
    cursor: default;
    background-color: transparent !important;
    color: rgba(40, 40, 40, 0.3) !important;
  }
}
</style>
