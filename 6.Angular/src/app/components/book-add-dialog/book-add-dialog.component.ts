import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { BookService, Book } from '../../services/book.service';
import { of, Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Component({
  selector: 'app-book-add-dialog',
  templateUrl: './book-add-dialog.component.html',
  styleUrls: ['./book-add-dialog.component.scss']
})
export class BookAddDialogComponent implements OnInit {
  bookForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    public dialogRef: MatDialogRef<BookAddDialogComponent>
  ) {
    this.bookForm = this.fb.group({
      isbn: ['', Validators.required],
      title: ['', Validators.required],
      authors: ['', Validators.required],
      year: ['', Validators.required],
      price: ['', Validators.required],
      category: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  onSave(): void {
    if (this.bookForm.valid) {
      const newBook: Book = {
        isbn: this.bookForm.get('isbn')?.value,
        title: this.bookForm.get('title')?.value,
        authors: this.bookForm.get('authors')?.value.split(',').map((author: string) => author.trim()),
        year: this.bookForm.get('year')?.value,
        price: this.bookForm.get('price')?.value,
        category: this.bookForm.get('category')?.value
      };

      this.checkISBNExists(newBook.isbn).subscribe((exists: boolean) => {
        if (exists) {
          alert('ISBN already exists. Please choose a different ISBN.');
        } else {
          this.addBook(newBook);
        }
      });
    }
  }

  checkISBNExists(isbn: string): Observable<boolean> {
    return this.bookService.getBookByISBN(isbn).pipe(
      map(() => true),
      catchError(() => of(false))
    );
  }

  addBook(book: Book): void {
    this.bookService.addBook(book).subscribe(() => {
      this.dialogRef.close(book);
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
