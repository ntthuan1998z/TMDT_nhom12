import Repository from './Repository';


const resource = 'http://localhost:5000/api/product';

export default {
    get() {
        //console.log(Repository.get(`${resource}`))
        return Repository.get(`${resource}`);
    },
    getProduct(id) {
        return Repository.get(`${resource}/${id}`);
    },
    getImage(id) {
        return Repository.get(`${resource}/image/${id}`);
    },
    getImageCart(id) {
        return Repository.get(`${resource}/img/${id}`);
    },
    getName(id) {
        return Repository.get(`${resource}/name/${id}`);
    },
    put(id, product) {
        return Repository.put(`${resource}/${id}`, product);
    },

    create(product) {
        return Repository.post(`${resource}`, product);
    },
    delete(id) {
        return Repository.delete(`${resource}/${id}`);
    },
    getTypes() {
        return Repository.get(`${resource}/type`);
    },
    getIndexProducts() {
        return Repository.get(`${resource}/index`);
    }
}
