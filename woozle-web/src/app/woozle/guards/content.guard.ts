import { inject } from '@angular/core';
import { CanActivateFn, RedirectCommand, Router } from '@angular/router';
import { TracksStore } from '../state/tracks.state';

export const contentGuard: CanActivateFn = (route, state) => {
  const tracksStore = inject(TracksStore);

  if (tracksStore.contentId() === '') {
    const router = inject(Router);
    const contentListRoute = router.parseUrl('/woozle/content');
    return new RedirectCommand(contentListRoute);
  }
  return true;
};
