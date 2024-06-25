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
  
  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/user/login`, { email, password });
  }

  register(email: string, password: string, confirmPassword: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/user/register`, { email, password, confirmPassword });
  }


}
