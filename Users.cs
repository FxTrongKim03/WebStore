using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore
{
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public void Login()
        {
            Console.WriteLine("Login as User");
        }

        public void ViewProduct(List<Product> products)
        {
            Console.WriteLine("Available Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"- {product.ProductName} (ID: {product.ProductID}, Price: {product.Price:C})");
            }
        }
    }

    public class Customer : User
    {
        public string CustomerID { get; private set; }

        public Customer(string name, string email, string password)
            : base(name, email, password)
        {
            CustomerID = "C" + GenerateRandomID();
        }

        public void Register()
        {
            Console.WriteLine("Customer Registered with ID: " + CustomerID);
        }

        public void PlaceOrder(List<Product> products, List<Order> orders)
        {
            Console.WriteLine("Select a product to order:");
            ViewProduct(products);

            Console.Write("Enter Product ID: ");
            string productId = Console.ReadLine();

            Product selectedProduct = products.Find(p => p.ProductID.Equals(productId, StringComparison.OrdinalIgnoreCase));

            if (selectedProduct != null)
            {
                Console.Write("Enter quantity: ");
                int quantity;
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Please enter a valid quantity.");
                }

                decimal totalPrice = selectedProduct.Price * quantity;

                Order newOrder = new Order(this.CustomerID, selectedProduct.ProductID, quantity, totalPrice, "Processing");
                orders.Add(newOrder);

                Console.WriteLine($"You have ordered {quantity} x {selectedProduct.ProductName} for a total of {totalPrice:C}.");
                Console.WriteLine("Your order has been placed successfully!");
            }
            else
            {
                Console.WriteLine("Product ID not found. Please try again.");
            }
        }


        public void Payment()
        {
            Console.WriteLine("Select Payment Method: 1. PayPal, 2. Credit Card");
            string option = Console.ReadLine();

            Payment payment;
            if (option == "1")
            {
                payment = new PaypalPayment();
            }
            else if (option == "2")
            {
                payment = new CreditCardPayment();
            }
            else
            {
                Console.WriteLine("Invalid payment method. Payment failed.");
                return;
            }

            payment.ProcessPayment(100);
        }

        private string GenerateRandomID()
        {
            Random random = new Random();
            return random.Next(0, 10000).ToString("D4");
        }
    }

    public class Staff : User
    {
        public string StaffID { get; private set; }

        public Staff(string name, string email, string password)
            : base(name, email, password)
        {
            StaffID = "ST" + GenerateRandomID();
        }

        public void CheckOrder(List<Order> orders)
        {
            Console.Clear();
            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found.");
                return;
            }

            Console.WriteLine("List of Orders:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.OrderID}");
                Console.WriteLine($"Customer ID: {order.CustomerID}");
                Console.WriteLine($"Product ID: {order.ProductID}");
                Console.WriteLine($"Quantity: {order.Quantity}");
                Console.WriteLine($"Total Price: {order.TotalPrice:C}");
                Console.WriteLine($"Status: {order.Status}");
                Console.WriteLine(new string('-', 20)); // Đường ngăn cách giữa các đơn hàng
            }

            Console.WriteLine("Select an order to update its status.");
            Console.Write("Enter Order ID: ");
            string orderId = Console.ReadLine();

            Order selectedOrder = orders.FirstOrDefault(o => o.OrderID == orderId);

            if (selectedOrder != null)
            {
                Console.WriteLine($"Selected Order {selectedOrder.OrderID} with Status: {selectedOrder.Status}");
                Console.WriteLine("1. Mark as Shipped");
                Console.WriteLine("2. Mark as Delivered");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        selectedOrder.Status = "Shipped";
                        Console.WriteLine("Order status updated to 'Shipped'.");
                        break;
                    case "2":
                        selectedOrder.Status = "Delivered";
                        Console.WriteLine("Order status updated to 'Delivered'.");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Order ID not found.");
            }
        }


        private string GenerateRandomID()
        {
            Random random = new Random();
            return random.Next(0, 10000).ToString("D4");
        }
    }

    public class Admin : User
    {
        public string AdminID { get; private set; }

        public Admin(string name, string email, string password)
            : base(name, email, password)
        {
            AdminID = "AD" + GenerateRandomID();
        }
        private string GenerateRandomID()
        {
            Random random = new Random();
            return random.Next(0, 10000).ToString("D4");
        }

        public void ManageProduct(List<Product> products)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Manage Products:");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("0. Back");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ViewProducts(products);
                        break;
                    case "2":
                        AddProduct(products);
                        break;
                    case "3":
                        DeleteProduct(products);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
                Console.ReadLine();
            }
        }

        private void ViewProducts(List<Product> products)
        {
            Console.WriteLine("Product List:");
            foreach (var product in products)
            {
                Console.WriteLine($"- {product.ProductName} (ID: {product.ProductID}, Price: {product.Price:C})");
            }
        }

        private void AddProduct(List<Product> products)
        {
            Console.Clear();
            Console.WriteLine("Add New Product");
            Console.Write("Product Name: ");
            string productName = Console.ReadLine();
            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            string productId;
            do
            {
                productId = GenerateRandomID();
            } while (products.Any(p => p.ProductID == productId));

            Product newProduct = new Product(productId, productName, price);
            products.Add(newProduct);

            Console.WriteLine($"Product '{productName}' added with ID: {productId}");
        }

        private void DeleteProduct(List<Product> products)
        {
            Console.Write("Enter Product ID to delete: ");
            string productId = Console.ReadLine();
            Product productToDelete = products.Find(p => p.ProductID == productId);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
                Console.WriteLine($"Product with ID '{productId}' has been deleted.");
            }
            else
            {
                Console.WriteLine($"Product with ID '{productId}' not found.");
            }
        }

        public void ViewStaffAccounts(List<User> users)
        {
            Console.WriteLine("Staff Accounts:");
            foreach (var user in users)
            {
                if (user is Staff staff)
                {
                    Console.WriteLine($"- {staff.Name} (ID: {staff.StaffID}, Email: {staff.Email})");
                }
            }
        }

        public void ViewCustomerAccounts(List<User> users)
        {
            Console.WriteLine("Customer Accounts:");
            foreach (var user in users)
            {
                if (user is Customer customer)
                {
                    Console.WriteLine($"- {customer.Name} (ID: {customer.CustomerID}, Email: {customer.Email})");
                }
            }
        }

        public void DeleteUser(List<User> users, string userType)
        {
            Console.Write($"Enter the email of the {userType} to delete: ");
            string email = Console.ReadLine();
            User userToDelete = users.Find(u => u.Email == email);

            if (userToDelete != null)
            {
                users.Remove(userToDelete);
                Console.WriteLine($"{userType} with email '{email}' has been deleted.");
            }
            else
            {
                Console.WriteLine($"{userType} with email '{email}' not found.");
            }
        }
    }
}

