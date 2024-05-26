using AutoMapper;
using BookingSystem._2._DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController] // מציין שזהו בקר של API
[Route("api/books")] // מציין את הנתיב של הבקר
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    // בנאי של הבקר שמקבל את המאגרים וה-Mapper
    public BooksController(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    // פונקציה שמחזירה את כל הספרים
    [HttpGet]
    public ActionResult<List<BookDto>> GetAllBooks()
    {
        var books = _bookRepository.GetAllBooks(); // קבלת כל הספרים מהמאגר
        var bookDtos = _mapper.Map<List<BookDto>>(books); // מיפוי הספרים ל-DTOs
        return Ok(bookDtos); // החזרת הספרים בפורמט DTO
    }

    // פונקציה שמחזירה ספר לפי ISBN
    [HttpGet("{isbn}")]
    public ActionResult<BookDto> GetBookByISBN(string isbn)
    {
        var book = _bookRepository.GetBookByISBN(isbn); // קבלת הספר מהמאגר לפי ISBN
        if (book == null) return NotFound(); // אם הספר לא נמצא, מחזירים 404
        var bookDto = _mapper.Map<BookDto>(book); // מיפוי הספר ל-DTO
        return Ok(bookDto); // החזרת הספר בפורמט DTO
    }

    // פונקציה להוספת ספר חדש
    [HttpPost]
    public ActionResult<BookDto> AddBook([FromBody] BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto); // מיפוי ה-DTO לספר
        _bookRepository.AddBook(book); // הוספת הספר למאגר
        return CreatedAtAction(nameof(GetBookByISBN), new { isbn = book.ISBN }, bookDto); // החזרת הספר שנוסף עם מיקום
    }

    // פונקציה לעדכון ספר לפי ISBN
    [HttpPut("{isbn}")]
    public IActionResult UpdateBook(string isbn, [FromBody] BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto); // מיפוי ה-DTO לספר

        // בדיקה אם ה-ISBN שב-DTO שונה מה-ISBN המקורי
        if (bookDto.ISBN != isbn)
        {
            var existingBookWithNewIsbn = _bookRepository.GetBookByISBN(bookDto.ISBN);
            if (existingBookWithNewIsbn != null)
            {
                return BadRequest("A book with this new ISBN already exists."); // אם הספר עם ה-ISBN החדש כבר קיים, מחזירים שגיאה
            }
        }

        if (!_bookRepository.UpdateBook(isbn, book)) return NotFound(); // עדכון הספר במאגר
        return NoContent(); // אם העדכון הצליח, מחזירים 204
    }

    // פונקציה למחיקת ספר לפי ISBN
    [HttpDelete("{isbn}")]
    public IActionResult DeleteBook(string isbn)
    {
        if (!_bookRepository.DeleteBook(isbn)) return NotFound(); // אם הספר לא נמצא, מחזירים 404
        return NoContent(); // אם המחיקה הצליחה, מחזירים 204
    }
}
