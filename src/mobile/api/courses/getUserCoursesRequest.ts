import { coursesPaths } from "../common";

export interface IGetUserCourses {
  courses: IUserCourse[];
}

export interface IUserCourse {
  id: number;
  name: string;
  xp: number;
  description: string;
  passPercentTreshold: number;
  userPassed: boolean;
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

export const getUserCoursesRequest = () => {
  return {
    url: coursesPaths.getUserCourses,
    method: "GET"
  };
}
