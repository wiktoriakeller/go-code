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
        }
    }
}
