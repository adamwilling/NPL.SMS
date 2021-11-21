/****** Tạo database SMS ******/
CREATE DATABASE SMS
GO

USE SMS
GO


/****** Tạo UDF ComputeOrderTotal ******/
create function search_ComputeOrderTotal
(@order_id int)
returns float
as
begin
return (select SUM(quantity*price) as total_price from LineItem where order_id=@order_id)
end
GO


/****** Tạo bảng Customer ******/
CREATE TABLE Customer
(
	customer_id int PRIMARY KEY NOT NULL,
	customer_name nvarchar(100) NOT NULL,
)
GO
/****** Thêm dữ liệu ban đầu cho bảng Customer ******/
INSERT Customer (customer_id, customer_name) VALUES (1, N'Nguyễn Võ Song Toàn')
INSERT Customer (customer_id, customer_name) VALUES (2, N'Bùi Quốc Định')
INSERT Customer (customer_id, customer_name) VALUES (3, N'Trần Ngô Bích Du')
INSERT Customer (customer_id, customer_name) VALUES (4, N'Đoàn Thị Thanh Phương')
INSERT Customer (customer_id, customer_name) VALUES (5, N'Đào Thị Thanh Vi')
GO


/****** Tạo bảng Employee ******/
CREATE TABLE Employee
(
	employee_id int PRIMARY KEY NOT NULL,
	employee_name nvarchar(100) NOT NULL,
	salary float NOT NULL,
	supervisor_id int NOT NULL,
)
GO
/****** Thêm dữ liệu ban đầu cho bảng Employee ******/
INSERT Employee (employee_id, employee_name, salary, supervisor_id) VALUES (1, N'Trương Nhựt Thanh', 1200000, 1)
INSERT Employee (employee_id, employee_name, salary, supervisor_id) VALUES (2, N'Phan Tấn Lộc', 20000000, 1)
INSERT Employee (employee_id, employee_name, salary, supervisor_id) VALUES (3, N'Đỗ Thị Châm Anh', 19000000, 1)
INSERT Employee (employee_id, employee_name, salary, supervisor_id) VALUES (4, N'Huỳnh Chi', 15000000, 1)
INSERT Employee (employee_id, employee_name, salary, supervisor_id) VALUES (5, N'Lê Quang Đông', 10000000, 1)
GO


/****** Tạo bảng Product ******/
CREATE TABLE Product
(
	product_id int PRIMARY KEY NOT NULL,
	product_name nvarchar(100) NOT NULL,
	product_price float NOT NULL,
)
GO
/****** Thêm dữ liệu ban đầu cho bảng Product ******/
INSERT Product (product_id, product_name, product_price) VALUES (1, N'Giáo trình môn Lập trình hướng đối tượng', 50000)
INSERT Product (product_id, product_name, product_price) VALUES (2, N'Giáo trình môn Cấu trúc dữ liệu & Giải thuật', 35000)
INSERT Product (product_id, product_name, product_price) VALUES (3, N'Giáo trình môn Kỹ thuật lập trình', 45000)
INSERT Product (product_id, product_name, product_price) VALUES (4, N'Giáo trình môn Lý thuyết đồ thị', 40000)
INSERT Product (product_id, product_name, product_price) VALUES (5, N'Giáo trình môn Kiến trúc máy tính', 38000)
INSERT Product (product_id, product_name, product_price) VALUES (6, N'Card Viettel 10k', 10000)
INSERT Product (product_id, product_name, product_price) VALUES (7, N'Card Viettel 20k', 20000)
INSERT Product (product_id, product_name, product_price) VALUES (8, N'Card Viettel 50k', 50000)
INSERT Product (product_id, product_name, product_price) VALUES (9, N'Card Viettel 100k', 100000)
INSERT Product (product_id, product_name, product_price) VALUES (10, N'Card Viettel 500k', 500000)
GO


/****** Tạo bảng Orders ******/
CREATE TABLE Orders
(
	order_id int PRIMARY KEY NOT NULL,
	order_date datetime NOT NULL,
	customer_id int NOT NULL,
	employee_id int NOT NULL,
	total float NOT NULL,
)
GO
/****** Thêm dữ liệu ban đầu cho bảng Orders ******/
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (1, '2021-11-19 23:15:25.23', 1, 1, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (2, '2021-11-19 23:15:25.23', 1, 2, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (3, '2021-11-19 23:15:25.23', 1, 3, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (4, '2021-11-19 23:15:25.23', 1, 4, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (5, '2021-11-19 23:15:25.23', 1, 5, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (6, '2021-11-19 23:15:25.23', 2, 1, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (7, '2021-11-19 23:15:25.23', 2, 2, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (8, '2021-11-19 23:15:25.23', 2, 3, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (9, '2021-11-19 23:15:25.23', 2, 4, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (10, '2021-11-19 23:15:25.23', 2, 5, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (11, '2021-11-19 23:15:25.23', 3, 1, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (12, '2021-11-19 23:15:25.23', 3, 2, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (13, '2021-11-19 23:15:25.23', 3, 3, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (14, '2021-11-19 23:15:25.23', 3, 4, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (15, '2021-11-19 23:15:25.23', 3, 5, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (16, '2021-11-19 23:15:25.23', 4, 1, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (17, '2021-11-19 23:15:25.23', 4, 2, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (18, '2021-11-19 23:15:25.23', 4, 3, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (19, '2021-11-19 23:15:25.23', 4, 4, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (20, '2021-11-19 23:15:25.23', 4, 5, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (21, '2021-11-19 23:15:25.23', 5, 1, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (22, '2021-11-19 23:15:25.23', 5, 2, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (23, '2021-11-19 23:15:25.23', 5, 3, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (24, '2021-11-19 23:15:25.23', 5, 4, 0)
INSERT Orders (order_id, order_date, customer_id, employee_id, total) VALUES (25, '2021-11-19 23:15:25.23', 5, 5, 0)
GO

/****** Tạo bảng LineItem ******/
CREATE TABLE LineItem
(
	order_id int NOT NULL,
	product_id int NOT NULL,
	quantity int NOT NULL,
	price float NOT NULL,
)
GO
/****** Thêm dữ liệu ban đầu cho bảng LineItem ******/
INSERT LineItem (order_id, product_id, quantity, price) VALUES (1, 1, 2, 100000)
INSERT LineItem (order_id, product_id, quantity, price) VALUES (1, 2, 2, 70000)
INSERT LineItem (order_id, product_id, quantity, price) VALUES (1, 3, 2, 90000)
INSERT LineItem (order_id, product_id, quantity, price) VALUES (1, 4, 2, 80000)
INSERT LineItem (order_id, product_id, quantity, price) VALUES (1, 5, 2, 76000)
GO

/****** Tạo StoredProcedure add_Customer (chức năng thứ 5) ******/
create proc add_Customer
(@customer_name nvarchar(100))
as
	insert into Customer(customer_name) values(@customer_name);
GO


/****** Tạo StoredProcedure delete_Customer (chức năng thứ 6) ******/
create proc delete_Customer
(@customer_id int)
as
begin try
	begin tran
		delete LineItem where order_id in(select order_id from Orders where customer_id=@customer_id);
		delete Orders where customer_id=@customer_id;
		delete Customer where customer_id=@customer_id;
	commit tran
end try
begin catch
	rollback tran
end catch
GO

/****** Tạo StoredProcedure update_Customer (chức năng thứ 7) ******/
create proc update_Customer
(
 @customer_id int
,@customer_name nvarchar(100)
)
as
	update Customer set customer_name=@customer_name where customer_id=@customer_id;
GO

USE [master]
GO
ALTER DATABASE [SMS] SET  READ_WRITE 
GO
