import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ILogin } from "./interfaces/ILogin";
import { IToken } from "./interfaces/IToken";
import { Observable } from "rxjs";
import { IRegistration } from "./interfaces/IRegistration";
import { Message } from "../../shared/interfaces/Message";
import { Router } from "@angular/router";
import { TokenStorageService } from "../storage/token-storage.service";

@Injectable()
export class AuthService {
  private controllerName = "auth";
  constructor(
    private readonly httpClient: HttpClient,
    private readonly router: Router,
    private readonly tokenStorageService: TokenStorageService
  ) {
  }

  public async login(data: ILogin): Promise<ILogin | Message> {
    const response = await this.httpClient.post<IToken>(`${this.controllerName}/login`, data, {observe: "response"}).toPromise()
      .catch((res) => res.error);
    if (!response.body || response.status !== 200) {
      return new Message({message: response.Message});
    }
    this.tokenStorageService.setAccessTokenName(response.body.token);
    await this.router.navigate([""]);
    return response.body.token;
  }

  public registration(data: IRegistration): Observable<Message> {
    return this.httpClient.post<Message>(`${this.controllerName}/registration`, data);
  }

  public logout(): void {
    this.tokenStorageService.deleteAccessToken();
    this.router.navigate(["login"]);
  }
}
