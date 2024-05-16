using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // דוגמא למיפוי בין המודל ל-DTO
        CreateMap<Book, BookDto>();
        CreateMap<BookDto, Book>();
    }
}