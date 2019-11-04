import { IUser } from "../../auth/interfaces/IUser";

export interface INews extends IUser {
  userId: number;

  title: string;

  description: string;

  pathToImages: string[];

  createdAt: Date;
}
