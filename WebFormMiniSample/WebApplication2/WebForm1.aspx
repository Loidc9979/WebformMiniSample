<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script>
        // 注意順序，要先初始化再執行
<%--        var val = <%= this.ForJSInt %>;
        var val2 = <%= (this.ForJSBool) ? "true" : "false" %>;
        var val3 = '<%= this.ForJSString %>';

        var ForJSBase = <%= this.ForJSBase %>;
        var ForJSCoffection = <%= this.ForJSCoffection %>;--%>

        var obj = <%= this.StringObject %>;
    </script>
    <script src="Scripts/WebForm1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <button type="button" onclick="exec()">Click me</button>
    </form>
</body>
</html>
