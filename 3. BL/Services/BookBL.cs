using AutoMapper;
using BookingSystem._2._DAL.Interfaces;
using BookingSystem._3._BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem._3._BL.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookBL(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public BookDto GetBookByISBN(string isbn)
        {
            var book = _bookRepository.GetBookByISBN(isbn);
            if (book == null) throw new Exception("Book not found");
            return _mapper.Map<BookDto>(book);
        }

        public BookDto AddBook(BookDto bookDto)
        {
            ValidateBookDto(bookDto);
            var book = _mapper.Map<Book>(bookDto);
            _bookRepository.AddBook(book);
            return bookDto;
        }

        public void UpdateBook(string isbn, BookDto bookDto)
        {
            ValidateBookDto(bookDto);
            var book = _mapper.Map<Book>(bookDto);
            book.ISBN = isbn; // Ensure ISBN is carried over correctly
           // _bookRepository.UpdateBook(book);
        }

        public void DeleteBook(string isbn)
        {
            _bookRepository.DeleteBook(isbn);
        }

        private void ValidateBookDto(BookDto bookDto)
        {
            // Add validation logic here
            if (string.IsNullOrWhiteSpace(bookDto.ISBN))
                throw new Exception("ISBN is required.");

            if (string.IsNullOrWhiteSpace(bookDto.Title))
                throw new Exception("Title is required.");

            if (!bookDto.Authors.Any())
                throw new Exception("At least one author is required.");

            if (bookDto.Year < 1000 || bookDto.Year > DateTime.Now.Year)
                throw new Exception("Year is out of range.");
        }
    }
}