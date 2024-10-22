using System;
using System.Collections.Generic;
using System.Linq;

namespace WebStore
{
    class Program
    {
        private static List<User> users = new List<User>();
        private static List<Product> products = new List<Product>();
        private static List<Order> orders = new List<Order>();

        static void Main(string[] args)
        {
            SeedUsers();
            SeedProducts();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome! Please select an option:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register (Customer)");
                Console.WriteLine("3. Register (Staff)");
                Console.WriteLine("4. Register (Admin)");
                Console.WriteLine("0. Exit");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        RegisterCustomer();
                        break;
                    case "3":
                        RegisterStaff();
                        break;
                    case "4":
                        RegisterAdmin();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }

        private static void SeedUsers()
        {
            users.Add(new Admin("Kim", "kim@mail.com", "1"));
            users.Add(new Staff("Harry", "harry@mail.com", "1"));
            users.Add(new Customer("Joe", "joe@mail.com", "1"));
        }

        private static void SeedProducts()
        {
            products.Add(new Product("0001", "Umbrella", 19.99m));
            products.Add(new Product("0002", "Phone", 29.99m));
            products.Add(new Product("0003", "Machine Washing", 39.99m));
        }

        private static void Login()
        {
            Console.Clear();
            Console.WriteLine("Login");
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            User user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                if (user is Admin admin)
                {
                    AdminMenu(admin);
                }
                else if (user is Staff staff)
                {
                    StaffMenu(staff);
                }
                else if (user is Customer customer)
                {
                    CustomerMenu(customer);
                }
            }
            else
            {
                Console.WriteLine("Invalid credentials. Please try again.");
                Console.ReadLine();
            }
        }

        private static void RegisterCustomer()
        {
            Console.Clear();
            Console.WriteLine("Register as a Customer");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            Customer customer = new Customer(name, email, password);
            users.Add(customer);
            Console.WriteLine("Registration successful. Your Customer ID: " + customer.CustomerID);
            Console.ReadLine();
        }

        private static void RegisterStaff()
        {
            Console.Clear();
            Console.WriteLine("Register as a Staff");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            Staff staff = new Staff(name, email, password);
            users.Add(staff);
            Console.WriteLine("Registration successful. Your Staff ID: " + staff.StaffID);
            Console.ReadLine();
        }

        private static void RegisterAdmin()
        {
            Console.Clear();
            Console.WriteLine("Register as an Admin");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            Admin admin = new Admin(name, email, password);
            users.Add(admin);
            Console.WriteLine("Registration successful. Your Admin ID: " + admin.AdminID);
            Console.ReadLine();
        }

        private static void AdminMenu(Admin admin)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome {admin.Name}! Admin Menu:");
                Console.WriteLine("1. Manage Products");
                Console.WriteLine("2. View Staff Accounts");
                Console.WriteLine("3. View Customer Accounts");
                Console.WriteLine("4. Delete User");
                Console.WriteLine("0. Logout");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        admin.ManageProduct(products);
                        break;
                    case "2":
                        admin.ViewStaffAccounts(users);
                        break;
                    case "3":
                        admin.ViewCustomerAccounts(users);
                        break;
                    case "4":
                        Console.WriteLine("Manage Users (Staff/Customer):");
                        Console.Write("Type (Staff/Customer): ");
                        string userType = Console.ReadLine();
                        admin.DeleteUser(users, userType);
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

        private static void StaffMenu(Staff staff)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome {staff.Name}! Staff Menu:");
                Console.WriteLine("1. Check Orders");
                Console.WriteLine("0. Logout");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        staff.CheckOrder(orders);
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


        private static void CustomerMenu(Customer customer)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome {customer.Name}! Customer Menu:");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Place an Order");
                Console.WriteLine("3. Make Payment");
                Console.WriteLine("0. Logout");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        customer.ViewProduct(products);
                        break;
                    case "2":
                        customer.PlaceOrder(products, orders);
                        break;
                    case "3":
                        customer.Payment();
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
    }
}