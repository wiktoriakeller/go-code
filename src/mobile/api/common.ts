const baseUrl = "http://192.168.1.74:5219/api/v1/";
  
const identityPaths = {
  signUp: "identity/register",
  signIn: "identity/login",
  refreshToken: "identity/refresh"
}

const coursesPaths = {
  getAll: "courses",
  getAllForUser: "courses/user",
  signUp: "courses/signup"
}

enum StatusCodes {
  BadRequest = 400,
  Unauthorized = 401,
  Forbidden = 403,
  NotFound = 404,
  InternalServerError = 500
}

interface IApiRequest<T> {
  url: string;
  method: string;
  data?: T;
  headers?: any;
}

interface IApiResponse<T> {
  data?: T;
  errors: string[];
  responseError: IResponseError;
  httpStatusCode: number;
  succeeded: boolean;
}

interface IResponseError {
  id: number;
  name: string;
}

const tokenKey = "jwt";
const refreshTokenKey = "refresh";

export { 
  baseUrl,
  identityPaths, 
  coursesPaths, 
  StatusCodes, 
  IApiRequest,
  IApiResponse,
  IResponseError,
  tokenKey,
  refreshTokenKey
}
