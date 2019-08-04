using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class Service_pages_uploadimage : System.Web.UI.Page
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
                    MemoryStream stream = new MemoryStream(user.Image);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                    
                    string guid = Guid.NewGuid().ToString().Replace("-", "");
                    string path = @"C:\HWSite\HWSite\Files\" + guid + ".png";
                    image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    
                    string expression = "insert photos values ('" + guid + "', @email, '" + path + "', @description)";
                    Service.Exec_command(expression, "@email", email, "@description", user.Description);

                    response.type = ResponseType.OK;
                }

                string json_response = JsonConvert.SerializeObject(response);
                Response.Write(json_response);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}