<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/imagegallery.master" CodeFile="Default.aspx.cs" Inherits="Default" Theme="Default" %>
<%@ Register TagPrefix="uc" TagName="Gallery" Src="~/UserControls/Gallery.ascx" %>

<asp:Content ID="ImageGallery" ContentPlaceHolderID="MainContent" runat="Server">
<uc:Gallery runat="server"></uc:Gallery>
</asp:Content>
