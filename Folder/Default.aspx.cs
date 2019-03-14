using System;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        string sqlExpression = "INSERT INTO Files (Filepath) VALUES (@filepath)";
        string connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;

        byte[] data = Request.BinaryRead(Request.ContentLength);

        Response.Write(data.Length);

        if (Request.Files.Count > 0)
        {
            var file = Request.Files[0];            

            string filename = Path.GetFileName(file.FileName);
            string filepath = Server.MapPath("~/Files/" + filename);

            file.SaveAs(filepath);
            uploadDB(filepath, connectionString, sqlExpression);           

            Response.Write("залупа загружена");
        } 	       
    }   

    void uploadDB(string filepath, string connectionString, string sqlExpression)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlParameter parameter = new SqlParameter("@filepath", filepath);

            command.Parameters.Add(parameter);
            command.ExecuteNonQuery();
        }
    }    
}
