using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public partial class Service_pages_getimages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string cookie_value = "";
            if (Request.Cookies.AllKeys.Contains("auth"))
                cookie_value = Request.Cookies["auth"].Value;
            string email = Security.Check_cookie(cookie_value);

            List<UserImage> images = new List<UserImage>();
            ServerResponse response = new ServerResponse();
            if (email == null)
                response.type = ResponseType.ERROR;
            else
            {
                images = Service.GetImages(email);
                response.type = ResponseType.OK;
                response.list = images;
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