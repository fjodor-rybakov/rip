import { Component, OnInit } from "@angular/core";
import { NewsService } from "../../../services/news/news.service";
import { INews } from "../../../services/news/interfaces/INews";
import { BehaviorSubject, Subject } from "rxjs";

@Component({
  templateUrl: "./page/news.component.html",
  styleUrls: ["./page/news.component.scss"],
  providers: [NewsService]
})
export class NewsComponent implements OnInit {
  private isOnlyMy = false;
  public newsListData: INews[] = [];

  constructor(private readonly newsService: NewsService) {
  }

  async ngOnInit() {
    this.newsListData = await this.newsService.getNewsList(this.isOnlyMy).toPromise();
  }

  public async filterNews() {
    this.isOnlyMy = !this.isOnlyMy;
    this.newsListData = await this.newsService.getNewsList(this.isOnlyMy).toPromise();
  }
}
