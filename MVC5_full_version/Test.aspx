<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="MVC5_full_version.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
                <div class="panel-body" style="height: 600px;">
                             <%--@Html.Partial("~/Views/Shared/_verificacionFacturaBase.cshtml")--%>   
                             @Html.Partial("~/Views/Recepciones/Grid.cshtml")  
                         
                </div>

    </form>
</body>
</html>




