import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { ContentsStore } from './contents.state';

export const availableContentResolver: ResolveFn<boolean> = (route, state) => {
  const contentsStore = inject(ContentsStore);
  contentsStore.loadContent();
  contentsStore.resetFilters();
  return true;
};
