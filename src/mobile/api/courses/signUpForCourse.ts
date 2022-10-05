import { callApi } from "../callApi";
import { 
  coursesPaths, 
  IApiRequest, 
  IApiResponse
} from "../common";

export interface ISignUpForCourseRequest {
  id: number;
}

export interface ISignUpForCourseResponse {
  id: number;
}

export const signUpForCourseRequest = (params: ISignUpForCourseRequest) => {
  return {
    url: `${coursesPaths.signUp}/${params.id}`,
    method: "PATCH",
    data: params
  };
}
