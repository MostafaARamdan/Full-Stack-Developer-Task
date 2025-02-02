import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { Store } from '@ngrx/store';
import { selectIsLoggedIn } from '../state/auth.selectors';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationGuard implements CanActivate {
  constructor(private router: Router, private store: Store) {}

  async canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {
    const loggedIn = await firstValueFrom(this.store.select(selectIsLoggedIn));
    const isLoginRoute = state.url.includes('login') || state.url == '/';

    if (loggedIn && isLoginRoute) {
      await this.router.navigate(['/users']);
      return false;
    }

    if (!loggedIn && !isLoginRoute) {
      await this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
