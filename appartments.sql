create database RentDB;
use RentDB
create table Users(
user_id int identity(1,1) primary key,
name nvarchar(25),
surname nvarchar(25),
login nvarchar(25),
password nvarchar(20)
);
insert into Users(name,surname,login,password)
values('yuliya','sherbakova','yulish','chapathebest');
insert into Users(name,surname,login,password)
values('kate','nova','hedgehoginthe','hedgehoginthe');
--select * from Users;
create table Appartments(
building_id int identity(1,1) primary key,
town nvarchar(50),
address nvarchar(50),
number_of_rooms int,
price_month float,
description nvarchar(max),
landlord_id int,
status nvarchar(20),
FOREIGN KEY (landlord_id) REFERENCES Users(user_id)
);
insert into Appartments(town,address,number_of_rooms,price_month,description,landlord_id,status)
values('kiev','str. yangelya 22, ap.5-16',1,570,'without toilet and bath',2,'not rented');
insert into Appartments(town,address,number_of_rooms,price_month,description,landlord_id,status)
values('kiev','str. yangelya 22, ap.2-08',1,570,'without toilet and bath',2,'not rented');
--select * from Appartments;
create table Rent(
rent_id int identity(1,1) primary key,
user_id int,
building_id int,
start_date date,
finish_date date,
price float,
FOREIGN KEY (user_id) REFERENCES Users(user_id),
FOREIGN KEY (building_id) REFERENCES Appartments(building_id)
);
insert into Rent(user_id,building_id,start_date,finish_date,price)
values(2,1,'2018-11-12','2019-01-12',1140);
insert into Rent(user_id,building_id,start_date,finish_date,price)
values(2,2,'2018-05-22','2018-08-22',1710);
--select * from Rent;