import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { TokenStorageService } from "./services/storage/token-storage.service";
import { AuthService } from "./services/auth/auth.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  constructor(
    public router: Router,
    private readonly tokenStorageService: TokenStorageService,
    private readonly authService: AuthService
  ) {
  }
}
