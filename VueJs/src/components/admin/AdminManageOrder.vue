<template>
  <div class="container-fluid">
    <!-- Page Heading -->
    <h1 class="h3 mb-4 text-gray-800">Order Manage</h1>
    <!-- <h1 class="h3 mb-4 text-gray-800">{{products}}</h1> -->
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
            <i class="fa fa-check"></i>
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
const OrderRepository = RepositoryFactory.get("order");
import Vuetable from "vuetable-2";
import VuetablePagination from "vuetable-2/src/components/VuetablePagination";
import _ from "lodash";
import orderFields from '@/components/admin/orderFields';
export default {
  components: {
    Vuetable,
    VuetablePagination
  },
  data() {
    return {
      fields: orderFields,
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
      orderData: {}
    };
  },
  created() {
    //this.fetchTypes();
  },
  watch: {
    data() {
      this.$refs.vuetable.refresh();
    }
  },
  mounted() {
    OrderRepository.get().then(response => {
      this.data = response.data;
      //console.log(this.data);
    });
  },
  methods: {
    // async fetchTypes() {
    //   const { data } = await ProductRepository.getTypes();
    //   this.types = data;
    // },
    editProduct() {
      OrderRepository.put(this.orderData.id, this.orderData);
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
      this.$swal
        .fire({
          toast: true,
          icon: "question",
          title: "Confirm update order?",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Yes!"
        })
        .then(result => {
          if (result.isConfirmed) {
            if (data.status < 2) {
              data.status += 1;
            }
            console.log(OrderRepository.put(data));
          }
        });
    },
    deleteItem(data) {
      this.$swal
        .fire({
          toast: true,
          icon: "warning",
          title: "Are you sure cancel this order?",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Yes, delete it!"
        })
        .then(result => {
          if (result.isConfirmed) {
            const index = this.data.findIndex(x => x.id == data.id);
            this.$delete(this.data, index);
            OrderRepository.delete(data.id);
            this.data;        
          }
        });
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
