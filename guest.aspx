<%@ Page Language="C#" AutoEventWireup="true" CodeFile="guest.aspx.cs" Inherits="guest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link href="style1.css" rel="stylesheet" />
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title></title>
        <script src="//ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js">
        </script>
    </head>
    <body>        
        <form id="sadf" runat="server">

            <div class="container">
                <div class="box1">
                    <asp:TextBox runat="server" ID="Email" CssClass="tex"></asp:TextBox>
                    <asp:Button runat="server" CssClass="but" OnClick="send_mail" Text="Выслать код" />
                    <asp:TextBox runat="server" ID="Cookie" CssClass="tex"></asp:TextBox>
                    <asp:Button runat="server" cssclass="but" OnClick="check_cookie" text="Подтвердить" />            
                </div>

                <div class="box2">                                            
                       
                    <input type="button" id="im1" class="im" value="Изображение 1" />    
                    <asp:FileUpload runat="server" id="real1" CssClass="hid" />

                    <input type="button" id="im2" class="im" value="Изображение 2" />
                    <asp:FileUpload runat="server" id="real2" CssClass="hid" />

                    <script>
                        $(function () {
                            $('#im1').click(function (e) { e.preventDefault(); $('#real1').click(); });
                            $('#im2').click(function (e) { e.preventDefault(); $('#real2').click(); });
                        });
                    </script>

                    <script>
                        $('#real1').change(function () {
                            var file = this.files[0];
                            var reader = new FileReader();
                            reader.onloadend = function () {
                                $('#im1').css('background-image', 'url("' + reader.result + '")');
				                $('#im1').css('background-size', 'cover');
				                $('#im1').css('background-repeat', 'round');
                            }
                            if (file) {
                                reader.readAsDataURL(file);
                            } else {
                            }
                        });

                        $('#real2').change(function () {
                            var file = this.files[0];
                            var reader = new FileReader();
                            reader.onloadend = function () {
                                $('#im2').css('background-image', 'url("' + reader.result + '")');
				                $('#im2').css('background-size', 'cover');
				                $('#im2').css('background-repeat', 'round');
                            }
                            if (file) {
                                reader.readAsDataURL(file);
                            } else {
                            }
                        });
                    </script>

                </div>

                <div class="box3">
                   
                    <asp:Button runat="server" CssClass="comp" Text="Получить результат" OnClick="comp_Click" />

                </div>
                
            </div>

        </form>
        
    </body>
</html>
