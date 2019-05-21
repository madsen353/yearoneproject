CREATE DATABASE FunHall
USE FunHall

create table Bookings
(
	BookingId nvarchar(100) PRIMARY KEY NOT NULL,
	Name nvarchar(100) NOT NULL,
	CusTel nvarchar(20) NOT NULL,
	CusTelAlt nvarchar(20),
	CusMail nvarchar(100),
	Date nvarchar(100),
	TotalPrice nvarchar(100),
	CusComment text,
	IntComment text	
);

create table Activities
(
	ActivityId INT IDENTITY(1,1),
	Description nvarchar(250) PRIMARY KEY,
);



create table BookedActivities
(
	BookingId nvarchar(20) NOT NULL,
	TimeDesc nvarchar(254) NOT NULL,
	StartTime nvarchar(50),
	Endtime nvarchar(50),
	IsFinished bit,
	PRIMARY KEY (BookingId,TimeDesc)
);

----SELECT * from BookedActivities

create table BookedProducts
(
	Id INT Identity(1,1) ,
	BookingId nvarchar(20) NOT NULL,
	ProductDesc nvarchar(254) NOT NULL,	
	ProductAmount nvarchar(20),
	ProductPrice nvarchar(20),
	ProducttotPrice nvarchar(20),
	PRIMARY KEY (BookingId,ProductDesc)
);



create table Guests
(
	GuestId INT Identity(1,1),
	BookingId nvarchar(20) NOT NULL,
	Name nvarchar(200) NOT NULL,
	Email nvarchar(100) NOT NULL,
	AgreeTerms bit,
	Subscription bit,
	ChekedInTime datetime,
	PRIMARY KEY (BookingId,Email)
)
 
create table GuestActivities
(
	GuestId INT,
	TimeDesc nvarchar(200) ,
	Points int,	
	PRIMARY KEY (GuestId,TimeDesc)
)





