
namespace Task3
{
    public interface IFairRandomGenerator
    {
        string generateRandomNumber(int value);
        (string key, int value) Reveal();
        bool Verify(string hmac, string key, int value);
    }
}