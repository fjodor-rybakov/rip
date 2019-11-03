import { NewsService } from "../../../services/news/news.service";
import { Component, OnInit } from "@angular/core";

@Component({
  templateUrl: "./page/news-create.component.html",
  styleUrls: ["./page/news-create.component.scss"],
  providers: [NewsService]
})
export class NewsCreateComponent implements OnInit {
  constructor(private readonly newsService: NewsService) {
  }

  async ngOnInit() {
  }
}
