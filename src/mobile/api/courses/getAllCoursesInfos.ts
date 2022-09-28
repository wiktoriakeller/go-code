import { callApi } from "../callApi";
import { 
  coursesPaths, 
  IApiRequest, 
  IApiResponse
} from "../common";

interface IGetAllCoursesInfosResponse {
  token: string;
  refreshToken: string;
  courses: ICourseInfo[];
}

interface ICourseInfo {
  id: number;
  name: string;
  xp: number;
  description: string;
  passPercentTreshold: number;
  isUserSignedUp: boolean;
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

async function getAllCoursesInfos(): Promise<IApiResponse<IGetAllCoursesInfosResponse>> {
  const request: IApiRequest<any> = {
    url: coursesPaths.getAll,
    method: "GET"
  };

  const response = await callApi<any, IGetAllCoursesInfosResponse>(request);

  if(response.succeeded && response.data) {
    return response;
  }

  throw response;
}

export { 
  IGetAllCoursesInfosResponse, 
  ICourseInfo, 
  IQuestion, 
  IAnswear, 
  getAllCoursesInfos 
};
