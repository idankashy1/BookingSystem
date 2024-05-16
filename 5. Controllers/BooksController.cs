using AutoMapper;
using BookingSystem._2._DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BooksController(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<List<BookDto>> GetAllBooks()
    {
        var books = _bookRepository.GetAllBooks();
        var bookDtos = _mapper.Map<List<BookDto>>(books);
        return Ok(bookDtos);
    }

    [HttpGet("{isbn}")]
    public ActionResult<BookDto> GetBookByISBN(string isbn)
    {
        var book = _bookRepository.GetBookByISBN(isbn);
        if (book == null) return NotFound();
        var bookDto = _mapper.Map<BookDto>(book);
        return Ok(bookDto);
    }

    [HttpPost]
    public ActionResult<BookDto> AddBook([FromBody] BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        _bookRepository.AddBook(book);
        return CreatedAtAction(nameof(GetBookByISBN), new { isbn = book.ISBN }, bookDto);
    }

    [HttpPut("{isbn}")]
    public IActionResult UpdateBook(string isbn, [FromBody] BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);

        if (bookDto.ISBN != isbn)
        {
            var existingBookWithNewIsbn = _bookRepository.GetBookByISBN(bookDto.ISBN);
            if (existingBookWithNewIsbn != null)
            {
                return BadRequest("A book with this new ISBN already exists.");
            }
        }

        if (!_bookRepository.UpdateBook(isbn, book)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{isbn}")]
    public IActionResult DeleteBook(string isbn)
    {
        if (!_bookRepository.DeleteBook(isbn)) return NotFound();
        return NoContent();
    }
}
