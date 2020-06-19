 import ProductRepository from './ProductRepository';
 import CartRepository from './CartRepository';
import OrderRepository from './OrderRepository';

 const repositories = {
     'product': ProductRepository,
     'cart': CartRepository,
     'order': OrderRepository,
 };

 export default {
     get: name => repositories[name]
 }