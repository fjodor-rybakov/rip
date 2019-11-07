import { IBaseComment } from "./IBaseComment";

export interface IComment extends IBaseComment {
  avatar: string;
  nickname: string;
  createdAt: Date;
  id?: number;
}
