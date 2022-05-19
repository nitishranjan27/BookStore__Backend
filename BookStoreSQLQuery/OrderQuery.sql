use BookStoreDB;

----Creating Order Table------

create table Orders
(
	OrderId int identity(1,1) not null primary key,
	TotalPrice int not null,
	BookQuantity int not null,
	OrderDate Date not null,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
	BookId INT NOT NULL FOREIGN KEY REFERENCES Book(BookId),
	AddressId int not null FOREIGN KEY REFERENCES Address(AddressId)
);
select * from Orders;
-----Add Order in Stored Procedure-----

Create Proc AddOrders
(
	@BookQuantity int,
	@UserId int,
	@BookId int,
	@AddressId int
)
as
Declare @TotalPrice int
BEGIN
	set @TotalPrice = (select DiscountPrice from Book where BookId = @BookId);
	If(Exists(Select * from Book where BookId = @BookId))
		BEGIN
			If(Exists (Select * from Book where BookId = @BookId))
				BEGIN
					Begin try
						Begin Transaction
						Insert Into Orders(TotalPrice, BookQuantity, OrderDate, UserId, BookId, AddressId)
						Values(@TotalPrice*@BookQuantity, @BookQuantity, GETDATE(), @UserId, @BookId, @AddressId);
						Update Book set BookQuantity=BookQuantity-@BookQuantity where BookId = @BookId;
						Delete from Carts where BookId = @BookId and UserId = @UserId;
						select * from Orders;
						commit Transaction
					End try
					Begin Catch
							rollback;
					End Catch
				END
			Else
				Begin
					Select 3;
				End
		End
	Else
		Begin
			Select 2;
		End
END;

----------Get All Order-----

Create Proc GetAllOrders
(
	@UserId int
)
as
begin
		Select 
		Orders.OrderId, Orders.UserId, Orders.AddressId, Book.BookId,
		Orders.TotalPrice, Orders.BookQuantity, Orders.OrderDate,
		Book.BookName, Book.AuthorName, Book.BookImage
		FROM Book 
		inner join Orders on Orders.BookId = Book.BookId 
		where 
			Orders.UserId = @UserId;
END