import { identityPaths } from "../common";

export interface ISignUpResponse {
  id: string;
}

export interface ISignUpRequest {
  email: string;
  password: string;
  username: string;
}

export const getSignUpRequest = (params: ISignUpRequest) => {
  return {
    url: identityPaths.signUp,
    method: "POST",
    data: params
  };
}
