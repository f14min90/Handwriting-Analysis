using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Drawing;
using System.Diagnostics;

public class Service
{
    public static string connectionString = ConfigurationManager.ConnectionStrings["Standart"].ConnectionString;

    public static void Exec_command(string expression, params string[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(expression, connection);
            for (int i = 0; i < parameters.Length; i += 2)
                command.Parameters.AddWithValue(parameters[i], parameters[i + 1] ?? "NULL");

            connection.Open();
            command.ExecuteNonQuery();

        }
    }

    public static void Send_mail(string to, string code)
    {
        using (MailMessage message = new MailMessage())
        using(SmtpClient smtp = new SmtpClient())
        {
            message.From = new MailAddress("hwanalysis@dsoft.ru");
            message.To.Add(new MailAddress(to));

            message.Subject = "Cookie code";
            message.Body = "Ваш cookie: " + code;

            smtp.Host = "smtp.yandex.ru";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("hwanalysis@dsoft.ru", "hw2202");

            smtp.Send(message);
        }        
    }

    public static List<UserImage> GetImages(string email)
    {
        List<UserImage> images = new List<UserImage>();
        string expression = "select * from photos where Email=@email";
   
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(expression, connection);
            command.Parameters.AddWithValue("@email", email);

            connection.Open();
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = reader["PhotoId"].ToString();
                    string path = reader["Path"].ToString();
                    string description = reader["Description"].ToString();

                    Image image = Image.FromFile(path);
                    MemoryStream stream = new MemoryStream();
                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                    byte[] image_byte = stream.ToArray();
                    UserImage user_image = new UserImage() { Id = id, Description = description, Image = image_byte};
                    images.Add(user_image);
                }
            }
        }

        return images;
    }

    public static double[] GetCenter(byte[] bytes)
    {
        MemoryStream stream = new MemoryStream(bytes);
        Image image = Image.FromStream(stream);

        string answer;
        string guid = Guid.NewGuid().ToString().Replace("-", "");
        string path = @"C:\HWSite\HWSite\Temp\" + guid + ".bmp";
        image.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
        
        using (Process process = new Process())
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = @"C:\Users\HIXIX\AppData\Local\Programs\Python\Python37\python.exe",
                Arguments = @"C:\HI19-master\p.py " + path
            };

            process.StartInfo = info;
            process.Start();

            StreamReader reader = process.StandardOutput;
            answer = reader.ReadToEnd();                       
        }
        File.Delete(path);

        answer = answer.Replace("]", "");
        answer = answer.Remove(0, answer.IndexOf('[') + 1);        
        double[] center = answer.Split(',').Select(x => double.Parse(x.Replace('.', ','))).ToArray();
        return center;
    }

    public static double GetDistance(double[] c1, double[] c2)
    {
        double distance = 0;

        for (int i = 0; i < 300; i++)
            distance += (c1[i] - c2[i]) * (c1[i] - c2[i]);

        return Math.Sqrt(distance);
    }
}