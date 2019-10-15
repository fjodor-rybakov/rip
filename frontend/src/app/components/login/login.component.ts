import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../services/auth/auth.service";

@Component({
  templateUrl: "./page/login.component.html",
  styleUrls: ["./page/login.component.scss"],
  providers: [AuthService]
})
export class LoginComponent implements OnInit {
  constructor(private readonly authService: AuthService) {
  }

  ngOnInit(): void {
  }

}
