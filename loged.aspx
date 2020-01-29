<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loged.aspx.cs" Inherits="loged" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style2.css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js">
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="container1">
            <div class="box1">
                <asp:Label runat="server" ID="label" CssClass="label"></asp:Label>
                <asp:Button runat="server" ID="exit" CssClass="exit" Text="ВЫХОД" OnClick="exit_Click" />
            </div>
            <div class="container2">
                <div class="box2">

                    <input type="button" id="im1" class="im" value="Изображение 1" />
                    <asp:FileUpload runat="server" ID="real2" CssClass="hid" />

                    <input type="button" id="im2" class="im" value="Изображение 2" />
                    <asp:FileUpload runat="server" ID="real3" CssClass="hid" />

                    <asp:Button runat="server" ID="comp" CssClass="comp" Text="Получить результат" OnClick="comp_Click" />


                    <script>
                        $('#im1').click(function (e) { e.preventDefault(); $('#real2').click(); });
                        $('#im2').click(function (e) { e.preventDefault(); $('#real3').click(); });
                    </script>

                    <script>
                        $('#real2').change(function () {
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

                        $('#real3').change(function () {
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
                    <div class="box5">                            

                        <asp:FileUpload runat="server" ID="real1" class="hid" ClientIDMode="Static"/>

                        <asp:TextBox runat="server" ID="Email" CssClass="tex" ReadOnly="true"></asp:TextBox>
                        <asp:Button runat="server" CssClass="comp2" ID="choose" Text="Выбрать файл" />
                        <asp:TextBox runat="server" ID="Cookie" CssClass="tex"></asp:TextBox>
                        <asp:Button runat="server" cssclass="comp3" ID="add" text="Добавить" OnClick="add_Click" />            

                        <script>
                            $(function () {                                    
                                $('#choose').click(function (e) { e.preventDefault(); $('#real1').click(); });
                                $('#real1').change(function () {                                        
                                        
                                    $('#Email').val( $('#real1').val() );
                                });
                            });
                        </script>

                    </div>
                    <div class="box6">

                        <asp:Repeater runat="server" ID="rep" DataSourceID="dsImg">
                            <ItemTemplate>
                                <div class="gal">
                                    <img src="Files\<%# Eval("PhotoId")%>.png" title="<%# Eval("Description")%>" />
                                    <div class="opt"><asp:Button runat="server" id="del" CssClass="del" Text="Удалить"
                                        CommandArgument='<%# Eval("PhotoId") %>' OnCommand="del_Click" /></div>                                    
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <asp:SqlDataSource
                            runat="server"
                            ID="dsImg"
                            ConnectionString="<%$ ConnectionStrings:Standart%>"                            
                            SelectCommandType="Text">
                        </asp:SqlDataSource>

                    </div>
                </div>                    
            </div>
        </div>

    </form>
</body>
</html>
