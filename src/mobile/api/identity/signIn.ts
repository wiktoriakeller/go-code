import { callApi } from "../callApi";
import { storeData } from "../storage";
import { 
  baseUrl,
  identityPaths, 
  ApiRequest, 
  ApiResponse, 
} from "../common";

interface SignInResponse {
  token: string;
  refreshToken: string;
}

interface SignInRequest {
  email: string;
  password: string;
}

async function signIn(params: SignInRequest): Promise<ApiResponse<SignInResponse>> {
  const request: ApiRequest<SignInRequest> = {
    url: baseUrl + identityPaths.login,
    method: "POST",
    data: params,
  };

  const response = await callApi<SignInRequest, SignInResponse>(request);

  if(response.succeeded && response.data) {
    await storeData("jwt", response.data.token);
    await storeData("refresh", response.data.refreshToken);
  }

  return response;
}

export { signIn, SignInRequest, SignInResponse };
