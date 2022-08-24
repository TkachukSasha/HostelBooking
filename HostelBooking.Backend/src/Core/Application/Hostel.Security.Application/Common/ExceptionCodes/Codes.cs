namespace Hostel.Security.Application.Common.ExceptionCodes
{
    public static class Codes
    {
        public static string EmailIsNotFound => "email_not_found";
        public static string PasswordIsNotCorrect => "incorrect_password";
        public static string TokenIsNotCorrect => "incorrect_token";
        public static string UserNotFound => "user_not_found";
    }
}
