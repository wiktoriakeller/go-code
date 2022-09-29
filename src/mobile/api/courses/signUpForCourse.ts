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
    method: "PATCH"
  };

  console.log(request);
  const response = await callApi<any, ISignUpForCourseResponse>(request);

  if(response.succeeded && response.data) {
    return response;
  }

  throw response;
}
