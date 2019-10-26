import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { NewsComponent } from "./components/news/news.component";
import { RegistrationComponent } from "./components/registration/registration.component";
import { LoginComponent } from "./components/login/login.component";


const routes: Routes = [
  {path: "", component: NewsComponent},
  {path: "registration", component: RegistrationComponent},
  {path: "login", component: LoginComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
