<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

<%@ Register Src="~/UserControls/ucPager2.ascx" TagPrefix="uc1" TagName="ucPager2" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan="2">
                    <h1>流水帳管理系統 - 後台</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="UserInfo.aspx">使用者資訊</a>
                    <br />
                    <a href="AccountingList.aspx">流水帳管理</a>
                </td>
                <td>
                    <!--這邊放主要內容-->
                    <asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click" />

                    <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAccountingList_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="標題" DataField="Caption" />
                            <asp:BoundField HeaderText="金額" DataField="Amount" />

                            <asp:TemplateField HeaderText="In/Out">
                                <ItemTemplate>
                                    <%--<%# ((int)Eval("ActType") == 0) ? "支出" : "收入" %>--%>
                                    <%--<asp:Literal runat="server" ID="ltActType"></asp:Literal>--%>
                                    <asp:Label ID="lblActType" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:TemplateField HeaderText="Act">
                                <ItemTemplate>
                                    <a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">Edit</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <asp:Literal runat="server" ID="ltPager"></asp:Literal>

                    <div>
                    <uc1:ucPager2 runat="server" id="ucPager2" PageSize="5" Url="/SystemAdmin/AccountingList.aspx"/>
                    </div>

                    <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                        <p style="color: aqua; background-color: orange">
                            No data in your Accounting Note.
                        </p>
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
