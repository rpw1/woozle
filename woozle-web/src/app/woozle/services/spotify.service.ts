import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, EMPTY, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SpotifyService {
  public static readonly SPOTIFY_BASE_URL = 'https://api.spotify.com/v1';
  private readonly httpClient = inject(HttpClient);

  playPlayer(trackUri: string): Observable<any> {
    return this.httpClient
      .put(`${SpotifyService.SPOTIFY_BASE_URL}/me/player/play`, {
        position_ms: 0,
        uris: [trackUri],
        offset: {
          position: 0,
        },
      })
      .pipe(
        catchError((err) => {
          console.error(err);
          return EMPTY;
        })
      );
  }

  pausePlayer(): Observable<any> {
    return this.httpClient
      .put(`${SpotifyService.SPOTIFY_BASE_URL}/me/player/pause`, null)
      .pipe(
        catchError((err) => {
          console.error(err);
          return EMPTY;
        })
      );
  }

  transferPlayback(deviceId: string): Observable<void> {
    return this.httpClient
      .put<void>(`${SpotifyService.SPOTIFY_BASE_URL}/me/player`, {
        device_ids: [deviceId],
      })
      .pipe(
        catchError((err) => {
          console.error(err);
          return EMPTY;
        })
      );
  }
}
