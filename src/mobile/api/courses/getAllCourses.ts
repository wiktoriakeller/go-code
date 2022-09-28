import { callApi } from "../callApi";
import { 
  coursesPaths, 
  IApiRequest, 
  IApiResponse
} from "../common";

interface IGetAllCoursesResponse {
  token: string;
  refreshToken: string;
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

async function getAllCourses(): Promise<IApiResponse<IGetAllCoursesResponse>> {
  const request: IApiRequest<any> = {
    url: coursesPaths.getAll,
    method: "GET"
  };

  const response = await callApi<any, IGetAllCoursesResponse>(request);

  if(response.succeeded && response.data) {
    return response;
  }

  throw response;
}

export { 
  IGetAllCoursesResponse, 
  ICourse, 
  IQuestion, 
  IAnswear, 
  getAllCourses 
};
