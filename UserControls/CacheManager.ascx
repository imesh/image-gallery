<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/CacheManager.ascx.cs" Inherits="Textpencil.ImageGallery.UserControls.CacheManager" %>

<h2><asp:Label ID="lblPageTitle" runat="server" /></h2>            
<p>Secret Key: <asp:TextBox ID="txtSecretKey" runat="server" TextMode=Password></asp:TextBox></p>
<p><asp:Button ID="btnRefresh" runat="server" Text="Refresh" onclick="btnRefresh_Click" /></p>
<p><asp:Label ID="lblMessage" runat="server" /></p>
</br></br>
<p><asp:Label ID="lblErrorDesc" runat="server" /></p>