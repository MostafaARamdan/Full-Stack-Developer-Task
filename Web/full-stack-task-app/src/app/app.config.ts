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
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { authReducer } from '../shared/util-auth/domain/state/auth.reducer';
import { AuthEffects } from '../shared/util-auth/domain/state/auth.effects';

import { routerReducer, StoreRouterConnectingModule } from '@ngrx/router-store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { NgxPermissionsModule } from 'ngx-permissions';
import { ToastrModule } from 'ngx-toastr';
import { AuthInterceptor } from '../shared/util-auth/domain/Interceptors/auth.interceptor';
import { ErrorInterceptor } from '../shared/util-auth/domain/Interceptors/error.interceptor';
import { environment } from '../shared/util-common/domain/environments/environment';

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
      StoreDevtoolsModule.instrument({
        maxAge: 25,
        logOnly: environment.production,
      }),
      ToastrModule.forRoot({
        timeOut: 5000,
        positionClass: 'toast-bottom-right',
        preventDuplicates: true,
        resetTimeoutOnDuplicate: true,
        maxOpened: 5,
        newestOnTop: true,
        easeTime: 300,
        progressBar: true,
        progressAnimation: 'decreasing',
      })
    ),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
  ],
};
