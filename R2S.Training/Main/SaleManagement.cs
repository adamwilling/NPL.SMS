using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.Entities;
using R2S.Training.Domain;
namespace R2S.Training
{
    class SaleManagement
    {

        static void Main()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            #region Giao diện chính
            int choice;
            do
            {
                Console.WriteLine("=================================================Sale Management=================================================");
                Console.WriteLine("0. Exit program.");
                Console.WriteLine("1. Get all customer.");
                Console.WriteLine("2. Get all orders by customer id.");
                Console.WriteLine("3. Get all items by order id");
                Console.WriteLine("4. Compute order total by order id.");
                Console.WriteLine("5. Add customer.");
                Console.WriteLine("6. Delete customer.");
                Console.WriteLine("7. Update customer.");
                Console.WriteLine("8. Add order.");
                Console.WriteLine("9. Add line item.");
                Console.WriteLine("10. Update order total.");
                Console.Write("- Enter your choice: ");
                do
                {
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice < 0 || choice > 12)
                    {
                        Console.Write("* Invalid choice! Re-enter: ");
                    }
                } while (choice < 0 || choice > 12);
                switch (choice)
                {
                    case 0:
                        break;
                    case 1:
                        {
                            GetAllCustomer();
                            break;
                        }
                    case 2:
                        {
                            int customerId;
                            Console.Write("- Enter customer id: ");
                            customerId = int.Parse(Console.ReadLine());

                            GetAllOrdersByCustomerId(customerId);

                            break;
                        }
                    case 3:
                        {
                            int orderId;
                            Console.Write("- Enter order id: ");
                            orderId = int.Parse(Console.ReadLine());

                            GetAllItemsByOrderId(orderId);

                            break;
                        }
                    case 4:
                        {
                            int orderId;
                            Console.Write("- Enter order id: ");
                            orderId = int.Parse(Console.ReadLine());

                            ComputeOrderTotal(orderId);

                            break;
                        }
                    case 5:
                        {
                            string customerName;
                            Console.Write("- Enter customer name: ");
                            customerName = Console.ReadLine();

                            Customer customer = new Customer(1, customerName);
                            AddCutomer(customer);

                            break;
                        }
                    case 6:
                        {
                            int customerId;
                            Console.Write("- Enter customer id: ");
                            customerId = int.Parse(Console.ReadLine());

                            DeleteCustomer(customerId);

                            break;
                        }
                    case 7:
                        {
                            int customerId;
                            Console.Write("- Enter customer id: ");
                            customerId = int.Parse(Console.ReadLine());

                            String customerName;
                            Console.Write("- Enter new name: ");
                            customerName = Console.ReadLine();

                            Customer customer = new Customer(customerId, customerName);
                            UpdateCustomer(customer);

                            break;
                        }
                    case 8:
                        {
                            int customerId;
                            Console.Write("- Enter customer id: ");
                            customerId = int.Parse(Console.ReadLine());

                            int employeeId;
                            Console.Write("- Enter employee id: ");
                            employeeId = int.Parse(Console.ReadLine());

                            Order order = new Order(1, DateTime.Now, customerId, employeeId, 0);
                            AddOrder(order);

                            break;
                        }
                    case 9:
                        {
                            int orderId;
                            Console.Write("- Enter order id: ");
                            orderId = int.Parse(Console.ReadLine());

                            int productId;
                            Console.Write("- Enter product id: ");
                            productId = int.Parse(Console.ReadLine());

                            int quantity;
                            Console.Write("- Enter quantity: ");
                            quantity = int.Parse(Console.ReadLine());

                            double price;
                            Console.Write("- Enter price: ");
                            price = double.Parse(Console.ReadLine());

                            LineItem lineItem = new LineItem(orderId, productId, quantity, price);
                            AddLineItem(lineItem);

                            break;
                        }
                    case 10:
                        {
                            int orderId;
                            Console.Write("- Enter order id: ");
                            orderId = int.Parse(Console.ReadLine());

                            UpdateOrderTotal(orderId);

                            break;
                        }
                    case 11:
                        {
                            string employeeName;
                            Console.Write("- Enter employee name: ");
                            employeeName = Console.ReadLine();

                            double salary;
                            Console.Write("- Enter employee's salary: ");
                            salary = double.Parse(Console.ReadLine());

                            int spvrId;
                            Console.Write("- Enter supervisor id: ");
                            spvrId = int.Parse(Console.ReadLine());

                            Employee employee = new Employee(1, employeeName, salary, spvrId);
                            AddEmployee(employee);

                            break;
                        }
                    case 12:
                        {
                            string productName;
                            Console.Write("- Enter product name: ");
                            productName = Console.ReadLine();

                            double productPrice;
                            Console.Write("- Enter product price: ");
                            productPrice = double.Parse(Console.ReadLine());

                            Product product = new Product(1, productName, productPrice);
                            AddProduct(product);

                            break;
                        }
                }
            } while (choice != 0);

            #endregion

            Console.ReadKey();

        }

        #region Lấy thông tin tất cả khách hàng
        private static void GetAllCustomer()
        {
            CustomerDomain customerDomain = new CustomerDomain();
            List<Customer> listCustomer = customerDomain.GetAllCustomer();
            if (listCustomer != null)
            {
                Console.WriteLine("* List customer: " + listCustomer.Count + " (customer)");
                foreach (Customer customer in listCustomer)
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------------------");
                    Console.WriteLine("- Customer id: " + customer.CustomerId);
                    Console.WriteLine("- Customer name: " + customer.CustomerName);
                }
            }
            else
            {
                Console.WriteLine("* List customer is empty");
            }

        }
        #endregion

        #region Lấy thông tin tất cả đơn đặt hàng theo mã khách hàng
        private static void GetAllOrdersByCustomerId(int customerId)
        {
            OrderDomain orderDomain = new OrderDomain();
            CustomerDomain customerDomain = new CustomerDomain();
            EmployeeDomain employeeDomain = new EmployeeDomain();
            List<Order> listOrder = orderDomain.GetAllOrdersByCustomerId(customerId);
            if (listOrder != null)
            {
                Console.WriteLine("* All orders: " + customerId + ": " + listOrder.Count + " (order)");
                foreach (Order order in listOrder)
                {
                    Customer customer = customerDomain.SearchCustomerById(order.CustomerId);
                    Employee employee = employeeDomain.SearchEmployeeById(order.EmployeeId);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------");
                    Console.WriteLine("- Order id: " + order.OrderId);
                    Console.WriteLine("- Order date: " + order.OrderDate.ToString());
                    Console.WriteLine("- Customer: {0} - {1}", order.CustomerId, customer.CustomerName);
                    Console.WriteLine("- Employee: {0} - {1}", order.EmployeeId, employee.EmployeeName);
                    Console.WriteLine("- Total: " + order.Total + " VND");
                }
            }
            else
            {
                Console.WriteLine("* Empty!");
            }
        }
        #endregion

        #region Lấy thông tin chi tiết đơn hàng theo mã đơn hàng
        private static void GetAllItemsByOrderId(int orderId)
        {
            ProductDomain productDomain = new ProductDomain();
            LineItemDomain lineItemDomain = new LineItemDomain();
            List<LineItem> listLineItem = lineItemDomain.GetAllItemsByOrderId(orderId);
            if (listLineItem != null)
            {
                Console.WriteLine("*All items of order id " + orderId + ": " + listLineItem.Count + " (items)");
                foreach (LineItem lineItem in listLineItem)
                {
                    Product product = productDomain.SearchProductById(lineItem.ProductId);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------");
                    Console.WriteLine("- Product: {0} - {1} ", lineItem.ProductId, product.ProductName);
                    Console.WriteLine("- Quantity: " + lineItem.Quantity);
                    Console.WriteLine("- Price: " + lineItem.Price);
                }
            }
            else
            {
                Console.WriteLine("* Empty!");
            }
        }
        #endregion

        #region Tính tổng trá trị đơn hàng
        private static void ComputeOrderTotal(int orderId)
        {
            OrderDomain orderDomain = new OrderDomain();
            double total = orderDomain.ComputeOrderTotal(orderId);
            Console.WriteLine("*Total order with id " + orderId + ": " + total + " VND");
        }
        #endregion

        #region Thêm khách hàng
        private static void AddCutomer(Customer customer)
        {
            CustomerDomain customerDomain = new CustomerDomain();
            if (customerDomain.AddCustomer(customer))
            {
                Console.WriteLine("* Add customer successfully!");
            }
            else
            {
                Console.WriteLine("* Add customer unsuccessfully!");
            }
        }
        #endregion

        #region Xóa khách hàng
        private static void DeleteCustomer(int customerId)
        {
                CustomerDomain customerDomain = new CustomerDomain();
                if (customerDomain.DeleteCustomer(customerId))
                {
                    Console.WriteLine("* Delete customer successfully!");
                }
                else
                {
                    Console.WriteLine("* Delete customer unsuccessfully!");
                }
        }
        #endregion

        #region Cập nhật thông tin khách hàng
        private static void UpdateCustomer(Customer customer)
        {
            CustomerDomain customerDomain = new CustomerDomain();
            if (customerDomain.UpdateCustomer(customer))
            {
                Console.WriteLine("* Update customer successfully!");
            }
            else
            {
                Console.WriteLine("* Update customer unsuccessfully!");
            }
        }
        #endregion

        #region Thêm đơn hàng
        private static void AddOrder(Order order)
        {
            OrderDomain orderDomain = new OrderDomain();
            if (orderDomain.AddOrder(order))
            {
                Console.WriteLine("* Add order successfully!");
            }
            else
            {
                Console.WriteLine("* Add order unsuccessfully!");
            }
        }
        #endregion

        #region Thêm chi tiết đơn hàng
        private static void AddLineItem(LineItem lineItem)
        {
            LineItemDomain lineItemDomain = new LineItemDomain();
            if (lineItemDomain.AddLineItem(lineItem))
            {
                Console.WriteLine("* Add line item successfully!");
            }
            else
            {
                Console.WriteLine("* Add line item usuccessfully!");
            }
        }
        #endregion

        #region Cập nhật tổng giá trị đơn hàng
        private static void UpdateOrderTotal(int orderId)
        {
            OrderDomain orderDomain = new OrderDomain();
            if (orderDomain.UpdateOrderTotal(orderId))
            {
                Console.WriteLine("* Update order total successfully!");
            }
            else
            {
                Console.WriteLine("* Update order total unsuccessfully!");
            }
        }
        #endregion

        #region Thêm nhân viên
        private static void AddEmployee(Employee employee)
        {
            EmployeeDomain employeeDomain = new EmployeeDomain();
            if (employeeDomain.AddEmployee(employee))
            {
                Console.WriteLine("* Add employee successfully!");
            }
            else
            {
                Console.WriteLine("* Add employee unsuccessfully!");
            }
        }
        #endregion

        #region Thêm sản phẩm
        private static void AddProduct(Product product)
        {
            ProductDomain productDomain = new ProductDomain();
            if (productDomain.AddProduct(product))
            {
                Console.WriteLine("* Add product successfully!");
            }
            else
            {
                Console.WriteLine("* Add product unsuccessfully!");
            }
        }
        #endregion
    }
}
