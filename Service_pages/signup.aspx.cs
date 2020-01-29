using System;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlClient;

public partial class Service_pages_signup : System.Web.UI.Page
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

                string email = user.Email;
                string cookie_value = Guid.NewGuid().ToString().Replace("-", "");
                string cookie_hash = Security.Encrypt(cookie_value);

                string expression = "if exists(select * from users where Email=@email) " +
                                        "update users set CookieHash=@cookie_hash where Email=@email " +
                                    "else " +
                                        "insert users values(@email, @cookie_hash, null, 0)";
                Service.Exec_command(expression, "@email", email, "@cookie_hash", cookie_hash);
                Service.Send_mail(email, cookie_value);

                ServerResponse response = new ServerResponse() { type = ResponseType.OK };
                string json_response = JsonConvert.SerializeObject(response);
                Response.Write(json_response);
            }
            catch(Exception ex)
            {
                ServerResponse response = new ServerResponse() { type = ResponseType.ERROR, e = ex };

                string json_response = JsonConvert.SerializeObject(response);
                Response.Write(json_response);
            }
        }
    }
}