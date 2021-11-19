﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

using R2S.Training.PropertyManager;
using R2S.Training.Entities;

namespace R2S.Training.ConnectionManager
{
    class ConnectionManagers
    {
        SqlConnection conn = null;
        SqlCommand comm = null;
        SqlDataAdapter da = null;
        DataTable dt = new DataTable();
        public ConnectionManagers()
        {
            conn = new SqlConnection(PropertyManagers.connectionString);
            comm = conn.CreateCommand();
        }

        public SqlConnection Open()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
        }
        public SqlConnection Close()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            return conn;
        }
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close(); conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public bool MyExecuteNonQuery(string strSQL, CommandType ct)
        {
            bool f = false;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            try
            {
                comm.ExecuteNonQuery();
                f = true;
            }
            catch (SqlException ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
            return f;
        }

        public DataTable GetAllCustomer()
        {
            dt.Clear();
            dt = this.ExecuteQueryDataSet("select customer_id, customer_name from Customer", CommandType.Text).Tables[0];
            return dt;
        }

        public DataTable GetAllOrderByCutomerId(int customerId)
        {
            dt.Clear();
            dt = this.ExecuteQueryDataSet("select * from Orders where customer_id=" + customerId, CommandType.Text).Tables[0];
            return dt;
        }

        public DataTable GetAllItemByOrderId(int orderid)
        {
            dt.Clear();
            dt = this.ExecuteQueryDataSet("select * from OrderItem where order_id=" + orderid, CommandType.Text).Tables[0];
            return dt;
        }

        public DataTable ComputeOrderTotal(int orderId)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("select * from dbo.search_ComputeOrderTotal(@order_id)", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", orderId));
            comm.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(comm);
            da.Fill(dt);
            Close();
            return dt;
        }

        public bool AddCustmer(Customer customer)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("add_Customer", conn);
            comm.Parameters.Add(new SqlParameter("@customer_name", customer.getCustomerName()));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        public bool DeleteCustmer(int customerId)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("delete_Customer", conn);
            comm.Parameters.Add(new SqlParameter("@customer_id", customerId));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        public bool UpdateCustmer(Customer customer)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("update_Customer", conn);
            comm.Parameters.Add(new SqlParameter("@customer_id", customer.getCustomerId()));
            comm.Parameters.Add(new SqlParameter("@customer_name", customer.getCustomerName()));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        public bool AddOrder(Order order)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("insert_Order", conn);
            comm.Parameters.Add(new SqlParameter("@order_date", DateTime.Now));
            comm.Parameters.Add(new SqlParameter("@customer_id", order.getCustomerId()));
            comm.Parameters.Add(new SqlParameter("@employee_id", order.getEmployeeId()));
            comm.Parameters.Add(new SqlParameter("@total", order.getTotal()));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        public bool AddLineItem(LineItem lineItem)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("insert_LineItem", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", lineItem.getOrderId()));
            comm.Parameters.Add(new SqlParameter("@product_id", lineItem.getProductId()));
            comm.Parameters.Add(new SqlParameter("@quantity", lineItem.getQuantity()));
            comm.Parameters.Add(new SqlParameter("@price", lineItem.getPrice()));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        public bool UpdateOrderTotal(double orderId)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("update_TotalOrder", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", orderId));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        // Tìm kiếm khách hàng theo id
        public DataTable SearchCustomerById(int customerId)
        {
            dt.Clear();
            dt = this.ExecuteQueryDataSet("select * from Customer where customer_id=" + customerId, CommandType.Text).Tables[0];
            return dt;
        }

        //Xóa đơn đặt hàng
        public bool DeleteOrderById(int orderId)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("delete_OrderById", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", orderId));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        //Tìm kiếm đơn đặt hàng theo id
        public DataTable SearchOrderById(int orderId)
        {
            dt.Clear();
            dt = this.ExecuteQueryDataSet("select * from Orders where order_id=" + orderId, CommandType.Text).Tables[0];
            return dt;
        }

        //Cập nhật LineItem
        public bool UpdateLineItemById(LineItem lineItem)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("update_LineItemById", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", lineItem.getOrderId()));
            comm.Parameters.Add(new SqlParameter("@product_id", lineItem.getProductId()));
            comm.Parameters.Add(new SqlParameter("@quantity", lineItem.getQuantity()));
            comm.Parameters.Add(new SqlParameter("@price", lineItem.getPrice()));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        //Xóa LineItem theo id
        public bool DeleleLineItemById(LineItem lineItem)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("delete_LineItemById", conn);
            comm.Parameters.Add(new SqlParameter("@order_id", lineItem.getOrderId()));
            comm.Parameters.Add(new SqlParameter("@product_id", lineItem.getProductId()));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        public DataTable SearchAllEmployee()
        {
            dt.Clear();
            dt = this.ExecuteQueryDataSet("select * from Employee", CommandType.Text).Tables[0];
            return dt;
        }
        //Tìm kiếm nhân viên theo id
        public DataTable SearchemployeeByid(int employeeid)
        {
            dt.Clear();
            dt = this.ExecuteQueryDataSet("select * from Employee where employee_id=" + employeeid, CommandType.Text).Tables[0];
            return dt;
        }

        //Thêm mới employee 
        public bool AddEmployyee(Employee employee)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("insert_Employee", conn);
            comm.Parameters.Add(new SqlParameter("@employee_id", employee.getEmployeeId()));
            comm.Parameters.Add(new SqlParameter("@employee_name", employee.getEmployeeName()));
            comm.Parameters.Add(new SqlParameter("@salary", employee.getSalary()));
            comm.Parameters.Add(new SqlParameter("@supervisor_id", employee.getSpvrId()));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        //Cập nhật employee theo id
        public bool UpdateEmployeeById(Employee employee)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("update_Employee", conn);
            comm.Parameters.Add(new SqlParameter("@employee_id", employee.getEmployeeId()));
            comm.Parameters.Add(new SqlParameter("@employee_name", employee.getEmployeeName()));
            comm.Parameters.Add(new SqlParameter("@salary", employee.getSalary()));
            comm.Parameters.Add(new SqlParameter("@supervisor_id", employee.getSpvrId()));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        //Tìm kiếm sản phẩm
        public DataTable SearchProductById(int productId)
        {
            dt.Clear();
            dt = this.ExecuteQueryDataSet("select * from Product where product_id=" + productId, CommandType.Text).Tables[0];
            return dt;
        }

        //Thêm mới sản phẩm
        public bool InsertProduct(Product product)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("insert_Product", conn);
            comm.Parameters.Add(new SqlParameter("@product_name", product.getProductName()));
            comm.Parameters.Add(new SqlParameter("@product_price", product.getPrice()));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        //Cập nhật sản phẩm
        public bool UpdateProduct(Product product)
        {
            dt.Clear();
            Open();
            comm = new SqlCommand("update_Product", conn);
            comm.Parameters.Add(new SqlParameter("@product_id", product.getProductId()));
            comm.Parameters.Add(new SqlParameter("@product_name", product.getProductName()));
            comm.Parameters.Add(new SqlParameter("@product_price", product.getPrice()));
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();
            Close();
            return true;
        }

        //Lấy tất cả sản phẩm có trong hệ thống
        public DataTable SearchProduct()
        {
            dt.Clear();
            dt = this.ExecuteQueryDataSet("select * from Product", CommandType.Text).Tables[0];
            return dt;
        }

    }
}
