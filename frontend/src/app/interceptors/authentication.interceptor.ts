import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ETokenType } from "../shared/enums/ETokenType";

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
  private readonly blackList = ["login", "registration"];

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem(ETokenType.ACCESS_TOKEN);
    if (!this.blackList.includes(req.url)) {
      req.headers.append("Authorization", `Bearer ${token}`);
    }
    return next.handle(req);
  }

}

