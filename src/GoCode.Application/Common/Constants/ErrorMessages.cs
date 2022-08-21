namespace GoCode.Application.Common.Constants
{
    public static class ErrorMessages
    {
        public const string NotFound = "{0} was not found";

        public static class Identity
        {
            public const string UnauthorizedUser = "User is unauthorized";
            public const string UserNotFound = "User not found";
            public const string InvalidToken = "Token is invalid";
            public const string IncorrectCredentials = "Incorrect credentials";
            public const string UserExists = "User with that email already exists";
        }

        public static class Answear
        {
            public const string OnlyOneAnswarCanBeCorrect = "Only one answear can be correct";
            public const string AnswersMinimumCount = "At least two answears are required";
            public const string AnswersMustBeUnique = "Answers within a question should be unique";
        }

        public static class Question
        {
            public const string QuestionsMustBeUnique = "Questions within a course should be unique";
        }
    }
}
