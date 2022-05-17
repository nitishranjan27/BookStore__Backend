using Common_Layer.Models;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Service
{
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public BookModel AddBook(AddBookModel addBook)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("AddBook", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@BookId", addBook.bookId);
                    cmd.Parameters.AddWithValue("@BookName", addBook.bookName);
                    cmd.Parameters.AddWithValue("@AuthorName", addBook.authorName);
                    cmd.Parameters.AddWithValue("@Rating", addBook.rating);
                    cmd.Parameters.AddWithValue("@RatingCount", addBook.ratingCount);
                    cmd.Parameters.AddWithValue("@DiscountPrice", addBook.discountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", addBook.actualPrice);
                    cmd.Parameters.AddWithValue("@Description", addBook.description);
                    cmd.Parameters.AddWithValue("@BookImage", addBook.bookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", addBook.bookQuantity);
                    cmd.Parameters.Add("@BookId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlConnection.Open();
                    int res = cmd.ExecuteNonQuery();
                    int bookID = Convert.ToInt32(cmd.Parameters["@BookId"].Value.ToString());
                    sqlConnection.Close();
                    if (res != 0)
                    {
                        BookModel bookModel = new BookModel()
                        {
                            bookId = bookID,
                            bookName = addBook.bookName,
                            authorName = addBook.authorName,
                            rating = addBook.rating,
                            ratingCount = addBook.ratingCount,
                            discountPrice = addBook.discountPrice,
                            actualPrice = addBook.actualPrice,
                            description = addBook.description,
                            bookImage = addBook.bookImage,
                            bookQuantity = addBook.bookQuantity,
                        };
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public UpdateBookModel UpdateBook(int book_id, UpdateBookModel book)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("UpdateBook", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", book_id);
                    cmd.Parameters.AddWithValue("@BookName", book.bookName);
                    cmd.Parameters.AddWithValue("@AuthorName", book.authorName);
                    cmd.Parameters.AddWithValue("@Rating", book.rating);
                    cmd.Parameters.AddWithValue("@RatingCount", book.ratingCount);
                    cmd.Parameters.AddWithValue("@DiscountPrice", book.discountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", book.actualPrice);
                    cmd.Parameters.AddWithValue("@Description", book.description);
                    cmd.Parameters.AddWithValue("@BookImage", book.bookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", book.bookQuantity);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return book;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteBook(int book_Id)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("DeleteBook", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", book_Id);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BookModel GetBookByBookId(int book_Id)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetBookById", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", book_Id);
                    sqlConnection.Open();
                    BookModel book = new BookModel();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            book.bookId = Convert.ToInt32(reader["BookId"]);
                            book.bookName = reader["BookName"].ToString();
                            book.authorName = reader["AuthorName"].ToString();
                            book.rating = Convert.ToInt32(reader["Rating"]);
                            book.ratingCount = Convert.ToInt32(reader["RatingCount"]);
                            book.discountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            book.actualPrice = Convert.ToInt32(reader["ActualPrice"]);
                            book.description = reader["Description"].ToString();
                            book.bookImage = reader["BookImage"].ToString();
                            book.bookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                        }
                        sqlConnection.Close();
                        return book;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<BookModel> GetAllBooks()
        {
            List<BookModel> book = new List<BookModel>();
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetAllBooks", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            book.Add(new BookModel
                            {
                                bookId = Convert.ToInt32(reader["BookId"]),
                                bookName = reader["BookName"].ToString(),
                                authorName = reader["AuthorName"].ToString(),
                                rating = Convert.ToInt32(reader["Rating"]),
                                ratingCount = Convert.ToInt32(reader["RatingCount"]),
                                discountPrice = Convert.ToInt32(reader["DiscountPrice"]),
                                actualPrice = Convert.ToInt32(reader["ActualPrice"]),
                                description = reader["Description"].ToString(),
                                bookImage = reader["BookImage"].ToString(),
                                bookQuantity = Convert.ToInt32(reader["BookQuantity"])
                            });
                        }
                        sqlConnection.Close();
                        return book;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
