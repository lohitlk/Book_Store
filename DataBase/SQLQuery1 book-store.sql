create table Register(
id INT PRIMARY KEY IDENTITY (1, 1),
name Varchar(255) NOT NULL,
password Varchar(255) NOT NULL,
email varchar(255) NOT NULL unique
);

create table Books(
Book_Id Int Primary Key Identity(1,1),
Book_Type Varchar(255) Not Null,
Name Varchar(255) Not Null,
Cost Decimal Not Null,
Author varchar(255) Not Null,
Discription varchar(300) 
);

create table Cart(
cart_id INT PRIMARY KEY IDENTITY (1, 1),
cart_totalprice decimal NOT NULL,
Book_id int FOREIGN KEY REFERENCES Books(Book_id),
user_email varchar(255) foreign key references Register(email)
);

create table UserDetails(
userdetails_id INT PRIMARY KEY IDENTITY (1, 1),
userdetails_name Varchar(255) NOT NULL,
userdetais_email varchar(255),
userdetails_address varchar(255),
userdetails_state varchar(255),
userdetails_pincode int,
id int FOREIGN KEY REFERENCES Register(id)
);
drop table Register;
drop table UserDetails;
drop table Cart;

select * from Register;
select* from Books;

truncate table Books;