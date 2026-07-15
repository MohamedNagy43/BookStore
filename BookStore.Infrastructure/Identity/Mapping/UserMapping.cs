namespace BookStore.Infrastructure.Identity.Mapping;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email.Substring(0, src.Email.IndexOf('@')));

        config.NewConfig<CreateUserRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email.Substring(0, src.Email.IndexOf('@')));

        config.NewConfig<UpdateUserRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email.Substring(0, src.Email.IndexOf('@')))
            .Map(dest => dest.NormalizedUserName, src => src.Email.Substring(0, src.Email.IndexOf('@')).ToUpper());
    }
}
