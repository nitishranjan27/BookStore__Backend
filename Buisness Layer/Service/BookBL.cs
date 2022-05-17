using Buisness_Layer.Interface;
using Common_Layer.Models;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Service
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public BookModel AddBook(AddBookModel addBook)
        {
            try
            {
                return this.bookRL.AddBook(addBook);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UpdateBookModel UpdateBook(int book_id, UpdateBookModel book)
        {
            try
            {
                return this.bookRL.UpdateBook(book_id, book);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteBook(int book_Id)
        {
            try
            {
                return this.bookRL.DeleteBook(book_Id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                return this.bookRL.GetBookByBookId(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
