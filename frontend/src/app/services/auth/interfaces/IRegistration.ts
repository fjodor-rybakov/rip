import { ILogin } from "./ILogin";

export interface IRegistration extends ILogin {
  nickname: string;
}
