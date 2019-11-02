import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IProfile } from "./interfaces/IProfile";

@Injectable()
export class ProfileService {
  constructor(private httpClient: HttpClient) {
  }

  public getProfile(): Observable<IProfile> {
    return this.httpClient.get<IProfile>("profile");
  }
}
