import { Constructible } from "../helps/constructible";

export class Message extends Constructible<Message> {
  public message!: string;
}
