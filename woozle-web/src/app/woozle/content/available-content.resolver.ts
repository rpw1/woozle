import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { ContentsStore } from './contents.state';

export const availableContentResolver: ResolveFn<boolean> = async (route,state) => {
  const contentsStore = inject(ContentsStore);
  await contentsStore.loadContent();
  contentsStore.resetFilters();
  return true;
};
