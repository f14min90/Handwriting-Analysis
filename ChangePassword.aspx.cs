using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text;

public partial class ChangePassword : System.Web.UI.Page
{
    class User
    {
        public string Login { get; }
        public string Old_Password { get; }
        public string New_Password { get; }

        public User(string login, string old_password, string new_password)
        {
            Login = login;
            Old_Password = old_password;
            New_Password = new_password;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.ContentLength > 0)
        {
            byte[] data = Request.BinaryRead(Request.ContentLength);
            string s = Encoding.ASCII.GetString(data);
            User user = JsonConvert.DeserializeObject<User>(s);

            Response.Write(user.Login + " " + user.Old_Password + " " + user.New_Password);
        }
    }
}