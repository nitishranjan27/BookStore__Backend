create database BookStoreDB;
use BookStoreDB;

-----CREATE TABLE-------------

Create table Users(
   UserId INT IDENTITY (1,1) Primary key,
   FullName VARCHAR(100) Not NULL,
   EmailId VARCHAR (100) Not NULL UNIQUE,
   Password VARCHAR (100) Not NULL,
   MobileNumber bigint Not NULL
);

select * from Users;
DELETE FROM Users WHERE UserId=1;

---CREATE STORED PROCEDURE------

create proc SpAddUsersDetails
@FullName varchar(80),
@EmailId varchar(100),
@Password varchar(100),
@MobileNumber bigint
AS
BEGIN TRY
	insert into Users 
	values(@FullName, @EmailId, @Password, @MobileNumber)
END TRY
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec SpAddUsersDetails

-----USER LOGIN---

create proc SpUsersLogin
@EmailId varchar(100)
AS
BEGIN TRY
	SELECT * FROM Users WHERE EmailId = @EmailId
END TRY
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
END CATCH

------FORGOT PASSWORD----


create proc SpForgotPassword
@EmailId varchar(100)
AS
BEGIN TRY
	SELECT * FROM Users WHERE EmailId = @EmailId 
END TRY
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
END CATCH


-----RESET PASSWORD----


create proc SpResetPasswords
@EmailId varchar(100),
@Password varchar(100)
AS
BEGIN TRY
	UPDATE Users Set Password = @Password Where EmailId = @EmailId
END TRY
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
END CATCH