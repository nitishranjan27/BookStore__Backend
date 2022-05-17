using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(AddBookModel addBook);
        public UpdateBookModel UpdateBook(int book_Id, UpdateBookModel book);
        public bool DeleteBook(int book_Id);
        public BookModel GetBookByBookId(int book_Id);
        public List<BookModel> GetAllBooks();
    }
}
