import { ContentType } from './content-type';
import { OnlineImage } from './online-image';

export interface Content {
  id: string;
  contentType: ContentType;
  name: string;
  description?: string;
  image: OnlineImage;
}


