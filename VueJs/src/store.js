import Vue from 'vue';
import vuex from 'vuex';
import mgr from './services/security';
import RepositoryFactory from '@/repository/RepositoryFactory';
const CartRepo = RepositoryFactory.get('cart');
Vue.use(vuex);
export const store = new vuex.Store({
    state: {
        baseDomain: 'http://localhost:5000',
        isAuthenticated: mgr.querySessionStatus().then(success => {
            return success;
        })
            .catch(error => {
                return error;
            }),
        user: mgr.getUser(),
        amountInCart: 0,
        cart: [],
    },
    mutations: {
        updateUser(state) {
            state.user = mgr.getUser()
        },
        getCart(state, customerid) {
            state.cart = CartRepo.get(customerid);
        },
        updateCart(state, cart) {
            CartRepo.put(cart).then(re => {
                console.log(re)
            })
            state.cart = cart;
        },
        updateAmountCart(state, amount) {
            state.amountInCart = amount;
        },
        setEmptyCart(state) {
            state.cart = [];
            state.amountInCart = 0;
        }
    },
    actions: {
        async getCart(context) {
            const isLoginError = (await store.state.isAuthenticated).error;
            if (!isLoginError) {
                const user = (await store.state.user);
                var id = "";
                if (user) {
                    id = user.profile.sub;
                    context.commit('getCart', id);
                }
                //console.log(store.state.cart)
                var amount = 0;
                var t_id = "";
                const cart = (await store.state.cart).data;
                //console.log(cart);
                if (cart != "" && cart != undefined) {
                    t_id = cart.id;
                    amount = cart.items.length;
                    context.commit('updateAmountCart', amount);
                };
                if (t_id != id && id != "") {
                    const temp = { "Id": id, "items": [] }
                    CartRepo.put(temp);
                    context.commit('getCart', id);
                };
            }
            else {
                context.commit('setEmptyCart');
            }
        },
        updateCart(context, cart) {
            const amount = cart.items.length;
            context.commit('updateAmountCart', amount);
            context.commit('updateCart', cart);
        },
        finishCart(context, id) {
            CartRepo.delete(id);
            context.commit('setEmptyCart');
        }
    }
});