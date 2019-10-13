import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { INews } from "../../shared/interfaces/INews";
import { Observable } from "rxjs";
import { IId } from "../../shared/interfaces/IId";
import { ICUNews } from "../../shared/interfaces/ICUNews";
import { IMessage } from "../../shared/interfaces/IMessage";

@Injectable()
export class NewsService {
  constructor(private httpClient: HttpClient) {
  }

  public async getNewsList(): Promise<INews[]> {
    return this.httpClient.get<INews[]>("news").toPromise();
  }

  public createNews(body: ICUNews): Observable<IId> {
    return this.httpClient.post<IId>("news", body);
  }

  public updateNews(id: number, body: ICUNews): Observable<IId> {
    return this.httpClient.put<IId>(`news/${id}`, body);
  }

  public deleteNews(id: number): Observable<IMessage> {
    return this.httpClient.delete<IMessage>(`news/${id}`);
  }
}
