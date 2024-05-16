import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BookService, Book } from '../../services/book.service';
import { BookEditDialogComponent } from '../book-edit-dialog/book-edit-dialog.component';
import { BookAddDialogComponent } from '../book-add-dialog/book-add-dialog.component';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {
  books: Book[] = [];
  filteredBooks: Book[] = [];
  displayedColumns: string[] = ['isbn', 'title', 'authors', 'year', 'price', 'category', 'actions'];

  constructor(private bookService: BookService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.bookService.getAllBooks().subscribe((data: Book[]) => {
      this.books = data;
      this.filteredBooks = data;
    });
  }

  addBook(): void {
    const dialogRef = this.dialog.open(BookAddDialogComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBooks();
      }
    });
  }

  editBook(book: Book): void {
    const dialogRef = this.dialog.open(BookEditDialogComponent, {
      width: '400px',
      data: book
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBooks();
      }
    });
  }

  deleteBook(book: Book): void {
    if (confirm(`Are you sure you want to delete the book titled "${book.title}"?`)) {
      this.bookService.deleteBook(book.isbn).subscribe(() => {
        this.loadBooks();
      });
    }
  }

  searchISBN(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    const isbn = inputElement.value;
    if (!isbn) {
      this.filteredBooks = this.books;
    } else {
      this.filteredBooks = this.books.filter(book => book.isbn.includes(isbn));
    }
  }
}
