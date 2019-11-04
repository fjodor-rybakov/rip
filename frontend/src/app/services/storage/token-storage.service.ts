import { Injectable } from "@angular/core";

@Injectable()
export class TokenStorageService {
  private accessTokenName = "access_token";

  public setAccessTokenName(accessToken: string): void {
    localStorage.setItem(this.accessTokenName, accessToken);
  }

  public getAccessTokenName(): string | null {
    return localStorage.getItem(this.accessTokenName);
  }

  public deleteAccessToken() {
    localStorage.removeItem(this.accessTokenName);
  }
}
