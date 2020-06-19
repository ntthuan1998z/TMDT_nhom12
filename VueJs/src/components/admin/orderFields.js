
export default [
  {
    name: 'customerName',
    title: 'Product Name',
  },
  {
    name: 'orderDate',
    sortField: 'price'
  },
  {
    name: 'status',
    formatter: (value) => {
        let status = "Delivered";
        switch(value) {
            case 0: 
            status = "Preparing";
            break;
            case 1:
            status = "Shipped";
            break;
            default:
                break; 
        }
        return status;
    }
  },
  'address',
  'total',
  'phoneNumber',
  'actions'
]