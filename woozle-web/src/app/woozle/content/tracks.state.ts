import { computed, inject } from '@angular/core';
import { tapResponse } from '@ngrx/operators';
import { patchState, signalStore, withComputed, withMethods, withState } from '@ngrx/signals';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { pipe, switchMap } from 'rxjs';
import { Content } from './content';
import { ContentType } from './content-type';
import { ContentService } from './content.service';
import { Track } from './track';

export type TrackState = {
  tracks: Track[];
  contentId: string;
  contentType: ContentType;
  contentName: string;
};

const initialState: TrackState = {
  tracks: [],
  contentId: '',
  contentType: ContentType.Playlist,
  contentName: '',
};

export const TracksStore = signalStore(
  withState(initialState),
  withComputed(({ tracks }) => ({
    randomTracks: computed(() => {
      for (let i = tracks().length - 1; i >= 0; i--) {
        const randomIndex = Math.floor(Math.random() * (i + 1));
        const temp = tracks()[i];
        tracks()[i] = tracks()[randomIndex];
        tracks()[randomIndex] = temp;
      }
      return tracks();
    }),
  })),
  withMethods((store, contentService = inject(ContentService)) => ({
    loadTracks: rxMethod<Content>(
      pipe(
        switchMap(content =>
          contentService.getTracks(content.id, content.contentType).pipe(
            tapResponse({
              next: tracks =>
                patchState(store, {
                  tracks,
                  contentId: content.id,
                  contentType: content.contentType,
                  contentName: content.name,
                }),
              error: console.error,
            }),
          ),
        ),
      ),
    ),
  })),
);
