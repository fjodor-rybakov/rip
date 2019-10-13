import { IUser } from "./IUser";

export interface INews extends IUser {
  title: string;

  description: string;

  imagePaths: string[];

  createdAt: Date;
}
