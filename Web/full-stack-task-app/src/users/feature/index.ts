import { Routes } from '@angular/router';
import { UsersListComponent } from './users-list/users-list.component';
import { PersistUserComponent } from './persist-user/persist-user.component';

export const USERS_ROUTES: Routes = [
  {
    path: '',
    component: UsersListComponent,
  },
  {
    path: 'create',
    component: PersistUserComponent,
  },
  {
    path: 'edit',
    component: PersistUserComponent,
  },
];
