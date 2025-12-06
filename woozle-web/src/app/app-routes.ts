import { Routes } from '@angular/router';
import { homeRoutes } from './home/home-routes';
import { ForbiddenComponent } from './shared/components/forbidden/forbidden.component';
import { GameStore } from './woozle/state/game.state';
import { authCallbackResolver } from './woozle/resolvers/auth-callback.resolver';
import { authGuard } from './woozle/guards/auth.guard';
import { ContentsStore } from './woozle/state/contents.state';
import { TracksStore } from './woozle/state/tracks.state';

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
    loadChildren: () => import('./woozle/woozle-routes').then((x) => x.woozleRoutes),
    providers: [ContentsStore, TracksStore, GameStore],
  }
];
