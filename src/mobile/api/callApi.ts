import axios, { AxiosError } from "axios";
import { getData } from "./storage";
import {  
  StatusCodes, 
  ApiRequest,
  ApiResponse
} from "./common";

const requestInterceptor = axios.interceptors.request.use(
  async (config) => {
    const jwt = await getData("jwt");

    if(!config.headers) {
      config.headers = {}
    }

    if(jwt) {
      config.headers.Authorization = `${jwt}`;
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

async function callApi<TRequest, TResponse> (params: ApiRequest<TRequest>): Promise<ApiResponse<TResponse>> {
  let response: ApiResponse<TResponse>;

  try {
    const { data } = await axios.request(params);
    response = data;
  }
  catch(error){
    const err = error as AxiosError;
    response = err.response?.data as ApiResponse<TResponse>;
  }

  return response;
}

export { callApi, responseInterceptor, requestInterceptor };
