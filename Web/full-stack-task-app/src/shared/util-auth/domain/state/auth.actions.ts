import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { AuthResponse } from '../dtos/auth.response';

export const AuthActions = createActionGroup({
  source: 'Auth',
  events: {
    Login: props<{ username: string; password: string }>(),
    'Login Success': props<{ user: AuthResponse }>(),
    'Login Failure': props<{ error: string[] }>(),
    Logout: emptyProps(),
  },
});
