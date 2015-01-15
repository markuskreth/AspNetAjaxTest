<%@ Page Title="Startseite" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AspNetAjaxTest._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Willkommen bei ASP.NET!</h2>
    <input id="Button1" type="button" value="Hole Liste" onclick="alert('Hole Liste');getHst();" />
    <p>
        Weitere Informationen zu ASP.NET finden Sie auf <a href="http://www.asp.net" title="ASP.NET-Website">
            www.asp.net</a>.
    </p>
    <p>
        <a href="http://go.microsoft.com/fwlink/?LinkID=152368" title="MSDN-ASP.NET-Dokumente">
            Dokumentation finden Sie auch unter ASP.NET bei MSDN</a>.
    </p>
    <div id="ContentLeft">
        <asp:ListBox ID="ListBox1" runat="server" ClientIDMode="Static" Height="231px"></asp:ListBox>
    </div>
    <div id="ContentRight">
    </div>
    <script type="text/javascript">
        list = document.getElementById('<%= ListBox1.ClientID %>');
        getHst();
    </script>
</asp:Content>