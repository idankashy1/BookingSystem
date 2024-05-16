using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot(ElementName = "bookstore", Namespace = "")]
public class Bookstore
{
    [XmlElement("book")]
    public List<Book> Books { get; set; }
}