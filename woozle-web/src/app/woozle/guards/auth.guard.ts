import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  RouterStateSnapshot,
} from '@angular/router';
import { SpotifyIdentityService } from '../services/spotify-identity.service';

export const authGuard: CanActivateFn = async (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const accessToken = localStorage.getItem('access_token');
  const refreshToken = localStorage.getItem('refresh_token');
  const spotifyIdentityService = inject(SpotifyIdentityService);
  if (!accessToken || !refreshToken) {
    return spotifyIdentityService.authorize();
  }

  return Promise.resolve(true);
};
