using System.Text;

namespace ProjectService.Utilities;

public static class JwtSecretDecoder
{
    public static byte[] Decode(string secret)
    {
        var normalizedSecret = secret.Trim();

        if (normalizedSecret.Length % 2 == 0 && normalizedSecret.All(Uri.IsHexDigit))
        {
            return Convert.FromHexString(normalizedSecret);
        }

        try
        {
            return Convert.FromBase64String(normalizedSecret);
        }
        catch (FormatException)
        {
            return Encoding.UTF8.GetBytes(normalizedSecret);
        }
    }
}
