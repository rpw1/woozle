import { Routes } from '@angular/router';
import { homeRoutes } from './home/home-routes';
import { ForbiddenComponent } from './shared/forbidden/forbidden.component';
import { GameStore } from './woozle/game/game.state';
import { authCallbackResolver } from './shared/authorization/auth-callback.resolver';
import { authGuard } from './shared/authorization/auth.guard';
import { ContentsStore } from './woozle/content/contents.state';
import { TracksStore } from './woozle/content/tracks.state';

export const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    loadChildren: () => homeRoutes,
  },
  {
    path: 'auth/callback',
    resolve: { data: authCallbackResolver },
    loadComponent: () => ForbiddenComponent,
  },
  {
    path: 'forbidden',
    loadComponent: () => ForbiddenComponent,
  },
  {
    path: 'woozle',
    canActivateChild: [authGuard],
    loadChildren: () => import('./woozle/woozle-routes').then(x => x.woozleRoutes),
    providers: [ContentsStore, TracksStore, GameStore],
  },
];
