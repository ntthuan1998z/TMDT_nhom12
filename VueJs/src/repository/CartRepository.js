import Repository from './Repository';


const resource = 'http://localhost:5002/api/cart';

// const Repos = Repository.interceptors.request.use((config) => {
//     config.baseURL = 'http://localhost:5000';
//     return config;
// })
export default {
    get(id) {
        //console.log(Repository.get(`${resource}`))
        return Repository.get(`${resource}/${id}`);
    },
    put(cart) {
        return Repository.put(`${resource}`, cart); 
    },
    delete(id) {
        return Repository.delete(`${resource}/${id}`);
    }
}
