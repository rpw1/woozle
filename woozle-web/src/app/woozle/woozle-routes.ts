import { Routes } from "@angular/router";
import { playerGuard } from './guards/player.guard';
import { contentGuard } from './guards/content.guard';
import { availableContentResolver } from './resolvers/available-content.resolver';

export const woozleRoutes: Routes = [
  {
    path: '',
    redirectTo: 'play',
    pathMatch: 'full'
  },
  {
    path: 'play',
    canActivate: [ contentGuard, playerGuard ],
    loadComponent: () => import('./components/game/game.component')
      .then(x => x.GameComponent)
  },
  {
    path: 'content',
    resolve: { data: availableContentResolver },
    loadComponent: () => import('./components/content-list/content-list.component')
      .then(x => x.ContentListComponent),
  },
]