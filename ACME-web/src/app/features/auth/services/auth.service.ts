import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { RegisterRequest } from '../models/register-request.model';
import { RegisterResponse } from '../models/register-response.model';
import { throwError, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private apiUrl = 'https://localhost:7144/api/v1/auth';

  constructor(private http: HttpClient) {}

  login(data: LoginRequest) {
    return this.http.post<LoginResponse>(`${this.apiUrl}/signin`, data)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.accessToken);
        })
        //   catchError((error) => {
        // return throwError(() => error);
      // })
      );
  }

  register(data: RegisterRequest) {
    return this.http.post<RegisterResponse>(`${this.apiUrl}/signup`, data)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.accessToken);
        })
      );
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
  }
}