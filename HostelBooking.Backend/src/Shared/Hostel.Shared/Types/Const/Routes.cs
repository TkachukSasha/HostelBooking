namespace Hostel.Shared.Types.Const
{
    public static class Routes
    {
        public const string Login = "/login";
        public const string Register = "/register";
        public const string Refresh = "/refresh";
        public const string Logout = "/logout";

        public const string GetRooms = "/rooms";
        public const string GetRoomById = "/rooms/{id}";
        public const string CreateRoom = "/rooms";
        public const string UpdateRoom = "/rooms";
        public const string DeleteRoom = "/rooms/{id}";

        public const string GetCompanies = "/companies";
        public const string GetCompanyById = "/companies/{id}";
        public const string CreateCompany = "/companies";
        public const string UpdateCompany = "/companies";
        public const string DeleteCompany = "/companies/{id}";
    }
}
