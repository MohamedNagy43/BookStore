namespace BookStore.Application.Features.User.Errors;

public record UserErrors
{
    public static Error UserNotFound => new Error("User.NotFound"
       , "No User Found with this key", ErrorType.NotFound);
    public static Error DuplicatedEmail => new Error("User.DublicatedEmail"
        , "Email already has been taken", ErrorType.Conflict);
    public static Error UserAlreadyUnlocked => new Error("User.AlreadyUnlocked"
       , "User is Already Unlocked , you already can sign in", ErrorType.Validation);
    public static Error InvalidEmailConfirmationCode => new Error("User.InvalidEmailConfirmationCode"
        , "Invalid Email Confirmation Code", ErrorType.Unauthorized);
    public static Error EmailAlreadyConfirmed => new Error("User.EmailAlreadyConfirmed"
        , "Email Is Aleady confirmed for this User", ErrorType.Validation);
    public static Error PasswordAlreadyExist => new Error("User.PasswordAlreadyExist"
        , "Password is exist please contact admin", ErrorType.Validation);
}
