import { callApi } from "../callApi";
import { 
  coursesPaths, 
  IApiRequest, 
  IApiResponse
} from "../common";

interface IGetUserCourses {
  courses: ICourse[];
}

interface ICourse {
  id: number;
  name: string;
  xp: number;
  description: string;
  passPercentTreshold: number;
  questions: IQuestion[];
}

interface IQuestion {
  id: number;
  content: string;
  answers: IAnswear[];
}

interface IAnswear {
  id: number;
  content: string;
  isCorrect: boolean;
}

async function getUserCourses(): Promise<IApiResponse<IGetUserCourses>> {
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

export { 
  IGetUserCourses,
  ICourse,
  IQuestion, 
  IAnswear, 
  getUserCourses 
};
