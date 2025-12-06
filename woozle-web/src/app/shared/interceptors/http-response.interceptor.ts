import { HttpErrorResponse, HttpInterceptorFn, HttpStatusCode } from '@angular/common/http';
import { inject, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs';
import { ToastService } from '../services/toast.service';
import { SpotifyIdentityService } from '../../woozle/services/spotify-identity.service';
import { ForbiddenErrorsService } from '../services/forbidden-error.service';

export const httpResponseInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const injector = inject(Injector);
  const toastService = inject(ToastService);
  const forbiddenErrorsService = inject(ForbiddenErrorsService);
  return next(req).pipe(
    catchError(async (error, caught) => {
      if (error instanceof HttpErrorResponse) {
        switch(error.status) {
          case HttpStatusCode.Unauthorized:
            const refreshToken = localStorage.getItem('refresh_token');
            if (!refreshToken) {
              void router.navigate(['/home']);
              return error;
            }
            const spotifyIdentityService = injector.get(SpotifyIdentityService);
            const isAuthenticated = await spotifyIdentityService.refreshAccessToken({
              refreshToken: refreshToken,
            });

            if (isAuthenticated) {
              toastService.showSuccess('Successfully authenticated');
            } else {
              forbiddenErrorsService.addErrors('Unable to refresh access token. Please try again.');
              localStorage.removeItem('access_token');
              localStorage.removeItem('refresh_token');
            }

            void router.navigate(['/woozle']);
            break;
        }
      }

      return error;
    })
  )
};