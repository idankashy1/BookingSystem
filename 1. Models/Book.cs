using System.Collections.Generic;
using System.Xml.Serialization;

public class Book
{
    [XmlElement("isbn")]
    public string ISBN { get; set; }

    [XmlElement("title")]
    public string Title { get; set; }

    [XmlElement("author")]
    public List<string> Authors { get; set; }

    [XmlElement("year")]
    public int Year { get; set; }

    [XmlElement("price")]
    public decimal Price { get; set; }

    [XmlAttribute("category")]
    public string Category { get; set; }

    [XmlAttribute("cover")]
    public string Cover { get; set; } 
}
