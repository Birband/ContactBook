import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../services/api/api.service';

@Component({
  selector: 'app-new-contact',
  templateUrl: './new-contact.component.html',
  styleUrl: './new-contact.component.css'
})
export class NewContactComponent implements OnInit {
  contactForm!: FormGroup;
  categories: any[] = [];
  errors: any[] = [];

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.fetchCategories();
  }

  private initForm(): void {
    this.contactForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      birthDate: ['', Validators.required],
      category: [null, Validators.required],
      subcategory: [''],
      password: ['', [Validators.required]] // Added password with validation
    });
  }

  private fetchCategories(): void {
    this.apiService.fetchCategories().subscribe((data: any[]) => {
      this.categories = data;
      if (this.categories.length > 0) {
        this.contactForm.get('category')?.setValue(this.categories[0]);
      }
    },
    (error) => {
      console.error('Error fetching categories: ', error);
    });
  }

  onSubmit(): void {
    if (this.contactForm.invalid) {
      return;
    }

    this.apiService.createContact(this.contactForm.value).subscribe(() => {
      this.router.navigate(['/contacts']);
    },
    (error) => {
      this.errors = error.error.error;
    });
  }
}
