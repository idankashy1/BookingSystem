import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Book {
  isbn: string;
  title: string;
  authors: string[];
  year: number;
  price: number;
  category: string;
}

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:7032/api/books';

  constructor(private http: HttpClient) { }

  getAllBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl);
  }

  getBookByISBN(isbn: string): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/${isbn}`);
  }

  addBook(book: Book): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, book);
  }

  updateBook(originalIsbn: string, book: Book): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${originalIsbn}`, book);
  }

  deleteBook(isbn: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${isbn}`);
  }
}
