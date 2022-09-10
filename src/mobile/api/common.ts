const baseUrl = "http://192.168.1.74:5219/api/v1/";
  
const identityPaths = {
  register: "identity/register",
  login: "identity/login",
  refreshToken: "identity/refresh"
}

const coursesPaths = {
  getAll: "courses",
  getAllForUser: "courses/user",
  signUp: "courses/signup/"
}

enum StatusCodes {
  BadRequest = 400,
  Unauthorized = 401,
  Forbidden = 403,
  NotFound = 404,
  InternalServerError = 500
}

interface ApiRequest<T> {
  url: string;
  method: string;
  data?: T;
  headers?: any;
}

interface ApiResponse<T> {
  data?: T;
  errors: string[];
  responseError: ResponseError;
  httpStatusCode: number;
  succeeded: boolean;
}

interface ResponseError {
  id: number;
  name: string;
}

export { 
  baseUrl, 
  identityPaths, 
  coursesPaths, 
  StatusCodes, 
  ApiRequest, 
  ApiResponse, 
  ResponseError 
}
