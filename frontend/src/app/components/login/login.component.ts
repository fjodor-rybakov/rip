import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../services/auth/auth.service";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { IRegistration } from "../../services/auth/interfaces/IRegistration";
import { ILogin } from "../../services/auth/interfaces/ILogin";
import { TokenStorageService } from "../../services/storage/token-storage.service";

@Component({
  templateUrl: "./page/login.component.html",
  styleUrls: ["./page/login.component.scss"],
  providers: [AuthService, TokenStorageService]
})
export class LoginComponent implements OnInit {
  public loginForm = new FormGroup({
    email: new FormControl("", [Validators.required, Validators.email]),
    password: new FormControl("", Validators.required)
  });

  constructor(private readonly authService: AuthService, private readonly tokenStorageService: TokenStorageService) {
  }

  ngOnInit(): void {
  }

  onSubmit() {
    console.log(this.loginForm);
    if (!this.loginForm.valid) {
      console.log("Некорректные данные!");
      return;
    }
    const data = this.loginForm.value as ILogin;
    this.authService.login(data).subscribe((result) => this.tokenStorageService.setAccessTokenName(result.token));
  }
}
