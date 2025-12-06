import { SpotifyPlaylistTrack } from './spotify-playlist-track';

export interface SpotifyPlaylistItemsResponse {
  limit: number,
  next: string,
  offset: number,
  previous: string,
  total: number,
  items: SpotifyPlaylistTrack[]
}