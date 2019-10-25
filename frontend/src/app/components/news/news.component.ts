import { Component, OnInit } from "@angular/core";
import { NewsService } from "../../services/news/news.service";
import { INews } from "../../shared/interfaces/INews";

@Component({
  templateUrl: "./page/news.component.html",
  styleUrls: ["./page/news.component.scss"],
  providers: [NewsService]
})
export class NewsComponent implements OnInit {
  public newsListData: INews[] = [];

  constructor(private readonly newsService: NewsService) { }

  async ngOnInit() {
    this.newsListData = await this.newsService.getNewsList();
    console.log(this.newsListData[0].title);
  }
}