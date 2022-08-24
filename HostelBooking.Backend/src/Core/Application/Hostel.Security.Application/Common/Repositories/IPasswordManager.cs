namespace Hostel.Security.Application.Common.Repositories
{
    public interface IPasswordManager
    {
        string Secure(string password);
        bool Validate(string password, string passwordHash);
    }
}
