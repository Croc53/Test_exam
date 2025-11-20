create table user_syst (
UserID INT IDENTITY(1,1) PRIMARY KEY,
RoleID int NOT NULL,
FIO nvarchar(255) NOT NULL,
login nvarchar(255) NOT NULL,
pas nvarchar(255) NOT NULL,
);



create table items (
ItemID INT IDENTITY(1,1) PRIMARY KEY,
Price Decimal(10,2) NOT NULL,
Factory nvarchar(255) NOT NULL,
Type nvarchar(255) NOT NULL,
sale int NOT NULL,
count_item int NOT NULL,
description nvarchar(255) NOT NULL,
photo_link nvarchar(255) 
);


create table Point (
PintID INT IDENTITY(1,1) PRIMARY KEY,
adress nvarchar(255) NOT NULL
);


create table Order1 (
OrderID INT IDENTITY(1,1) PRIMARY KEY,
articul nvarchar(255) NOT NULL,
data_order date NOT NULL,
data_del date NOT NULL,
post_id int NOT NULL,
user_sys_id int NOT NULL,
Code int NOT NULL,
status nvarchar (255) NOT NULL,
);

create table order_item (
Order_item_ID INT IDENTITY(1,1) PRIMARY KEY,
item_id int NOT NULL,
count_item int NOT NULL,
order_id int NOT NULL,
);

CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL
);


ALTER TABLE Order1
ADD CONSTRAINT FK_Order1_Post 
FOREIGN KEY (post_id) REFERENCES Point(PintID);

ALTER TABLE Order1
ADD CONSTRAINT FK_Order1_User 
FOREIGN KEY (user_sys_id) REFERENCES user_syst(UserID);


ALTER TABLE order_item
ADD CONSTRAINT FK_OrderItem_Item 
FOREIGN KEY (item_id) REFERENCES items(ItemID);

ALTER TABLE order_item
ADD CONSTRAINT FK_OrderItem_Order 
FOREIGN KEY (order_id) REFERENCES Order1(OrderID);

ALTER TABLE user_syst 
ADD CONSTRAINT FK_user_syst_Roles 
FOREIGN KEY (RoleID) REFERENCES Roles(RoleID);


