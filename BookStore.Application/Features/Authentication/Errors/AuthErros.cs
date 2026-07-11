namespace BookStore.Application.Features.Authentication.Errors;

public static class AuthError
{
    public static Error InvalidCredentials => new Error("Account.InvalidCredentials"
       , "Invalid Email Or Password", ErrorType.Unauthorized);
    public static Error AccountDisabled => new Error("Account.Disabled"
        , "Account has been disabled,please contact the admin", ErrorType.Unauthorized);

    public static Error InvalidEmailConfirmationCode => new Error("Account.InvalidEmailConfirmationCode"
        , "Invalid Email Confirmation Code", ErrorType.Unauthorized);

    public static Error InvalidForgetPasswordCode => new Error("Account.InvalidForgetPasswordCode"
        , "Invalid Forget PasswordCode Code", ErrorType.Unauthorized);

    public static Error EmailAlreadyConfirmed => new Error("Account.EmailAlreadyConfirmed"
        , "Email Is Aleady confirmed for this Account", ErrorType.Validation);

    public static Error InvalidTokens => new Error("Account.InvalidTokens"
        , "Invalid Tokens", ErrorType.Validation);

    public static Error DuplicatedEmail => new Error("Account.DublicatedEmail"
        , "Email already has been taken", ErrorType.Conflict);

    public static Error AccountLockedOut => new Error("Account.LockedOut"
        , "You have enterd a wrong password so many times please try again later", ErrorType.Unauthorized);

    public static Error EmailNotConfirmed => new Error("Account.EmailNotConfirmed"
        , "Email Has not been confirmed", ErrorType.Unauthorized);
}
