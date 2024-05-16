import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BookService, Book } from '../../services/book.service';
import { of, Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Component({
  selector: 'app-book-edit-dialog',
  templateUrl: './book-edit-dialog.component.html',
  styleUrls: ['./book-edit-dialog.component.scss']
})
export class BookEditDialogComponent implements OnInit {
  bookForm: FormGroup;
  originalISBN: string;

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    public dialogRef: MatDialogRef<BookEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Book
  ) {
    this.originalISBN = data.isbn;

    this.bookForm = this.fb.group({
      isbn: [data.isbn, Validators.required],
      title: [data.title, Validators.required],
      authors: [data.authors.join(', '), Validators.required],
      year: [data.year, Validators.required],
      price: [data.price, Validators.required],
      category: [data.category, Validators.required]
    });
  }

  ngOnInit(): void {}

  onSave(): void {
    if (this.bookForm.valid) {
      const updatedBook: Book = {
        isbn: this.bookForm.get('isbn')?.value,
        title: this.bookForm.get('title')?.value,
        authors: this.bookForm.get('authors')?.value.split(',').map((author: string) => author.trim()),
        year: this.bookForm.get('year')?.value,
        price: this.bookForm.get('price')?.value,
        category: this.bookForm.get('category')?.value
      };

      if (updatedBook.isbn !== this.originalISBN) {
        this.checkISBNExists(updatedBook.isbn).subscribe((exists: boolean) => {
          if (exists) {
            alert('ISBN already exists. Please choose a different ISBN.');
          } else {
            this.updateBook(updatedBook);
          }
        });
      } else {
        this.updateBook(updatedBook);
      }
    }
  }

  checkISBNExists(isbn: string): Observable<boolean> {
    return this.bookService.getBookByISBN(isbn).pipe(
      map(() => true),
      catchError(() => of(false))
    );
  }

  updateBook(book: Book): void {
    this.bookService.updateBook(this.originalISBN, book).subscribe(() => {
      this.dialogRef.close(book);
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
