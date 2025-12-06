import { computed, inject, Signal } from '@angular/core';
import {
  patchState,
  signalStore,
  withComputed,
  withMethods,
  withState,
} from '@ngrx/signals';
import { firstValueFrom } from 'rxjs';
import { ContentService } from '../services/content.service';
import { Content } from '../models/content';
import { ContentType } from '../models/content-type';

export enum ContentFilterType {
  Ascending,
  Descending,
  Recent,
}

export type ContentFilters = {
  filterType: ContentFilterType;
  name?: string;
};

export type ContentState = {
  contents: Content[];
  isLoading: boolean;
  filters: ContentFilters;
};

const initialState: ContentState = {
  contents: [],
  isLoading: false,
  filters: {
    filterType: ContentFilterType.Recent,
  },
};

export const ContentsStore = signalStore(
  withState(initialState),
  withComputed(({ contents, filters }) => ({
    albums: computed(() =>
      getAvailableContent(ContentType.Album, contents, filters)
    ),
    artists: computed(() =>
      getAvailableContent(ContentType.Artist, contents, filters)
    ),
    playlists: computed(() =>
      getAvailableContent(ContentType.Playlist, contents, filters)
    ),
  })),
  withMethods((store, contentService = inject(ContentService)) => ({
    async loadContent(): Promise<void> {
      patchState(store, { isLoading: true });
      const contents = await firstValueFrom(contentService.getContent());
      patchState(store, { contents, isLoading: false });
    },
    resetFilters(): void {
      patchState(store, () => ({ filters: { ...initialState.filters } }));
    },
    updateFilters(filters: ContentFilters): void {
      patchState(store, () => ({ filters: filters }));
    },
  }))
);

const getAvailableContent = (
  contentType: ContentType,
  contents: Signal<Content[]>,
  filters: Signal<ContentFilters>
) => {
  const availableContents = contents().filter(
    (content) =>
      content.name
        .toLowerCase()
        .trim()
        .includes(filters().name?.toLowerCase().trim() ?? '') &&
      content.contentType == contentType
  );

  switch (filters().filterType) {
    case ContentFilterType.Ascending:
      return availableContents.sort((a, b) => a.name.localeCompare(b.name));
    case ContentFilterType.Descending:
      return availableContents.sort(
        (a, b) => a.name.localeCompare(b.name) * -1
      );
    default:
      return availableContents;
  }
};
