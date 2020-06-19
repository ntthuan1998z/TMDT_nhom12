import RepositoryFactory from '@/repository/RepositoryFactory'
const ProductRepo = RepositoryFactory.get('product');
var types = [];
ProductRepo.getTypes().then(result => {
    types = result.data;
});
export default [
  {
    name: 'name',
    title: 'Product Name',
  },
  {
    name: 'price',
    sortField: 'price'
  },
  {
    name: 'typeId',
    title: 'Type',
    formatter: (value) => {
        const type = types.find(element => element.id == value);
      return type.name;
    }
  },
  'actions'
]