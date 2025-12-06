import { inject } from '@angular/core';
import { RedirectCommand, ResolveFn, Router } from '@angular/router';
import { SpotifyIdentityService } from '../services/spotify-identity.service';
import { ForbiddenErrorsService } from '../../shared/services/forbidden-error.service';

export const authCallbackResolver: ResolveFn<RedirectCommand> = async (route, _) => {
  const router = inject(Router);
  const spotifyIdentityService = inject(SpotifyIdentityService);
  const forbiddenErrorsService = inject(ForbiddenErrorsService);
  const error = route.queryParams['error'];
  if (error) {
    forbiddenErrorsService.addErrors(error);
    return new RedirectCommand(router.parseUrl('/forbidden'));
  }

  const isAuthenticated = spotifyIdentityService.requestAccessToken(
    route.queryParams['code']
  );

  if (!isAuthenticated) {
    forbiddenErrorsService.addErrors('Failed to authenticate. Please reach out to me personally to look into this further.');
    return new RedirectCommand(router.parseUrl('/forbidden'));
  }

  return new RedirectCommand(router.parseUrl(''));
};
