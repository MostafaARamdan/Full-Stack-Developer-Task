// state/auth.effects.ts
import { Injectable } from '@angular/core';
import {
  Actions,
  createEffect,
  ofType,
  ROOT_EFFECTS_INIT,
} from '@ngrx/effects';
import { EMPTY, of } from 'rxjs';
import { catchError, map, mergeMap, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { AuthActions } from './auth.actions';
import { ResponseStatus } from '../../../util-common/domain/constants/response-status.enum';
import { PermissionsService } from '../services/permissions.service';

@Injectable()
export class AuthEffects {
  ROOT_EFFECTS_INIT$;
  login$;
  loginSuccess$;
  logout$;
  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private router: Router,
    private permissionsService: PermissionsService
  ) {
    this.ROOT_EFFECTS_INIT$ = createEffect(() =>
      this.actions$.pipe(
        ofType(ROOT_EFFECTS_INIT),
        mergeMap(() => {
          const authResponse = this.authService.getSession();
          if (authResponse) {
            return of(AuthActions.loginSuccess({ user: authResponse }));
          }
          return EMPTY;
        })
      )
    );
    this.login$ = createEffect(() =>
      this.actions$.pipe(
        ofType(AuthActions.login),
        mergeMap((action) =>
          this.authService.login(action.username, action.password).pipe(
            map((response) => {
              if (response.status == ResponseStatus.Success) {
                return AuthActions.loginSuccess({ user: response.resource });
              } else
                return AuthActions.loginFailure({ error: response.messages });
            }),
            catchError((error) =>
              of(AuthActions.loginFailure({ error: error.message }))
            )
          )
        )
      )
    );

    this.loginSuccess$ = createEffect(
      () =>
        this.actions$.pipe(
          ofType(AuthActions.loginSuccess),
          tap((authResponse) => {
            this.authService.storeSession(authResponse.user);
            this.permissionsService.clearPermissions();
            this.permissionsService.loadPermissions(authResponse.user.token);
            this.router.navigate(['/users']);
          })
        ),
      { dispatch: false }
    );

    this.logout$ = createEffect(
      () =>
        this.actions$.pipe(
          ofType(AuthActions.logout),
          tap(() => {
            this.router.navigate(['/login']);
            this.authService.clearSession();
          })
        ),
      { dispatch: false }
    );
  }
}
