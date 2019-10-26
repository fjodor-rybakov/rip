import { IUser } from "../../auth/interfaces/IUser";

export interface INews extends IUser {
  title: string;

  description: string;

  imagePaths: string[];

  createdAt: Date;
}
