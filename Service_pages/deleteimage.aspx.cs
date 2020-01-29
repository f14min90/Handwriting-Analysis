using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IO;

public partial class Service_pages_deleteimage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.ContentLength > 0)
        {
            try
            {
                byte[] message = Request.BinaryRead(Request.ContentLength);
                string json = Encoding.UTF8.GetString(message);
                User user = JsonConvert.DeserializeObject<User>(json);

                string cookie_value = Request.Cookies["auth"].Value;
                string email = Security.Check_cookie(cookie_value);

                ServerResponse response = new ServerResponse();
                if (email == null)
                    response.type = ResponseType.ERROR;
                else
                {
                    string path = @"C:\Users\hixix\Documents\HWSite\HWSite\Files\" + user.Image_id + ".png";
                    File.Delete(path);

                    string expression = "delete from photos where PhotoId=@image_id and Email=@email";
                    Service.Exec_command(expression, "@email", email, "@image_id", user.Image_id);

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