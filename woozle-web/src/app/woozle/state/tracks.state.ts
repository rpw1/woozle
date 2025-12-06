import { computed, inject } from '@angular/core';
import {
  patchState,
  signalStore,
  withComputed,
  withMethods,
  withState,
} from '@ngrx/signals';
import { firstValueFrom } from 'rxjs';
import { ContentService } from '../services/content.service';
import { Track } from '../models/track';
import { ContentType } from '../models/content-type';
import { Content } from '../models/content';

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
  withMethods((store, 
    contentService = inject(ContentService)) => ({
    async loadTracks(content: Content): Promise<void> {
      const tracks = await firstValueFrom(
        contentService.getTracks(content.id, content.contentType)
      );
      patchState(store, {
        tracks,
        contentId: content.id,
        contentType: content.contentType,
        contentName: content.name,
      });
    },
  }))
);
