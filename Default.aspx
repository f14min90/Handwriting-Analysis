<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html>

    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title></title>
    </head>

    <body>               
        <form method="post" enctype="multipart/form-data">
            <asp:Label ID="textBox" runat="server">Ожидаю загрузки фотографии</asp:Label><br />
            <input type="file" name="zhopa" />
            <input type="submit" />
        </form>
    </body>

</html>
