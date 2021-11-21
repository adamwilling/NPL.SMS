using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

using System.Data;

using R2S.Training.PropertyManager;
using R2S.Training.Entities;

namespace R2S.Training.ConnectionManager
{
    class ConnectionManagers
    {
        SqlConnection conn = null;
        SqlCommand comm = null;
        public ConnectionManagers()
        {
            conn = new SqlConnection(PropertyManagers.connectionString);
            comm = conn.CreateCommand();
        }

        // Chức năng 1: Lấy thông tin tất cả khách hàng
        public List<Customer> GetAllCustomer()
        {
            try
            {
                // Mở kết nối
                conn.Open();

                // Khởi tạo danh sách nhân viên
                List<Customer> listCustomer = new List<Customer>();
                comm = new SqlCommand("select * from Customer", conn);
                comm.CommandType = CommandType.Text;
                SqlDataReader dataReader = comm.ExecuteReader();
                while (dataReader.Read())
                {
                    // Lấy dữ liệu từng hàng
                    Customer customer = new Customer(dataReader.GetInt32(0), dataReader.GetString(1));

                    // Thêm vào danh sách
                    listCustomer.Add(customer);
                }
                return listCustomer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        // Chức năng 2: Lấy tất cả đơn hàng theo mã khách hàng
        public List<Order> GetAllOrderByCutomerId(int customerId)
        {
            try
            {
                conn.Open();        // Mở kết nối
                List<Order> listOrder = new List<Order>();      // Khởi tạo danh sách đơn hàng

                // Lấy dữ liệu từ server
                comm = new SqlCommand("select * from Orders where customer_id=@customer_id", conn);        // Lấy những đơn hàng có mã khách hàng được truyền vào
                comm.Parameters.Add(new SqlParameter("@customer_id", customerId));
                comm.CommandType = CommandType.Text;
                SqlDataReader dataReader = comm.ExecuteReader();

                // Lấy dữ liệu từng đơn hàng trong bản và thêm vào danh sách
                while (dataReader.Read())       
                {
                    Order order = new Order(dataReader.GetInt32(0), dataReader.GetDateTime(1), dataReader.GetInt32(2), dataReader.GetInt32(3), dataReader.GetDouble(4));
                    listOrder.Add(order);
                }

                return listOrder;       // Trả về danh sách đơn hàng có mã id là 
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        // Chức năng 3: Lấy tất cả chi tiết đơn hàng theo mã đơn hàng
        public List<LineItem> GetAllItemsByOrderId(int orderId)
        {
            try
            {
                // Mở kết nối
                conn.Open();

                // Khởi tạo danh sách chi tiết đơn hàng
                List<LineItem> listLineItem = new List<LineItem>();

                // Lấy dữ liệu từ server
                comm = new SqlCommand("select * from LineItem where order_id=" + orderId, conn);
                comm.CommandType = CommandType.Text;
                SqlDataReader dataReader = comm.ExecuteReader();
                
                // Lấy dữ liệu từng chi tiết đơn hàng trong bảng và thêm vào danh sách
                while (dataReader.Read())
                {
                    // Lấy dữ liệu từng hàng
                    LineItem order = new LineItem(dataReader.GetInt32(0), dataReader.GetInt32(1), dataReader.GetInt32(2), dataReader.GetDouble(3));

                    // Thêm vào danh sách
                    listLineItem.Add(order);
                }
                return listLineItem;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        // Chức năng 4: Tính tổng giá của tất cả đơn hàng theo mã đơn hàng
        public double ComputeOrderTotal(int orderId)
        {
            conn.Open();
            comm = new SqlCommand("select * from dbo.search_ComputeOrderTotal(@order_id)", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", orderId));
            comm.CommandType = CommandType.StoredProcedure;
            double total = (double)comm.ExecuteScalar();
            conn.Close();
            return total;
        }

        // Chức năng 5: Thêm khách hàng
        public bool AddCustmer(Customer customer)
        {
            conn.Open();
            comm = new SqlCommand("add_Customer", conn);
            comm.Parameters.Add(new SqlParameter("@customer_name", customer.CustomerName));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }


        // Chức năng 6: Xóa khách hàng
        public bool DeleteCustmer(int customerId)
        {
            conn.Open();
            comm = new SqlCommand("delete_Customer", conn);
            comm.Parameters.Add(new SqlParameter("@customer_id", customerId));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        // Chức năng 7: Cập nhật thông tin khách hàng
        public bool UpdateCustmer(Customer customer)
        {
            conn.Open();
            comm = new SqlCommand("update_Customer", conn);
            comm.Parameters.Add(new SqlParameter("@customer_id", customer.CustomerId));
            comm.Parameters.Add(new SqlParameter("@customer_name", customer.CustomerName));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        // Chức năng 8: Thêm đơn hàng
        public bool AddOrder(Order order)
        {
            try
            {
                conn.Open();
                comm = new SqlCommand("insert into Orders(order_date,customer_id,employee_id,total) values(@order_date,@customer_id,@employee_id,@total)", conn);
                comm.Parameters.Add(new SqlParameter("@order_date", order.OrderDate));
                comm.Parameters.Add(new SqlParameter("@customer_id", order.CustomerId));
                comm.Parameters.Add(new SqlParameter("@employee_id", order.EmployeeId));
                comm.Parameters.Add(new SqlParameter("@total", order.Total));
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        // Chức năng 9: Thêm chi tiết đơn hàng mới
        public bool AddLineItem(LineItem lineItem)
        {
            conn.Open();
            comm = new SqlCommand("insert into LineItem(order_id,product_id,quantity,price) values(@order_id,@product_id,@quantity,@price)", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", lineItem.OrderId));
            comm.Parameters.Add(new SqlParameter("@product_id", lineItem.ProductId));
            comm.Parameters.Add(new SqlParameter("@quantity", lineItem.Quantity));
            comm.Parameters.Add(new SqlParameter("@price", lineItem.Price));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        // Chức năng 10: Cập nhật giá đơn hàng
        public bool UpdateOrderTotal(int orderId)
        {
            try
            {
                conn.Open();
                comm = new SqlCommand("update Orders set total = (select SUM(quantity*price) from LineItem where order_id=" + orderId + ")", conn);
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        // Tìm kiếm đơn hàng theo mã đơn hàng
        public Order SearchOrderById(int orderId)
        {
            try
            {
                conn.Open();
                Order order = null;
                comm = new SqlCommand("select * from Order where order_id=" + orderId, conn);
                comm.CommandType = CommandType.Text;
                SqlDataReader dataReader = comm.ExecuteReader();
                while (dataReader.Read())
                {
                    order = new Order(dataReader.GetInt32(0), dataReader.GetDateTime(1), dataReader.GetInt32(2), dataReader.GetInt32(3), dataReader.GetDouble(4));
                }
                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        // Tìm kiếm khách hàng theo mã khách hàng
        public Customer SearchCustomerById(int customerId)
        {
            try
            {
                conn.Open();
                Customer customer = null;
                comm = new SqlCommand("select * from Customer where customer_id=@customer_id", conn);
                comm.Parameters.Add(new SqlParameter("@customer_id", customerId));
                comm.CommandType = CommandType.Text;
                SqlDataReader dataReader = comm.ExecuteReader();
                customer = new Customer(dataReader.GetInt32(0), dataReader.GetString(1));
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        // Xóa đơn đặt hàng theo mã khách hàng
        public bool DeleteOrderById(int orderId)
        {
            conn.Open();
            comm = new SqlCommand("delete_OrderById", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", orderId));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //Cập nhật LineItem
        public bool UpdateLineItemById(LineItem lineItem)
        {
            conn.Open();
            comm = new SqlCommand("update_LineItemById", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", lineItem.OrderId));
            comm.Parameters.Add(new SqlParameter("@product_id", lineItem.ProductId));
            comm.Parameters.Add(new SqlParameter("@quantity", lineItem.Quantity));
            comm.Parameters.Add(new SqlParameter("@price", lineItem.Price));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //Xóa LineItem theo id
        public bool DeleleLineItemById(LineItem lineItem)
        {
            conn.Open();
            comm = new SqlCommand("delete_LineItemById", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", lineItem.OrderId));
            comm.Parameters.Add(new SqlParameter("@product_id", lineItem.ProductId));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        public List<Employee> GetAllEmployee()
        {
            try
            {
                conn.Open();
                List<Employee> listEmployee = new List<Employee>();
                comm = new SqlCommand("select * from Employee", conn);
                comm.CommandType = CommandType.Text;
                SqlDataReader dataReader = comm.ExecuteReader();
                while (dataReader.Read())
                {
                    // Lấy dữ liệu từng hàng
                    Employee employee = new Employee(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetDouble(2), dataReader.GetInt32(3));

                    // Thêm vào danh sách
                    listEmployee.Add(employee);
                }
                return listEmployee;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        //Tìm kiếm nhân viên theo id
        public Employee SearchEmployeeById(int employeeId)
        {
            try
            {
                conn.Open();
                Employee employee = null;
                comm = new SqlCommand("select * from Employee where employee_id=" + employeeId, conn);
                comm.CommandType = CommandType.Text;
                SqlDataReader dataReader = comm.ExecuteReader();
                employee = new Employee(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetDouble(2), dataReader.GetInt32(3));
                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //Thêm mới employee 
        public bool AddEmployee(Employee employee)
        {
            conn.Open();
            comm = new SqlCommand("insert_Employee", conn);
            comm.Parameters.Add(new SqlParameter("@employee_id", employee.EmployeeId));
            comm.Parameters.Add(new SqlParameter("@employee_name", employee.EmployeeName));
            comm.Parameters.Add(new SqlParameter("@salary", employee.Salary));
            comm.Parameters.Add(new SqlParameter("@supervisor_id", employee.SpvrId));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //Cập nhật employee theo id
        public bool UpdateEmployeeById(Employee employee)
        {
            conn.Open();
            comm = new SqlCommand("update_Employee", conn);
            comm.Parameters.Add(new SqlParameter("@employee_id", employee.EmployeeId));
            comm.Parameters.Add(new SqlParameter("@employee_name", employee.EmployeeName));
            comm.Parameters.Add(new SqlParameter("@salary", employee.Salary));
            comm.Parameters.Add(new SqlParameter("@supervisor_id", employee.SpvrId));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //Thêm mới sản phẩm
        public bool InsertProduct(Product product)
        {
            conn.Open();
            comm = new SqlCommand("insert_Product", conn);
            comm.Parameters.Add(new SqlParameter("@product_name", product.ProductName));
            comm.Parameters.Add(new SqlParameter("@product_price", product.Price));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //Cập nhật sản phẩm
        public bool UpdateProduct(Product product)
        {
            conn.Open();
            comm = new SqlCommand("update_Product", conn);
            comm.Parameters.Add(new SqlParameter("@product_id", product.ProductId));
            comm.Parameters.Add(new SqlParameter("@product_name", product.ProductName));
            comm.Parameters.Add(new SqlParameter("@product_price", product.Price));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        //Lấy tất cả sản phẩm có trong hệ thống
        public List<Product> GetAllProduct()
        {
            try
            {
                conn.Open();
                List<Product> listProduct = new List<Product>();
                comm = new SqlCommand("select * from Product", conn);
                comm.CommandType = CommandType.Text;
                SqlDataReader dataReader = comm.ExecuteReader();
                while (dataReader.Read())
                {
                    // Lấy dữ liệu
                    Product product = new Product(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetDouble(2));

                    // Thêm vào danh sách
                    listProduct.Add(product);
                }
                return listProduct;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        //Tìm kiếm sản phẩm theo mã sản phẩm
        public Product SearchProductById(int productId)
        {
            try
            {
                conn.Open();
                Product product = null;
                comm = new SqlCommand("select * from Employee where employee_id=" + productId, conn);
                comm.CommandType = CommandType.Text;
                SqlDataReader dataReader = comm.ExecuteReader();
                product = new Product(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetDouble(2));
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*Lỗi: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}