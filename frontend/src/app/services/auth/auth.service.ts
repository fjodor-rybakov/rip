import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ILogin } from "./interfaces/ILogin";
import { IToken } from "./interfaces/IToken";
import { Observable } from "rxjs";
import { IRegistration } from "./interfaces/IRegistration";
import { IMessage } from "../../shared/interfaces/IMessage";

@Injectable()
export class AuthService {
  private controllerName = "auth";
  constructor(private httpClient: HttpClient) {
  }

  public login(data: ILogin): Observable<IToken> {
    return this.httpClient.post<IToken>(`${this.controllerName}/login`, data);
  }

  public registration(data: IRegistration): Observable<IMessage> {
    return this.httpClient.post<IMessage>(`${this.controllerName}/registration`, data);
  }
}
