export interface ICourseForm {
  courseId: number;
  answers: IFormAnswear[];
}

export interface IFormAnswear {
  questionId: number;
  answearId: number;
}
