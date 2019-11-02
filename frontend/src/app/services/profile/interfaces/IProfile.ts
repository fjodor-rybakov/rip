import { IUser } from "../../auth/interfaces/IUser";

export interface IProfile extends IUser {
  email: string;
  password: string;
  roleId: number;
}
