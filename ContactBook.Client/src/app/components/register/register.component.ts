import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api/api.service';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  errorMessage: string[] = [];

  constructor(private apiService: ApiService, 
              private authService: AuthService,
              private router: Router) {}

  register() {
    this.apiService.register(this.email, this.password, this.confirmPassword).subscribe((response) => {
      this.authService.setToken(response.token);
      this.router.navigate(['/login']);
    },
    (error) => {
      this.errorMessage = error.error.error
    });
  }
}
