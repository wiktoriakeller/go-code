import { BASE_API_URL } from "@env";

export const baseUrl = BASE_API_URL;

export const identityPaths = {
  signUp: "users/",
  signIn: "users/tokens",
  refreshToken: "users/refresh-tokens",
};

export const coursesPaths = {
  getAll: "courses",
  getUserCourses: "courses/users",
  signUp: "courses/users",
};

export const formPaths = {
  sendCourseForm: "forms/",
};

export enum StatusCodes {
  BadRequest = 400,
  Unauthorized = 401,
  Forbidden = 403,
  NotFound = 404,
  InternalServerError = 500,
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
