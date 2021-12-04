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
                conn.Open();

                List<Customer> listCustomer = new List<Customer>();

                comm = new SqlCommand("select Customer.customer_id, Customer.customer_name from Customer where Customer.customer_id in (Select Orders.customer_id from Orders where Orders.customer_id=Customer.customer_id) ", conn);

                SqlDataReader dataReader = comm.ExecuteReader();
                while (dataReader.Read())
                {
                    Customer customer = new Customer(dataReader.GetInt32(0), dataReader.GetString(1));
                    listCustomer.Add(customer);
                }

                return listCustomer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Customer> GetCustomer()
        {
            try
            {
                conn.Open();

                List<Customer> listCustomer = new List<Customer>();

                comm = new SqlCommand("select * from Customer", conn);

                SqlDataReader dataReader = comm.ExecuteReader();
                while (dataReader.Read())
                {
                    Customer customer = new Customer(dataReader.GetInt32(0), dataReader.GetString(1));
                    listCustomer.Add(customer);
                }

                return listCustomer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
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
                conn.Open();

                List<Order> listOrder = new List<Order>();

                comm = new SqlCommand("select * from Orders where customer_id=@customer_id", conn);

                comm.Parameters.Add(new SqlParameter("@customer_id", customerId));

                SqlDataReader dataReader = comm.ExecuteReader();
                while (dataReader.Read())
                {
                    Order order = new Order(dataReader.GetInt32(0), dataReader.GetDateTime(1), dataReader.GetInt32(2), dataReader.GetInt32(3), dataReader.GetDouble(4));
                    listOrder.Add(order);
                }

                return listOrder;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
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
                conn.Open();
                List<LineItem> listLineItem = new List<LineItem>();

                comm = new SqlCommand("select * from LineItem where order_id=" + orderId, conn);

                SqlDataReader dataReader = comm.ExecuteReader();
                while (dataReader.Read())
                {
                    LineItem order = new LineItem(dataReader.GetInt32(0), dataReader.GetInt32(1), dataReader.GetInt32(2), dataReader.GetDouble(3));
                    listLineItem.Add(order);
                }

                return listLineItem;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        // Chức năng 4: Tính tổng giá của đơn hàng theo mã đơn hàng
        public double ComputeOrderTotal(int orderId)
        {
            try
            {
                conn.Open();

                comm = new SqlCommand("select dbo.ComputeOrderTotal(@order_id)", conn);
                comm.Parameters.Add(new SqlParameter("@order_id", orderId));

                double total = (double)(comm.ExecuteScalar());
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while retrieving data from SQL Server: " + ex);
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        // Chức năng 5: Thêm khách hàng
        public bool AddCustomer(Customer customer)
        {
            try
            {
                conn.Open();

                comm = new SqlCommand("AddCustomer", conn);
                comm.Parameters.Add(new SqlParameter("@customer_name", customer.CustomerName));
                comm.CommandType = CommandType.StoredProcedure;

                comm.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }


        // Chức năng 6: Xóa khách hàng
        public bool DeleteCustmer(int customerId)
        {
            try
            {
                conn.Open();

                comm = new SqlCommand("DeleteCustomer", conn);
                comm.Parameters.Add(new SqlParameter("@customer_id", customerId));
                comm.CommandType = CommandType.StoredProcedure;

                comm.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        // Chức năng 7: Cập nhật thông tin khách hàng
        public bool UpdateCustmer(Customer customer)
        {
            try
            {
                conn.Open();

                comm = new SqlCommand("UpdateCustomer", conn);
                comm.Parameters.Add(new SqlParameter("@customer_id", customer.CustomerId));
                comm.Parameters.Add(new SqlParameter("@customer_name", customer.CustomerName));
                comm.CommandType = CommandType.StoredProcedure;

                comm.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
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

                comm.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
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
            try
            {
                conn.Open();

                comm = new SqlCommand("insert into LineItem(order_id,product_id,quantity,price) values(@order_id,@product_id,@quantity,@price)", conn);
                comm.Parameters.Add(new SqlParameter("@order_id", lineItem.OrderId));
                comm.Parameters.Add(new SqlParameter("@product_id", lineItem.ProductId));
                comm.Parameters.Add(new SqlParameter("@quantity", lineItem.Quantity));
                comm.Parameters.Add(new SqlParameter("@price", lineItem.Price));

                comm.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        // Chức năng 10: Cập nhật tổng giá giá đơn hàng
        public bool UpdateOrderTotal(int orderId)
        {
            try
            {
                comm = new SqlCommand("update Orders set total = " + ComputeOrderTotal(orderId) + " where order_id=" + orderId, conn);
                conn.Open();
                comm.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
                return false;
            }
        }

        // Lấy thông tin tất cả đơn hàng
        public List<Order> GetAllOrder()
        {
            try
            {
                conn.Open();        // Mở kết nối
                List<Order> listOrder = new List<Order>();      // Khởi tạo danh sách đơn hàng

                // Lấy dữ liệu từ server
                comm = new SqlCommand("select * from Orders", conn);        // Lấy những đơn hàng có mã khách hàng được truyền vào

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
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        // Tìm kiếm đơn hàng theo mã đơn hàng
        public Order SearchOrderById(int orderId)
        {
            foreach (Order order in GetAllOrder())
            {
                if (order.OrderId == orderId)
                {
                    return order;
                }
            }
            return null;
        }

        // Tìm kiếm khách hàng theo mã khách hàng
        public Customer SearchCustomerById(int customerId)
        {
            foreach (Customer customer in GetCustomer())
            {
                if (customer.CustomerId == customerId)
                {
                    return customer;
                }
            }
            return null;
        }

        #region Quản lý nhân viên
        public List<Employee> GetAllEmployee()
        {
            try
            {
                conn.Open();
                List<Employee> listEmployee = new List<Employee>();
                comm = new SqlCommand("select * from Employee", conn);

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
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
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
            foreach (Employee employee in GetAllEmployee())
            {
                if (employee.EmployeeId == employeeId)
                {
                    return employee;
                }
            }
            return null;
        }
        #endregion

        #region Quản lý sản phẩm
        // Lấy danh sách tất cả sản phẩm
        public List<Product> GetAllProduct()
        {
            try
            {
                conn.Open();
                List<Product> listProduct = new List<Product>();
                comm = new SqlCommand("select * from Product", conn);

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
                Console.WriteLine("* An error occurred while interacting with SQL Server: " + ex);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        // Tìm kiếm sản phẩm theo mã sản phẩm
        public Product SearchProductById(int productId)
        {
            foreach (Product product in GetAllProduct())
            {
                if (product.ProductId == productId)
                {
                    return product;
                }
            }
            return null;
        }
        #endregion
    }
}