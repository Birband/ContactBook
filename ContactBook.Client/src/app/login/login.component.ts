import { Component } from '@angular/core';
import { ApiService } from '../services/api/api.service';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string[] = [];

  constructor(private apiService: ApiService,
              private authService: AuthService,
              private router: Router) {}

  login() {
    this.apiService.login(this.email, this.password).subscribe((response) => {
      this.authService.setToken(response.token);
      this.router.navigate(['/contacts']);
    },
    (error) => {
      this.errorMessage = error.error.error
    });
  }
}
