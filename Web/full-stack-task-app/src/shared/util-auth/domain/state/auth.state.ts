import { AuthResponse } from '../dtos/auth.response';

export interface AuthState {
  user: AuthResponse | null;
  loading: boolean;
  error: string[] | null;
  isLoggedIn: boolean;
}

export const initialAuthState: AuthState = {
  user: null,
  loading: false,
  error: null,
  isLoggedIn: false,
};
