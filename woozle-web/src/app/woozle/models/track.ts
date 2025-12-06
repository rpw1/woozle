import { OnlineImage } from './online-image';

export interface Track {
  id: string;
  explicit?: boolean;
  name: string;
  artist: string;
  trackUri: string
  image: OnlineImage;
}