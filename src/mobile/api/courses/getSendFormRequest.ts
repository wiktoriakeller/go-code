import { formPaths } from "../common";

export interface ICourseFormRequest {
  courseId?: number;
  formAnswers: IFormAnswear[];
}

export interface IFormAnswear {
  questionId: number;
  answearId: number;
}

export interface IQuestionEvaluation {
  questionId: number;
  isCorrect: boolean;
}

export interface ICourseFormResponse {
  gainedXP: number;
  passed: boolean;
  levelUp: boolean;
  responsesEvaluation: IQuestionEvaluation[];
}

export const getSendFormRequest = (params: ICourseFormRequest) => {
  return {
    url: formPaths.sendCourseForm,
    method: "POST",
    data: params
  };
}
