import { SpotifyImage } from './spotify-image';

export interface SpotifyPlaylist {
  id: string,
  description: string,
  images: SpotifyImage[],
  name: string
}
