import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IUploadResponse } from "./interfaces/IUploadResponse";

@Injectable()
export class MediaService {
  constructor(private httpClient: HttpClient) {
  }

  public uploadFiles(formData: FormData): Observable<IUploadResponse> {
    return this.httpClient.put<IUploadResponse>("media/upload", formData);
}
}
