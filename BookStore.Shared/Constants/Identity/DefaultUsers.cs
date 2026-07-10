namespace BookStore.Shared.Constants.Identity;

public static class DefaultUsers
{
    public static partial class Admin
    {
        public const string Id = "019f4b06-0a78-70e9-aee1-9e0861a695c0";
        public const string Email = "admin@Book-Store.com";
        public const string Password = "Admin@123";
        public const string FirstName = "Admin";
        public const string LastName = "Admin";
        public const string PasswordHashed = "AQAAAAIAAYagAAAAEP7bd0t9M0qOCsSmg6nspHIHx9rdLDt3/FlxL32oJguf3zkwbeC+OXKOT+j3vmP0Gg==";
        public const string SecurityStamp = "B8365D46F9A4446CAE8CDB60F93C7CB5";
        public const string ConcurrencyStamp = "019f4b06-0a78-70e9-aee1-9e09f7140ca7";
    }
    public static partial class Customer
    {
        public const string Id = "019f4b09-c8b9-74ef-9a6b-157523f014b6";
        public const string Email = "customer@Book-Store.com";
        public const string Password = "User@123";
        public const string FirstName = "Customer";
        public const string LastName = "Customer";
        public const string PasswordHashed = "AQAAAAIAAYagAAAAEKCMVljoHz4RkVOR105MOrrEE8Pr19oMvf/ESOUa5fAnEX25CFQO1+krR9XCGw6C3A==";
        public const string SecurityStamp = "3EE0EFBA7D364829B7732F9B2B4203C1";
        public const string ConcurrencyStamp = "019f4b09-c8b9-74ef-9a6b-15763275ac64";
    }
}
