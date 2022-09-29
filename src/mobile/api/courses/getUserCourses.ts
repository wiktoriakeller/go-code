import { callApi } from "../callApi";
import { 
  coursesPaths, 
  IApiRequest, 
  IApiResponse
} from "../common";

export interface IGetUserCourses {
  courses: ICourse[];
}

export interface ICourse {
  id: number;
  name: string;
  xp: number;
  description: string;
  passPercentTreshold: number;
  questions: IQuestion[];
}

export interface IQuestion {
  id: number;
  content: string;
  answers: IAnswear[];
}

export interface IAnswear {
  id: number;
  content: string;
  isCorrect: boolean;
}

export async function getUserCourses(): Promise<IApiResponse<IGetUserCourses>> {
  const request: IApiRequest<any> = {
    url: coursesPaths.getUserCourses,
    method: "GET"
  };

  const response = await callApi<any, IGetUserCourses>(request);

  if(response.succeeded && response.data) {
    return response;
  }

  throw response;
}
