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

export async function signUpForCourse(params: ISignUpForCourseRequest): Promise<IApiResponse<ISignUpForCourseResponse>> {
  const request: IApiRequest<any> = {
    url: `${coursesPaths.signUp}/${params.id}`,
    method: "PATCH",
    data: params
  };

  const response = await callApi<ISignUpForCourseRequest, ISignUpForCourseResponse>(request);

  if(response.succeeded && response.data) {
    return response;
  }

  throw response;
}
