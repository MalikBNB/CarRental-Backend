namespace CarRental.Configuration.Messages;

public static class ErrorMessages
{
    public static class Generic
    {
        public static string UnableToProcess = "Unable to process request.";
        public static string SomethingWentWrong = "Something went wrong, please try later.";
        public static string BadRequest = "Bad request.";
        public static string InvalidPayload = "Invalid payload.";
        public static string InvalidRequest = "Invalid request.";
        public static string ObjectNotFound = "Object not found.";
    }

    public static class Profile
    {
        public static string UserNotFound = "User not found!";
    }

    public static class User
    {
        public static string UserNotFound = "User not found!.";
        public static string UserAlreadyExist = "User already exists.";
    }

    public static class Register
    {
        public static string EmailInUse = "Email in use.";
        public static string UsernameInUse = "Username in use.";

    }

    public static class Login
    {
        public static string InvalidAuthentication = "Invalid authentication request.";

    }
    
    public static class Role
    {
        public static string InvalidUserIdOrRolr = "Invalid user ID or Role.";
        public static string AlreadyAssigned = "User already assigned to this role";

    }
}
