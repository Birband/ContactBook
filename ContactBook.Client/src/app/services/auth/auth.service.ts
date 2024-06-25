import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenSubject: BehaviorSubject<string | null>;
  public token$: Observable<string | null>;

  constructor() {
    const token = localStorage.getItem('token');
    this.tokenSubject = new BehaviorSubject<string | null>(token);
    this.token$ = this.tokenSubject.asObservable();
  }

  isLoggedIn$(): Observable<boolean> {
    return this.token$.pipe(
      map(token => !!token)
    );
  }
  getToken(): string | null {
    return this.tokenSubject.value;
  }

  setToken(token: string | null): void {
    if (token) {
      localStorage.setItem('token', token);
      this.tokenSubject.next(token);
    } else {
      this.clearToken();
    }
  }

  clearToken(): void {
    localStorage.removeItem('token');
    this.tokenSubject.next(null);
  }
}
