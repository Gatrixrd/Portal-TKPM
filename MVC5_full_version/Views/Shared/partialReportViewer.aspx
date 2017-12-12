<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="partialReportViewer.aspx.cs" Inherits="MVC5_full_version.Views.Shared.partialReportViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div  id="Cuerpo">
    <script type="text/javascript">
        @model ClickFactura_Entidades.BD.Entidades.view_todoWorkflow
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate> 
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%">
                        </rsweb:ReportViewer>
               </ContentTemplate>
  </asp:UpdatePanel>
</div>
    </form>
</body>
</html>
