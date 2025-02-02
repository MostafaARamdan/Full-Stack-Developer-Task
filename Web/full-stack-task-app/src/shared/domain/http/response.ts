import { ResponseStatus } from '../constants/response-status.enum';

export class Response<T> {
  resource!: T;
  status!: ResponseStatus;
  messages!: string[];
}
