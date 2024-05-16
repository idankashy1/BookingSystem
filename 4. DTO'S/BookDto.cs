public class BookDto
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}
