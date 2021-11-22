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

        static void Main(string[] args)
        {
            // Hàm nhập và xuất tiếng việt
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            #region Giao diện chính
            int choice;
            do
            {
                Console.WriteLine("=================================================QUẢN LÝ BÁN HÀNG=================================================");
                Console.WriteLine("1. Lấy thông tin tất cả khách hàng.");
                Console.WriteLine("2. Lấy thông tin tất cả đơn hàng theo mã khách hàng.");
                Console.WriteLine("3. Lấy thông tin chi tiết đơn hàng theo mã đơn hàng.");
                Console.WriteLine("4. Tính tổng giá của đơn hàng theo mã đơn hàng.");
                Console.WriteLine("5. Thêm khách hàng.");
                Console.WriteLine("6. Xóa khách hàng.");
                Console.WriteLine("7. Cập nhật thông tin khách hàng.");
                Console.WriteLine("8. Thêm đơn hàng.");
                Console.WriteLine("9. Thêm chi tiết đơn hàng.");
                Console.WriteLine("10. Cập nhật tổng giá trị đơn hàng.");
                Console.Write("- Lựa chọn chức năng: ");
                do
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice <= 0 || choice > 10)
                    {
                        Console.Write("*Vui lòng chọn chức năng hợp lệ: ");
                    }
                } while (choice <= 0 || choice > 10);
                switch (choice)
                {
                    case 1:         // Chức năng 1: Lấy thông tin tất cả khách hàng
                        {
                            GetAllCustomer();
                            break;
                        }
                    case 2:         // Chức năng 2: Lấy thông tin tất cả đơn hàng theo mã khách hàng
                        {
                            int customerId;
                            Console.Write("- Nhập mã khách hàng: ");
                            customerId = int.Parse(Console.ReadLine());

                            GetAllOrderByCustomerId(customerId);

                            break;
                        }
                    case 3:         // Chức năng 3: Lấy thông tin chi tiết đơn hàng theo mã đơn hàng
                        {
                            int orderId;
                            Console.Write("- Nhập mã đơn hàng: ");
                            orderId = int.Parse(Console.ReadLine());

                            GetAllItemByOrderId(orderId);

                            break;
                        }
                    case 4:         // Chức năng 4: Tính tổng giá của tất cả đơn hàng theo mã đơn hàng
                        {
                            int orderId;
                            Console.Write("- Nhập mã đơn hàng: ");
                            orderId = int.Parse(Console.ReadLine());

                            ComputeOrderTotal(orderId);

                            break;
                        }
                    case 5:         // Chức năng 5: Thêm khách hàng
                        {
                            string customerName;
                            Console.Write("- Nhập tên khách hàng cần thêm: ");
                            customerName = Console.ReadLine();

                            Customer customer = new Customer(1, customerName);
                            AddCutomer(customer);

                            break;
                        }
                    case 6:         // Chức năng 6: Xóa khách hàng
                        {
                            int customerId;
                            Console.Write("- Nhập mã khách hàng cần xóa: ");
                            customerId = int.Parse(Console.ReadLine());

                            DeleteCustomer(customerId);

                            break;
                        }
                    case 7:         // Chức năng 7: Cập nhật thông tin khách hàng
                        {
                            int customerId;
                            Console.Write("- Nhập mã khách hàng cần sửa: ");
                            customerId = int.Parse(Console.ReadLine());

                            String customerName;
                            Console.Write("- Nhập tên mới: ");
                            customerName = Console.ReadLine();

                            Customer customer = new Customer(customerId, customerName);
                            UpdateCustomer(customer);

                            break;
                        }
                    case 8:         // Chức năng 8: Thêm đơn hàng
                        {
                            int customerId;
                            Console.Write("- Nhập mã khách hàng: ");
                            customerId = int.Parse(Console.ReadLine());

                            int employeeId;
                            Console.Write("- Nhập mã nhân viên: ");
                            employeeId = int.Parse(Console.ReadLine());

                            Order order = new Order(1, DateTime.Now, customerId, employeeId, 0);
                            AddOrder(order);

                            break;
                        }
                    case 9:         // Chức năng 9: Thêm chi tiết đơn hàng mới
                        {
                            int orderId;
                            Console.Write("- Nhập mã đơn hàng: ");
                            orderId = int.Parse(Console.ReadLine());

                            int productId;
                            Console.Write("- Nhập mã sản phẩm: ");
                            productId = int.Parse(Console.ReadLine());

                            int quantity;
                            Console.Write("- Nhập số lượng: ");
                            quantity = int.Parse(Console.ReadLine());

                            LineItem lineItem = new LineItem(orderId, productId, quantity, 0);
                            AddLineItem(lineItem);

                            break;
                        }
                    case 10:           // Chức năng 10: Cập nhật giá đơn hàng
                        {
                            int orderId;
                            Console.Write("- Nhập mã đơn hàng cần cập nhật giá: ");
                            orderId = int.Parse(Console.ReadLine());

                            UpdateOrderTotal(orderId);

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
            try
            {
                CustomerDomain customerDomain = new CustomerDomain();
                List<Customer> listCustomer = customerDomain.GetAllCustomer();
                if (listCustomer != null)
                {
                    Console.WriteLine("*Danh sách khách hàng: " + listCustomer.Count);
                    foreach (Customer customer in listCustomer)
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                        Console.WriteLine("- Mã khách hàng: " + customer.CustomerId);
                        Console.WriteLine("- Tên khách hàng: " + customer.CustomerName);
                    }
                }
                else
                {
                    Console.WriteLine("*Danh sách khách hàng trống");
                }
            }
            catch
            {
                Console.WriteLine("Lỗi!");
            }

        }
        #endregion

        #region Lấy thông tin tất cả đơn đặt hàng theo mã khách hàng
        private static void GetAllOrderByCustomerId(int customerId)
        {
            try
            {
                OrderDomain orderDomain = new OrderDomain();
                CustomerDomain customerDomain = new CustomerDomain();
                EmployeeDomain employeeDomain = new EmployeeDomain();
                List<Order> listOrder = orderDomain.GetAllOrdersByCustomerId(customerId);
                if (listOrder != null)
                {
                    Console.WriteLine("*Thông tin đơn đặt hàng có mã khách hàng: " + customerId + ": " + listOrder.Count + " (đơn)");
                    foreach (Order order in listOrder)      // Hiện thị từng đơn hàng
                    {
                        Customer customer = customerDomain.SearchCustomerById(order.CustomerId);
                        Employee employee = employeeDomain.SearchEmployeeById(order.EmployeeId);
                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                        Console.WriteLine("- Mã đơn hàng: " + order.OrderId);
                        Console.WriteLine("- Thời gian đặt hàng: " + order.OrderDate.ToString());
                        Console.WriteLine("- Khách hàng: " + customer.CustomerName);
                        Console.WriteLine("- Nhân viên: " + employee.EmployeeName);
                        Console.WriteLine("- Tổng: " + order.Total + " VND");
                    }
                }
                else
                {
                    Console.WriteLine("*Thông tin đơn đặt hàng có mã khách hàng " + customerId + ": trống!!!");
                }
            }
            catch
            {
                Console.WriteLine("*Có lỗi xảy ra khi lấy thông tin đơn hàng có mã khách hàng: " + customerId + "!!!");
            }
        }
        #endregion

        #region Lấy thông tin chi tiết đơn hàng theo mã đơn hàng
        private static void GetAllItemByOrderId(int orderId)
        {
            try
            {
                LineItemDomain lineItemDomain = new LineItemDomain();
                List<LineItem> listLineItem = lineItemDomain.GetAllItemsByOrderId(orderId);
                if (listLineItem != null)
                {
                    Console.WriteLine("*Thông tin chi tiết đơn hàng có mã đơn hàng " + orderId + ": " + listLineItem.Count + " (đơn)");
                    foreach (LineItem lineItem in listLineItem)
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                        Console.WriteLine("- Sản phẩm: " + lineItem.ProductId);
                        Console.WriteLine("- Số lượng: " + lineItem.Quantity);
                        Console.WriteLine("- Giá: " + lineItem.Price);
                    }
                }
                else
                {
                    Console.WriteLine("*Thông tin chi tiết đơn hàng có mã đơn hàng " + orderId + " trống!!!");
                }
            }
            catch
            {
                Console.WriteLine("*Có lỗi xảy ra khi lấy thông tin chi tiết đơn hàng có mã đơn hàng " + orderId + "!!!");
            }
        }
        #endregion

        #region Tính tổng trá trị đơn hàng
        private static void ComputeOrderTotal(int orderId)
        {
            try
            {
                OrderDomain orderDomain = new OrderDomain();
                double total = orderDomain.ComputeOrderTotal(orderId);
                Console.WriteLine("*Tổng giá tất cả đơn hàng có mã đơn hàng " + orderId + ": " + total + " VND");
            }
            catch
            {
                Console.WriteLine("*Có lỗi xảy ra khi tính tổng giá trị đơn hàng có mã đơn hàng " + orderId + "!!!");
            }
        }
        #endregion

        #region Thêm khách hàng
        private static void AddCutomer(Customer customer)
        {
            try
            {
                CustomerDomain customerDomain = new CustomerDomain();
                if (customerDomain.AddCustomer(customer))
                {
                    Console.WriteLine("*Thêm thành công!");
                }
                else
                {
                    Console.WriteLine("*Thêm thất bại!");
                }
            }
            catch
            {
                Console.WriteLine("*Có lỗi xảy ra khi thêm khách hàng!!!");
            }
        }
        #endregion

        #region Xóa khách hàng
        private static void DeleteCustomer(int customerId)
        {
            try
            {
                CustomerDomain customerDomain = new CustomerDomain();
                if (customerDomain.DeleteCustomer(customerId))
                {
                    Console.WriteLine("*Xóa thành công!");
                }
                else
                {
                    Console.WriteLine("*Xóa thất bại!");
                }
            }
            catch
            {
                Console.WriteLine("*Có lỗi xảy ra khi xóa khách hàng!!!");
            }
        }
        #endregion

        #region Cập nhật thông tin khách hàng
        private static void UpdateCustomer(Customer customer)
        {
            try
            {
                CustomerDomain customerDomain = new CustomerDomain();
                if (customerDomain.UpdateCustomer(customer))
                {
                    Console.WriteLine("*Cập nhật thông tin khách hàng thành công!");
                }
                else
                {
                    Console.WriteLine("*Cập nhật thông tin khách hàng thất bại!");
                }
            }
            catch
            {
                Console.WriteLine("*Có lỗi xảy ra khi cập nhật thông tin khách hàng!");
            }
        }
        #endregion

        #region Thêm đơn hàng
        private static void AddOrder(Order order)
        {
            try
            {
                OrderDomain orderDomain = new OrderDomain();
                if (orderDomain.AddOrder(order))
                {
                    Console.WriteLine("*Thêm đơn hàng thành công!");
                }
                else
                {
                    Console.WriteLine("*Thêm đơn hàng thất bại!");
                }
            }
            catch
            {
                Console.WriteLine("*Có lỗi xảy ra khi thêm đơn hàng!!!");
            }
        }
        #endregion

        #region Thêm chi tiết đơn hàng
        private static void AddLineItem(LineItem lineItem)
        {
            try
            {
                LineItemDomain lineItemDomain = new LineItemDomain();
                if (lineItemDomain.AddLineItem(lineItem))
                {
                    Console.WriteLine("*Thêm chi tiết đơn hàng thành công!");
                }
                else
                {
                    Console.WriteLine("*Thêm chi tiết đơn hàng thất bại!");
                }
            }
            catch
            {
                Console.WriteLine("*Có lỗi xảy ra khi thêm chi tiết đơn hàng!!!");
            }
        }
        #endregion

        #region Cập nhật tổng giá trị đơn hàng
        private static void UpdateOrderTotal(int orderId)
        {
            try
            {
                OrderDomain orderDomain = new OrderDomain();
                if (orderDomain.UpdateOrderTotal(orderId))
                {
                    Console.WriteLine("*Cập nhật tổng giá trị đơn hàng thành công!");
                }
                else
                {
                    Console.WriteLine("*Cập nhật tổng giá trị đơn hàng thất bại!");
                }
            }
            catch
            {
                Console.WriteLine("*Có lỗi xảy ra khi cập nhật tổng giá trị đơn hàng!!!");
            }
        }
        #endregion
    }
}
