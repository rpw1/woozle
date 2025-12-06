import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { mergeMap, of } from 'rxjs';
import { PlayerService } from '../services/player.service';

export const playerGuard: CanActivateFn = (route, state) => {
  const playerService = inject(PlayerService);
  return of(playerService.loadPlayer()).pipe(
    mergeMap(() => playerService.playerActive$)
  );
};
