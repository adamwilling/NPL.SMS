using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.Entities;
using R2S.Training.Dao;
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
            byte choice;
            do
            {
                Console.WriteLine("=================================================Menu Quản Lý=================================================");
                Console.WriteLine("1. Lấy thông tin tất cả khách hàng có trong hệ thống");
                Console.WriteLine("2. Lấy thông tin tất cả đơn hàng theo mã khách hàng");
                Console.WriteLine("3. Lấy thông tin chi tiết đơn hàng theo mã đơn hàng");
                Console.WriteLine("4. Tính tổng giá của đơn hàng theo mã đơn hàng");
                Console.WriteLine("5. Thêm khách hàng mới");
                Console.WriteLine("6. Xóa khách hàng");
                Console.WriteLine("7. Cập nhật thông tin khách hàng");
                Console.WriteLine("8. Thêm đơn hàng");
                Console.WriteLine("9. Thêm chi tiết đơn hàng");
                Console.WriteLine("10. Cập nhật tổng giá trị đơn hàng");
                Console.Write("- Lựa chọn chức năng: ");
                do
                {
                    choice = byte.Parse(Console.ReadLine());
                    if (choice < 0 || choice > 10)
                    {
                        Console.Write("*Vui lòng chọn chức năng hợp lệ: ");
                    }
                } while (choice < 0 || choice > 10);
                switch (choice)
                {
                    case 1:
                        {
                            GetAllCustomer();

                            break;
                        }
                    case 2:
                        {
                            int customerId;
                            Console.Write("- Nhập mã khách hàng: ");
                            customerId = int.Parse(Console.ReadLine());

                            GetAllOrderByCustomerId(customerId);

                            break;
                        }
                    case 3:
                        {
                            int orderId;
                            Console.Write("- Nhập mã đơn hàng: ");
                            orderId = int.Parse(Console.ReadLine());

                            GetAllItemByOrderId(orderId);

                            break;
                        }
                    case 4:
                        {
                            int orderId;
                            Console.Write("- Nhập mã đơn hàng: ");
                            orderId = int.Parse(Console.ReadLine());

                            ComputeOrderTotal(orderId);

                            break;
                        }
                    case 5:
                        {
                            String customerName;
                            Console.Write("- Nhập tên khách hàng cần thêm: ");
                            customerName = Console.ReadLine();

                            Customer customer = new Customer(1, customerName);
                            AddCutomer(customer);

                            break;
                        }
                    case 6:
                        {
                            int customerId;
                            Console.Write("- Nhập mã khách hàng cần xóa: ");
                            customerId = int.Parse(Console.ReadLine());

                            DeleteCustomer(customerId);

                            break;
                        }
                    case 7:
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
                    case 8:
                        {
                            int customerId;
                            Console.Write("- Nhập mã khách hàng nhận: ");
                            customerId = int.Parse(Console.ReadLine());

                            int employeeId;
                            Console.Write("- Nhập mã nhân viên bán: ");
                            employeeId = int.Parse(Console.ReadLine());

                            double total;
                            Console.Write("- Nhập tổng giá: ");
                            total = double.Parse(Console.ReadLine());

                            Order order = new Order(1, DateTime.Now.ToString(), customerId, employeeId, total);
                            AddOrder(order);

                            break;
                        }
                    case 9:
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

                            double price;
                            Console.Write("- Nhập giá tiền (VND): ");
                            price = double.Parse(Console.ReadLine());

                            LineItem lineItem = new LineItem(orderId, productId, quantity, price);
                            addLineItem(lineItem);

                            break;
                        }
                    case 10:
                        {
                            int orderId;
                            Console.Write("- Nhập mã đơn hàng cần sửa: ");
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
                CustomerDAO customerDAO = new CustomerDAO();
                List<Customer> listCustomer = customerDAO.GetAllCustomer();
                if (listCustomer != null)
                {
                    Console.WriteLine("*Danh sách khách hàng: " + listCustomer.Count);
                    foreach (Customer customer in listCustomer)
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                        Console.WriteLine("- Mã khách hàng: " + customer.getCustomerId());
                        Console.WriteLine("- Tên khách hàng: " + customer.getCustomerName());
                    }
                }
                else
                {
                    Console.WriteLine("*Danh sách khách hàng trống");
                }
            }
            catch
            {
                Console.WriteLine("*Có lỗi xảy ra khi lấy thông tin khách hàng từ hệ thống!!!");
            }

        }
        #endregion

        #region Lấy thông tin tất cả đơn đặt hàng theo mã khách hàng
        private static void GetAllOrderByCustomerId(int customerId)
        {
            try
            {
                OrderDAO orderDAO = new OrderDAO();
                List<Order> listOrder = orderDAO.GetAllOrdersByCustomerId(customerId);
                if (listOrder != null)
                {
                    Console.WriteLine("*Thông tin đơn đặt hàng có mã khách hàng: " + customerId + ": " + listOrder.Count + " (đơn)");
                    foreach (Order order in listOrder)
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                        Console.WriteLine("- Mã đơn hàng: " + order.getOrderId());
                        Console.WriteLine("- Ngày đặt hàng: " + order.getOrderDate());
                        Console.WriteLine("- Nhân viên thực hiện: " + order.getEmployeeId());
                        Console.WriteLine("- Tổng: " + order.getTotal() + " VND");
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
                LineItemDAO lineItemDAO = new LineItemDAO();
                List<LineItem> listLineItem = lineItemDAO.GetAllItemsByOrderId(orderId);
                if (listLineItem != null)
                {
                    Console.WriteLine("*Thông tin chi tiết đơn hàng có mã đơn hàng " + orderId + ": " + listLineItem.Count + " (đơn)");
                    foreach (LineItem lineItem in listLineItem)
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                        Console.WriteLine("- Mã sản phẩm: " + lineItem.getProductId());
                        Console.WriteLine("- Số lượng: " + lineItem.getQuantity());
                        Console.WriteLine("- Giá: " + lineItem.getPrice());
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
                LineItemDAO lineItemDAO = new LineItemDAO();
                double total = lineItemDAO.ComputeOrderTotal(orderId);
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
                CustomerDAO customerDAO = new CustomerDAO();
                if (customerDAO.AddCustomer(customer))
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
                Console.WriteLine("*Lỗi khi thêm khách hàng!!!");
            }
        }
        #endregion

        #region Xóa khách hàng
        private static void DeleteCustomer(int customerId)
        {
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                if (customerDAO.DeleteCustomer(customerId))
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
                Console.WriteLine("*Lỗi khi xóa khách hàng!!!");
            }
        }
        #endregion

        #region Cập nhật thông tin khách hàng
        private static void UpdateCustomer(Customer customer)
        {
            try
            {
                CustomerDAO customerDAO = new CustomerDAO();
                if (customerDAO.UpdateCustomer(customer))
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
                Console.WriteLine("*Lỗi khi cập nhật thông tin khách hàng!");
            }
        }
        #endregion

        #region Thêm đơn hàng
        private static void AddOrder(Order order)
        {
            try
            {
                OrderDAO orderDAO = new OrderDAO();
                if (orderDAO.AddOrder(order))
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
                Console.WriteLine("*Lỗi khi thêm đơn hàng, vui lòng kiểm tra lại mã người dùng!!!");
            }
        }
        #endregion

        #region Thêm chi tiết đơn hàng
        private static void addLineItem(LineItem lineItem)
        {
            try
            {
                LineItemDAO lineItemDAO = new LineItemDAO();
                if (lineItemDAO.AddLineItem(lineItem))
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
                Console.WriteLine("*Lỗi khi thêm chi tiết đơn hàng!!!");
            }
        }
        #endregion

        #region Cập nhật tổng giá trị đơn hàng
        private static void UpdateOrderTotal(int orderId)
        {
            try
            {
                OrderDAO orderDAO = new OrderDAO();
                if (orderDAO.UpdateOrderTotal(orderId))
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
                Console.WriteLine("*Lỗi khi cập nhật tổng giá trị đơn hàng!!!");
            }
        }
        #endregion
    }
}
