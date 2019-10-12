namespace HMS.Service.Auth.Interfaces
{
    public interface ISecurityService
    {
        string GetSha256Hash(string input);
    }
}