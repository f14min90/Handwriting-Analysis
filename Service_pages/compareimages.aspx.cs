using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;

public partial class Service_pages_compareimages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.ContentLength > 0)
        {
            byte[] message = Request.BinaryRead(Request.ContentLength);
            string json = Encoding.UTF8.GetString(message);
            User user = JsonConvert.DeserializeObject<User>(json);

            double[] center1 = Service.GetCenter(user.Image);
            double[] center2 = Service.GetCenter(user.Second_image);
            double distance = Service.GetDistance(center1, center2);

            ServerResponse response = new ServerResponse { type = ResponseType.VALUE };
            response.value = distance;

            string json_response = JsonConvert.SerializeObject(response);
            Response.Write(json_response);
        }
    }
}