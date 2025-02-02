// auth.service.ts
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../../../util-common/domain/http/http.service';
import { AuthResponse } from '../dtos/auth.response';
import { Response } from '../../../util-common/domain/http/response';

@Injectable({ providedIn: 'root' })
export class AuthService {
  LOCAL_STORAGE_KEY = 'auth_token';
  private readonly apiUrl = 'https://localhost:7277/api/Account';
  constructor(private http: HttpService) {}

  login(
    username: string,
    password: string
  ): Observable<Response<AuthResponse>> {
    return this.http.post(`${this.apiUrl}/Login`, { username, password });
  }

  clearSession(): void {
    localStorage.removeItem(this.LOCAL_STORAGE_KEY);
  }
  public storeSession(authResponse: AuthResponse): void {
    localStorage.setItem(this.LOCAL_STORAGE_KEY, JSON.stringify(authResponse));
  }
  public getSession(): AuthResponse | null {
    const session = localStorage.getItem(this.LOCAL_STORAGE_KEY);
    if (session) {
      return JSON.parse(session) as AuthResponse;
    }
    return null;
  }
}
