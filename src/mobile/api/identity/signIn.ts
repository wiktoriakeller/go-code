import { callApi } from "../callApi";
import { storeData } from "../storage";
import { 
  identityPaths, 
  ApiRequest, 
  ApiResponse, 
  tokenKey,
  refreshTokenKey
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
    url: identityPaths.signIn,
    method: "POST",
    data: params,
  };

  const response = await callApi<SignInRequest, SignInResponse>(request);

  if(response.succeeded && response.data) {
    await storeData(tokenKey, response.data.token);
    await storeData(refreshTokenKey, response.data.refreshToken);
  }
  return response;
}

export { signIn, SignInRequest, SignInResponse };
