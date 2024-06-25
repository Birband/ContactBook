import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api/api.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-contact-detail',
  templateUrl: './contact-detail.component.html',
  styleUrl: './contact-detail.component.css'
})
export class ContactDetailComponent implements OnInit {
  contact: any;

  constructor(private apiService: ApiService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const email = params['email'];
      this.fetchContactDetail(email);
    });
  }

  fetchContactDetail(email: string) {
    this.apiService.fetchContact(email).subscribe((data) => {
      this.contact = data;
    },
    (error) => {
      console.error('Error fetching contact detail: ', error);
    });
  }

  formatDate(date: string): string {
    if (date) {
      const parsedDate = new Date(date);
      return parsedDate.toLocaleDateString();
    } 
    return '';
  }
}
