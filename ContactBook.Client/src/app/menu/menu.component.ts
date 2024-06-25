import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { Router } from '@angular/router';
import { Observable} from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css'
})
export class MenuComponent implements OnInit {
  isLogged$: Observable<boolean>;

  constructor(private authService: AuthService, private router: Router) {
    this.isLogged$ = this.authService.isLoggedIn$();
  }

  ngOnInit(): void {
  }

  logout() {
    this.authService.clearToken();
  }
}
