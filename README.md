תקציר הפרויקט -
מערכת דיגיטלית לניהול ספרים, במערכת ניתן לצפות ברשימת הספרים כולל מזהה ייחודי, כותרת, כותבים, שנה, מחיר וקטגוריה. בנוסף, ניתן לבצע פעולות בסיסיות של הוספת ספר חדש, עריכת עמודות ומחיקת הספר. פונקציות נוספות הן טעינת כל רשימת הספרים וחיפוש ספר ספציפי לפי מזהה ייחודי (ISBN) או חלק ממנו.

קצת על החשיבה בבניית הפרויקט...

בקנד:
לפני תחילת כתיבת הקוד, התרכזתי באפיון שכבות וסידור תיקיות (באופן כללי אני מאמין בסדר ובשכבות). הפרויקט מחולק לשש שכבות (השכבה השישית היא הפרונטאנד).

השכבה הראשונה - Models

השכבה השנייה - DAL (מכיל את ה-Repository מחוקלים לחוזים ומימושים)

השכבה השלישית - BL (בפרויקט זה אין שימוש ב-BL כיוון שהפעולות בסיסיות ואין צורך בלוגיקה מורכבת מדי, ולכן השימוש וההזרקה נעשים מהקונטרולרים לריפוזיטורי)

השכבה הרביעית - DTOs

השכבה החמישית - Controllers (CRUD)

השכבה השישית - Angular (מכיל את פרויקט האנגולר)

בנוסף, קיימת תיקיית Data שמכילה את מסד הנתונים ב-XML כיוון שמסד הנתונים הוא מקומי ולא שירות כמו SQL Server.

פרונטנד:

קומפוננטות:
הפיתוח נעשה באנגולר עם שימוש בשלוש קומפוננטות עיקריות:

1. בוק ליסט : BookListComponent - משמש כדף הבית ומציג את המסך הראשי.
2. בוק אדד דיאלוג : BookAddDialogComponent - משמש להוספת ספר חדש (נפתח כדיאלוג).
3. בוק אדיט דיאלוג : BookEditDialogComponent - משמש לעריכת ספר (נפתח כדיאלוג).
4. 
סרביסים:
יש סרביס אחד בלבד בשם book.service.ts המכיל API לשליחת בקשות לקונטרולר.

מודולים:
הפרויקט משתמש במודול מרכזי אחד (app.module.ts).
*לרוב אני מפריד את המודול של Angular Materials למודול נפרד ומייבא אותו ל-app.module.ts, אך הרגשתי שאין צורך בכך כאן מכיוון שהפרויקט אינו גדול ואינו מכיל יותר מדי פקדי Angular Material.

קצת על הפרויקט במילים שלי:
המטרה הייתה לבנות מערכת דיגיטלית פשוטה וקלה לתפעול עבור אדם הרוצה לשמור את הספרים שלו בצורה יעילה ופשוטה. התחלתי בבניית הבקנד ובדקתי אותו בעזרת Swagger. ברגע שראיתי שהפונקציונליות עובדת היטב (גם בעזרת Debug כמובן), התחלתי בבניית הפרונטאנד. לא התעמקתי בעיצוב (SCSS), אלא התמקדתי בעיצוב בסיסי בלבד.
בסופו של דבר, נהניתי לבנות את הפרויקט ואני מקווה שהוא עונה על הדרישה.
