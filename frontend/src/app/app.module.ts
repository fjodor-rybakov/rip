import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { NewsComponent } from "./components/news/list/news.component";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { AuthenticationInterceptor } from "./interceptors/authentication.interceptor";
import { ApiInterceptor } from "./interceptors/api.interceptor";
import { LoginComponent } from "./components/login/login.component";
import { RegistrationComponent } from "./components/registration/registration.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { NewsCreateComponent } from "./components/news/create/news-create.component";
import { ProfileComponent } from "./components/profile/profile.component";
import { TokenStorageService } from "./services/storage/token-storage.service";
import { AuthGuard } from "./guards/auth.guard";
import { AuthService } from "./services/auth/auth.service";
import { CommentService } from "./services/comment/comment.service";
import { ProfileService } from "./services/profile/profile.service";
import { SharedService } from "./shared/services/shared.service";

@NgModule({
  declarations: [
    AppComponent,
    NewsComponent,
    LoginComponent,
    RegistrationComponent,
    NewsCreateComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    TokenStorageService,
    AuthGuard,
    AuthService,
    CommentService,
    ProfileService,
    SharedService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true,
      deps: [TokenStorageService]
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ApiInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
