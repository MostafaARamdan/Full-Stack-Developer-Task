export enum ResponseStatus {
  Success = 1,
  Error = -1,
  Exception = -2,
  Security = -3,
  ConcurrentSession = -4,
  Forbidden = -403,
  BadGateway = -502,
  Unauthorized = -401,
  LDAPUser = -5,
}
