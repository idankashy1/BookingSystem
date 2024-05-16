namespace BookingSystem._3._BL.Interfaces
{
    public interface IBookBL
    {
        IEnumerable<BookDto> GetAllBooks();
        BookDto GetBookByISBN(string isbn);
        BookDto AddBook(BookDto bookDto);
        void UpdateBook(string isbn, BookDto bookDto);
        void DeleteBook(string isbn);
    }
}
