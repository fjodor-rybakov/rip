import { Component, OnInit } from "@angular/core";
import { NewsService } from "../../services/news/news.service";
import { INews } from "../../shared/interfaces/INews";

@Component({
  templateUrl: "./pages/news.component.html",
  styleUrls: ["./pages/news.component.scss"],
  providers: [NewsService]
})
export class NewsComponent implements OnInit {
  public newsListData: INews[] = [];

  constructor(private newsService: NewsService) { }

  async ngOnInit() {
    this.newsListData = await this.newsService.getNewsList();
    console.log(this.newsListData[0].title);
  }
}
