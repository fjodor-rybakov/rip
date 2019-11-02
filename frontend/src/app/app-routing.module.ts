import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { NewsComponent } from "./components/news/list/news.component";
import { RegistrationComponent } from "./components/registration/registration.component";
import { LoginComponent } from "./components/login/login.component";
import { NewsCreateComponent } from "./components/news/create/news-create.component";
import { ProfileComponent } from "./components/profile/profile.component";


const routes: Routes = [
  {path: "", component: NewsComponent},
  {path: "registration", component: RegistrationComponent},
  {path: "login", component: LoginComponent},
  {path: "profile", component: ProfileComponent},
  {path: "news/create", component: NewsCreateComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
