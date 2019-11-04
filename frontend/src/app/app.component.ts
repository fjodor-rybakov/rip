import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { TokenStorageService } from "./services/storage/token-storage.service";
import { AuthService } from "./services/auth/auth.service";
import { NewsComponent } from "./components/news/list/news.component";
import { NewsService } from "./services/news/news.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
  providers: [NewsComponent, NewsService]
})
export class AppComponent {
  constructor(
    public router: Router,
    private readonly tokenStorageService: TokenStorageService,
    private readonly authService: AuthService,
    private readonly newsComponent: NewsComponent
  ) {
  }
}
