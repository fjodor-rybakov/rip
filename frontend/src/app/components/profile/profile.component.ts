import { Component, OnInit } from "@angular/core";
import { ProfileService } from "../../services/profile/profile.service";

@Component({
  templateUrl: "./page/profile.component.html",
  styleUrls: ["./page/profile.component.scss"],
  providers: [ProfileService]
})
export class ProfileComponent implements OnInit {
  constructor(private readonly profileService: ProfileService) {

  }

  async ngOnInit() {
  }
}
