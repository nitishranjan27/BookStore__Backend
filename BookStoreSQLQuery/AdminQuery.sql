use BookStoreDB;

-----------Create a Admin Table--------

create Table Admins
(
	AdminId int Identity(1,1) primary key not null,
	FullName varchar(255) not null,
	EmailId varchar(255) not null,
	Password varchar(255) not null,
	MobileNumber varchar(50) not null,
	Address varchar(255) not null
);

select * from Admins

INSERT INTO Admins VALUES ('Admin Nitish','admin@bookstore.com', 'Admin@12345', '+91 8163475881', '42, 14th Main, 15th Cross, Sector 4 ,opp to BDA complex, near Kumarakom restaurant, HSR Layout, Bangalore 560034');



Create Proc LoginAdmin
(
	@EmailId varchar(max),
	@Password varchar(max)
)
as
BEGIN
	If(Exists(select * from Admins where EmailId= @EmailId and Password = @Password))
		Begin
			select * from Admins where EmailId= @EmailId and Password = @Password;
		end
	Else
		Begin
			select 2;
		End
END;

