import { callApi } from "../callApi";
import { 
  coursesPaths, 
  IApiRequest, 
  IApiResponse
} from "../common";

interface ISignUpForCourseRequest {
  id: number;
}

interface ISignUpForCourseResponse {
  id: number;
}

async function signUpForCourse(params: ISignUpForCourseRequest): Promise<IApiResponse<ISignUpForCourseResponse>> {
  const request: IApiRequest<any> = {
    url: `${coursesPaths.signUp}\\${params.id}`,
    method: "POST"
  };

  const response = await callApi<any, ISignUpForCourseResponse>(request);

  if(response.succeeded && response.data) {
    return response;
  }

  throw response;
}

export { 
  ISignUpForCourseRequest, 
  ISignUpForCourseResponse, 
  signUpForCourse 
};
