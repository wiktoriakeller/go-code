import { callApi } from "../callApi";
import { IApiRequest, IApiResponse, identityPaths, refreshTokenKey, tokenKey } from "../common";
import { getData, storeData } from "../storage";

export interface IRefreshTokenRequest {
  token: string;
  refreshToken: string;
}

export interface IRefreshTokenResponse {
  token: string;
  refreshToken: string;
}

export async function refreshToken(): Promise<IApiResponse<IRefreshTokenResponse>> {
  const jwt = await getData(tokenKey) ?? "";
  const refresh = await getData(refreshTokenKey) ?? "";

  const request: IApiRequest<IRefreshTokenRequest> = {
    url: identityPaths.refreshToken,
    method: "POST",
    data: {
      token: jwt,
      refreshToken: refresh
    }
  };

  const response = await callApi<IRefreshTokenRequest, IRefreshTokenResponse>(request);

  if(response.succeeded && response.data) {
    await storeData(tokenKey, response.data.token);
    await storeData(refreshTokenKey, response.data.refreshToken);
    return response;
  }

  throw response;
}