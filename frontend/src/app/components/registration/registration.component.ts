import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../services/auth/auth.service";

@Component({
  templateUrl: "./page/registration.component.html",
  styleUrls: ["./page/registration.component.scss"],
  providers: [AuthService]
})
export class RegistrationComponent implements OnInit {
  constructor(private readonly authService: AuthService) {
  }

  ngOnInit(): void {
  }

}
