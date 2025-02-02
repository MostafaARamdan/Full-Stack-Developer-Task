// user.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { HttpService } from '../../../shared/domain/http/http.service';
import { GetUsersResponse } from '../dtos/users-response';
import { GetUsersQuery } from '../dtos/users.query';
import { Response } from '../../../shared/domain/http/response';
import { catchError, Observable, of } from 'rxjs';
import { ResponseStatus } from '../../../shared/domain/constants/response-status.enum';

@Injectable({ providedIn: 'root' })
export class UserService {
  private readonly apiUrl = 'https://localhost:7277/api/User';

  constructor(private http: HttpService) {}

  getUsers(usersQuery: GetUsersQuery): Observable<Response<GetUsersResponse>> {
    return this.http.post<Response<GetUsersResponse>>(
      `${this.apiUrl}/GetUsers`,
      usersQuery
    );
  }
}
