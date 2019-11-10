import { Injectable } from "@angular/core";

@Injectable()
export class SharedService {
  get backendServerAddress(): string {
    return this._backendServerAddress;
  }
  get staticServerAddress(): string {
    return this._staticServerAddress;
  }

  // tslint:disable-next-line:variable-name
  private readonly _staticServerAddress = "http://localhost:5002";

  // tslint:disable-next-line:variable-name
  private readonly _backendServerAddress = "http://localhost:5001";
}
