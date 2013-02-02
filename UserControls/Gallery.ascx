<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/Gallery.ascx.cs" Inherits="Textpencil.ImageGallery.UserControls.Gallery" %>

<h2><asp:Label ID="lblPageTitle" runat="server" /></h2>    
<p><asp:Table ID="tableImages" runat="server" /></p>    
<p><asp:Table ID="tablePageNumbers" runat="server" /></p>
<h4><asp:LinkButton ID="linkThumbnail" runat="server">Thumbnail</asp:LinkButton> | <asp:LinkButton ID="linkMosaic" runat="server">Mosaic</asp:LinkButton> | <asp:LinkButton ID="linkFlickr" runat="server">Flickr</asp:LinkButton></h4>
<p><asp:Label ID="lblErrorDesc" runat="server" /></p>