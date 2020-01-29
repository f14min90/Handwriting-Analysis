using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

public partial class Service_pages_SetCookie : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.ContentLength > 0)
        {
            try
            {
                byte[] message = Request.BinaryRead(Request.ContentLength);
                string json = Encoding.UTF8.GetString(message);
                User user = JsonConvert.DeserializeObject<User>(json);

                string cookie_value = user.Cookie;
                string email = Security.Check_cookie(cookie_value);

                ServerResponse response = new ServerResponse();
                if (email == null)
                    response.type = ResponseType.ERROR;
                else
                {
                    string cookie_hash = Security.Encrypt(cookie_value);
                    string expression = "update users set Checked=1 where CookieHash=@cookie_hash";
                    Service.Exec_command(expression, "@cookie_hash", cookie_hash);

                    HttpCookie cookie = new HttpCookie("auth", cookie_value);
                    Response.Cookies.Add(cookie);

                    response.type = ResponseType.OK;
                }

                string json_response = JsonConvert.SerializeObject(response);
                Response.Write(json_response);
            }
            catch (Exception ex)
            {
                ServerResponse response = new ServerResponse() { type = ResponseType.ERROR, e = ex };

                string json_response = JsonConvert.SerializeObject(response);
                Response.Write(json_response);
            }
        }   
    }
}