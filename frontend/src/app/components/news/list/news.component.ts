import { Component, OnInit } from "@angular/core";
import { NewsService } from "../../../services/news/news.service";
import { INews } from "../../../services/news/interfaces/INews";
import { IComment } from "../../../services/comment/interfaces/IComment";
import { CommentService } from "../../../services/comment/comment.service";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { IBaseComment } from "../../../services/comment/interfaces/IBaseComment";
import { ProfileService } from "../../../services/profile/profile.service";
import { findIndex, remove } from "lodash";
import { IProfile } from "../../../services/profile/interfaces/IProfile";

@Component({
  templateUrl: "./page/news.component.html",
  styleUrls: ["./page/news.component.scss"],
  providers: [NewsService, CommentService]
})
export class NewsComponent implements OnInit {
  private isOnlyMy = false;
  private newsListData: INews[] = [];
  private commentList: IComment[] = [];
  public commentForm = new FormGroup({
    value: new FormControl("", Validators.required),
  });
  private profile!: IProfile;
  public error = "";

  constructor(
    private readonly newsService: NewsService,
    private readonly commentService: CommentService,
    private readonly profileService: ProfileService
  ) {
  }

  async ngOnInit(): Promise<void> {
    this.newsListData = await this.newsService.getNewsList(this.isOnlyMy).toPromise();
    this.commentList = await this.commentService.getCommentListByNews().toPromise();
    this.profile = await this.profileService.getProfile().toPromise();
  }

  public async filterNews(): Promise<void> {
    this.isOnlyMy = !this.isOnlyMy;
    this.newsListData = await this.newsService.getNewsList(this.isOnlyMy).toPromise();
  }

  public filterCommentListNyMews(newsId: number): IComment[] {
    return this.commentList.filter((value => value.newsId === newsId));
  }

  public formattingDate(date: Date): string {
    const instance = new Date(date);
    return instance.toString();
  }

  public async createComment(newsId: number): Promise<void> {
    const data: IBaseComment = {
      newsId,
      userId: this.profile.id,
      value: this.commentForm.value.value
    };
    if (!this.commentForm.valid) {
      this.error = "Некорректные данные!";
      return;
    }
    const commentResponse = await this.commentService.createComment(data).toPromise();
    const newData: IComment = {
      value: this.commentForm.value.value,
      newsId,
      userId: this.profile.id,
      createdAt: new Date(),
      avatar: this.profile.avatar,
      id: commentResponse.id,
      nickname: this.profile.nickname
    };
    this.commentList.push(newData);
  }

  public checkMy(userId: number): boolean {
    return this.profile.id === userId;
  }

  public async removeComment(commentId: number): Promise<void> {
    await this.commentService.deleteComment(commentId).toPromise();
    remove(this.commentList, (value) => value.id === commentId);
  }

  public async removeNews(newsId: number): Promise<void> {
    await this.newsService.deleteNews(newsId).toPromise();
    remove(this.newsListData, (value) => value.id === newsId);
  }
}
