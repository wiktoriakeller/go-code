import { callApi } from "../callApi";
import { 
  identityPaths, 
  ApiRequest, 
  ApiResponse, 
} from "../common";

interface SignUpResponse {
  id: string;
}

interface SignUpRequest {
  email: string;
  password: string;
  username: string;
}

async function signUp(params: SignUpRequest): Promise<ApiResponse<SignUpResponse>> {
  const request: ApiRequest<SignUpRequest> = {
    url: identityPaths.signUp,
    method: "POST",
    data: params,
  };

  const response = await callApi<SignUpRequest, SignUpResponse>(request);
  
  if(response.succeeded) {
    return response;
  }

  throw response;
}

export { signUp, SignUpRequest, SignUpResponse };
