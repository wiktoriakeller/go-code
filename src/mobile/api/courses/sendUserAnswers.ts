import { callApi } from "../callApi";
import { formPaths, IApiRequest, IApiResponse } from "../common";

export interface ICourseFormRequest {
  courseId: number;
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

export async function sendUserAnswers(params: ICourseFormRequest): Promise<IApiResponse<ICourseFormResponse>> {
  const request: IApiRequest<ICourseFormRequest> = {
    url: formPaths.sendCourseForm,
    method: "POST",
    data: params
  };

  const response = await callApi<any, ICourseFormResponse>(request);

  if(response.succeeded && response.data) {
    return response;
  }

  throw response;
}
