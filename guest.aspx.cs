using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class guest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cookie = "";
        if(Request.Cookies.AllKeys.Contains("auth"))
            cookie = Request.Cookies["auth"].Value;
        string email = Security.Check_cookie(cookie);

        if (email != null)
            Response.Redirect("loged.aspx");
    }

    protected void send_mail(object sender, EventArgs e)
    {
        try
        {
            string email = Email.Text;
            string cookie_value = Guid.NewGuid().ToString().Replace("-", "");
            string cookie_hash = Security.Encrypt(cookie_value);

            string expression = "if exists(select * from users where Email=@email) " +
                                    "update users set CookieHash=@cookie_hash where Email=@email " +
                                "else " +
                                    "insert users values(@email, @cookie_hash, null, 0)";
            Service.Send_mail(email, cookie_value);
            Service.Exec_command(expression, "@email", email, "@cookie_hash", cookie_hash);          

            Email.Text = "Код выслан";
        }
        catch(Exception ex)
        {
            Email.Text = ex.Message;
        }
    }

    protected void check_cookie(object sender, EventArgs e)
    {
        try
        {
            string cookie_value = Cookie.Text;
            string email = Security.Check_cookie(cookie_value);
            
            if (email == null)
                throw new Exception("Неверный код");

            string cookie_hash = Security.Encrypt(cookie_value);
            string expression = "update users set Checked=1 where CookieHash=@cookie_hash";
            Service.Exec_command(expression, "@cookie_hash", cookie_hash);

            HttpCookie cookie = new HttpCookie("auth", cookie_value);
            Response.Cookies.Add(cookie);
            
            Response.Redirect("guest.aspx");
        }
        catch (Exception ex)
        {
            Cookie.Text = ex.Message;
        }
    }

    protected void comp_Click(object sender, EventArgs e)
    {
        if (real1.HasFile && real2.HasFile)
        {
            try
            {
                double[] center1 = Service.GetCenter(real1.FileBytes);
                double[] center2 = Service.GetCenter(real2.FileBytes);

                double distance = Service.GetDistance(center1, center2);
                Response.Write("<script>alert('Вероятность совпадения:" + distance + "')</script>");
            }
            catch(Exception ex)
            {
		Response.Write(string.Format("<{0}>alert('" + ex.Message + "')</{0}>", "script"));
            }
        }
    }
}