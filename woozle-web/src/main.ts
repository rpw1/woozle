import { provideHttpClient, withInterceptors } from '@angular/common/http';
import {
  enableProdMode,
  isDevMode,
  provideZonelessChangeDetection,
} from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideServiceWorker } from '@angular/service-worker';
import { appRoutes } from './app/app-routes';
import { AppComponent } from './app/app.component';
import { httpResponseInterceptor } from './app/shared/interceptors/http-response.interceptor';
import { spotifyAuthInterceptor } from './app/shared/interceptors/spotify-auth.interceptor';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

bootstrapApplication(AppComponent, {
  providers: [
    provideZonelessChangeDetection(),
    provideRouter(appRoutes),
    provideHttpClient(withInterceptors([
      spotifyAuthInterceptor, 
      httpResponseInterceptor
    ])),
    provideServiceWorker('ngsw-worker.js', {
      enabled: !isDevMode(),
      registrationStrategy: 'registerWhenStable:30000',
    }),
  ],
}).catch((e) => console.error(e));
