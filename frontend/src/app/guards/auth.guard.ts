import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { ETokenType } from "../shared/enums/ETokenType";
import { TokenStorageService } from "../services/storage/token-storage.service";

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private readonly tokenStorageService: TokenStorageService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (!this.tokenStorageService.getAccessTokenName()) {
      this.router.navigate(["login"]);
      return false;
    }
    return true;
  }
}
