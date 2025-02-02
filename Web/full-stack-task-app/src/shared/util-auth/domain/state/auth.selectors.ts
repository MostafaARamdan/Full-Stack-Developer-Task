import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthState } from './auth.state';

export const selectAuthState = createFeatureSelector<AuthState>('auth');

export const selectIsLoggedIn = createSelector(
  selectAuthState,
  (state) => state.isLoggedIn
);

export const selectAuthLoading = createSelector(
  selectAuthState,
  (state) => state.loading
);

export const selectAuthError = createSelector(
  selectAuthState,
  (state) => state.error
);

export const selectCurrentUser = createSelector(
  selectAuthState,
  (state) => state.user
);
export const selectCurrentToken = createSelector(
  selectAuthState,
  (state) => state.user?.token
);
export const selectFullname = createSelector(
  selectAuthState,
  (state) => state.user?.fullname
);
