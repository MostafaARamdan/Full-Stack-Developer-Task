import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import {
  selectIsLoggedIn,
  selectCurrentUser,
  selectFullname,
} from '../shared/util-auth/domain/state/auth.selectors';
import { AuthActions } from '../shared/util-auth/domain/state/auth.actions';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  isLoggedIn$: Observable<boolean>;
  fullname$: Observable<string>;

  constructor(private store: Store) {
    this.isLoggedIn$ = this.store.select(selectIsLoggedIn);
    this.fullname$ = this.store.select(selectFullname);
  }

  logout() {
    this.store.dispatch(AuthActions.logout());
  }
}
