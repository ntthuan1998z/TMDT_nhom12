<template>
<div id="admin-container">
  <body id="page-top">
    <!-- Page Wrapper -->
    <div id="wrapper">
      <!-- Sidebar -->
      <sidebar></sidebar>
      <!-- End of Sidebar -->

      <!-- Content Wrapper -->
      <div id="content-wrapper" class="d-flex flex-column">
        <!-- Main Content -->
        <div id="content">
          <!-- Topbar -->
          <navbar></navbar>
          <!-- End of Topbar -->

          <!-- Begin Page Content -->
          <router-view></router-view>
          <!-- /.container-fluid -->
        </div>
        <!-- End of Main Content -->

        <!-- Footer -->
        <footer class="sticky-footer bg-white">
          <div class="container my-auto">
            <div class="copyright text-center my-auto">
              <span>Copyright &copy; Your Website 2019</span>
            </div>
          </div>
        </footer>
        <!-- End of Footer -->
      </div>
      <!-- End of Content Wrapper -->
    </div>
    <!-- End of Page Wrapper -->

    <!-- Scroll to Top Button-->
    <a class="scroll-to-top rounded" href="#page-top">
      <i class="fas fa-angle-up"></i>
    </a>

    <!-- Logout Modal-->
  </body>
</div>
</template>
<style lang="scss" scoped>
@import "~@/assets/admin/sb-admin-2.scss";
</style>
<script>
import Sidebar from "../components/admin/Sidebar.vue";
import Navbar from '../components/admin/Navbar'
export default {
  components: { Sidebar, Navbar }
};
// @ is an alias to /src
//
</script>
