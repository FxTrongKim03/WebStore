using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore
{
    public class Product
    {
        public string ProductID { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }

        public Product(string productId, string productName, decimal price)
        {
            ProductID = productId;
            ProductName = productName;
            Price = price;
        }
    }
}
