using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text;

public partial class DownloadImage : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.ContentLength > 0)
        {
            byte[] data = Request.BinaryRead(Request.ContentLength);
            string s = Encoding.ASCII.GetString(data);
            User user = JsonConvert.DeserializeObject<User>(s);

            Response.Write(user.Login + " " + user.Password + " " + user.ImageName);
        }
    }
}