use BookStoreDB;

-----Creating WishList Table---------
create table WishList
(
	WishListId int identity(1,1) not null primary key,
	UserId int foreign key references Users(UserId) on delete no action,
	BookId int foreign key references Book(BookId) on delete no action
);

select * from WishList

----Add WishList in Stored Procedure------

create proc AddWishList
(
@UserId int,
@BookId int
)
as
begin 
       insert into WishList
	   values (@UserId,@BookId);
end;

-----------Delete WishList in Stored Procedure-----------

create proc DeleteWishList
(
@WishListId int,
@UserId int
)
as
begin
delete WishList where WishListId = @WishListId and UserId=@UserId;
end;

------GetWhishList by Userid Stored Procedure-----

create proc GetWishListByUserId
(
	@UserId int
)
as
begin
	select WishListId,UserId,c.BookId,BookName,AuthorName,
	DiscountPrice,ActualPrice,BookImage from WishList c join Book b on c.BookId=b.BookId 
	where UserId=@UserId;
end;