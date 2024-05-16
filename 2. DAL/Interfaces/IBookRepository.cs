namespace BookingSystem._2._DAL.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookByISBN(string isbn);
        void AddBook(Book book);
        bool UpdateBook(string originalIsbn, Book updatedBook);
        bool DeleteBook(string isbn);
    }
}
