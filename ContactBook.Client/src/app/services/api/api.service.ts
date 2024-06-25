import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'http://localhost:5182/api';



  constructor(private http: HttpClient) { }

  fetchContacts(): Observable<any> {
    return this.http.get(`${this.apiUrl}/contact/all`);
  }

  fetchContact(email: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/contact/${email}`);
  }
  
  createContact(contactForm: any): Observable<any> {
    var contact = {
      firstName: contactForm.firstName,
      lastName: contactForm.lastName,
      email: contactForm.email,
      phoneNumber: contactForm.phoneNumber,
      address: contactForm.address,
      birthday: contactForm.birthday,
      category: contactForm.category.name,
      subcategory: contactForm.subcategory.name,
      password: contactForm.password
    };
    return this.http.post(`${this.apiUrl}/contact`, contact);
  }

  updateContact(contactForm: any): Observable<any> {
    var contact = {
      firstName: contactForm.firstName,
      lastName: contactForm.lastName,
      email: contactForm.email,
      phoneNumber: contactForm.phoneNumber,
      address: contactForm.address,
      birthday: contactForm.birthday,
      category: contactForm.category.name,
      subcategory: contactForm.subcategory.name,
      password: contactForm.password
    };
    return this.http.put(`${this.apiUrl}/contact`, contact);
  }

  deleteContact(email: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/contact/${email}`);
  }

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/user/login`, { email, password });
  }

  register(email: string, password: string, confirmPassword: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/user/register`, { email, password, confirmPassword });
  }

  fetchCategories(): Observable<any> {
    return this.http.get(`${this.apiUrl}/category/all`);
  }

}
