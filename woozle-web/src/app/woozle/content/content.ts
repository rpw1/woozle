import { OnlineImage } from './online-image';
import { ContentType } from './content-type';

export interface Content {
  id: string;
  contentType: ContentType;
  name: string;
  description?: string;
  image: OnlineImage;
}
