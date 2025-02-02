// user.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { HttpService } from '../../../shared/domain/http/http.service';
import { GetUsersResponse } from '../dtos/users-response';
import { GetUsersQuery } from '../dtos/users.query';
import { Response } from '../../../shared/domain/http/response';
import { catchError, Observable, of } from 'rxjs';
import { ResponseStatus } from '../../../shared/domain/constants/response-status.enum';
import { UserDTO } from '../dtos/user.dto';
import { UserDetailsDTO } from '../dtos/user-details.dto';
import { GetUserByIdResponse } from '../dtos/user-by-id-response';

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

  getUser(id: string | null): Observable<Response<GetUserByIdResponse>> {
    return this.http.get<Response<GetUserByIdResponse>>(
      `${this.apiUrl}/Get?id=${id}`
    );
  }

  addUser(user: UserDTO): Observable<Response<boolean>> {
    return this.http.post<Response<boolean>>(this.apiUrl, user);
  }

  updateUser(user: UserDTO): Observable<Response<boolean>> {
    return this.http.put<Response<boolean>>(`${this.apiUrl}`, user);
  }

  deleteUser(id: string): Observable<Response<boolean>> {
    return this.http.delete<Response<boolean>>(
      `${this.apiUrl}/Delete?id=${id}`
    );
  }
}
