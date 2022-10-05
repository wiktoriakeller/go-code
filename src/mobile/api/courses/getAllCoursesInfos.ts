import { 
  coursesPaths
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

export const getAllCoursesRequest = () => {
  return {
    url: coursesPaths.getAll,
    method: "GET"
  };
}
