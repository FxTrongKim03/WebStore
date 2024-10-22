using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore
{
    public class Order
    {
        public string OrderID { get; private set; }
        public string CustomerID { get; private set; }
        public string ProductID { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string Status { get; set; }

        public Order(string customerID, string productID, int quantity, decimal totalPrice, string status)
        {
            OrderID = "O" + GenerateRandomID();
            CustomerID = customerID;
            ProductID = productID;
            Quantity = quantity;
            TotalPrice = totalPrice;
            Status = status;
        }
        private string GenerateRandomID()
        {
            Random random = new Random();
            return random.Next(0, 10000).ToString("D4");
        }
    }
}
