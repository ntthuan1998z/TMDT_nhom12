import Repository from './Repository';


const resource = 'http://localhost:5004/api/order';

// const Repos = Repository.interceptors.request.use((config) => {
//     config.baseURL = 'http://localhost:5000';
//     return config;
// })
export default {
    get() {
        //console.log(Repository.get(`${resource}`))
        return Repository.get(`${resource}`);
    },
    post(order) {
        return Repository.post(`${resource}`, order); 
    },
    delete(id) {
        return Repository.delete(`${resource}/${id}`);
    },
    put(order) {
        return Repository.put(`${resource}`, order);
    }
}
