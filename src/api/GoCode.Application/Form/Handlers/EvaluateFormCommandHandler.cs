using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Form.Commands;
using GoCode.Application.Form.Responses;
using GoCode.Domain.Entities;

namespace GoCode.Application.Form.Handlers
{
    public class EvaluateFormCommandHandler : IHandlerWrapper<EvaluateFormCommand, EvaluateFormResponse>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IUserCoursesRepository _userCoursesRepository;

        public EvaluateFormCommandHandler(
            ICurrentUserService currentUserService,
            ICoursesRepository coursesRepository,
            IUserCoursesRepository userCoursesRepository)
        {
            _currentUser = currentUserService;
            _coursesRepository = coursesRepository;
            _userCoursesRepository = userCoursesRepository;
        }

        public async Task<Response<EvaluateFormResponse>> Handle(EvaluateFormCommand request, CancellationToken cancellationToken)
        {
            var questionsDictionary = new Dictionary<int, Question>();

            var course = await _coursesRepository.FirstOrDefaultWithAllAsync(x => x.Id == request.CourseId);
            if (course == null)
            {
                return ResponseResult.NotFound<EvaluateFormResponse>(string.Format(ErrorMessages.NotFound, "Course"));
            }

            var currentUserId = _currentUser.User.Id;
            var userCourse = course.UserCourses.FirstOrDefault(x => x.UserId == currentUserId);
            if (userCourse == null)
            {
                return ResponseResult.ValidationError<EvaluateFormResponse>(ErrorMessages.Form.UserIsNotSignedToThisCourse);
            }

            foreach (var question in course.Questions)
            {
                var requestQuestion = request.FormAnswers.FirstOrDefault(x => x.QuestionId == question.Id);
                if (requestQuestion == null)
                {
                    return ResponseResult.ValidationError<EvaluateFormResponse>(ErrorMessages.Form.FormDoesNotHaveAllQuestions);
                }

                questionsDictionary.Add(question.Id, question);
            }

            var correctAnswers = 0;
            var results = new List<FormAnswearResponseDto>();

            foreach (var response in request.FormAnswers)
            {
                if (!questionsDictionary.ContainsKey(response.QuestionId))
                {
                    return ResponseResult.NotFound<EvaluateFormResponse>(string.Format(ErrorMessages.NotFound, "Question"));
                }

                var question = questionsDictionary[response.QuestionId];
                var correctAnswear = question.Answers.First(x => x.IsCorrect);
                var responseResult = new FormAnswearResponseDto
                {
                    QuestionId = question.Id,
                    IsCorrect = false
                };

                if (correctAnswear.Id == response.AnswerId)
                {
                    correctAnswers++;
                    responseResult.IsCorrect = true;
                }

                results.Add(responseResult);
            }

            var gainedXP = 0;
            var percantage = (correctAnswers / results.Count) * 100.0f;
            var levelUp = false;

            if (!userCourse.UserGainedXP && percantage >= course.PassPercentTreshold)
            {
                gainedXP = course.XP;
                levelUp = await _currentUser.AddXP(gainedXP, currentUserId);
                userCourse.UserGainedXP = true;
                await _userCoursesRepository.UpdateAsync(userCourse);
            }

            var evaluationResponse = new EvaluateFormResponse
            {
                GainedXP = gainedXP,
                Passed = percantage >= course.PassPercentTreshold,
                LevelUp = levelUp,
                ResponsesEvaluation = results
            };

            return ResponseResult.Ok(evaluationResponse);
        }
    }
}
