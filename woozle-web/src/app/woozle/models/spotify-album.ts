import { SpotifyImage } from './spotify-image';

export interface SpotifyAlbum {
  total_tracks: number,
  id: string,
  images: SpotifyImage[],
  name: string,
}