import { Routes } from '@angular/router';
import { UsersListComponent } from './users-list/users-list.component';
import { PersistUserComponent } from './persist-user/persist-user.component';
import { AuthorizationGuard } from '../../shared/util-auth/domain/guards/authorization.guard';

export const USERS_ROUTES: Routes = [
  {
    path: '',
    component: UsersListComponent,
  },
  {
    path: 'add',
    component: PersistUserComponent,
    canActivate: [AuthorizationGuard],
    data: { permissions: ['Admin'] },
  },
  {
    path: 'edit',
    component: PersistUserComponent,
    canActivate: [AuthorizationGuard],
    data: { permissions: ['Admin'] },
  },
];
