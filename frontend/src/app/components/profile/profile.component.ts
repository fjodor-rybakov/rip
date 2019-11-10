import { Component, OnInit } from "@angular/core";
import { ProfileService } from "../../services/profile/profile.service";
import { IProfile } from "../../services/profile/interfaces/IProfile";
import { SharedService } from "../../shared/services/shared.service";

@Component({
  templateUrl: "./page/profile.component.html",
  styleUrls: ["./page/profile.component.scss"],
  providers: [ProfileService]
})
export class ProfileComponent implements OnInit {
  public userProfile?: IProfile;

  constructor(private readonly profileService: ProfileService, private readonly sharedService: SharedService) {
  }

  public async ngOnInit() {
    this.userProfile = await this.profileService.getProfile().toPromise();
  }
}
