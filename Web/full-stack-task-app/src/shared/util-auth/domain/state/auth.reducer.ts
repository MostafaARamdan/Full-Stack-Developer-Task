import { createReducer, on } from '@ngrx/store';
import { initialAuthState, AuthState } from './auth.state';
import { AuthActions } from './auth.actions';

export const authReducer = createReducer(
  initialAuthState,
  on(AuthActions.login, (state) => ({
    ...state,
    loading: true,
    error: null,
  })),
  on(AuthActions.loginSuccess, (state, { user }) => ({
    ...state,
    user,
    loading: false,
    isLoggedIn: true,
    error: null,
  })),
  on(AuthActions.loginFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false,
    user: null,
    isLoggedIn: false,
  })),
  on(AuthActions.logout, (state) => ({
    ...state,
    user: null,
    isLoggedIn: false,
  }))
);
