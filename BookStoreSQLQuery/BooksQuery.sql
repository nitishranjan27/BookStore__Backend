use BookStoreDB;

-------creating Book Table-------
create table Book(
BookId int identity(1,1) not null primary key,
BookName varchar(70) not null,
AuthorName varchar(80) not null,
Rating int ,
RatingCount int ,
DiscountPrice int,
ActualPrice int not null,
Description varchar(max) not null,
BookImage varchar(250),
BookQuantity int not null
);


select *from Book

---------Adding Book For STore Procedure------

 ALTER proc Addbook
(
	@BookName varchar(max),
	@AuthorName varchar(80),
	@Rating int,
	@RatingCount int,
	@DiscountPrice int,
	@ActualPrice int,
	@Description varchar(max),
	@BookImage varchar(250),
	@BookQuantity int,
	@BookId INT Output
)
as
begin
insert into Book (BookName,AuthorName,Rating,RatingCount,DiscountPrice,ActualPrice,Description,BookImage,BookQuantity)
values(@BookName,@AuthorName,@Rating,@RatingCount,@DiscountPrice,@ActualPrice,@Description,@BookImage,@BookQuantity);
SET @BookId= SCOPE_IDENTITY()
Return @BookId;

end;

-----Store procedure For Upadating Book=----------

create proc Updatebook
(
@BookId int,
@BookName varchar(max),
@AuthorName varchar(250),
@Rating int,
@RatingCount int,
@DiscountPrice int,
@ActualPrice int,
@Description varchar(max),
@BookImage varchar(250),
@BookQuantity int
)
as
begin
update Book set 
BookName=@BookName,
AuthorName=@AuthorName,
Rating=@Rating,
RatingCount=@RatingCount,
DiscountPrice=@DiscountPrice,
ActualPrice=@ActualPrice,
Description=@Description,
BookImage=@BookImage,
BookQuantity=@BookQuantity
where BookId=@BookId			
end;

-----For Deleting Book Store Procedure--------
create proc Deletebook
(
@BookId int
)
as
begin
delete from Book Where BookId=@BookId
end;

---Getting Book By Book Id Store Procedure---
create proc GetBookById
(
@BookId int
)
as 
begin
select * from Book where BookId=@BookId
end;

-----Get All Books -----
create proc GetAllBooks
as 
begin
select * from Book
end;
