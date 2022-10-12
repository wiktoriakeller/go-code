import { useState } from "react";
import { callApi } from "./callApi";
import { IApiRequest } from "./common";

export const useQuery = <TRequest, TResponse>() => {
  const [data, setData] = useState<TResponse>();
  const [isLoading, setIsLoading] = useState(false);
  const [isError, setIsError] = useState(false);
  const [isSuccess, setIsSuccess] = useState(false);

  const fetchData = async (request: IApiRequest<TRequest>) => {    
    setIsLoading(true);
    setIsError(false);
    setIsSuccess(false);

    const response = await callApi<TRequest, TResponse>(request);
    
    setIsLoading(false);
    setData(response.data);

    if(response.succeeded) {
      setIsSuccess(true);
      return response;
    }
  
    setIsError(true);
    throw response;
  }

  return {
    fetchData,
    data,
    isLoading,
    isError,
    isSuccess
  }
}
