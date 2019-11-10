import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { SharedService } from "../shared/services/shared.service";

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
    constructor(private readonly sharedService: SharedService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const apiReq = request.clone({ url: `${this.sharedService.backendServerAddress}/api/${request.url}` });
    return next.handle(apiReq);
  }

}

