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
            try
            {
                byte[] message = Request.BinaryRead(Request.ContentLength);
                string json = Encoding.UTF8.GetString(message);
                User user = JsonConvert.DeserializeObject<User>(json);

                using (FileStream str = new FileStream(@"C:\Users\hixix\Documents\HWSite\HWSite\Log.txt", FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(str))
                {
                    sw.WriteLine("[server]: recieved images from user with cookie: " + user.Cookie);
                    sw.WriteLine("          with image1 size = " + user.Image.Length.ToString());
                    sw.WriteLine("               image2 size = " + user.Second_image.Length.ToString());
                }

                File.WriteAllBytes(@"C:\Users\hixix\Documents\HWSite\HWSite\img1.png", user.Image);
                File.WriteAllBytes(@"C:\Users\hixix\Documents\HWSite\HWSite\img2.png", user.Second_image);

                double[] center1 = Service.GetCenter(user.Image);
                double[] center2 = Service.GetCenter(user.Second_image);
                if(center1 == null || center2 == null)
                {
                    Response.Write(JsonConvert.SerializeObject(new ServerResponse {
                        type = ResponseType.INVALID_IMAGE
                    }));
                    return;
                }

                double distance = Service.GetDistance(center1, center2);

                //using (FileStream str = new FileStream(@"C:\Users\hixix\Documents\HWSite\HWSite\Log.txt", FileMode.Append, FileAccess.Write))
                //using (StreamWriter sw = new StreamWriter(str))
                //{
                //    sw.WriteLine("[server]: distance generated, sending response...");
                //}

                ServerResponse response = new ServerResponse { type = ResponseType.VALUE };
                response.value = distance;

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