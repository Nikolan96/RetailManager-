namespace RMDesktopUI.Helpers
{
    public interface IPasswordEncryptor
    {
        string Decrypt(string hashedPassword);
        string Encrypt(string password);
    }
}