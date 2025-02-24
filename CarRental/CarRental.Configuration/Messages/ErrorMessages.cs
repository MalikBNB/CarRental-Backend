namespace CarRental.Configuration.Messages;

public static class ErrorMessages
{
    public static class Generic
    {
        public static string UnableToProcess = "Unable to process request.";
        public static string SomethingWentWrong = "Something went wrong!";
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
        public static string InvalidUsernameOrPassword = "Invalid username or password.";
        public static string InvalidToken = "Invalid token!";
        public static string TokenIsRequired = "Token is required!";

    }
    
    public static class Role
    {
        public static string InvalidUserIdOrRolr = "Invalid user ID or Role.";
        public static string AlreadyAssigned = "User already assigned to this role";

    }

    public static class CarCategory
    {
        public static string CategoryAlreadyExists = "Category already exists!";
        public static string CategoryNotExist = "Category does not exist!";
    }

    public static class Vehicle
    {
        public static string VehicleAlreadyExists = "Vehicle already exists!";
        public static string VehicleNotExist = "Vehicle does not exist!";
    }
}
