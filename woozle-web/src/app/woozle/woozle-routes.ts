import { Routes } from '@angular/router';
import { playerGuard } from './game/player.guard';
import { contentGuard } from './content/content.guard';
import { availableContentResolver } from './content/available-content.resolver';

export const woozleRoutes: Routes = [
  {
    path: '',
    redirectTo: 'play',
    pathMatch: 'full',
  },
  {
    path: 'play',
    canActivate: [contentGuard, playerGuard],
    loadComponent: () => import('./game/game/game.component').then(x => x.GameComponent),
  },
  {
    path: 'content',
    resolve: { data: availableContentResolver },
    loadComponent: () => import('./content/content-list/content-list.component').then(x => x.ContentListComponent),
  },
];
