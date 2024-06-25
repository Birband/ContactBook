import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from '../services/api/api.service';

@Component({
  selector: 'app-edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrls: ['./edit-contact.component.css']
})
export class EditContactComponent implements OnInit {
  contactForm!: FormGroup;
  categories: any[] = [];
  contactEmail: string = '';

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // get email from route
    this.route.params.subscribe((params) => {
      this.contactEmail = params['email'];
      this.initForm();
      this.fetchCategories();
      this.loadContactDetails();
    });
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
      password: ['', Validators.required] // For updating password
    });
  }

  private fetchCategories(): void {
    this.apiService.fetchCategories().subscribe(
      (data: any[]) => {
        this.categories = data;
      },
      (error) => {
        console.error('Error fetching categories: ', error);
      }
    );
  }

  private loadContactDetails(): void {
    this.apiService.fetchContact(this.contactEmail).subscribe(
      (contact: any) => {
        // find category with this name and set it as selected
        var cat = this.categories.find((c) => c.name === contact.category);
        
        var subcat = cat.subcategories.find((sc: any) => sc.name === contact.subcategory);
        if (cat.subcategories.length == 0) {
          subcat = contact.subcategory;
        }
        this.contactForm.patchValue({
          firstName: contact.firstName,
          lastName: contact.lastName,
          email: contact.email,
          phoneNumber: contact.phoneNumber,
          birthDate: new Date(contact.birthDate),
          category: cat,
          subcategory: subcat
        });
      },
      (error) => {
        console.error('Error fetching contact details: ', error);
      }
    );
  }

  onSubmit(): void {
    if (this.contactForm.invalid) {
      return;
    }

    // Update contact details
    this.apiService.updateContact(this.contactForm.value).subscribe(
      () => {
        this.router.navigate(['/contacts']);
      },
      (error) => {
        console.error('Error updating contact: ', error);
      }
    );
  }
}
