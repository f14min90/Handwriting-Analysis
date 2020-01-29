using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class loged : System.Web.UI.Page
{
    string email, cookie;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.Cookies.AllKeys.Contains("auth"))
            Response.Redirect("guest.aspx");
        cookie = Request.Cookies["auth"].Value;
        email = Security.Check_cookie(cookie);

        if (email == null)
            Response.Redirect("guest.aspx");       

        label.Text = email;
        dsImg.SelectCommand = "select PhotoId, Description from photos where Email='" + email + "'";
    }

    protected void exit_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("auth", "");
        Response.Cookies.Add(cookie);

        Response.Redirect("guest.aspx");
    }

    protected void comp_Click(object sender, EventArgs e)
    {
        if (real2.HasFile && real3.HasFile)
        {
            try
            {
                double[] center1 = Service.GetCenter(real2.FileBytes);
                double[] center2 = Service.GetCenter(real3.FileBytes);

                double distance = Service.GetDistance(center1, center2);
                Response.Write("<script>alert('Вероятность совпадения:" + distance + "')</script>");
            }
            catch(Exception ex)
            {
		Response.Write(string.Format("<{0}>alert('" + ex.Message + "')</{0}>", "script"));
            }
        }
    }

    protected void add_Click(object sender, EventArgs e)
    {
        if (real1.HasFiles)
        {
            byte[] data = real1.FileBytes;            

            MemoryStream stream = new MemoryStream(data);
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

            string guid = Guid.NewGuid().ToString().Replace("-", "");
	    string path = @"C:\sites\ru.dsoft.hwa\Files\"  + guid + ".png";
            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            stream.Close();
            stream.Dispose();
            image.Dispose();

            string expression = "insert photos values ('" + guid + "', @email, '" + path + "', @description)";
            Service.Exec_command(expression, "@email", email, "@description", Cookie.Text);
                        
            Response.Redirect("loged.aspx");            
        }
    }

    protected void del_Click(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();

        string path = @"C:\sites\ru.dsoft.hwa\Files\" + id + ".png";
        File.Delete(path);

        string expression = "delete from photos where PhotoId=@image_id and Email=@email";
        Service.Exec_command(expression, "@email", email, "@image_id", id);
        
        Response.Redirect("loged.aspx");
    }
}