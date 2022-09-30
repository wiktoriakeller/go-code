export const baseUrl = "http://192.168.1.74:5219/api/v1/";
  
export const identityPaths = {
  signUp: "identity/register",
  signIn: "identity/login",
  refreshToken: "identity/refresh"
}

export const coursesPaths = {
  getAll: "courses",
  getUserCourses: "courses/user",
  signUp: "courses/signup"
}

export const formPaths = {
  sendCourseForm: "form/evaluate"
}

export enum StatusCodes {
  BadRequest = 400,
  Unauthorized = 401,
  Forbidden = 403,
  NotFound = 404,
  InternalServerError = 500
}

export interface IApiRequest<T> {
  url: string;
  method: string;
  data?: T;
  headers?: any;
}

export interface IApiResponse<T> {
  data?: T;
  errors: string[];
  responseError: IResponseError;
  httpStatusCode: number;
  succeeded: boolean;
}

export interface IResponseError {
  id: number;
  name: string;
}

export const tokenKey = "jwt";
export const refreshTokenKey = "refresh";
