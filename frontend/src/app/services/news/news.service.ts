import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { INews } from "./interfaces/INews";
import { Observable } from "rxjs";
import { IId } from "../../shared/interfaces/IId";
import { ICUNews } from "./interfaces/ICUNews";
import { Message } from "../../shared/interfaces/Message";

@Injectable()
export class NewsService {
  constructor(private httpClient: HttpClient) {
  }

  public getNewsList(isOnlyMy: boolean): Observable<INews[]> {
    return this.httpClient.get<INews[]>("news", {params: {onlyMy: `${isOnlyMy}`}});
  }

  public createNews(body: ICUNews): Observable<IId> {
    return this.httpClient.post<IId>("news", body);
  }

  public updateNews(id: number, body: ICUNews): Observable<IId> {
    return this.httpClient.put<IId>(`news/${id}`, body);
  }

  public deleteNews(id: number): Observable<Message> {
    return this.httpClient.delete<Message>(`news/${id}`);
  }
}
