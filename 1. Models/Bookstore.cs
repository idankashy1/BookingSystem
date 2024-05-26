using System.Collections.Generic;
using System.Xml.Serialization;

// מיפוי השורש של קובץ ה-XML לתוך מחלקת Bookstore
[XmlRoot(ElementName = "bookstore", Namespace = "")]
public class Bookstore
{
    // מיפוי אלמנט הספרים בקובץ ה-XML, רשימה של אובייקטים מסוג Book
    [XmlElement("book")]
    public List<Book> Books { get; set; }
}
