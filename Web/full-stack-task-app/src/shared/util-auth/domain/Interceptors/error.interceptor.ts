import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { Store } from '@ngrx/store';
import { catchError } from 'rxjs/operators';
import { AuthActions } from '../state/auth.actions';
import { NotificationService } from '../../../util-common/domain/notifications/notification.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private store: Store,
    private notificationService: NotificationService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status === 401 || err.status === 502) {
          if (!err.url.includes('api/account/logout'))
            this.store.dispatch(AuthActions.logout());
          return throwError(() => err);
        } else if (err.status === 403) {
          this.notificationService.showError(
            'You are not authorized to perform this action. Please contact your system administrator.'
          );
          return of();
        } else {
          return throwError(() => err);
        }
      })
    );
  }
}
