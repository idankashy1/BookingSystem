using System.Collections.Generic;
using System.Xml.Serialization;

// מודל שמייצג ספר
public class Book
{
    // מיפוי שדה ה-ISBN בקובץ ה-XML
    [XmlElement("isbn")]
    public string ISBN { get; set; }

    // מיפוי שדה הכותרת בקובץ ה-XML
    [XmlElement("title")]
    public string Title { get; set; }

    // מיפוי שדה המחברים בקובץ ה-XML, רשימה של שמות מחברים
    [XmlElement("author")]
    public List<string> Authors { get; set; }

    // מיפוי שדה השנה בקובץ ה-XML
    [XmlElement("year")]
    public int Year { get; set; }

    // מיפוי שדה המחיר בקובץ ה-XML
    [XmlElement("price")]
    public decimal Price { get; set; }

    // מיפוי השדה קטגוריה כמאפיין בקובץ ה-XML
    [XmlAttribute("category")]
    public string Category { get; set; }

    // מיפוי השדה כריכה כמאפיין בקובץ ה-XML
    [XmlAttribute("cover")]
    public string Cover { get; set; }
}
