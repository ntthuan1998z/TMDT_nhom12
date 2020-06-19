import Vue from "vue";
import VueRouter from "vue-router";
import User from "../views/User.vue";
import Admin from '../views/Admin.vue';
import Home from '../components/user/Home.vue';
import Product from '@/components/Product';
import Checkout from '@/components/user/Checkout';
import AdminManageOrder from '@/components/admin/AdminManageOrder'
//import Login from '@/components/user/Login'
import AdminBlankPage from '@/components/admin/AdminBlankPage';
import AdminAddProduct from '@/components/admin/AdminAddProduct';
import AdminManageProduct from '@/components/admin/AdminManageProduct';
import Callback from '../views/Callback.vue';
import { store } from '../store.js';
import Unauthorized from '@/views/Unauthorized';
import mgr from '../services/security.js';
Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    component: User,
    children: [
      { path: '', component: Home },
      { path: 'shop', component: Product },
      { path: 'shoping-cart', component: Checkout },
      // {path:'login', component: Login}
    ]
  },
  {
    path: "/Unauthorized",
    component: Unauthorized
  },
  {
    path: "/admin",
    name: "Admin",
    component: Admin,
    meta: {
      requiresAuth: true,
      adminAuth: true,
    },
    children: [
      {
        path: 'blank',
        component: AdminBlankPage,
      },
      {
        path: 'add-product',
        name: 'add-product',
        component: AdminAddProduct,
      },
      {
        path: 'manage-product',
        name: 'manage-product',
        component: AdminManageProduct,
      },
      {
        path: 'manage-order',
        name: 'manage-order',
        component: AdminManageOrder,
      }
    ]
  },
  {
    path: "/about",
    name: "About",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue")
  },
  {
    path: '/callback',
    name: 'callback',
    component: Callback
  },
];

let router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  linkActiveClass: 'active-menu',
  routes
});
export default router;

router.beforeEach(async (to, from, next) => {
  const isLoginError = (await store.state.isAuthenticated).error;

  if (to.matched.some(m => m.meta.requiresAuth)) {
    mgr.getUser().then(
      success => {
        if (success) {   
          if (!isLoginError) {
            if (to.matched.some(m => m.meta.adminAuth)) {
              if (success.profile.role.includes("Admin")) {
                next()
              } else {
                router.push("/Unauthorized");
              }
            } else {
              next();
            }
          } else {
            mgr.signinRedirect({ state: to.path });
          }
        } else {
          mgr.signinRedirect({ state: to.path });
        }
      },
      err => {
        console.log(err);
      }
    )
  } else {
    next();
  }

});

