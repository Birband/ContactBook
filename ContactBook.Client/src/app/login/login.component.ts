import { Component } from '@angular/core';
import { ApiService } from '../services/api/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private apiService: ApiService, private router: Router) {}

  login() {
    this.apiService.login(this.email, this.password).subscribe((response) => {
      localStorage.setItem('token', response.token);
      this.router.navigate(['/contacts']);
    },
    (error) => {
      this.errorMessage = error.error.error
    });
  }
}
