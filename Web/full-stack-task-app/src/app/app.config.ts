import {
  ApplicationConfig,
  importProvidersFrom,
  inject,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter, RouterModule } from '@angular/router';

import { routes } from './app.routes';
import {
  META_REDUCERS,
  provideState,
  provideStore,
  StoreModule,
} from '@ngrx/store';
import { EffectsModule, provideEffects } from '@ngrx/effects';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { authReducer } from '../shared/util-auth/domain/state/auth.reducer';
import { AuthEffects } from '../shared/util-auth/domain/state/auth.effects';

import { routerReducer, StoreRouterConnectingModule } from '@ngrx/router-store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { NgxPermissionsModule } from 'ngx-permissions';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideHttpClient(withInterceptorsFromDi()),
    importProvidersFrom(
      NgxPermissionsModule.forRoot(),
      StoreModule.forRoot({
        router: routerReducer,
        auth: authReducer,
      }),
      EffectsModule.forRoot([AuthEffects]),
      BrowserModule,
      BrowserAnimationsModule,
      RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' }),
      StoreRouterConnectingModule.forRoot(),
      StoreDevtoolsModule.instrument({ maxAge: 25 })
    ),
  ],
};
