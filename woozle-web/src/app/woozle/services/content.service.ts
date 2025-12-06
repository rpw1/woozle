import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Content } from '../models/content';
import { ContentType } from '../models/content-type';
import { Track } from '../models/track';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ContentService {
  private readonly httpClient = inject(HttpClient);

  getContent(): Observable<Content[]> {
    return this.httpClient.get<Content[]>(
      `${environment.woozleApiBaseUrl}/api/spotify/content`
    );
  }

  getTracks(id: string, contentType: ContentType): Observable<Track[]> {
    let params = new HttpParams();
    params = params.append('contentType', contentType);
    return this.httpClient.get<Track[]>(
      `${environment.woozleApiBaseUrl}/api/spotify/content/${id}/tracks`,
      {
        params: params,
      }
    );
  }
}
