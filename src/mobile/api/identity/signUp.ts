import { callApi } from "../callApi";
import { 
  identityPaths, 
  IApiRequest, 
  IApiResponse, 
} from "../common";

interface ISignUpResponse {
  id: string;
}

interface ISignUpRequest {
  email: string;
  password: string;
  username: string;
}

async function signUp(params: ISignUpRequest): Promise<IApiResponse<ISignUpResponse>> {
  const request: IApiRequest<ISignUpRequest> = {
    url: identityPaths.signUp,
    method: "POST",
    data: params
  };

  const response = await callApi<ISignUpRequest, ISignUpResponse>(request);
  
  if(response.succeeded) {
    return response;
  }

  throw response;
}

export { signUp, ISignUpRequest, ISignUpResponse };
