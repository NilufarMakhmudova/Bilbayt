namespace Bilbayt.Core.Interfaces.Password
{
    public interface IPasswordManager
    {
        string GetHashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}
