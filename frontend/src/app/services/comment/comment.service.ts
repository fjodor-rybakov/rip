import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IComment } from "./interfaces/IComment";
import { Message } from "../../shared/interfaces/Message";
import { IBaseComment } from "./interfaces/IBaseComment";
import { IId } from "../../shared/interfaces/IId";

@Injectable()
export class CommentService {
  constructor(private httpClient: HttpClient) {
  }

  public getCommentListByNews(): Observable<IComment[]> {
    return this.httpClient.get<IComment[]>(`comment`);
  }

  public createComment(data: IBaseComment): Observable<IId> {
    console.log(data);
    return this.httpClient.post<IId>(`comment`, data);
  }

  public deleteComment(id: number): Observable<Message> {
    return this.httpClient.delete<Message>(`comment/${id}`);
  }
}
