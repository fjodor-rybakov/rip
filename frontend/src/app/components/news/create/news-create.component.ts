import { NewsService } from "../../../services/news/news.service";
import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ICUNews } from "../../../services/news/interfaces/ICUNews";
import { ProfileService } from "../../../services/profile/profile.service";
import { forEach } from "lodash";
import { MediaService } from "../../../services/media/media.service";

@Component({
  templateUrl: "./page/news-create.component.html",
  styleUrls: ["./page/news-create.component.scss"],
  providers: [NewsService, ProfileService, MediaService]
})
export class NewsCreateComponent implements OnInit {
  public newsCreateForm = new FormGroup({
    title: new FormControl("", Validators.required),
    description: new FormControl("")
  });

  private photos: FileList | null = null;

  constructor(
    private readonly newsService: NewsService,
    private readonly profileService: ProfileService,
    private readonly mediaService: MediaService
  ) {
  }

  async ngOnInit() {
  }

  async onSubmit() {
    const userId = (await this.profileService.getProfile().toPromise()).id;
    const newsData: ICUNews = {...this.newsCreateForm.value, userId};
    const newsId = (await this.newsService.createNews(newsData).toPromise()).id;
    const formData = new FormData();
    forEach(this.photos, (value) => {
      formData.append("files", value);
      formData.append("newsId", newsId.toString());
    });
    await this.mediaService.uploadFiles(formData).toPromise();
  }

  imageUpload(files: FileList): void {
    this.photos = files;
  }
}
