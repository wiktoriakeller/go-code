import { callApi } from "../callApi";
import { 
  coursesPaths, 
  IApiRequest, 
  IApiResponse
} from "../common";

export interface IGetAllCoursesInfosResponse {
  courses: ICourseInfo[];
}

export interface ICourseInfo {
  id: number;
  name: string;
  xp: number;
  description: string;
  passPercentTreshold: number;
  isUserSignedUp: boolean;
}

export async function getAllCoursesInfos(): Promise<IApiResponse<IGetAllCoursesInfosResponse>> {
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
