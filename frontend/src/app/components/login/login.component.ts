import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../services/auth/auth.service";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ILogin } from "../../services/auth/interfaces/ILogin";
import { Message } from "../../shared/interfaces/Message";

@Component({
  templateUrl: "./page/login.component.html",
  styleUrls: ["./page/login.component.scss"],
  providers: [AuthService]
})
export class LoginComponent implements OnInit {
  public error = "";
  public loginForm = new FormGroup({
    email: new FormControl("", [Validators.required, Validators.email]),
    password: new FormControl("", Validators.required)
  });

  constructor(private readonly authService: AuthService) {
  }

  ngOnInit(): void {
  }

  async onSubmit() {
    if (!this.loginForm.valid) {
      this.error = "Некорректные данные!";
      return;
    }
    const data = this.loginForm.value as ILogin;
    const response = await this.authService.login(data);
    if (response instanceof Message) {
      this.error = response.message;
    }
  }
}
