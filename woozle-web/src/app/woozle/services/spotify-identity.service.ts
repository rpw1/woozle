import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AccessTokenRequest } from '../models/access-token-request';
import { AccessTokenResponse } from '../models/access-token-response';
import { RefreshTokenRequest } from '../models/refresh-token-request';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SpotifyIdentityService {
  private readonly spotifyBaseUrl = 'https://accounts.spotify.com';
  private readonly httpClient = inject(HttpClient);
  private get redirectUri() { return environment + '/auth/callback'; };

  async authorize(): Promise<boolean> {
    const scopes = [
      'user-read-currently-playing',
      'user-read-playback-state',
      'user-modify-playback-state',
      'playlist-read-private',
      'user-follow-read',
      'user-library-read',
      'streaming',
      'user-read-email',
      'user-read-private'
    ]
    const scope = scopes.join(' ');
    const authUrl = new URL(this.spotifyBaseUrl  + '/authorize');

    const params =  {
      response_type: 'code',
      client_id: environment.spotifyClientId,
      scope,
      redirect_uri: this.redirectUri,
    }

    authUrl.search = new URLSearchParams(params).toString();
    window.location.href = authUrl.toString();
    return true;
  }

  async refreshAccessToken(request: RefreshTokenRequest): Promise<boolean> {
    const refreshAccessToken$ = this.httpClient.post<AccessTokenResponse>(`${environment.woozleApiBaseUrl}/api/spotify/identity/refreshToken`, request);
    const response = await firstValueFrom(refreshAccessToken$, { defaultValue: undefined });
    if (!response) {
      return false;
    }
  
    localStorage.setItem('access_token', response.accessToken);
    if (response.refreshToken) {
      localStorage.setItem('refresh_token', response.refreshToken);
    }

    return true;
  }

  async requestAccessToken(code: string): Promise<boolean> {
    const request: AccessTokenRequest = {
      code: code,
      redirectUri: this.redirectUri
    }

    const requestAccessToken$ = this.httpClient.post<AccessTokenResponse>(`${environment.woozleApiBaseUrl}/api/spotify/identity/accessToken`, request);
    const response = await firstValueFrom(requestAccessToken$, { defaultValue: undefined });

    if (!response) {
      return false;
    }
  
    localStorage.setItem('access_token', response.accessToken);
    if (response.refreshToken) {
      localStorage.setItem('refresh_token', response.refreshToken);
    }

    return true;
  }
}
