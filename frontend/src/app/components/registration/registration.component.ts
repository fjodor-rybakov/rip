import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../services/auth/auth.service";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { IRegistration } from "../../services/auth/interfaces/IRegistration";

@Component({
  templateUrl: "./page/registration.component.html",
  styleUrls: ["./page/registration.component.scss"],
  providers: [AuthService]
})
export class RegistrationComponent implements OnInit {
  public registrationForm = new FormGroup({
    email: new FormControl("", [Validators.required, Validators.email]),
    nickname: new FormControl("", Validators.required),
    password: new FormControl("", Validators.required)
  });

  constructor(private readonly authService: AuthService) {
  }

  ngOnInit(): void {
  }

  onSubmit() {
    console.log(this.registrationForm);
    if (!this.registrationForm.valid) {
      console.log("Некорректные данные!");
      return;
    }
    const data = this.registrationForm.value as IRegistration;
    this.authService.registration(data).subscribe((result) => console.log(result.message));
  }

}
