import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { ETokenType } from "../shared/enums/ETokenType";

@Injectable()
export class AuthGuard implements CanActivate {
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return !localStorage.getItem(ETokenType.ACCESS_TOKEN);
  }
}
