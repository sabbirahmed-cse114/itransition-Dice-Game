using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using System.Text;
using Task3;

public class FairRandomGenerator : IFairRandomGenerator
{
    private string _key = "";
    private int _value;
    private string _hmac = "";

    public string generateRandomNumber(int value)
    {
        _value = RandomNumberGenerator.GetInt32(0, value);

        byte[] keyBytes = new byte[32];
        RandomNumberGenerator.Fill(keyBytes);

        _key = BitConverter.ToString(keyBytes).Replace("-", "").ToLower();
        _hmac = CalculateHMAC(keyBytes, Encoding.UTF8.GetBytes(_value.ToString()));

        return _hmac.ToUpper();
    }

    public (string key, int value) Reveal()
    {
        return (_key.ToUpper(), _value);
    }

    public bool Verify(string hmac, string key, int value)
    {
        byte[] keyBytes = Convert.FromHexString(key);
        string computed = CalculateHMAC(keyBytes, Encoding.UTF8.GetBytes(value.ToString()));
        return string.Equals(hmac, computed, StringComparison.OrdinalIgnoreCase);
    }

    private string CalculateHMAC(byte[] key, byte[] message)
    {
        var hmacSha3 = new HMac(new Sha3Digest(256));
        hmacSha3.Init(new KeyParameter(key));
        hmacSha3.BlockUpdate(message, 0, message.Length);
        byte[] result = new byte[hmacSha3.GetMacSize()];
        hmacSha3.DoFinal(result, 0);
        return BitConverter.ToString(result).Replace("-", "").ToLower();
    }
}