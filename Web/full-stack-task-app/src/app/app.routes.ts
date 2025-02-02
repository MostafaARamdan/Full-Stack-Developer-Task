import { Routes } from '@angular/router';
import { USERS_ROUTES } from '../users/feature';

export const routes: Routes = [
  {
    path: 'users',
    loadChildren: () => USERS_ROUTES,
  },
];
