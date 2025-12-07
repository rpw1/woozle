import { SpotifyAlbum } from './spotify-album';
import { SpotifyArtist } from './spotify-artist';

export interface SpotifyTrack {
  album: SpotifyAlbum,
  artists: SpotifyArtist[],
  id: string,
  name: string,
  uri: string
}