import { HttpContextToken, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize } from 'rxjs';
import { LoadingSpinnerService } from '../services/loading-spinner.service';

export const SkipLoading = 
  new HttpContextToken<boolean>(() => true);

export const loadingSpinnerInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingSpinnerService = inject(LoadingSpinnerService);
  if (req.context.get(SkipLoading)) {
    return next(req);
  }
  loadingSpinnerService.startLoading();

  return next(req).pipe(
    finalize(() => {
      loadingSpinnerService.stopLoading();
    })
  );
};
