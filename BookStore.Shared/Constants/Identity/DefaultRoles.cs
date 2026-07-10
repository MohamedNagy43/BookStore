namespace BookStore.Shared.Constants.Identity;

public static class DefaultRoles
{
    public static partial class Admin
    {
        public const string Id = "019f4b14-a55c-7327-b3f2-3875a5147ea4";
        public const string Name = nameof(Admin);
        public const string ConcurrencyStamp = "019f4b14-a55c-7327-b3f2-38762c00aca5";
        public const bool IsDefault = false;
    }
    public static partial class Customer
    {
        public const string Id = "019f4b14-a55c-7327-b3f2-3877b4e85640";
        public const string Name = nameof(Customer);
        public const string ConcurrencyStamp = "019f4b14-a55c-7327-b3f2-3878920d8c5a";
        public const bool IsDefault = true;
    }
}
