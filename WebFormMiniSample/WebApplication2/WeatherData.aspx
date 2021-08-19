<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeatherData.aspx.cs" Inherits="WebApplication2.WeatherData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1">
            <tr><th>地點</th><td> <asp:Literal ID="ltLocation" runat="server"></asp:Literal> </td></tr>
            <tr><th>溫度</th><td> <asp:Literal ID="ltTemp" runat="server"></asp:Literal> </td></tr>
            <tr><th>降雨量</th><td> <asp:Literal ID="ltPop24" runat="server"></asp:Literal> </td></tr>
        </table>
    </form>
</body>
</html>
