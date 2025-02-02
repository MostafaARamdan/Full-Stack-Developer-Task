import { Routes } from '@angular/router';
import { USERS_ROUTES } from '../users/feature';
import { Auth_ROUTES } from '../shared/util-auth/feature';
import { AuthenticationGuard } from '../shared/util-auth/domain/guards/authentication.guard';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => Auth_ROUTES,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'users',
    loadChildren: () => USERS_ROUTES,
  },
];
