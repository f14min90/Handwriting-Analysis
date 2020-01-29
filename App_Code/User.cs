using System.IO;
using System;
using System.Drawing;
using System.Data.SqlClient;

public class UserImage
{
    public string Id { get; set; }
    public string Description { get; set; }
    public byte[] Image { get; set; }
}

public class User
{
    public string Email { get; set; }
    public string Cookie { get; set; }
    public string Image_id { get; set; }
    public string Description { get; set; }
    public byte[] Image { get; set; }
    public byte[] Second_image { get; set; }
}