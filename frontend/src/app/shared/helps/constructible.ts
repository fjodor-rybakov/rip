export class Constructible<T> {
  constructor(data?: T) {
    Object.assign(this, data);
  }
}
