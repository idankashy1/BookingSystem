using AutoMapper;

// מחלקת מיפוי המשתמשת ב-AutoMapper לצורך מיפוי בין אובייקטים
public class MappingProfile : Profile
{
    // בנאי המחלקה שבו מוגדרים המיפויים
    public MappingProfile()
    {
        // דוגמא למיפוי בין המודל Book ל-DTO BookDto
        CreateMap<Book, BookDto>();

        // דוגמא למיפוי הפוך בין ה-DTO BookDto למודל Book
        CreateMap<BookDto, Book>();
    }
}
