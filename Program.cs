using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace ConsoleApp2
{
    class User
    {
        public string Login { get; }
        public string Password { get; }
        public string ImageName { get; }

        public User(string login, string password, string imagename)
        {
            Login = login;
            Password = password;
            ImageName = imagename;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            User a = new User("artem", "levshin", "something");
            string s = JsonConvert.SerializeObject(a);
            Console.WriteLine(s);
            byte[] data = Encoding.ASCII.GetBytes(s);        

            string uri = "http://127.0.0.1/DeleteImage.aspx";
            WebRequest request = WebRequest.Create(uri);

            request.Method = "POST";
            request.ContentType = "multipart/form-data";
            request.ContentLength = data.Length;

            request.GetRequestStream().Write(data, 0, data.Length);

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string answer = reader.ReadToEnd();

            Console.WriteLine(answer);
        }        
    }
}