namespace GoCode.Application.Common.Constants
{
    public static class ErrorMessages
    {
        public const string NotFound = "{0} was not found";

        public static class Identity
        {
            public const string UnauthorizedUser = "User is unauthorized to access that resource";
            public const string UserNotFound = "User not found";
            public const string InvalidToken = "Token is invalid";
            public const string IncorrectCredentials = "Incorrect credentials";
            public const string UserExists = "User with that email already exists";
        }

        public static class Answear
        {
            public const string OnlyOneAnswearCanBeCorrect = "Only one answear can be correct";
            public const string AnswersMinimumCount = "At least two answears are required";
            public const string AnswersMustBeUnique = "Answers within a question should be unique";
        }

        public static class Question
        {
            public const string QuestionsMustBeUnique = "Questions within a course should be unique";
        }

        public static class Course
        {
            public const string NameMustBeUnique = "Courses name should be unique";
        }

        public static class Form
        {
            public const string FormDoesNotHaveAllQuestions = "Send form does not contain all questions from the course";
            public const string UserIsNotSignedToThisCourse = "User hasn't signed up for this course";
        }
    }
}
