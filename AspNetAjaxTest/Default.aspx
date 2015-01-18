<%@ Page Title="Startseite" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AspNetAjaxTest._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        #TextArea1
        {
            height: 168px;
            width: 624px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Willkommen bei ASP.NET!</h2>
    <input id="Button1" type="button" value="Hole Liste" onclick="getHst();" />
    <p>
        <asp:Label ID="Label1" runat="server" Text="Suche"></asp:Label>
        <asp:TextBox ID="TextBoxInput" runat="server" ClientIDMode="Static" Width="246px"></asp:TextBox>
        <asp:Button ID="ButtonSearch" runat="server" Text="Zeige!" OnClientClick="QueryNominatim();return false;" />
        <asp:Label runat="server" Width="3px"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Suche: "></asp:Label>
        <asp:DropDownList ID="DropDownSearch" runat="server" ClientIDMode="Static" Height="20px"
            Width="399px">
        </asp:DropDownList>
    </p>
    <p>
    </p>
    <div id="ContentLeft">
        <asp:ListBox ID="ListBox1" runat="server" ClientIDMode="Static" Height="159px"></asp:ListBox>
    </div>
    <div id="ContentRight" style="background-color: #3366FF">
    </div>
    <asp:TextBox ID="TextOutput" runat="server" ClientIDMode="Static" Height="164px"
        ReadOnly="True" TextMode="MultiLine" Width="622px" Style="margin-top: 25px"></asp:TextBox>
    <script type="text/javascript">
        list = document.getElementById('<%= ListBox1.ClientID %>');
        // getHst();
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/AjaxService.svc" />
        </Services>
    </asp:ScriptManager>
</asp:Content>