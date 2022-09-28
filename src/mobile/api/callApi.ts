import axios, { Axios, AxiosError } from "axios";
import { getData } from "./storage";
import {  
  baseUrl,
  StatusCodes, 
  IApiRequest,
  IApiResponse,
  tokenKey
} from "./common";

axios.defaults.baseURL = baseUrl;

const requestInterceptor = axios.interceptors.request.use(
  async (config) => {
    const jwt = await getData(tokenKey);
    if(jwt && config.headers) {
      config.headers.Authorization = `bearer ${jwt}`;
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
)

const responseInterceptor = axios.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    const originalRequest = error.config;
    if(error.response.status === StatusCodes.Unauthorized && !originalRequest._retry) {
      originalRequest._retry = true;
    }
    return Promise.reject(error);
  }
)

async function callApi<TRequest, TResponse> (params: IApiRequest<TRequest>): Promise<IApiResponse<TResponse>> {
  let response: IApiResponse<TResponse>;

  try {
    const { data } = await axios.request(params);
    response = data;
  }
  catch(error) {
    if(axios.isAxiosError(error)) {
      const err = error as AxiosError;
      response = err.response?.data as IApiResponse<TResponse>;
    }
    else {
      response = {
        data: undefined,
        errors: ["Something went wrong..."],
        responseError: {
          id: 6,
          name: "Unknown"
        },
        httpStatusCode: 500,
        succeeded: false
      }
    }
  }
  
  console.log(response);
  return response;
}

export { callApi, responseInterceptor, requestInterceptor };
