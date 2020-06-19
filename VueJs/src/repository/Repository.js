import axios from 'axios';
import mgr from '../services/security';
//import { store } from '../store.js';

//const baseDomain = "http://localhost:5000";
//const token = store.state.token;
//const baseUrl = `${baseDomain}`;

const repository = axios.create();
repository.interceptors.request.use(async (config) => {
    mgr.getUser().then(success => {
        if (success) {
            const access_token = success.access_token;
            config.headers.Authorization = `Bearer ${access_token}`;
        }
    }, error => {
        console.log(error);
    })
    return config;
});

repository.interceptors.response.use(response => {
    return response;
}, error => {
    if (error.response) {
        if (error.response.status === 401) {
            const redirectUrl = window.location.pathname;
            console.log(error.response);
            alert("chức năng này cần phải đăng nhập");
            mgr.signinRedirect({ state: redirectUrl });
        } else {
            return Promise.reject(error);
        }
    }
    else {
        return Promise.reject(error);
    }
});

// export default axios.create({
//     baseURL : baseUrl,
//     headers: {
//         "Authorization": `Bearer ${token}`,
//     }
// })

export default repository;