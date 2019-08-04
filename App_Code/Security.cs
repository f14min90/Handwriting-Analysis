using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;

public class Security
{
    public static string Encrypt(string content)
    {
        SHA256 sha = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        byte[] hash = sha.ComputeHash(bytes);

        string result = "";
        foreach (byte b in hash)
            result += b.ToString("x2");

        return result;
    }

    public static string Check_cookie(string cookie_value)
    {
        string cookie_hash = Encrypt(cookie_value);
        string expression = "select Email from users where CookieHash=@cookie_hash";

        using (SqlConnection connection = new SqlConnection(Service.connectionString))
        {
            SqlCommand command = new SqlCommand(expression, connection);
            command.Parameters.AddWithValue("@cookie_hash", cookie_hash);

            connection.Open();
            return command.ExecuteScalar()?.ToString();
        }
    }
}