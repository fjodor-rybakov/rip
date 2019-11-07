import { Component } from "@angular/core";
import { TokenStorageService } from "./services/storage/token-storage.service";
import { AuthService } from "./services/auth/auth.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
  providers: []
})
export class AppComponent {
  constructor(
    private readonly tokenStorageService: TokenStorageService,
    private readonly authService: AuthService,
  ) {
  }
}
