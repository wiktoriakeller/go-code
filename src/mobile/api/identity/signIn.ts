import { callApi } from "../callApi";
import { storeData } from "../storage";
import { 
  identityPaths, 
  IApiRequest, 
  IApiResponse, 
  tokenKey,
  refreshTokenKey
} from "../common";

interface ISignInResponse {
  token: string;
  refreshToken: string;
}

interface ISignInRequest {
  email: string;
  password: string;
}

async function signIn(params: ISignInRequest): Promise<IApiResponse<ISignInResponse>> {
  const request: IApiRequest<ISignInRequest> = {
    url: identityPaths.signIn,
    method: "POST",
    data: params
  };

  const response = await callApi<ISignInRequest, ISignInResponse>(request);

  if(response.succeeded && response.data) {
    await storeData(tokenKey, response.data.token);
    await storeData(refreshTokenKey, response.data.refreshToken);
    return response;
  }

  throw response;
}

export { signIn, ISignInRequest, ISignInResponse };
